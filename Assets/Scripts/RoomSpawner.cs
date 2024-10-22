using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding; // Не забудьте добавить это, если используете A* Pathfinding

public class RoomSpawner : MonoBehaviour
{
    public GameObject[] easyEnemies;
    public GameObject[] mediumEnemies;
    public GameObject[] hardEnemies;
    public int numberOfEnemies;
    public Transform[] spawnPoints;

    private float timePassed;
     

    private void Update()
    {
        timePassed += Time.deltaTime;
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemy();
        }
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

        // Спавн врага
        GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);

        // Поиск игрока по тегу "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Назначаем игрока как цель для компонента AIDestinationSetter у врага
            AIDestinationSetter destinationSetter = spawnedEnemy.GetComponent<AIDestinationSetter>();
            if (destinationSetter != null)
            {
                destinationSetter.target = player.transform;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnEnemies();
        }
    }
}
