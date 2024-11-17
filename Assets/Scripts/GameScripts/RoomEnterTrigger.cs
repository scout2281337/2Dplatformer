using UnityEngine;

public class RoomEnterTrigger : MonoBehaviour
{
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
        
        roomSpawner.SpawnEnemies();
        CameraManager.Instance.NewRoomView(roomSize, transform.position);
    }
}
