using System;
using NUnit.Framework;

[TestFixture]
class PointsPowerupTest
{
    [SetUp]
    public void testSetup()
    {
        StateManager.Instance.score = 0;
        StateManager.Instance.NumberOfPlayers = 1;
    }

    [Test]
    public void testInitialization()
    {
        PointsPowerup pp = new PointsPowerup();

        Assert.AreEqual(0, StateManager.Instance.score);
    }

    [Test]
    public void testCollisionSingle()
    {
        PointsPowerup pp = new PointsPowerup();
        pp.Collision();

        Assert.AreEqual(50, StateManager.Instance.score);
    }

    [Test]
    public void testCollisionDouble()
    {
        PointsPowerup pp = new PointsPowerup();
        pp.Collision();
        pp.Collision();

        Assert.AreEqual(100, StateManager.Instance.score);
    }
}
