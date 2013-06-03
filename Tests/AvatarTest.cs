using NUnit.Framework;
using Moq;

[TestFixture]
public class AvatarTest
{
    [SetUp]
    public void testSetup()
    {
        StateManager.Instance.Pause();
    }

    [Test]
    public void avatarInitializationTest()
    {
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        var mockUserInput = new Mock<IUserInput>();
        Avatar av = new Avatar(mockAvatarBehaviour.Object, mockUserInput.Object);

        Assert.AreEqual(2, av.Track);
    }

    [Test]
    public void avatarMovementTest()
    {
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        var mockUserInput = new Mock<IUserInput>();
        Avatar av = new Avatar(mockAvatarBehaviour.Object, mockUserInput.Object);

        av.Left();
        Assert.AreEqual(1, av.Track);

        av.Right();
        Assert.AreEqual(2, av.Track);

        av.Right();
        Assert.AreEqual(3, av.Track);

        av.Left();
        Assert.AreEqual(2, av.Track);
    }

    [Test]
    public void leftBoundryTest()
    {
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        var mockUserInput = new Mock<IUserInput>();
        Avatar av = new Avatar(mockAvatarBehaviour.Object, mockUserInput.Object);

        av.Left();
        Assert.AreEqual(1, av.Track);

        av.Left();
        Assert.AreEqual(1, av.Track);
    }

    [Test]
    public void rightBoundryTest()
    {
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        var mockUserInput = new Mock<IUserInput>();
        Avatar av = new Avatar(mockAvatarBehaviour.Object, mockUserInput.Object);

        av.Right();
        Assert.AreEqual(3, av.Track);

        av.Right();
        Assert.AreEqual(3, av.Track);
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

        StateManager.Instance.Die();

        // Act
        av.Right();

        // Assert
        Assert.AreEqual(av.Track, 2, "A dead avatar should be able to move");
    }
}