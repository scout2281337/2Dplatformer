using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubEnterTrigger : MonoBehaviour
{
    [SerializeField] private Vector2Int roomSize;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        // Sends room information to camera manager
        CameraManager.Instance.NewRoomView(roomSize, transform.position);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        TimeManager.Instance.ActivateTime();
    }
}
