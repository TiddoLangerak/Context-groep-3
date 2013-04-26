using System;
using NUnit.Framework;
using Moq;

[TestFixture]
class ObstacleTest
{
    [Test]
    public void testObstacleInit()
    {
        var mockObstacleBehaviour = new Mock<IObstalceBehaviour>();
        Obstacle obstacle = new Obstacle(mockObstacleBehaviour.Object);

        // ToDo: Asserts what needs to be true here..
    }
}