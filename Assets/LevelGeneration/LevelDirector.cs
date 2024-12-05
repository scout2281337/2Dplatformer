using UnityEngine;
using Zenject;

public class LevelDirector : Singleton<LevelDirector>
{
    public Rooms_SO rooms_SO;
    private LevelGenerator levelGenerator;
    [Inject]
    public void Init(LevelGenerator levelGenerator) 
    {
        this.levelGenerator = levelGenerator;
    
    }

    [SerializeField] private int iterationNumber;
    [SerializeField] private int pathLength;

    private void Start()
    {
        levelGenerator.WalkMenGenerator(new Vector2Int(0, 0), iterationNumber, pathLength);
        levelGenerator.GenerateBossRoom(rooms_SO);
        levelGenerator.GenerateRooms(rooms_SO);
        levelGenerator.GenerateWalls(rooms_SO);
    }
}
