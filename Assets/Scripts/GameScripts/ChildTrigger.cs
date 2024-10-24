using UnityEngine;

public class ChildTrigger : MonoBehaviour
{
    private RoomSpawner roomSpawner;

    private void Start()
    {
        // Ищем RoomSpawner на родительском объекте
        roomSpawner = GetComponentInParent<RoomSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            roomSpawner.SpawnEnemies(); // Вызываем спавн врагов на родительском объекте
        }
    }
}
