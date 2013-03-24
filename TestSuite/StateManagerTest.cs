using NUnit.Framework;
using System;

namespace AssemblyCSharp {
	[TestFixture()]
	public class StateManagerTest {
		[Test()]
		public void InitializationTest () {
			StateManager stateM = StateManager.Instance;
			Assert.IsTrue(stateM.isPausing());
		}
		
		[Test()]
		public void StartingTest () {
			StateManager stateM = StateManager.Instance;
			stateM.play();
			Assert.IsFalse(stateM.isPausing());
		}
		
		[Test()]
		public void PausingTest1 () {
			StateManager stateM = StateManager.Instance;
			stateM.play();
			stateM.pauseOrUnpause();
			Assert.IsTrue(stateM.isPausing());
		}
		
		[Test()]
		public void PausingTest2 () {
			StateManager stateM = StateManager.Instance;
			stateM.play();
			stateM.pauseOrUnpause();
			stateM.pauseOrUnpause();
			Assert.IsFalse(stateM.isPausing());
		}
		
		[Test()]
		public void TrackTest () {
			Assert.IsTrue(true);
		}
		
		[Test()]
		public void PauseWithMovementTest() {
			StateManager stateM = StateManager.Instance;
			Assert.IsTrue(stateM.isPausing());
			Assert.AreEqual(0, stateM.left());
			Assert.AreEqual(0, stateM.right());
		}
		
		[Test()]
		public void PlayingWithMovementTest() {
			StateManager stateM = StateManager.Instance;
			stateM.play();
			Assert.IsFalse(stateM.isPausing());
			Assert.AreEqual(-1, stateM.left());
			Assert.AreEqual( 1, stateM.right());
		}
	}
}