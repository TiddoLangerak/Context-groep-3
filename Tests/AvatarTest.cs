using System.Collections;
using NUnit.Framework;
using Moq;

[TestFixture]
public class AvatarTest
{
    [SetUp]
    public void testSetup()
    {
        StateManager.Instance.pause();
    }

    [Test]
    public void avatarInitializationTest()
    {
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        var mockUserInput = new Mock<IUserInput>();
        Avatar av = new Avatar(mockAvatarBehaviour.Object, mockUserInput.Object);

        Assert.AreEqual(2, av.track);
    }

    [Test]
    public void avatarMovementTest()
    {
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        var mockUserInput = new Mock<IUserInput>();
        Avatar av = new Avatar(mockAvatarBehaviour.Object, mockUserInput.Object);

        av.Left();
        Assert.AreEqual(1, av.track);

        av.Right();
        Assert.AreEqual(2, av.track);

        av.Right();
        Assert.AreEqual(3, av.track);

        av.Left();
        Assert.AreEqual(2, av.track);
    }

    [Test]
    public void leftBoundryTest()
    {
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        var mockUserInput = new Mock<IUserInput>();
        Avatar av = new Avatar(mockAvatarBehaviour.Object, mockUserInput.Object);

        av.Left();
        Assert.AreEqual(1, av.track);

        av.Left();
        Assert.AreEqual(1, av.track);
    }

    [Test]
    public void rightBoundryTest()
    {
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        var mockUserInput = new Mock<IUserInput>();
        Avatar av = new Avatar(mockAvatarBehaviour.Object, mockUserInput.Object);

        av.Right();
        Assert.AreEqual(3, av.track);

        av.Right();
        Assert.AreEqual(3, av.track);
    }

    [Test]
    public void moveDeadAvatarTest()
    {
        // Arrange
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        var mockUserInput = new Mock<IUserInput>();
        
        Avatar av = new Avatar(
            mockAvatarBehaviour.Object,
            mockUserInput.Object
        );

        StateManager.Instance.die();

        // Act
        av.Right();

        // Assert
        Assert.AreEqual(av.track, 2, "A dead avatar should be able to move");
    }
}