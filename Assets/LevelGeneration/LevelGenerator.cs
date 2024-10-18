using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    #region vars
    public int gridSize;
    public int pathLength;
    public int iterationNumber;

    public GameObject[] roomSmall = new GameObject[1];
    public GameObject[] roomTall = new GameObject[1];
    public GameObject[] roomLong = new GameObject[1];
    public GameObject[] roomBig = new GameObject[1];
    private GameObject[][] rooms = new GameObject[4][];


    public GameObject wall;
    private HashSet<Vector2Int> takenRooms = new HashSet<Vector2Int> {Vector2Int.zero};
    #endregion

    private void Start()
    {
        rooms[0] = roomSmall;
        rooms[1] = roomTall;
        rooms[2] = roomLong;
        rooms[3] = roomBig;

        GenrateLevel();
        GenerateWalls();
    }

    private void GenerateWalls()
    {
        foreach(Vector2Int v in takenRooms)
        {
            Vector3 wallPosition = new Vector3(v.x * gridSize, v.y * gridSize, 0);

            if (!takenRooms.Contains(v + Vector2Int.down))
            {
                Instantiate(wall, wallPosition, Quaternion.identity, transform);
            }
            if (!takenRooms.Contains(v + Vector2Int.right))
            {
                Instantiate(wall, wallPosition, Quaternion.Euler(new Vector3(0, 0, 90)), transform);
            }
            if (!takenRooms.Contains(v + Vector2Int.left))
            {
                Instantiate(wall, wallPosition, Quaternion.Euler(new Vector3(0, 0, -90)), transform);
            }
            if (!takenRooms.Contains(v + Vector2Int.up))
            {
                Instantiate(wall, wallPosition, Quaternion.Euler(new Vector3(0, 0, 180)), transform);
            }
        }
    }

    private void GenrateLevel()
    {
        HashSet<Vector2Int> hashGrid = WalkMenGenerator(new Vector2Int(0, 0), iterationNumber, pathLength); // Generates vector2int grid for rooms to take
        List<Vector2Int> sortedGrid = new List<Vector2Int>(hashGrid); // Makes a list out of the Hashset

        // Sort by x first, then by y
        sortedGrid.Sort((a, b) =>
        {
            int compareY = a.y.CompareTo(b.y);  // Compare by y first
            if (compareY == 0)
            {
                // If y values are the same, compare x
                return a.x.CompareTo(b.x);
            }
            return compareY;
        });

        foreach (Vector2Int v in sortedGrid)
        {
            if (takenRooms.Contains(v)) continue;

            Vector3 roomPosition = new Vector3(v.x * gridSize, v.y * gridSize, 0);

            GameObject newRoom = Instantiate(GetRandomRoom(v), roomPosition, Quaternion.identity, transform);
        }
    }
    private HashSet<Vector2Int> WalkMenGenerator (Vector2Int startPosition, int iterationAmount, int length)
    {
        HashSet < Vector2Int > path = new HashSet < Vector2Int >();
        Vector2Int currentPosition = startPosition;

        for (int i = 0; i < iterationAmount; i++)
        {
            currentPosition = startPosition;

            for (int j = 0; j < length; j++)
            {
                currentPosition += GetRandomDiraction();
                path.Add(currentPosition);
            }
        }

        return path;
    }

    private Vector2Int GetRandomDiraction()
    {
        Vector2Int diraction = new Vector2Int();
        switch (UnityEngine.Random.Range(0, 4))
        {
            case 0:
                diraction = new Vector2Int(1, 0);
                break;
            case 1:
                diraction = new Vector2Int(0, 1);
                break;
            case 2:
                diraction = new Vector2Int(-1, 0);
                break;
            case 3:
                diraction = new Vector2Int(0, -1);
                break;

        }

        return diraction;
    }

    private GameObject GetRandomRoom(Vector2Int position)
    {
        GameObject room = rooms[0][UnityEngine.Random.Range(0, rooms[0].Length)];
        takenRooms.Add(position);  // Mark this position as taken

        // Check if the room can be placed without intersections
        switch (UnityEngine.Random.Range(0, 4))
        {
            case 0: // 1x1 room
                break;

            case 1: // 1x2 room
                if (!takenRooms.Contains(position + Vector2Int.up))
                {
                    room = rooms[1][UnityEngine.Random.Range(0, rooms[1].Length)];
                    takenRooms.Add(position + Vector2Int.up);  // Mark both positions as taken
                }
                break;

            case 2: // 2x1 room
                if (!takenRooms.Contains(position + Vector2Int.right))
                {
                    room = rooms[2][UnityEngine.Random.Range(0, rooms[2].Length)];
                    takenRooms.Add(position + Vector2Int.right);  // Mark both positions as taken
                }
                break;

            case 3: // 2x2 room
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
}
