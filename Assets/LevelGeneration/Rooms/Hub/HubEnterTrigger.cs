using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubEnterTrigger : MonoBehaviour
{
    [SerializeField] private Vector2Int roomSize;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        CameraManager.instance.NewRoomView(roomSize, transform.position);
    }
}
