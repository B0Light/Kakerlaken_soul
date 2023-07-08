using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("SetOptions")]
    public Transform[] enemyPath;

    [Header("State")]
    public bool isDead;
    public GameObject target;

    [Header("reference")]
    private Health health;
    public EnemySpawner spawner;
    

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health != null)
        {
            if (health.isDead)
            {
                isDead = true;
                spawner.enemyCount -= 1;
                Destroy(this.gameObject);
            }
        }
    }
}
