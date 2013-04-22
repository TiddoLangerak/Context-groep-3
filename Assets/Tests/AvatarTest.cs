using UnityEngine;
using System.Collections;

public class AvatarTest : UUnitTestCase
{
	[UUnitTest]
	public void setupTest()
	{
		Avatar av = new Avatar();
		UUnitAssert.Equals(av.track, 2);
	}
	
	[UUnitTest]
	public void speedTest()
	{
		Avatar av = new Avatar();
		UUnitAssert.Equals(av.moveSpeed, 4);
		UUnitAssert.Equals(++av.moveSpeed, 5);
		av.moveSpeed = 10;
		UUnitAssert.Equals(av.moveSpeed, 10);
	}
	
	[UUnitTest]
	public void sideMovementTest()
	{
		Avatar av = new Avatar();
		av.Left();
		UUnitAssert.Equals(av.track, 3);
		av.Right();
		UUnitAssert.Equals(av.track, 2);
		av.Right();
		UUnitAssert.Equals(av.track, 1);
		av.Left();
		UUnitAssert.Equals(av.track, 2);
	}
	
	[UUnitTest]
	public void leftBoundryTest()
	{
		Avatar av = new Avatar();
		av.Left();
		UUnitAssert.Equals(av.track, 3);
		av.Left();
		UUnitAssert.Equals(av.track, 3);
	}
	
	[UUnitTest]
	public void rightBoundryTest()
	{
		Avatar av = new Avatar();
		av.Right();
		UUnitAssert.Equals(av.track, 1);
		av.Right();
		UUnitAssert.Equals(av.track, 1);
	}
}