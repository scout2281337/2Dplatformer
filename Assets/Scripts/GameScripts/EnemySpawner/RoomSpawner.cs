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

        // Получение стратегии на основе сложности
        var factory = new EnemySpawnStrategyFactory();
        float difficultyFactor = timePassed / 60f;
        var strategy = factory.GetStrategy(difficultyFactor);

        // Спавн врагов в каждой точке
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i >= numberOfEnemies) break;

            GameObject enemyToSpawn = strategy.SelectEnemy(easyEnemies, mediumEnemies, hardEnemies);
            Transform spawnPoint = spawnPoints[i];

            SpawnEnemyAtPoint(enemyToSpawn, spawnPoint);
        }

        hasSpawned = true;
    }

    private void SpawnEnemyAtPoint(GameObject enemyToSpawn, Transform spawnPoint)
    {
        GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);

        // Добавляем врага в CombatManager
        CombatManager.Instance.AddObject(spawnedEnemy);

        // Устанавливаем цель для врага
        if (spawnedEnemy.TryGetComponent<AIDestinationSetter>(out var destinationSetter))
        {
            destinationSetter.target = PlayerManager.Instance.player.transform;
        }
    }
}
