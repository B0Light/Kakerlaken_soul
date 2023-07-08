using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyList;
    [SerializeField] Transform enemyFolder;
    [SerializeField] int maxEnemy = 7;
    public int enemyCount = 0;
    [SerializeField] Transform[] spawnPos;
    private bool isSpawning = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyCount <  maxEnemy)
        {
            if (isSpawning == false)
            {
                isSpawning = true;
                enemyCount += 1;
                StartCoroutine("SpawnEnemy");
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3f);
        int spawnNo = Random.Range(0, spawnPos.Length);
        int spawnEnemyNo = Random.Range(0, enemyList.Length);
        GameObject iEnemy = Instantiate(enemyList[spawnEnemyNo], spawnPos[spawnNo]);
        EnemyManager EM = iEnemy.GetComponent<EnemyManager>();
        EM.spawner = this;
        EM.enemyPath = new Transform[spawnPos.Length];
        for(int i = 0; i < spawnPos.Length; i++)
        {
            EM.enemyPath[i] = spawnPos[i];
        }
        isSpawning = false;
    }
}
