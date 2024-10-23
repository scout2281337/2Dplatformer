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
    private bool hasSpawned = false; // Локальный флаг для каждой комнаты

    private void Update()
    {
        timePassed += Time.deltaTime;
    }

    public void SpawnEnemies()
    {
        if (hasSpawned) return; // Проверка на уже заспавненных врагов в комнате

        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemy();
        }

        hasSpawned = true; // Устанавливаем флаг после спавна врагов
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

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            AIDestinationSetter destinationSetter = spawnedEnemy.GetComponent<AIDestinationSetter>();
            if (destinationSetter != null)
            {
                destinationSetter.target = player.transform;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasSpawned)
        {
            SpawnEnemies(); // Спавним врагов, если это ещё не было сделано
        }
    }
}
