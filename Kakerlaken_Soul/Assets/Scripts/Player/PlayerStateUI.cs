using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStateUI : MonoBehaviour
{
    public Image HealthFillImage;
    public Image StaminaFillImage;

    public Health playerHealth;
    public Stamina playerStamina;
    //public TMP_Text text;

    void Update()
    {
        HealthFillImage.fillAmount = playerHealth.health.Value / playerHealth.health.MaxValue;
        StaminaFillImage.fillAmount = playerStamina.stamina.Value / playerStamina.stamina.MaxValue;
        //text.text = playerHealth.health.ToString();
    }
}