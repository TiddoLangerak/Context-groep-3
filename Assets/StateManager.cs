using System;

public class StateManager {
	private State currentState;	
	private enum State {
		PAUSING, PLAYING, DEAD	
	}
	
	public stateManager () {
		currentState = State.PAUSING;
	}
	
	public void start() {
		currentState = State.PLAYING;
	}
	
	public void pause() {
		currentState = State.PAUSING;	
	}
	
	public void restart() {
	
	}
	
	public int forward() {
		
	}
	
	public int backward() {
		
	}
	
	public int left() {
		if(State.PLAYING == 
	}
	
	public int right() {
		
	}
	
	public String getCurrentState() {
		switch (currentState) {
			case State.PAUSING 	: return "Pausing";
			case State.PLAYING 	: return "Playing";
			case State.DEAD 	: return "Dead";
			default 			: return null; // should not happen
		}
	}
}
