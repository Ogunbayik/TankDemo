using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [Header("Spawn Settings")]
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private Transform enemyTankPrefab;
    [SerializeField] private int maxSpawnTimer;
    [SerializeField] private int maxEnemyCount;
    [SerializeField] private int enemyCountInArea;

    private float spawnTimer;
    private int enemyCount = 0;
    private void Awake()
    {
        #region Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        #endregion
    }
    void Start()
    {
        InitializeEnemies();
    }
    void Update()
    {
        SpawnEnemies();
    }

    private void InitializeEnemies()
    {
        spawnTimer = maxSpawnTimer;
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            //Spawn enemies all spawn positions.
            enemyCount++;
            var enemy = Instantiate(enemyTankPrefab, spawnPositions[i].position, Quaternion.identity);
        }
    }

    private void SpawnEnemies()
    {
        if(enemyCount < enemyCountInArea)
        {
            if(spawnTimer <= 0 && enemyCount < maxEnemyCount)
            {
                //Spawn new enemy
                enemyCount++;
                spawnTimer = maxSpawnTimer;
                var enemy = Instantiate(enemyTankPrefab, GetRandomPosition().position, Quaternion.identity);
            }
            else
            {
                
                if (enemyCount == maxEnemyCount)
                {
                    spawnTimer = 0;
                }
                else
                {
                    //Countdown for new enemy, if max enemy count isn't less than enemy count
                    spawnTimer -= Time.deltaTime;
                }
            }
        }
        else
        {
            //Can not spawn new Enemy
            spawnTimer = maxSpawnTimer;
        }
    }

    private Transform GetRandomPosition()
    {
        //Getting new position
        var randomIndex = Random.Range(0, spawnPositions.Length);
        var randomPosition = spawnPositions[randomIndex];

        return randomPosition;
    }

    public void DecreaseEnemyCount()
    {
        //Decrease max enemy count
        maxEnemyCount--;
        enemyCount--;
    }
}
