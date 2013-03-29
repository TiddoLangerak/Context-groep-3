#pragma strict

class XAppleTest extends UUnitTestCase {
  @UUnitTest
  function firstTest() {
  	UUnitAssert.Equals( "one", XApple.returnOne() );
  	Debug.Log("javascript test runs");
  }
}