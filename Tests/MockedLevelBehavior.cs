/// <summary>
/// Mocked level behavior.
/// 
/// Counts the amount of blocks currently in the game and counts the amount of times the create function is called.
/// </summary>
class MockedLevelBehavior : ILevelBehaviour
{
    private int amountOfLevelBlocks = 0;
    private int amountOfObstacles = 0;
    private int amountOfPowerups = 0;
    private int amountOfDecorations = 0;
    private int timesCalledLevelBlock = 0;
    private int timesCalledObstacle = 0;
    object levelBlockObj = new object();
    object obstacleObj = new object();
    object powerupObj = new object();
    object decorationObj = new object();

    public object MakeLevelBlock(float pos)
    {
        amountOfLevelBlocks++;
        timesCalledLevelBlock++;
        return levelBlockObj;
    }
    public object MakeObstacle(int a, float pos)
    {
        amountOfObstacles++;
        timesCalledObstacle++;
        return obstacleObj;
    }
    public object MakePowerUp(int line, float position)
    {
        amountOfPowerups++;
        return powerupObj;
    }
    public object MakeDecoration(bool left, float position, int height)
    {
        amountOfDecorations++;
        return decorationObj;
    }
    public void DestroyObject(object gameObject)
    {
        if (gameObject == levelBlockObj)
        {
            amountOfLevelBlocks--;
        }
        else if (gameObject == obstacleObj)
        {
            amountOfObstacles--;
        }
        else if (gameObject == powerupObj)
        {
            amountOfPowerups--;
        }
        else if (gameObject == decorationObj)
        {
            amountOfDecorations--;
        }
    }
    public int getAmountOfLevelBlocks()
    {
        return amountOfLevelBlocks;
    }
    public int getAmountOfObstacles()
    {
        return amountOfObstacles;
    }
    public int getAmountOfPowerups()
    {
        return amountOfPowerups;
    }
    public int getAmountOfDecorations()
    {
        return amountOfDecorations;
    }

    public int getTimesCalledLevelBlock()
    {
        return timesCalledLevelBlock;
    }
    public int getTimesCalledObstacle()
    {
        return timesCalledObstacle;
    }
}
