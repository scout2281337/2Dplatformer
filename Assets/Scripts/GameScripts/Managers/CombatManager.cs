using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Singleton<CombatManager>
{
    public GameObject grid;
    public Rooms_SO rooms_SO;
    public Dictionary<GameObject, IDamageable> idamageableDict = new();
    private List<GameObject> enemyList = new();

    private GameObject wallLock;

    public void AddObject(GameObject obj)
    {
        if(obj.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            idamageableDict.Add(obj, damageable);
        }
    }

    public void RemoveObject(GameObject obj)
    {
        idamageableDict.Remove(obj);
    }

    public void AddEnemy(GameObject enemy)
    {
        enemyList.Add(enemy);
        AddObject(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemyList.Remove(enemy);
        RemoveObject(enemy);

        if(enemyList.Count > 0)
            return;

        UnLockRoom();
    }

    public void LockRoom(Vector2Int roomSize, Vector2 position)
    {
        // Small room
        if (roomSize == new Vector2Int(1, 1))
        {
            wallLock = Instantiate(rooms_SO.wallLock[0], position, Quaternion.identity, grid.transform);
        }
        // Tall room
        else if (roomSize == new Vector2Int(1, 2))
        {
            wallLock = Instantiate(rooms_SO.wallLock[1], position, Quaternion.identity, grid.transform);
        }
        // Long room
        else if (roomSize == new Vector2Int(2, 1))
        {
            wallLock = Instantiate(rooms_SO.wallLock[2], position, Quaternion.identity, grid.transform);
        }
        // Big room
        else if (roomSize == new Vector2Int(2, 1))
        {
            wallLock = Instantiate(rooms_SO.wallLock[3], position, Quaternion.identity, grid.transform);
        }
        else
        {
            Debug.LogWarning("incorrectroomsize");
        }
    }

    private void UnLockRoom()
    {
        Destroy(wallLock);
    }
}
