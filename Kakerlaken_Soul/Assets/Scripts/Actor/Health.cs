using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;

public class Health : MonoBehaviour
{
    private PlayerManager PM;

    public Gauge<float> health;
    [SerializeField] float maxHealth = 100f;
    [SerializeField, Range(0.01f,10f)] float recover = 0.01f;
    private MeshRenderer[] meshs;

    public bool isDead = false;
    public bool isDmg = false;
    private void Awake()
    {
        meshs = GetComponentsInChildren<MeshRenderer>();
        PM = GetComponent<PlayerManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        health = new Gauge<float>(maxHealth);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RecoverHealth(recover);
    }

    public void RecoverHealth(float value)
    {
        if (isDead) return;
        if (health.Value < health.MaxValue)
        {
            health.Value += value;
        }
    }

    public void TakeDmg(float dmg)
    {
        if (isDead) {
            if (PM != null)
                PM.state = PlayerManager.State.dead;
            return;
        }
        if(PM != null)
            PM.state = PlayerManager.State.dmg;
        
        if(isDmg == false)
        {
            StartCoroutine("OnDmg",dmg);
        }
    }
            

    IEnumerator OnDmg(float dmg)
    {
        isDmg = true;
        health.Value -= dmg;

        foreach (MeshRenderer mesh in meshs)
            mesh.material.color = Color.red;

        if (health.Value <= 0) { 
            isDead = true; 
            PM.isDead = true;
        }

        yield return new WaitForSeconds(0.2f);
        isDmg = false;

        if(isDead == false)
        {
            foreach (MeshRenderer mesh in meshs)
                mesh.material.color = Color.white;
            if (PM != null)
                PM.state = PlayerManager.State.idle;
        }
        else
        {
            foreach (MeshRenderer mesh in meshs)
                mesh.material.color = Color.gray;
            health.Value = 0;
            if (PM != null)
                PM.state = PlayerManager.State.dead;
        }
    }

    public void getHealth(float value)
    {
        health.MaxValue += value;
    }
}
