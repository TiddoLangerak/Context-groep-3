using UnityEngine;
using System.Collections;

public class XAppleCTest : UUnitTestCase
{
	[UUnitTest]
	public void firstTest() {
	  	UUnitAssert.Equals( "one", XAppleC.returnOne() );
	  	Debug.Log("C# test runs");
	}
}

