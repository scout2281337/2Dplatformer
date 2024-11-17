using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RoomSpawner : MonoBehaviour
{
    public GameObject[] easyEnemies;
    public GameObject[] mediumEnemies;
    public GameObject[] hardEnemies;
    public int numberOfEnemies;
    public Transform[] spawnPoints;

    private float timePassed;
    private bool hasSpawned = false;

    private void Update()
    {
        timePassed += Time.deltaTime;
    }

    public void SpawnEnemies()
    {
        if (hasSpawned)
            return;

        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemy();
        }

        hasSpawned = true;
    }

    private void SpawnEnemy()
    {
        GameObject[] selectedArray;
        float difficultyFactor = timePassed / 60f;

        if (difficultyFactor < 1)
        {
            selectedArray = easyEnemies;
        }
        else if (difficultyFactor < 2)
        {
            selectedArray = mediumEnemies;
        }
        else
        {
            selectedArray = hardEnemies;
        }

        GameObject enemyToSpawn = selectedArray[Random.Range(0, selectedArray.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
        EnemyManager.Instance.AddEnemy(spawnedEnemy);

        if (spawnedEnemy.TryGetComponent<AIDestinationSetter>(out var destinationSetter))
        {
            destinationSetter.target = PlayerManager.Instance.player.transform;
        }
    }
}
