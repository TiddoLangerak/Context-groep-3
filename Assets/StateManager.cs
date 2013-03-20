using System;

public class stateManagerStateManager {
	private State currentState;
	private int track = 2;
	private enum State {
		PAUSING, PLAYING	
	}
	
	public stateManagerStateManager () {
		currentState = State.PAUSING;
	}
	
	public void pauseOrUnpause() {
		if(currentState == State.PLAYING){
			currentState = State.PAUSING;	
		} else {
			currentState = State.PLAYING;	
		}		
	}

	public int right() {
		if(State.PLAYING == currentState && track > 1) {
			track--;
			return -1;
		} else {
			return 0;	
		}
	}
	
	public int left() {
		if(State.PLAYING == currentState && track < 3) {
			track++;
			return 1;
		} else {
			return 0;	
		}
	}
	
	public String getCurrentState() {
		switch (currentState) {
			case State.PAUSING 	: return "Pausing";
			case State.PLAYING 	: return "Playing";
			default 			: return null; // should not happen
		}
	}
}