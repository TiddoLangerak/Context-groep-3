using System;
using UnityEngine;

public class StateManager
{
	private State currentState;
	private int track = 2;
	private static StateManager instance;
	public enum State
	{
		PAUSING,
		PLAYING
	};
	
	public static StateManager Instance 
	{
		get 
		{
			if (instance == null) 
			{
				instance = new StateManager();
			} 
			return instance; 
		}
	}
	
	private StateManager ()
	{
		this.currentState = State.PAUSING;
	}
	
	/**
	 * Toggle between the pausing and playing state
	 */
	public void pauseOrUnpause()
	{
		if (currentState == State.PLAYING) {
			this.pause();
		} else {
			this.play();
		}
	}

	/**
	 * Pause the game
	 */
	public void pause()
	{
		this.currentState = State.PAUSING;
	}

	/**
	 * Resume the game
	 */
	public void play()
	{
		this.currentState = State.PLAYING;
	}
	
	/** 
	 * Returns true iff the game is in the PAUSING state 
	 */
	public Boolean isPausing()
	{
		return this.currentState == State.PAUSING;
	}
	
	
	public int right()
	{
		if (State.PLAYING == currentState && track > 1) {
			track--;
			return 1;
		} else {
			return 0;
		}
	}
	
	public int left()
	{
		if (State.PLAYING == currentState && track < 3) {
			track++;
			return -1;
		} else {
			return 0;	
		}
	}
	
	public State getCurrentState()
	{
		return this.currentState;
	}

	public String toString()
	{
		switch (currentState)
		{
			case State.PAUSING 	: return "Pausing";
			case State.PLAYING 	: return "Playing";
			default 			: return null; // should not happen
		}
	}
}