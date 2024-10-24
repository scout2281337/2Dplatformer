using UnityEngine;

public class ChildTrigger : MonoBehaviour
{
    private RoomSpawner roomSpawner;

    private void Start()
    {
        // ���� RoomSpawner �� ������������ �������
        roomSpawner = GetComponentInParent<RoomSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            roomSpawner.SpawnEnemies(); // �������� ����� ������ �� ������������ �������
        }
    }
}
