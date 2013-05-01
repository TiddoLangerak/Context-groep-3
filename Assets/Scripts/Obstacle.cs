using System;

/// <summary>
/// This class represents the avatar as domain object. Therefore, it
/// is a plain old C# object.
/// </summary>
public class Obstacle
{
    /// <summary>
    /// Reference to IObstacleBehaviour
    /// </summary>
    private IObstacleBehaviour _obstacleBehaviour;

    /// <summary>
    /// Initialize obstacle. It is dependend on an IObstacleBehaviour.
    /// </summary>
    /// <param name="obstacleBehaviour">The obstacle behaviour</param>
    public Obstacle(IObstacleBehaviour obstacleBehaviour)
    {
        this._obstacleBehaviour = obstacleBehaviour;
    }

    /// <summary>
    /// This method handles a collision with the avatar.
    /// </summary>
    public void Collision()
    {
        StateManager.Instance.die();
    }
}