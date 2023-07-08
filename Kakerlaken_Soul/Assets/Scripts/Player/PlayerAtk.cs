using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtk : MonoBehaviour
{
    [SerializeField] private PlayerManager PM;
    [SerializeField] private Stamina _stamina;
    [SerializeField] private float meleeRadius = 1f;
    [SerializeField] private float meleeRange = 1f;
    [SerializeField, Range(1f, 100f)] private float atkMana = 10f;

    public float dmg = 1;
    public bool isAtk = false;
    [SerializeField] private LayerMask atkTarget;

    [SerializeField] GameObject impactVfx;
    [SerializeField] Transform atkPos;

    private void Awake()
    {
        PM = GetComponent<PlayerManager>();
        _stamina = GetComponent<Stamina>();
    }
    void Update()
    {
        if(PM.isDead) return;

        if (Input.GetMouseButtonDown(0))
        {
            if(!(PM.state == PlayerManager.State.atk || PM.state == PlayerManager.State.wait
                || PM.state == PlayerManager.State.dmg))
            {
                if (_stamina != null && _stamina.stamina.Value >= atkMana)
                    _stamina.UseStamina(atkMana);
                PM.state = PlayerManager.State.atk;
                StartCoroutine("ATKBase");
            }
        }
    }

    IEnumerator ATKBase()
    {
        yield return new WaitForSeconds(0.5f);
        RaycastHit[] hits = Physics.SphereCastAll(transform.position,
            meleeRadius, transform.forward, meleeRange, atkTarget);
        foreach (var hit in hits)
        {
            Health health = hit.collider.GetComponent<Health>();
            if (health)
            {
                VFX(0.3f);
                health.TakeDmg(dmg);
            }
        }
        yield return new WaitForSeconds(1f);
        PM.state = PlayerManager.State.idle;
    }

    void VFX(float ImpactVfxLifetime)
    {
        if (impactVfx)
        {
            GameObject impactVfxInstance = Instantiate(impactVfx, atkPos.position,
                Quaternion.LookRotation(atkPos.position));
            if (ImpactVfxLifetime > 0)
            {
                Destroy(impactVfxInstance, ImpactVfxLifetime);
            }
        }
    }
}
