public class EnemySpawnStrategy
{
    public IEnemySpawnStrategy GetStrategy(float difficultyFactor)
    {
        if (difficultyFactor < 0.1)
        {
            return new EasyEnemyStrategy();
        }
        else if (difficultyFactor < 0.2)
        {
            return new MediumEnemyStrategy();
        }
        else
        {
            return new HardEnemyStrategy();
        }
    }
}
