using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int gridSize;
    public int pathLength;
    public int iterationNumber;
    public GameObject[] rooms = new GameObject[4];
    private HashSet<Vector2Int> takenRooms = new HashSet<Vector2Int> {Vector2Int.zero};

    private void Start()
    {
        GenrateLevel();
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
        GameObject room = null;

        switch(UnityEngine.Random.Range(0, 4))
        {
            case 0:
                room = rooms[0]; 
                takenRooms.Add(position);
                break;
            case 1:
                room = rooms[1];
                takenRooms.Add(position);
                takenRooms.Add(position + Vector2Int.up);
                break;
            case 2:
                room = rooms[2];
                takenRooms.Add(position);
                takenRooms.Add(position + Vector2Int.right);
                break;
            case 3:
                room = rooms[3];
                takenRooms.Add(position);
                takenRooms.Add(position + Vector2Int.up);
                takenRooms.Add(position + Vector2Int.right);
                takenRooms.Add(position + Vector2Int.right + Vector2Int.up);
                break;
        }

        return room;
    }
}
