using UnityEngine;
using System.Collections;

public class LevelTest : UUnitTestCase
{
	MockedLevelBehavior levelBehavior;
	Level level;
	
	const int blockAmount = 5;
	const float blockLength = 1.0f;
	
	protected override void SetUp()
	{
		levelBehavior = new MockedLevelBehavior();
	}
	
	/// <summary>
	/// Tests if the right amount of blocks are created at startup.
	/// </summary>
	[UUnitTest]
	public void blockAmountStartTest()
	{
		level = new Level(blockAmount, blockLength, levelBehavior);
		
		UUnitAssert.Equals(blockAmount,levelBehavior.getAmountOfLevelBlocks());
	}
	
	/// <summary>
	/// Tests if the right amount of blocks stay in the game.
	/// Every time a new block is created an old block must be destroyed.
	/// </summary>
	[UUnitTest]
	public void blockAmountUpdateTest()
	{
		level = new Level(blockAmount, blockLength, levelBehavior);
		
		level.update((int)blockLength);
		
		UUnitAssert.Equals(blockAmount,levelBehavior.getAmountOfLevelBlocks());
		
		level.update((int)(50*blockLength));
		
		UUnitAssert.Equals(blockAmount,levelBehavior.getAmountOfLevelBlocks());
	}
	
	/// <summary>
	/// Tests if the right amount of blocks get created by counting the times the create function is called.
	/// </summary>
	[UUnitTest]
	public void timesCalledTest()
	{
		level = new Level(blockAmount, blockLength, levelBehavior);
		
		level.update((int)(blockLength));
		
		UUnitAssert.Equals(blockAmount,levelBehavior.getTimesCalled());
		
		level.update((int)(50*blockLength));
		
		UUnitAssert.Equals(blockAmount+49,levelBehavior.getTimesCalled());
	}
}
