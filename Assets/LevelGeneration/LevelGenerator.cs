using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private int gridSize;
    [SerializeField] private int pathLength;
    [SerializeField] private int iterationNumber;

    [SerializeField] private Rooms_SO rooms_SO;
    private GameObject[][] rooms = new GameObject[5][];
    private HashSet<Vector2Int> takenRooms = new HashSet<Vector2Int> {Vector2Int.zero};

    private void Start()
    {
        rooms[0] = rooms_SO.smallRoom;
        rooms[1] = rooms_SO.tallRoom;
        rooms[2] = rooms_SO.longRoom;
        rooms[3] = rooms_SO.bigRoom;
        rooms[4] = rooms_SO.bossRoom;

        GenerateLevel();
        GenerateWalls();
    }

    private void GenerateWalls()
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

    private void GenerateLevel()
    {
        // Generates vector2int grid for rooms to take
        HashSet<Vector2Int> hashGrid = WalkMenGenerator(new Vector2Int(0, 0), iterationNumber, pathLength);

        // Boss room generation
        Vector2Int farthestRoom = Vector2Int.zero;
        foreach (Vector2Int v in hashGrid)
        {
            if (farthestRoom.magnitude < v.magnitude)
                farthestRoom = v;
        }
        Vector3 bossRoomPosition = new Vector3(farthestRoom.x * gridSize, farthestRoom.y * gridSize, 0);
        Instantiate(GetBossRoom(farthestRoom), bossRoomPosition, Quaternion.identity, transform);

        // All other room generation
        foreach (Vector2Int v in hashGrid)
        {
            if (takenRooms.Contains(v))
                continue;

            Vector3 roomPosition = new Vector3(v.x * gridSize, v.y * gridSize, 0);

            Instantiate(GetRandomRoom(v), roomPosition, Quaternion.identity, transform);
        }
    }

    /// <summary>
    /// Generates a set of vector2int coordinates, so that rooms can be placed in these coordinates
    /// </summary>
    private HashSet<Vector2Int> WalkMenGenerator (Vector2Int startPosition, int iterationAmount, int length)
{
    HashSet < Vector2Int > path = new HashSet < Vector2Int >();
    Vector2Int currentPosition = startPosition;

    for (int i = 0; i < iterationAmount; i++)
    {
        currentPosition = startPosition;

        for (int j = 0; j < length; j++)
        {
            currentPosition += GetRandomDirection();
            path.Add(currentPosition);
        }
    }

    return path;
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
    private GameObject GetRandomRoom(Vector2Int position)
    {
        GameObject room = rooms[0][UnityEngine.Random.Range(0, rooms[0].Length)];
        takenRooms.Add(position);  // Mark this position as taken

        // Check if the room can be placed without intersections
        switch (UnityEngine.Random.Range(0, 3))
        {
            case 0: // 1x2 room
                if (!takenRooms.Contains(position + Vector2Int.up))
                {
                    room = rooms[1][UnityEngine.Random.Range(0, rooms[1].Length)];
                    takenRooms.Add(position + Vector2Int.up);  // Mark both positions as taken
                }
                break;

            case 1: // 2x1 room
                if (!takenRooms.Contains(position + Vector2Int.right))
                {
                    room = rooms[2][UnityEngine.Random.Range(0, rooms[2].Length)];
                    takenRooms.Add(position + Vector2Int.right);  // Mark both positions as taken
                }
                break;

            case 2: // 2x2 room
                if (!takenRooms.Contains(position + Vector2Int.up) &&
                    !takenRooms.Contains(position + Vector2Int.right) &&
                    !takenRooms.Contains(position + Vector2Int.right + Vector2Int.up))
                {
                    room = rooms[3][UnityEngine.Random.Range(0, rooms[3].Length)];
                    takenRooms.Add(position + Vector2Int.up);
                    takenRooms.Add(position + Vector2Int.right);
                    takenRooms.Add(position + Vector2Int.right + Vector2Int.up);  // Mark all 4 positions as taken
                }
                break;
        }

        return room;
    }

    private GameObject GetBossRoom(Vector2Int position)
    {
        takenRooms.Add(position);
        takenRooms.Add(position + Vector2Int.up);
        takenRooms.Add(position + Vector2Int.right);
        takenRooms.Add(position + Vector2Int.right + Vector2Int.up);

        return rooms[4][UnityEngine.Random.Range(0, rooms[4].Length)];
    }
}
