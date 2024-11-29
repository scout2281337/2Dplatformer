using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector : MonoBehaviour
{
    [SerializeField] private int iterationNumber;
    [SerializeField] private int pathLength;

    [SerializeField] private LevelGenerator levelGenerator;

    private void Start()
    {
        levelGenerator.WalkMenGenerator(new Vector2Int(0, 0), iterationNumber, pathLength);
        levelGenerator.GenerateBossRoom();
        levelGenerator.GenerateRooms();
        levelGenerator.GenerateWalls();
    }
}
