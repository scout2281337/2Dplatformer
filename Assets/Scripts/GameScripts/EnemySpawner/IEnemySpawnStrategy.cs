using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawnStrategy
{
    GameObject SelectEnemy(GameObject[] easyEnemies, GameObject[] mediumEnemies, GameObject[] hardEnemies);
}
