using System.Timers;
using System;

public class StarPowerup : IPowerup
{
	Timer timer = new Timer();
	IStarPowerupBehaviour behaviour;
	
	public StarPowerup(IStarPowerupBehaviour behaviour)
	{
		this.behaviour = behaviour;
	}
	
	public void timerElapsed(object sender, EventArgs e)
	{
		timer.Stop();
		StateManager.Instance.undoInvincible();
	}
	
	public void Collision()
	{
		StateManager.Instance.makeInvincible();
		timer.Interval = 4000;
		timer.Elapsed += new ElapsedEventHandler(timerElapsed);
		timer.Start();
		behaviour.addParticles();
	}
}
