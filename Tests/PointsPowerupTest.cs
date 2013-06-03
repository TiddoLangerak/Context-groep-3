using NUnit.Framework;

[TestFixture]
class PointsPowerupTest
{
    [SetUp]
    public void testSetup()
    {
        StateManager.Instance.Score = 0;
        StateManager.Instance.NumberOfPlayers = 1;
    }

    [Test]
    public void testInitialization()
    {
        PointsPowerup pp = new PointsPowerup();

        Assert.AreEqual(0, StateManager.Instance.Score);
    }

    [Test]
    public void testCollisionSingle()
    {
        // Arrange
        PointsPowerup pp = new PointsPowerup();
        float score = StateManager.Instance.Score;

        // Act
        pp.Collision();

        // Assert
        Assert.AreEqual(score + PointsPowerup.POINTS_PER_PLAYER, StateManager.Instance.Score);
    }

    [Test]
    public void testCollisionDouble()
    {
        // Arrange
        PointsPowerup pp = new PointsPowerup();
        float score = StateManager.Instance.Score;

        // Act
        pp.Collision();
        pp.Collision();

        // Assert
        Assert.AreEqual(score + 2 * PointsPowerup.POINTS_PER_PLAYER, StateManager.Instance.Score);
    }
}
