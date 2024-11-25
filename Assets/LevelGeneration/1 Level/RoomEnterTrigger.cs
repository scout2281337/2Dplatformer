using UnityEngine;

public class RoomEnterTrigger : MonoBehaviour
{
    private bool hasSpawned = false;
    private RoomSpawner roomSpawner;
    [SerializeField] private Vector2Int roomSize;

    private void Start()
    {
        roomSpawner = GetComponentInParent<RoomSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        CameraManager.Instance.NewRoomView(roomSize, transform.position);

        if (hasSpawned)
            return;

        roomSpawner.SpawnEnemies();
        CombatManager.Instance.LockRoom(roomSize, transform.parent.position);

        hasSpawned = true;
    }
}
