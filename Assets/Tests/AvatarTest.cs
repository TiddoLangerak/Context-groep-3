using System.Collections;
using NUnit.Framework;

[TextFixture]
public class AvatarTest
{
    [Test]
	public void testAvatarInitialization()
	{
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        Avatar av = new Avatar(mockAvatarBehaviour);
        Assert.Equals(av.track, 2);
	}

    [Test]
	public void testAvatarSpeed()
	{
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        Avatar av = new Avatar(mockAvatarBehaviour);

		Assert.Equals(av.moveSpeed, 4);

        av.moveSpeed++;
        Assert.Equals(av.moveSpeed, 5);

		av.moveSpeed = 10;
		Assert.Equals(av.moveSpeed, 10);
	}
	
	[Test]
	public void avatarMovementTest()
	{
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        Avatar av = new Avatar(mockAvatarBehaviour);
		
        av.Left();
		Assert.Equals(av.track, 1);
		
        av.Right();
		Assert.Equals(av.track, 2);
		
        av.Right();
		Assert.Equals(av.track, 3);
		
        av.Left();
		Assert.Equals(av.track, 2);
	}
	
	[Test]
	public void leftBoundryTest()
	{
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        Avatar av = new Avatar(mockAvatarBehaviour);
		
        av.Left();
		Assert.Equals(av.track, 1);
		
        av.Left();
		Assert.Equals(av.track, 1);
	}
	
	[Test]
	public void rightBoundryTest()
	{
        var mockAvatarBehaviour = new Mock<IAvatarBehaviour>();
        Avatar av = new Avatar(mockAvatarBehaviour);

		av.Right();
		Assert.Equals(av.track, 3);

		av.Right();
		Assert.Equals(av.track, 3);
	}
}