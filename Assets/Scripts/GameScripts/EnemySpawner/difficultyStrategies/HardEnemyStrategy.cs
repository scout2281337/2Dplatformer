using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardEnemyStrategy : IEnemySpawnStrategy
{

    public GameObject SelectEnemy(GameObject[] easyEnemies, GameObject[] mediumEnemies, GameObject[] hardEnemies)
    {
        return hardEnemies[Random.Range(0, easyEnemies.Length)];
    }
}
