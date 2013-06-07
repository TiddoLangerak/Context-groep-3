using NUnit.Framework;

/// <summary>
/// Tests the level.
/// </summary>
[TestFixture]
public class LevelTest
{
    MockedLevelBehavior levelBehavior;
    Level level;

    const int blockAmount = 5;
    const int powerupOffset = 0;
    const float blockLength = 1.0f;

    [SetUp]
    public void SetUp()
    {
        levelBehavior = new MockedLevelBehavior();
    }


    /// <summary>
    /// Tests if the right amount of blocks are created at startup.
    /// </summary>
    [Test]
    public void blockAmountStartTest()
    {
        level = new Level(blockAmount, powerupOffset, blockLength, levelBehavior);

        Assert.AreEqual(blockAmount, levelBehavior.getAmountOfLevelBlocks());
    }

    /// <summary>
    /// Tests if the right amount of blocks stay in the game.
    /// Every time a new block is created an old block must be destroyed.
    /// </summary>
    [Test]
    public void blockAmountUpdateTest()
    {
        level = new Level(blockAmount, powerupOffset, blockLength, levelBehavior);

        level.Update((int)blockLength);

        Assert.AreEqual(blockAmount, levelBehavior.getAmountOfLevelBlocks());

        level.Update((int)(50 * blockLength));

        Assert.AreEqual(blockAmount, levelBehavior.getAmountOfLevelBlocks());
    }

    /// <summary>
    /// Tests if the right amount of blocks get created by counting the times the create function is called.
    /// </summary>
    [Test]
    public void timesCalledTest()
    {
        level = new Level(blockAmount, powerupOffset, blockLength, levelBehavior);

        level.Update((int)(blockLength));

        Assert.AreEqual(blockAmount, levelBehavior.getTimesCalledLevelBlock());

        level.Update((int)(50 * blockLength));

        Assert.AreEqual(blockAmount + 49, levelBehavior.getTimesCalledLevelBlock());
    }

    [Test]
    public void itemsOffsetTest()
    {
        level = new Level(blockAmount, 2, blockLength, levelBehavior);

        Assert.AreEqual(6, levelBehavior.getAmountOfObstacles());
        Assert.AreEqual(3, levelBehavior.getAmountOfPowerups());
    }

    [Test]
    public void powerupAmountTest()
    {
        level = new Level(blockAmount, 2, blockLength, levelBehavior);

        level.Update((int)(50 * blockLength));

        Assert.AreEqual(blockAmount, levelBehavior.getAmountOfPowerups());
    }
}
