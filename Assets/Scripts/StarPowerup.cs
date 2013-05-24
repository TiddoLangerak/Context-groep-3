using System.Timers;
using System;

public class StarPowerup : IPowerup
{
    /// <summary>
    /// StarPowerup behaviour
    /// </summary>
    IStarPowerupBehaviour behaviour;

    /// <summary>
    /// Timer to keep track of the invincibility time
    /// </summary>
    ITimer timer;

    /// <summary>
    /// Initiates a new StarPowerup. It requiers a reference to
    /// the behaviour and a timer that it uses to keep track of
    /// the invincibility.
    /// </summary>
    /// <param name="behaviour"></param>
    /// <param name="timer"></param>
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
		StateManager.Instance.makeInvincible();

		timer.Start();

		behaviour.addParticles();
	}

    /// <summary>
    /// Undo the invincibility. Normally called upon elapse event
    /// of the timer.
    /// </summary>
    public void UndoInvincibility(object sender, EventArgs e)
    {
        StateManager.Instance.undoInvincible();
        timer.Stop();
    }
}