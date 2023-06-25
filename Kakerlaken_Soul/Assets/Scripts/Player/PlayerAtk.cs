using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtk : MonoBehaviour
{
    [SerializeField] private PlayerManager PM;

    [SerializeField] private float meleeRadius = 1f;
    [SerializeField] private float meleeRange = 1f;
    public float dmg = 1;
    public bool isAtk = false;

    private void Awake()
    {
        PM = GetComponent<PlayerManager>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Atk Input");
            if(PM.state != PlayerManager.State.atk)
            {
                PM.state = PlayerManager.State.atk;
                Debug.Log("Do Atk");
                StartCoroutine("ATKBase");
            }
        }
    }

    IEnumerator ATKBase()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position,
            meleeRadius, transform.forward, meleeRange);
        foreach (var hit in hits)
        {
            Health health = hit.collider.GetComponent<Health>();
            if (health)
            {
                health.TakeDmg(dmg);
            }
        }
        yield return new WaitForSeconds(1f);
        PM.state = PlayerManager.State.idle;
    }
}
