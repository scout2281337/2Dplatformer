using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemyStrategy : IEnemySpawnStrategy
{
    
    public GameObject SelectEnemy(GameObject[] easyEnemies, GameObject[] mediumEnemies, GameObject[] hardEnemies)
        {
            return easyEnemies[Random.Range(0, easyEnemies.Length)];
        }
}
