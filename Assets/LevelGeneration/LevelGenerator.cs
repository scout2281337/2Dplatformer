using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private int gridSize;

    private HashSet<Vector2Int> avalableRooms = new();
    private HashSet<Vector2Int> takenRooms = new HashSet<Vector2Int> {Vector2Int.zero};

    public void GenerateWalls(Rooms_SO rooms_SO)
    {
        foreach(Vector2Int v in takenRooms)
        {
            Vector3 wallPosition = new Vector3(v.x * gridSize, v.y * gridSize, 0);

            if (!takenRooms.Contains(v + Vector2Int.down))
            {
                Instantiate(rooms_SO.walls[0], wallPosition, Quaternion.identity, transform);
            }
            if (!takenRooms.Contains(v + Vector2Int.right))
            {
                Instantiate(rooms_SO.walls[0], wallPosition, Quaternion.Euler(new Vector3(0, 0, 90)), transform);
            }
            if (!takenRooms.Contains(v + Vector2Int.left))
            {
                Instantiate(rooms_SO.walls[0], wallPosition, Quaternion.Euler(new Vector3(0, 0, -90)), transform);
            }
            if (!takenRooms.Contains(v + Vector2Int.up))
            {
                Instantiate(rooms_SO.walls[0], wallPosition, Quaternion.Euler(new Vector3(0, 0, 180)), transform);
            }
        }
    }

    public void GenerateRooms(Rooms_SO rooms_SO)
    {
        foreach (Vector2Int v in avalableRooms)
        {
            if (takenRooms.Contains(v))
                continue;

            Vector3 roomPosition = new Vector3(v.x * gridSize, v.y * gridSize, 0);

            Instantiate(GetRandomRoom(v, rooms_SO), roomPosition, Quaternion.identity, transform);
        }
    }

    public void GenerateBossRoom(Rooms_SO rooms_SO)
    {
        // Boss room generation
        Vector2Int farthestRoom = Vector2Int.zero;
        foreach (Vector2Int v in avalableRooms)
        {
            if (farthestRoom.magnitude < v.magnitude)
                farthestRoom = v;
        }
        Vector3 bossRoomPosition = new Vector3(farthestRoom.x * gridSize, farthestRoom.y * gridSize, 0);
        Instantiate(GetBossRoom(farthestRoom, rooms_SO), bossRoomPosition, Quaternion.identity, transform);
    }

    /// <summary>
    /// Generates a set of vector2int coordinates, so that rooms can be placed in these coordinates
    /// </summary>
    public void WalkMenGenerator (Vector2Int startPosition, int iterationNumber, int pathLength)
    {
        HashSet < Vector2Int > path = new HashSet < Vector2Int >();
        Vector2Int currentPosition = startPosition;

        for (int i = 0; i < iterationNumber; i++)
        {
            currentPosition = startPosition;

            for (int j = 0; j < pathLength; j++)
            {
                currentPosition += GetRandomDirection();
                path.Add(currentPosition);
            }
        }

        avalableRooms = path;
    }

    private Vector2Int GetRandomDirection()
    {
        Vector2Int direction = new Vector2Int();
        switch (UnityEngine.Random.Range(0, 4))
        {
            case 0:
                direction = new Vector2Int(1, 0);
                break;
            case 1:
                direction = new Vector2Int(0, 1);
                break;
            case 2:
                direction = new Vector2Int(-1, 0);
                break;
            case 3:
                direction = new Vector2Int(0, -1);
                break;

        }

        return direction;
    }

    /// <summary>
    /// Gets a random room from rooms_SO, and checks so it doesn't intersect takenRooms
    /// </summary>
    private GameObject GetRandomRoom(Vector2Int position, Rooms_SO rooms_SO)
    {
        GameObject room = rooms_SO.smallRoom[UnityEngine.Random.Range(0, rooms_SO.smallRoom.Length)];
        takenRooms.Add(position);  // Mark this position as taken

        // Check if the room can be placed without intersections
        switch (UnityEngine.Random.Range(0, 3))
        {
            case 0: // 1x2 room
                if (!takenRooms.Contains(position + Vector2Int.up))
                {
                    room = rooms_SO.tallRoom[UnityEngine.Random.Range(1, rooms_SO.tallRoom.Length)];
                    takenRooms.Add(position + Vector2Int.up);  // Mark both positions as taken
                }
                break;

            case 1: // 2x1 room
                if (!takenRooms.Contains(position + Vector2Int.right))
                {
                    room = rooms_SO.longRoom[UnityEngine.Random.Range(0, rooms_SO.tallRoom.Length)];
                    takenRooms.Add(position + Vector2Int.right);  // Mark both positions as taken
                }
                break;

            case 2: // 2x2 room
                if (!takenRooms.Contains(position + Vector2Int.up) &&
                    !takenRooms.Contains(position + Vector2Int.right) &&
                    !takenRooms.Contains(position + Vector2Int.right + Vector2Int.up))
                {
                    room = rooms_SO.bigRoom[UnityEngine.Random.Range(0, rooms_SO.bigRoom.Length)];
                    takenRooms.Add(position + Vector2Int.up);
                    takenRooms.Add(position + Vector2Int.right);
                    takenRooms.Add(position + Vector2Int.right + Vector2Int.up);  // Mark all 4 positions as taken
                }
                break;
        }

        return room;
    }

    private GameObject GetBossRoom(Vector2Int position, Rooms_SO rooms_SO)
    {
        takenRooms.Add(position);
        takenRooms.Add(position + Vector2Int.up);
        takenRooms.Add(position + Vector2Int.right);
        takenRooms.Add(position + Vector2Int.right + Vector2Int.up);

        return rooms_SO.bossRoom[UnityEngine.Random.Range(0, rooms_SO.bossRoom.Length)];
    }
}
