using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private int gridSize;
    [SerializeField] private Vector2 cameraSize = new();
    private Vector2 minViewPos = new();
    private Vector2 maxViewPos = new();
    private GameObject player;

    protected override void Awake()
    {
        base.Awake();

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("CameraManager could not find Player");
            return;
        }
    }

    private void Update()
    {
        // Clampin camera positions so that it wont go trought a room
        transform.position = new Vector2(
            Mathf.Clamp(player.transform.position.x, minViewPos.x, maxViewPos.x),
            Mathf.Clamp(player.transform.position.y, minViewPos.y, maxViewPos.y));
    }

    public void NewRoomView(Vector2Int size, Vector2 position)
    {
        // Geting the corners of the room
        Vector2 leftBotom = position - new Vector2(gridSize, gridSize) * size / 2;
        Vector2 rightTop = position + new Vector2(gridSize, gridSize) * size / 2;

        // Adding camera offset
        minViewPos = leftBotom + cameraSize / 2;
        maxViewPos = rightTop - cameraSize / 2;

        // Sentering camera in x, because camera is wider than room of size 1 by x
        if (size.x == 1)
        {
            minViewPos.x = position.x;
            maxViewPos.x = position.x;
        }
    }
}
