using System.Collections;
using System;

/// <summary>
/// Level generator.
/// </summary>
public class Level{
	
	private int blockAmount;
	private float blockLength;
	private int lastAdded = 0;
	public Queue levelBlockQueue = new Queue();
	public Queue obstacleBlockQueue = new Queue();
	public Queue powerUpQueue = new Queue();
	ILevelBehavior behavior;
	
	/// <summary>
	/// Sets up the <see cref="LevelGenerator"/> class and creates the first LevelBlocks.
	/// </summary>
	/// <param name='blockAmount'>
	/// Block amount.
	/// </param>
	/// <param name='blockLength'>
	/// Block length.
	/// </param>
	/// <param name='blockFactory'>
	/// Block factory.
	/// </param>
	public Level(int blockAmount, int powerupOffset, float blockLength, ILevelBehavior levelBehavior)
	{
		this.blockAmount = blockAmount;
		this.blockLength = blockLength;
		behavior = levelBehavior;
		for(int i=0; i<blockAmount; i++)
		{
			addBlock();
			if (i>=powerupOffset)
			{
				addObstacles();
				AddPowerUp();
			}
		}
		levelBlockQueue.TrimToSize();
	}
	
	/// <summary>
	/// Checks if new blocks need to be added.
	/// </summary>
	public void update (int position) {
		while(position > (lastAdded-(blockAmount-1))*blockLength)
		{
			behavior.destroyObject(levelBlockQueue.Dequeue());
			if(obstacleBlockQueue.Count == blockAmount *2)
			{
				behavior.destroyObject(obstacleBlockQueue.Dequeue());
				behavior.destroyObject(obstacleBlockQueue.Dequeue());
			}
			if(powerUpQueue.Count == blockAmount)
			{
				behavior.destroyObject(powerUpQueue.Dequeue());
			}
			addBlock();
			addObstacles();
			AddPowerUp();
		}
	}
	
	private void addBlock() 
	{
		levelBlockQueue.Enqueue(behavior.makeLevelBlock(blockLength*lastAdded));
		lastAdded++;
	}
	
	private void addObstacles()
	{
		int line1 = randomLine();
		int line2 = (line1 + 2) % 3 - 1;
		obstacleBlockQueue.Enqueue(behavior.makeObstacle(line1, blockLength*lastAdded - blockLength));
		obstacleBlockQueue.Enqueue(behavior.makeObstacle(line2, blockLength*lastAdded - blockLength));
	}
	
	private void AddPowerUp()
	{
		powerUpQueue.Enqueue(behavior.makePowerUp(randomLine(), blockLength* lastAdded - blockLength/2));
	}
	
	private int randomLine()
	{
		Random r = new Random();
		int output = r.Next(-1, 2);
		return output;
	}
}
