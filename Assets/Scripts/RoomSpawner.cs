using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding; // �� �������� �������� ���, ���� ����������� A* Pathfinding

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

        // ����� �����
        GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);

        // ����� ������ �� ���� "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // ��������� ������ ��� ���� ��� ���������� AIDestinationSetter � �����
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
