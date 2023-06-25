using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private PlayerManager PM;

    public Gauge<float> health;
    [SerializeField] float maxHealth = 100f;

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
        health = new Gauge<float>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDmg(float dmg)
    {
        if (isDead) return;
        PM.state = PlayerManager.State.dmg;
        health.Value -= dmg;
        if(health.Value < 0 )
            isDead = true;
        StartCoroutine("OnDmg");
    }

    IEnumerator OnDmg()
    {
        foreach (MeshRenderer mesh in meshs)
            mesh.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        if(isDead == false)
        {
            foreach (MeshRenderer mesh in meshs)
                mesh.material.color = Color.white;
            PM.state = PlayerManager.State.idle;
        }
        else
        {
            foreach (MeshRenderer mesh in meshs)
                mesh.material.color = Color.gray;
            health.Value = 0;
            PM.state = PlayerManager.State.dead;
        }
    }
}
