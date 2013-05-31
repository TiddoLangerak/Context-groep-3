using System;
using System.Diagnostics;

/// <summary>
/// The StateManager class (=singleton class) is used to keep track of the game
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
    /// Tells if the start screen should be shown or not
    /// </summary>
    public bool ShowStartScreen { get; set; }

    /// <summary>
    /// Holds true iff there is a new money powerup
    /// </summary>
    public bool NewMoneyPowerup { get; set; }

    /// <summary>
    /// The current state of the game
    /// </summary>
    private State currentState;

    /// <summary>
    /// The instance
    /// </summary>
    private static StateManager instance;

    /// <summary>
    /// Stores the score.
    /// </summary>
    private float _score;

    /// <summary>
    /// Keeps track of the nr. of players (= the multiplier)
    /// </summary>
    public int NumberOfPlayers { get; set; }

    /// <summary>
    /// Getter and setter for the score. (INV: score >= 0)
    /// </summary>
    public float Score
    {
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

    /// <summary>
    /// Reset the state manager to a new instance
    /// </summary>
    public static void Reset()
    {
        instance = new StateManager();
    }

    /// <summary>
    /// Hold true iff the player is currently invincible.
    /// </summary>
    private int _invincible;
    public bool Invincible
    {
        get
        {
            return (_invincible >= 1);
        }
    }

    /// <summary>
    /// Makes the avatar invincible
    /// </summary>
    public void MakeInvincible()
    {
        _invincible++;
    }

    /// <summary>
    /// Makes the avatar vincible
    /// </summary>
    public void UndoInvincible()
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

    /// <summary>
    /// Private constructor to prevent creation of multiple StateManager
    /// objects. Instead, refer to the Instance property.
    /// </summary>
    private StateManager()
    {
        this.currentState = State.PAUSING;
        Score = 0;
    }

    /// <summary>
    /// Toggle between the pausing and playing state.
    /// </summary>
    public void PauseOrUnpause()
    {
        if (currentState == State.PLAYING)
        {
            this.Pause();
        }
        else
        {
            this.Play();
        }
    }

    /// <summary>
    /// Pause the game
    /// </summary>
    public void Pause()
    {
        this.currentState = State.PAUSING;
    }

    /// <summary>
    /// Resume the game
    /// </summary>
    public void Play()
    {
        this.currentState = State.PLAYING;
    }

    /// <summary>
    /// The player has died
    /// </summary>/
    public void Die()
    {
        this.currentState = State.DEAD;
    }

    /// <summary>
    /// Returns true iff the game is in the PAUSING state.
    /// </summary>
    /// <returns>True iff the game state is PAUSING</returns>
    public bool IsPausing()
    {
        return State.PAUSING == this.currentState || State.DEAD == this.currentState;
    }

    /// <summary>
    /// Checks if the game is playing
    /// </summary>
    /// <returns>True if the game is playing, false otherwise</returns>
    public bool IsPlaying()
    {
        return State.PLAYING == this.currentState;
    }

    /// <summary>
    /// Checks if the player is dead
    /// </summary>
    /// <returns>True iff the player is dead</returns>
    public bool IsDead()
    {
        return State.DEAD == this.currentState;
    }

    /// <summary>
    /// Returns the current state.
    /// </summary>
    /// <returns>Current state</returns>
    public State GetCurrentState()
    {
        return this.currentState;
    }

    /// <summary>
    /// String representation of the crurent state.
    /// </summary>
    /// <returns>Current state</returns>
    public override String ToString()
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