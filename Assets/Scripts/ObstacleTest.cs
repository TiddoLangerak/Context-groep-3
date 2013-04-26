using System;
using NUnit.Framework;
using Moq;

[TestFixture]
class ObstacleTest
{
    [Test]
    public void testObstacleCollision()
    {
        var mockObstacleBehaviour = new Mock<IObstalceBehaviour>();
        Obstacle obstacle = new Obstacle(mockObstacleBehaviour.Object);
        
        obstacle.Collision();
        Assert.AreEqual(StateManager.State.DEAD, StateManager.Instance.getCurrentState());
    }
}