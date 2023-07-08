using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

[RequireComponent(typeof(Collider))]
public class Zone : MonoBehaviour
{
    Collider m_Collider;
    [Header("Cinematic")]
    [SerializeField] Animator spiderAni;
    [SerializeField] GameObject TimeLine;
    [SerializeField] GameObject TextField;
    [SerializeField] TMP_Text textFieldName;
    [SerializeField] TMP_Text textFieldText;
    [SerializeField] GameObject Scene;
    [SerializeField] GameObject Camera;
    PlayerManager PM;

    public enum UIState
    {
        wait,
        coment,
        camMove
    }
    public UIState uistate;



    private void Awake()
    {
        m_Collider = GetComponent<Collider>();
        TextField.SetActive(false);
        TimeLine.SetActive(false);
        Camera.SetActive(false);
    }

    void Start()
    {
        uistate = UIState.wait;
    }

    void Update()
    {
        switch (uistate)
        {
            case UIState.coment:
                if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
                {
                    StartCoroutine("CamMove");
                }
                break;
            case UIState.camMove:
                if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
                {
                    TimeLine.SetActive(false);
                    Camera.SetActive(false);
                    PM.state = PlayerManager.State.idle;
                    Scene.SetActive(false);
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PM = other.GetComponent<PlayerManager>();
        
        StartCoroutine("StartScene");
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(1f);
        PM.state = PlayerManager.State.Cine;
        Scene.SetActive(true);
        uistate = UIState.coment;
        spiderAni.SetTrigger("taunt");
        yield return new WaitForSeconds(2f);
        TextField.SetActive(true);
        textFieldName.text = "거미";
        textFieldText.text = "저기 보이는 집으로 가면 돌아갈 수 있는 방법을 찾을 수도...";
    }

    IEnumerator CamMove()
    {
        Debug.Log("CamMove");
        uistate = UIState.camMove;
        TextField.SetActive(false);
        TimeLine.SetActive(true);
        Camera.SetActive(true);
        yield return new WaitForSeconds(5f);
    }
}
