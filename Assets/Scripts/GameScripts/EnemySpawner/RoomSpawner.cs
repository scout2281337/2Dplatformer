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

    public void SpawnEnemies()
    {
        // Получение стратегии на основе сложности
        var factory = new LevelOneSpawnStrategy();
        float difficultyFactor = TimeManager.Instance.time / 60f;
        var strategy = factory.GetStrategy(difficultyFactor);

        // Спавн врагов в каждой точке
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i >= numberOfEnemies) break;

            GameObject enemyToSpawn = strategy.SelectEnemy(easyEnemies, mediumEnemies, hardEnemies);
            Transform spawnPoint = spawnPoints[i];

            SpawnEnemyAtPoint(enemyToSpawn, spawnPoint);
        }
    }

    private void SpawnEnemyAtPoint(GameObject enemyToSpawn, Transform spawnPoint)
    {
        GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);

        // Устанавливаем цель для врага
        if (spawnedEnemy.TryGetComponent<AIDestinationSetter>(out var destinationSetter))
        {
            destinationSetter.target = PlayerManager.Instance.player.transform;
        }
    }
}
