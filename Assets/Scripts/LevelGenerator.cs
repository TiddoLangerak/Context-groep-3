using System.Collections;

/// <summary>
/// Level generator.
/// </summary>
public class LevelGenerator{
	
	private int amount;
	private float length;
	private int lastAdded = 0;
	public Queue levelBlockQueue = new Queue();
	ILevelBlockFactory factory;
	
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
	public LevelGenerator(int blockAmount, float blockLength, ILevelBlockFactory blockFactory)
	{
		amount = blockAmount;
		length = blockLength;
		factory = blockFactory;
		for(int i=0; i<blockAmount; i++)
		{
			levelBlockQueue.Enqueue(factory.makeLevelBlock(length*lastAdded));
			lastAdded++;
		}
		levelBlockQueue.TrimToSize();
	}
	
	/// <summary>
	/// Checks if new blocks need to be added.
	/// </summary>
	public void update (int position) {
		if(position > (lastAdded-(amount-1))*length)
		{
			factory.destroyLevelBlock(levelBlockQueue.Dequeue());
			levelBlockQueue.Enqueue(factory.makeLevelBlock(length*lastAdded));
			lastAdded++;
		}
	}
}
