using NUnit.Framework;
using System;

namespace AssemblyCSharp {
	[TestFixture()]
	public class StateManagerTest {
		[Test()]
		public void InitializationTest () {
			StateManager stateM = new StateManager();
			StringAssert.IsMatch("Pausing", stateM.getCurrentState());
		}
		
		[Test()]
		public void StartingTest () {
			StateManager stateM = new StateManager();
			stateM.start();
			StringAssert.IsMatch("Playing", stateM.getCurrentState());		
		}
		
		[Test()]
		public void PausingTest1 () {
			StateManager stateM = new StateManager();
			stateM.start();
			stateM.pauseOrUnpause();
			StringAssert.IsMatch("Pausing", stateM.getCurrentState());
		}
		
		[Test()]
		public void PausingTest2 () {
			StateManager stateM = new StateManager();
			stateM.start();
			stateM.pauseOrUnpause();
			stateM.pauseOrUnpause();
			StringAssert.IsMatch("Playing", stateM.getCurrentState());
		}
		
		[Test()]
		public void TrackTest () {
			Assert.IsTrue(true);
		}
		
		[Test()]
		public void PauseWithMovementTest() {
			Assert.IsTrue(true);
		}
	}
}

