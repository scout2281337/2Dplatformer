using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemyStrategy : IEnemySpawnStrategy
{

    public GameObject SelectEnemy(GameObject[] easyEnemies, GameObject[] mediumEnemies, GameObject[] hardEnemies)
    {
        return mediumEnemies[Random.Range(0, easyEnemies.Length)];
    }
}
