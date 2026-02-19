using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaveHandler : NetworkBehaviour
{
    [SerializeField] public List<Wave> waves;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private EnemyPath enemyPath;
    [SerializeField] private GameObject victoryCanvas;
    [SerializeField] private GameObject gameOverCanvas;
   // [SerializeField] private 

    [SerializeField] private GameObject NextWave;
    public int waveCounter;
    public int currentWaveIndex=0;
    private int aliveEnemies;

    // spiller variabler\\
    public int playerHealth = 100;
    //public override void OnNetworkSpawn()
    //{
    //    Debug.Log("OnNetworkSpawn");
    //    if (!IsServer) return;
    //    StartCoroutine(StartNextWave());

    //}

    //funktion kaldes af knap
    public void StartGame()
    {
        NextWave.SetActive(false);
        StartCoroutine(StartNextWave());

    }

    private IEnumerator StartNextWave()
    {
        waveCounter = currentWaveIndex;
        Debug.Log("StartNextWave");
        if (currentWaveIndex >= waves.Count)
        {
            Debug.Log("ALLE WAVES KLARET");
            ShowVictoryUI();
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

    private void ShowVictoryUI()
    {
        if (victoryCanvas != null)
        {
            victoryCanvas.SetActive(true);
        }
    }

    private void ShowGameOverUI()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
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
        playerHealth=-10;
        
        if (playerHealth <= 0)
        {
            ShowGameOverUI();
        }
        if (aliveEnemies <= 0)
        {
            currentWaveIndex++;
            NextWave.SetActive(true);
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
