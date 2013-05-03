using System;
using NUnit.Framework;
using Moq;

[TestFixture]
public class ObstacleTest
{
    [Test]
    public void testObstacleCollision()
    {
        var mockObstacleBehaviour = new Mock<IObstacleBehaviour>();
        Obstacle obstacle = new Obstacle(mockObstacleBehaviour.Object);
        
        obstacle.Collision();
        Assert.AreEqual(StateManager.State.DEAD, StateManager.Instance.getCurrentState());
    }
}