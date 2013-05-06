using System.Collections;
using System;

/// <summary>
/// Level generator.
/// </summary>
public class Level
{

    private int blockAmount;
    private float blockLength;
    private int lastAdded = 0;
    public Queue levelBlockQueue = new Queue();
    public Queue obstacleBlockQ = new Queue();
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
        this.blockAmount = blockAmount;
        this.blockLength = blockLength;
        behavior = levelBehavior;
        for (int i = 0; i < blockAmount; i++)
        {
            addBlock();
            addObstacles();
        }
        levelBlockQueue.TrimToSize();
    }

    /// <summary>
    /// Checks if new blocks need to be added.
    /// </summary>
    public void update(int position)
    {
        while (position > (lastAdded - (blockAmount - 1)) * blockLength)
        {
            behavior.destroyObject(levelBlockQueue.Dequeue());
            behavior.destroyObject(obstacleBlockQ.Dequeue());
            behavior.destroyObject(obstacleBlockQ.Dequeue());
            addBlock();
            addObstacles();
        }
    }

    private void addBlock()
    {
        levelBlockQueue.Enqueue(behavior.makeLevelBlock(blockLength * lastAdded));
        lastAdded++;
    }

    private void addObstacles()
    {
        obstacleBlockQ.Enqueue(behavior.makeObstacle(randomLine(), blockLength * lastAdded - blockLength));
        obstacleBlockQ.Enqueue(behavior.makeObstacle(randomLine(), blockLength * lastAdded - blockLength));
    }

    private int randomLine()
    {
        Random r = new Random();

        return r.Next(-1, 2);
    }
}
