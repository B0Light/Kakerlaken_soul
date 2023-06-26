using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldspaceHealthBar : MonoBehaviour
{
    public Health Health;
    public Image HealthBarImage;
    public Transform HealthBarPivot;

    public bool HideFullHealthBar = true;

    void Update()
    {
        HealthBarImage.fillAmount = Health.health.Value / Health.health.GetMaxValue();

        HealthBarPivot.LookAt(Camera.main.transform.position);

        if (HideFullHealthBar)
            HealthBarPivot.gameObject.SetActive(HealthBarImage.fillAmount != 1);
    }
}
