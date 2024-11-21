public class EnemySpawnStrategyFactory
{
    public IEnemySpawnStrategy GetStrategy(float difficultyFactor)
    {
        if (difficultyFactor < 1)
        {
            return new EasyEnemyStrategy();
        }
        else if (difficultyFactor < 2)
        {
            return new MediumEnemyStrategy();
        }
        else
        {
            return new HardEnemyStrategy();
        }
    }
}
