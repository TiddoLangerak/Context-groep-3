using System;
using UnityEngine;

/// <summary>
/// The StateManager class is used to keep track of the game
/// state and the track the player is on.
/// </summary>
public class StateManager
{
    /// <summary>
    /// Enumeration of possible states
    /// </summary>
    public enum State
	{
		PAUSING,
		PLAYING
	};

    /// <summary>
    /// Current state of the game
    /// </summary>
    private State currentState;

    /// <summary>
    /// Current track the player is on
    /// </summary>
    private int track = 2;

    /// <summary>
    /// Singleton StateManager instance
    /// </summary>
    private static StateManager instance;
	
    /// <summary>
    /// Property used to create and return one StateManager instance
    /// </summary>
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
	
    /// <summary>
    /// Private constructor to prevent creation of multiple StateManager
    /// objects. Instead, refer to the Instance property.
    /// </summary>
	private StateManager ()
	{
		this.currentState = State.PAUSING;
	}

    /// <summary>
    /// Toggle between the pausing and playing state.
    /// </summary>
	public void pauseOrUnpause()
	{
		if (currentState == State.PLAYING) {
			this.pause();
		} else {
			this.play();
		}
	}

    /// <summary>
    /// Pause the game
    /// </summary>
	public void pause()
	{
		this.currentState = State.PAUSING;
	}

    /// <summary>
    /// Resume the game
    /// </summary>
	public void play()
	{
		this.currentState = State.PLAYING;
	}

	/// <summary>
    /// Returns true iff the game is in the PAUSING state.
	/// </summary>
	/// <returns></returns>
	public Boolean isPausing()
	{
		return this.currentState == State.PAUSING;
	}
	
    /// <summary>
    /// Move player to the right track.
    /// </summary>
    /// <returns>1 iff a movement is possible</returns>
	public int right()
	{
		if (State.PLAYING == currentState && track > 1) {
			track--;
			return 1;
		} else {
			return 0;
		}
	}
	
    /// <summary>
    /// Move player to the left track.
    /// </summary>
    /// <returns>1 iff a movement is possible</returns>
	public int left()
	{
		if (State.PLAYING == currentState && track < 3) {
			track++;
			return -1;
		} else {
			return 0;	
		}
	}
	
    /// <summary>
    /// Returns the current state.
    /// </summary>
    /// <returns>Current state</returns>
	public State getCurrentState()
	{
		return this.currentState;
	}

    /// <summary>
    /// String representation of the crurent state.
    /// </summary>
    /// <returns>Current state</returns>
	public String toString()
	{
		switch (currentState)
		{
			case State.PAUSING: return "Pausing";
			case State.PLAYING: return "Playing";
		}
	}
}