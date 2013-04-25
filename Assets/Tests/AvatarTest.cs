using UnityEngine;
using System.Collections;

public class AvatarTest : UUnitTestCase
{
	protected override void SetUp() 
	{
		Debug.Log("-");
		StateManager.Instance.play();
	}
	
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
		UUnitAssert.Equals(av.moveSpeed, 10);
		UUnitAssert.Equals(++av.moveSpeed, 11);
		av.moveSpeed = 20;
		UUnitAssert.Equals(av.moveSpeed, 20);
	}
	
	[UUnitTest]
	public void sideMovementTest()
	{
		Avatar av = new Avatar();
		av.Left();
		UUnitAssert.Equals(3, av.track);
		av.Right();
		UUnitAssert.Equals(2, av.track);
		av.Right();
		UUnitAssert.Equals(1, av.track);
		av.Left();
		UUnitAssert.Equals(2, av.track);
	}
	
	[UUnitTest]
	public void leftBoundryTest()
	{
		Avatar av = new Avatar();
		av.Left();
		UUnitAssert.Equals(3, av.track);
		av.Left();
		UUnitAssert.Equals(3, av.track);
	}
	
	[UUnitTest]
	public void rightBoundryTest()
	{
		Avatar av = new Avatar();
		av.Right();
		UUnitAssert.Equals(1, av.track);
		av.Right();
		UUnitAssert.Equals(1, av.track);
	}
}