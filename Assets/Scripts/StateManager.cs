using System;

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
		PLAYING,
		DEAD
	};

    /// <summary>
    /// Current state of the game
    /// </summary>
    private State currentState;

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
	/// The player is dead.
	/// </summary>/
	public void die()
	{
		this.currentState = State.DEAD;
	}
	
	/// <summary>
    /// Returns true iff the game is in the PAUSING state.
	/// </summary>
	/// <returns></returns>
	public Boolean isPausing()
	{
		return State.PAUSING == this.currentState || State.DEAD == this.currentState;
	}
	
	/// <summary>
	/// Ises the playing.
	/// </summary>
	/// <returns>
	/// The playing.
	/// </returns>
	public Boolean isPlaying()
	{
		return State.PLAYING == this.currentState;
	}
	
	public Boolean isDead()
	{
		return State.DEAD == this.currentState;
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
			case State.DEAD:	return "Dead";
		}
		return null;
	}
}