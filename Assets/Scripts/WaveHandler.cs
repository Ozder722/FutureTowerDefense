using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;


public class WaveHandler : NetworkBehaviour
{
    [SerializeField] private List<Wave> waves;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private EnemyPath enemyPath;

    private int currentWaveIndex;
    private int aliveEnemies;

    public override void OnNetworkSpawn()
    {
        Debug.Log("OnNetworkSpawn");
        if (!IsServer) return;
        StartCoroutine(StartNextWave());
        
    }

    private IEnumerator StartNextWave()
    {
        Debug.Log("StartNextWave");
        if (currentWaveIndex >= waves.Count)
        {
            Debug.Log("ALLE WAVES KLARET");
            yield break;
        }

        Wave currentWave = waves[currentWaveIndex];
        Debug.Log($"Starter wave {currentWaveIndex + 1}");

        foreach (EnemySpawnData enemyData in currentWave.enemies)
        {
            for (int i = 0; i < enemyData.count; i++)
            {
                SpawnEnemy(enemyData.enemyPrefab);
                yield return new WaitForSeconds(enemyData.spawnDelay);
            }
        }
    }

    private void SpawnEnemy(GameObject prefab)
    {
        GameObject enemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        enemy.GetComponent<NetworkObject>().Spawn();

       

        aliveEnemies++;
        enemy.GetComponent<EnemyHealth>().OnEnemyRemoved += HandleEnemyDeath;
    }

   
    private void HandleEnemyDeath()
    {
        aliveEnemies--;
        

        if (aliveEnemies <= 0)
        {
            currentWaveIndex++;
            StartCoroutine(StartNextWave());
        }
    }
}



[System.Serializable]
public class EnemySpawnData
{
    public GameObject enemyPrefab;
    public int count;
    public float spawnDelay;
}

[System.Serializable]
public class Wave
{
    public List<EnemySpawnData> enemies;
}
