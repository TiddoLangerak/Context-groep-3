using System.Collections;
//using NUnit.Framework;
//using Moq;
/*
[TestFixture]
public class AvatarTest
{
    [SetUp]
    public void testSetup()
    {
        StateManager.Instance.pause();
    }

    [Test]
    public void testAvatarInitialization()
    {
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        Avatar av = new Avatar(mockAvatarBehaviour.Object);
        Assert.AreEqual(av.track, 2);
    }

    [Test]
    public void testAvatarSpeed()
    {
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        Avatar av = new Avatar(mockAvatarBehaviour.Object);

        Assert.AreEqual(av.moveSpeed, 4);

        av.moveSpeed++;
        Assert.AreEqual(av.moveSpeed, 5);

        av.moveSpeed = 10;
        Assert.AreEqual(av.moveSpeed, 10);
    }

    [Test]
    public void avatarMovementTest()
    {
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        Avatar av = new Avatar(mockAvatarBehaviour.Object);

        av.Left();
        Assert.AreEqual(av.track, 1);

        av.Right();
        Assert.AreEqual(av.track, 2);

        av.Right();
        Assert.AreEqual(av.track, 3);

        av.Left();
        Assert.AreEqual(av.track, 2);
    }

    [Test]
    public void leftBoundryTest()
    {
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        Avatar av = new Avatar(mockAvatarBehaviour.Object);

        av.Left();
        Assert.AreEqual(av.track, 1);

        av.Left();
        Assert.AreEqual(av.track, 1);
    }

    [Test]
    public void rightBoundryTest()
    {
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        Avatar av = new Avatar(mockAvatarBehaviour.Object);

        av.Right();
        Assert.AreEqual(av.track, 3);

        av.Right();
        Assert.AreEqual(av.track, 3);
    }
}*/