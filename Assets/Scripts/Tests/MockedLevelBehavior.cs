/// <summary>
/// Mocked level behavior.
/// 
/// Counts the amount of blocks currently in the game and counts the amount of times the create function is called.
/// </summary>
class MockedLevelBehavior : ILevelBehavior
{
	private int amountOfLevelBlocks = 0;
	private int timesCalled = 0;
	public object makeLevelBlock(float pos)
	{
		amountOfLevelBlocks++;
		timesCalled++;
		return new object();
	}
	public void destroyLevelBlock(object levelBlock)
	{
		amountOfLevelBlocks--;
	}
	public int getAmountOfLevelBlocks()
	{
		return amountOfLevelBlocks;
	}
	public int getTimesCalled()
	{
		return timesCalled;
	}
}
