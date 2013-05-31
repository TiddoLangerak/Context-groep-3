using NUnit.Framework;

[TestFixture]
public class StateManagerTest
{
    [Test]
    public void InitializationTest()
    {
        StateManager stateM = StateManager.Instance;
        Assert.IsTrue(stateM.IsPausing());
    }

    [Test]
    public void StartingTest()
    {
        StateManager stateM = StateManager.Instance;
        stateM.Play();
        Assert.IsFalse(stateM.IsPausing());
    }

    [Test]
    public void PausingTest1()
    {
        StateManager stateM = StateManager.Instance;
        stateM.Play();
        stateM.PauseOrUnpause();
        Assert.IsTrue(stateM.IsPausing());
    }

    [Test]
    public void PausingTest2()
    {
        StateManager stateM = StateManager.Instance;
        stateM.Play();
        stateM.PauseOrUnpause();
        stateM.PauseOrUnpause();
        Assert.IsFalse(stateM.IsPausing());
    }

    [Test]
    public void TrackTest()
    {
        Assert.IsTrue(true);
    }

    [Test]
    public void TestDead()
    {
        // Act
        StateManager.Instance.Die();

        // Assert
        Assert.AreEqual(true, StateManager.Instance.IsDead());
    }

    /*[Test]
    public void PauseWithMovementTest()
    {
        StateManager stateM = StateManager.Instance;
        Assert.IsTrue(stateM.IsPausing());
        Assert.AreEqual(0, stateM.left());
        Assert.AreEqual(0, stateM.right());
    }
	
    [Test]
    public void PlayingWithMovementTest()
    {
        StateManager stateM = StateManager.Instance;
        stateM.Play();
        Assert.IsFalse(stateM.IsPausing());
        Assert.AreEqual(-1, stateM.left());
        Assert.AreEqual( 1, stateM.right());
    }*/
}