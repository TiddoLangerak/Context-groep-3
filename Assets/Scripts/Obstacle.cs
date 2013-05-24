using System;
using System.Timers;

/// <summary>
/// This class represents the avatar as domain object. Therefore, it
/// is a plain old C# object.
/// </summary>
public class Obstacle
{
    /// <summary>
    /// Reference to IObstacleBehaviour
    /// 
    /// TODO: This reference is currently not used, but will be used in the future.
    /// </summary>
    private IObstacleBehaviour _obstacleBehaviour;
	
	ITimer timer;

    /// <summary>
    /// Initialize obstacle. It is dependend on an IObstacleBehaviour.
    /// </summary>
    /// <param name="obstacleBehaviour">The obstacle behaviour</param>
    public Obstacle(IObstacleBehaviour obstacleBehaviour, ITimer timer)
    {
        this._obstacleBehaviour = obstacleBehaviour;
		this.timer = timer;
		
		this.timer = timer;
        this.timer.Interval = 3000;
        this.timer.Elapsed += new ElapsedEventHandler(ResetGame);
    }

    /// <summary>
    /// This method handles a collision with the avatar.
    /// </summary>
    public void Collision()
    {
		if(!StateManager.Instance.invincible)
		{
        	StateManager.Instance.die();
			timer.Start();
		}
    }
	
	public void ResetGame(object sender, EventArgs e)
    {
		timer.Stop();
		_obstacleBehaviour.ReloadScene();
    }
}