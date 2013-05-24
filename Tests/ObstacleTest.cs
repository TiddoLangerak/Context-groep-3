using System;
using NUnit.Framework;
using Moq;

[TestFixture]
public class ObstacleTest
{
    [Test]
    public void testPlayerDiesOnCollision()
    {
        // Arrange
        var mockObstacleBehaviour = new Mock<IObstacleBehaviour>();
        Obstacle obstacle = new Obstacle(mockObstacleBehaviour.Object);

        // Act
        obstacle.Collision();

        // Assert
        Assert.AreEqual(StateManager.State.DEAD, StateManager.Instance.getCurrentState());
    }

    [Test]
    public void testResetGame()
    {
        // Arrange
        var mockObstacleBehaviour = new Mock<IObstacleBehaviour>();
        mockObstacleBehaviour
            .Expect(m => m.ReloadScene())
            .Verifiable();

        Obstacle obstacle = new Obstacle(mockObstacleBehaviour.Object);

        // Act
        obstacle.Collision();

        // Assert
        mockObstacleBehaviour.Verify(m => m.ReloadScene());
    }

    [Test]
    public void testScoreAfterReset()
    {
        // Arrange
        var mockObstacleBehaviour = new Mock<IObstacleBehaviour>();
        mockObstacleBehaviour
            .Expect(m => m.ReloadScene())
            .Verifiable();

        Obstacle obstacle = new Obstacle(mockObstacleBehaviour.Object);

        // Act
        obstacle.Collision();

        // Assert
        Assert.AreEqual(0, StateManager.Instance.score);
    }
}