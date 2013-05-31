/// <summary>
/// The business logic of an obstacle
/// </summary>
public class Obstacle
{
    /// <summary>
    /// Reference to IObstacleBehaviour
    /// </summary>
    private IObstacleBehaviour _obstacleBehaviour;

    /// <summary>
    /// Initialize obstacle. It depends on an IObstacleBehaviour.
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
        if (!StateManager.Instance.Invincible)
        {
            StateManager.Instance.Die();
            _obstacleBehaviour.ReloadScene();
        }
    }
}