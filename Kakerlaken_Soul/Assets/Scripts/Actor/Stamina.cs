using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] private PlayerManager PM;

    public Gauge<float> stamina;
    [SerializeField] float maxStamina = 100f;

    private void Awake()
    {
        PM = GetComponent<PlayerManager>();
    }
    void Start()
    {
        stamina = new Gauge<float>(maxStamina);
    }

    void FixedUpdate()
    {
        if (PM != null)
        {
            if(PM.state == PlayerManager.State.idle ||
               PM.state == PlayerManager.State.move)
            {
                RecoverStamina(0.5f);
            }
        }
    }

    private void RecoverStamina(float value)
    {
        if(stamina.Value < maxStamina)
        {
            stamina.Value += value;
        }
    }
    public void UseStamina(float value)
    {
        stamina.Value -= value;
    }

    public void GetStamina(float value)
    {
        stamina.MaxValue = value;
    }
}
