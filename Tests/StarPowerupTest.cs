using System;
using System.Timers;
using NUnit.Framework;
using Moq;

[TestFixture]
class StarPowerupTest
{
    [SetUp]
    public void testSetup()
    {
        StateManager.Instance.Score = 0;
        StateManager.Instance.ResetInvincibility();
    }

    [Test]
    public void testInitialState()
    {
        Assert.AreEqual(false, StateManager.Instance.Invincible);
    }

    [Test]
    public void testCollision()
    {
        // Arrange
        var mockStarPowerupBehaviour = new Mock<IStarPowerupBehaviour>();
        var mockTimer = new Mock<ITimer>();

        StarPowerup starPowerup = new StarPowerup(
            mockStarPowerupBehaviour.Object,
            mockTimer.Object
        );

        // Act
        starPowerup.Collision();

        // Assert
        Assert.AreEqual(true, StateManager.Instance.Invincible);
    }

    [Test]
    public void testInvincibility()
    {
        // Arrange
        var mockStarPowerupBehaviour = new Mock<IStarPowerupBehaviour>();
        var mockTimer = new Mock<ITimer>();

        StarPowerup starPowerup = new StarPowerup(
            mockStarPowerupBehaviour.Object,
            mockTimer.Object
        );

        // Act
        starPowerup.Collision();
        mockTimer.Raise(m => m.Elapsed += null, new EventArgs() as ElapsedEventArgs);

        // Assert
        Assert.AreEqual(false, StateManager.Instance.Invincible);
    }
}