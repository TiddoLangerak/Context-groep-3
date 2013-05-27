public class PointsPowerup : IPowerup
{
    public const int PointsPerPlayer = 150;
	public void Collision()
	{
		StateManager.Instance.score += PointsPerPlayer * StateManager.Instance.NumberOfPlayers;
	}
}
