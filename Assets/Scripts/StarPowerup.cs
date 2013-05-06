class StarPowerup : IPowerup
{
	public void Collision()
	{
		StateManager.Instance.invincible = true;
	}
}
