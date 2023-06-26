using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
    public Image HealthFillImage;

    public Health playerHealth;
    //public TMP_Text text;

    void Update()
    {
        HealthFillImage.fillAmount = playerHealth.health.Value / playerHealth.health.GetMaxValue();
        //text.text = playerHealth.health.ToString();
    }
}
