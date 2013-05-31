using System.Timers;
using System;

/// <summary>
/// Implement the business logic of the star powerup
/// </summary>
public class StarPowerup : IPowerup
{
    /// <summary>
    /// The associated behaviour
    /// </summary>
    private IStarPowerupBehaviour behaviour;

    /// <summary>
    /// Timer to keep track of the invincibility time
    /// </summary>
    private ITimer timer;

    /// <summary>
    /// Initiates a new StarPowerup. It requires a reference to
    /// the behaviour and a timer that it uses to keep track of
    /// the invincibility.
    /// </summary>
    /// <param name="behaviour">The behaviour to be used</param>
    /// <param name="timer">The timer to be used</param>
    public StarPowerup(IStarPowerupBehaviour behaviour, ITimer timer)
    {
        this.behaviour = behaviour;
        this.timer = timer;
        this.timer.Elapsed += new ElapsedEventHandler(UndoInvincibility);
    }

    /// <summary>
    /// Called when the avatar and this StarPowerup collide. It
    /// changes state to invicible, adds particles (through the
    /// behaviour).
    /// </summary>
    public void Collision()
    {
        StateManager.Instance.MakeInvincible();
        timer.Start();
        behaviour.AddParticles();
    }

    /// <summary>
    /// Undo the invincibility. Normally called upon elapse event
    /// of the timer.
    /// </summary>
    public void UndoInvincibility(object sender, EventArgs e)
    {
        StateManager.Instance.UndoInvincible();
        timer.Stop();
    }
}