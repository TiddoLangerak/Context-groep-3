/// <summary>
/// Interface for the behaviour of the level
/// </summary>
public interface ILevelBehaviour
{
    /// <summary>
    /// Creates a new level block
    /// </summary>
    /// <param name="pos">The position of the new block</param>
    /// <returns>The new level block</returns>
    object MakeLevelBlock(float pos);

    /// <summary>
    /// Destroys the game object
    /// </summary>
    /// <param name="gameObject">The game object to be destroyed</param>
    void DestroyObject(object gameObject);

    /// <summary>
    /// Create a new obtacle
    /// </summary>
    /// <param name="line">The line on which the powerup is placed</param>
    /// <param name="position">The position of the new obstacle</param>
    /// <returns>The new obstacle object</returns>
    object MakeObstacle(int line, float position);

    /// <summary>
    /// Create a new powerup
    /// </summary>
    /// <param name="line">The line on which the powerup is placed</param>
    /// <param name="position">The position of the new powerup</param>
    /// <returns>The new powerup object</returns>
    object MakePowerUp(int line, float position);

    /// <summary>
    /// Create a decoration
    /// </summary>
    /// <param name="left">Decoration left or right</param>
    /// <param name="position">The position of the new decoration</param>
    /// <param name="height">The height of the new decoration</param>
    /// <returns>A new decoration object</returns>
    object MakeDecoration(bool left, float position, int height);
}