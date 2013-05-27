using System;
using System.Diagnostics;

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

    public bool ShowStartScreen { get; set; }
    public bool NewMoneyPowerup { get; set; } 

    /// <summary>
    /// Current state of the game
    /// </summary>
    private State currentState;

    /// <summary>
    /// Singleton StateManager instance
    /// </summary>
    private static StateManager instance;
	
	/// <summary>
	/// Stores the score.
	/// </summary>
	private float _score;
    public int NumberOfPlayers { get; set; }
	
	/// <summary>
	/// Get and setters for the score. (INV: score >= 0)
	/// </summary>
	public float score {
		get
		{
			return _score;
		}
		set
		{
			Debug.Assert(value >= 0);
			_score = value;
		}
	}

    public static void Reset()
    {
        instance = new StateManager();
    }
	
	/// <summary>
	/// If the player is currently invincible.
	/// </summary>
	private int _invincible;
	public bool invincible {
		get
		{
			return (_invincible >= 1);
		}
	}
	
	public void makeInvincible()
	{
		_invincible++;
	}
	public void undoInvincible()
	{
		_invincible--;
	}

    /// <summary>
    /// Reset invincibility to 0. This is used mainly by the unit
    /// tests to force the StateManager into a specific state.
    /// </summary>
    public void ResetInvincibility()
    {
        _invincible = 0;
    }

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

    public static void reset()
    {
        instance = new StateManager();
    }


    /// <summary>
    /// Private constructor to prevent creation of multiple StateManager
    /// objects. Instead, refer to the Instance property.
    /// </summary>
    private StateManager()
    {
        this.currentState = State.PAUSING;
		score = 0;
    }

    /// <summary>
    /// Toggle between the pausing and playing state.
    /// </summary>
    public void pauseOrUnpause()
    {
        if (currentState == State.PLAYING)
        {
            this.pause();
        }
        else
        {
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
    public bool isPausing()
    {
        return State.PAUSING == this.currentState || State.DEAD == this.currentState;
    }

    /// <summary>
    /// Checks if the game is playing
    /// </summary>
    /// <returns>
    /// True if the game is playing, false otherwise.
    /// </returns>
    public bool isPlaying()
    {
        return State.PLAYING == this.currentState;
    }

    /// <summary>
    /// Checks if the player is dead
    /// </summary>
    /// <returns>
    /// True if the player is dead, false otherwise.
    /// </returns>
    public bool isDead()
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
            case State.DEAD: return "Dead";
        }
        return null;
    }
}