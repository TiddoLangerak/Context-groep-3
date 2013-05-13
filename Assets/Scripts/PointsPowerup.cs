public class PointsPowerup : IPowerup
{
	public void Collision()
	{
		StateManager.Instance.score += 50;
	}
}
