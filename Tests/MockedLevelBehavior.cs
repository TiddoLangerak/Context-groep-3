/// <summary>
/// Mocked level behavior.
/// 
/// Counts the amount of blocks currently in the game and counts the amount of times the create function is called.
/// </summary>
class MockedLevelBehavior : ILevelBehavior
{
	private int amountOfLevelBlocks = 0;
	private int amountOfObstacles = 0;
	private int timesCalledLevelBlock = 0;
	private int timesCalledObstacle = 0;
	object levelBlockObj = new object();
	object obstacleObj = new object();
	
	public object makeLevelBlock(float pos)
	{
		amountOfLevelBlocks++;
		timesCalledLevelBlock++;
		return levelBlockObj;
	}
	public object makeObstacle(int a, float pos)
	{
		amountOfObstacles++;
		timesCalledObstacle++;
		return obstacleObj;
	}
	public void destroyObject(object gameObject)
	{
		if(gameObject == levelBlockObj)
		{
			amountOfLevelBlocks--;
		}
		else if (gameObject == obstacleObj)
		{
			amountOfObstacles--;
		}
	}
	public int getAmountOfLevelBlocks()
	{
		return amountOfLevelBlocks;
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
