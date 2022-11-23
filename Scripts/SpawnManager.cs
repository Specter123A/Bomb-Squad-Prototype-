using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   //Initialize fields in the inspecter
    [SerializeField]
    private GameObject enemyPre;

    [SerializeField]
    private GameObject enemyPre1;


     [SerializeField]
    private float enemyPreInterval = 4.5f;

     [SerializeField]
    private float enemyPre1Interval= 2.5f;

    private float spawnRange = 9f;
    public int enemyCount;
    public int waveNumber =1;

    public GameObject powerupPrefab;  //health pickup

// Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(enemyPreInterval, enemyPre));
        StartCoroutine(spawnEnemy(enemyPre1Interval, enemyPre1));
        SpawnEnemyWaves(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemyPre, new Vector3(Random.Range(-2f,6), Random.Range(-2f, 6), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }

    void Update()
    {
        if(enemyCount ==0)
        {
            waveNumber ++;
            SpawnEnemyWaves(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    void SpawnEnemyWaves(int enemySpawn)
    {
       for (int i=0; i<enemySpawn; i++)
       {
       Instantiate (enemyPre, GenerateSpawnPosition(), Quaternion.identity);
       Instantiate (enemyPre1, GenerateSpawnPosition(), Quaternion.identity);
       }
    }

    private Vector3 GenerateSpawnPosition()
    {
       //calculations 
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3( spawnPosX, 0 , spawnPosZ);

        //return the value of random position
        return randomPos;
    }

}
