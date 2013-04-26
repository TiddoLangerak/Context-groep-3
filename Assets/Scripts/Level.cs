using System.Collections;

/// <summary>
/// Level generator.
/// </summary>
public class Level{
	
	private int amount;
	private float length;
	private int lastAdded = 0;
	public Queue levelBlockQueue = new Queue();
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
	public Level(int blockAmount, float blockLength, ILevelBehavior levelBehavior)
	{
		amount = blockAmount;
		length = blockLength;
		behavior = levelBehavior;
		for(int i=0; i<blockAmount; i++)
		{
			levelBlockQueue.Enqueue(behavior.makeLevelBlock(length*lastAdded));
			lastAdded++;
		}
		levelBlockQueue.TrimToSize();
	}
	
	/// <summary>
	/// Checks if new blocks need to be added.
	/// </summary>
	public void update (int position) {
		while(position > (lastAdded-(amount-1))*length)
		{
			behavior.destroyLevelBlock(levelBlockQueue.Dequeue());
			levelBlockQueue.Enqueue(behavior.makeLevelBlock(length*lastAdded));
			lastAdded++;
		}
	}
}
