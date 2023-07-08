using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class fadeinout : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] GameManager GM;

    [Header("FadeInOut")]
    public Image fadeOutUIImage;
    public float fadeSpeed = 1f;
    bool FadeDir;      // true -> Alpha = 1  , false -> Alpha = 0	

    [Header("BookText")]
    [SerializeField] GameObject bookCover;
    [SerializeField] TMP_Text textField;
    [SerializeField] TMP_Text bookTitle;
    [SerializeField] private BookText bookText;
    public enum UIState
    {
        wait,
        cover,
        page1,
        page2,
        page3,
        page4
    }
    public UIState uistate;
    private void Start()
    {
        FadeDir = true;
        textField.text = string.Empty;
        bookTitle.text = string.Empty;
        bookCover.SetActive(false);
        uistate = UIState.wait;
    }
    private void Update()
    {
        switch (uistate)
        {
            case UIState.cover:
                if (Input.anyKeyDown || Input.GetMouseButton(0))
                {
                    CoverOff();
                    Invoke("Page1",.3f);
                }
                break;
            case UIState.page1:
                if (Input.anyKeyDown || Input.GetMouseButton(0))
                {
                    Invoke("Page2", .3f);
                }
                break; 
            case UIState.page2:
                if (Input.anyKeyDown || Input.GetMouseButton(0))
                {
                    Invoke("Page3", .3f);
                }
                break;
            case UIState.page3:
                if (Input.anyKeyDown || Input.GetMouseButton(0))
                {
                    Invoke("Page4", .3f);
                }
                break;
            case UIState.page4:
                if (Input.anyKeyDown || Input.GetMouseButton(0))
                {
                    this.gameObject.SetActive(false);
                    GM.StartGame();
                }
                break;
        }
    }
    public void FadeOut()
    {
        FadeDir = false;
        StartCoroutine(Fade(FadeDir));
    }

    public void FadeIn()
    {
        FadeDir = true;
        StartCoroutine(Fade(FadeDir));
    }
    private IEnumerator Fade(bool FDir)
    {
        float alpha = (FDir) ? 1 : 0;
        float fadeEndValue = (FDir) ? 0 : 1;
        if (FDir)
        {
            while (alpha >= fadeEndValue)
            {
                SetColorImage(ref alpha, FDir);
                yield return null;
            }
            fadeOutUIImage.enabled = false;
        }
        else
        {
            fadeOutUIImage.enabled = true;
            while (alpha <= fadeEndValue)
            {
                SetColorImage(ref alpha, FDir);
                yield return null;
            }
        }
    }

    private void SetColorImage(ref float alpha, bool FDir)
    {
        fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
        alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((FDir) ? -1 : 1);
    }

    public void CoverOn()
    {
        uistate = UIState.cover;
        bookCover.SetActive(true);
    }

    public void CoverOff()
    {
        bookCover.SetActive(false);
    }

    public void Page1()
    {
        uistate = UIState.page1;
        textField.text = bookText.Text1;
        bookTitle.text = "[변신] 중에서";
    }

    public void Page2()
    {
        uistate = UIState.page2;
        textField.text = bookText.Text2;
    }

    public void Page3()
    {
        uistate = UIState.page3;
        textField.text = bookText.Text3;
    }
    
    public void Page4()
    {
        uistate = UIState.page4;
        textField.text = bookText.Text4;
    }
}