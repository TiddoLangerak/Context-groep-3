/// <summary>
/// Implements the business logic of the points(=money) powerup
/// </summary>
public class PointsPowerup : IPowerup
{
    /// <summary>
    /// The points that are added for each player after a collision
    /// </summary>
    public const int POINTS_PER_PLAYER = 150;

    /// <summary>
    /// Increase the score based on the nr. of players after collision
    /// </summary>
    public void Collision()
    {
        StateManager.Instance.Score += POINTS_PER_PLAYER * StateManager.Instance.NumberOfPlayers;
    }
}
