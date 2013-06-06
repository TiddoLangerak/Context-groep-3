using System.Collections;
using System;

/// <summary>
/// Generator for level blocks
/// </summary>
public class Level
{
    /// <summary>
    /// The amount of blocks
    /// </summary>
    private int blockAmount;

    /// <summary>
    /// The length of a block
    /// </summary>
    private float blockLength;

    /// <summary>
    /// The nr of a block that was last added
    /// </summary>
    private int lastAdded = 0;

    /// <summary>
    /// The queue of level blocks
    /// </summary>
    public Queue levelBlockQueue = new Queue();

    /// <summary>
    /// The queue of obstacles
    /// </summary>
    public Queue obstacleBlockQueue = new Queue();

    /// <summary>
    /// The queue of powerups
    /// </summary>
    public Queue powerUpQueue = new Queue();

    /// <summary>
    /// The queue of decorations
    /// </summary>
    public Queue decorationQueue = new Queue();

    /// <summary>
    /// The behaviour of the level
    /// </summary>
    private ILevelBehaviour behaviour;

    /// <summary>
    /// Sets up the <see cref="LevelGenerator"/> class and creates the first LevelBlocks.
    /// </summary>
    /// <param name='blockAmount'>The amount of blocks</param>
    /// <param name='blockLength'>The length of the blocks</param>
    /// <param name='levelBehavior'>The behaviour of the level</param>
    public Level(int blockAmount, int powerupOffset, float blockLength, ILevelBehaviour levelBehavior)
    {
        this.blockAmount = blockAmount;
        this.blockLength = blockLength;
        behaviour = levelBehavior;
        for (int i = 0; i < blockAmount; i++)
        {
            AddBlock();
            AddDecoration();
            if (i >= powerupOffset)
            {
                AddObstacles();
                AddPowerUp();
            }
        }
        levelBlockQueue.TrimToSize();
    }

    /// <summary>
    /// Checks if new blocks need to be added and adds them if needed
    /// </summary>
    public void Update(int position)
    {
        while (position > (lastAdded - (blockAmount - 1)) * blockLength)
        {
            behaviour.DestroyObject(levelBlockQueue.Dequeue());
            DestroyObstaclesWhenNeeded();
            DestroyPowerupWhenNeeded();
            DestroyDecoration();
            AddBlock();
            AddObstacles();
            AddPowerUp();
            AddDecoration();
        }
    }

    /// <summary>
    /// Destroy a powerup when necessary
    /// </summary>
    private void DestroyPowerupWhenNeeded()
    {
        if (powerUpQueue.Count == blockAmount)
        {
            behaviour.DestroyObject(powerUpQueue.Dequeue());
        }
    }

    /// <summary>
    /// Destroy two obstacles when necessary
    /// </summary>
    private void DestroyObstaclesWhenNeeded()
    {
        if (obstacleBlockQueue.Count == blockAmount * 2)
        {
            behaviour.DestroyObject(obstacleBlockQueue.Dequeue());
            behaviour.DestroyObject(obstacleBlockQueue.Dequeue());
        }
    }

    /// <summary>
    /// Add a new level block to the game environment
    /// </summary>
    private void AddBlock()
    {
        levelBlockQueue.Enqueue(behaviour.MakeLevelBlock(blockLength * lastAdded));
        lastAdded++;
    }
	
	private void AddOneObstacle()
	{
		obstacleBlockQueue.Enqueue(behaviour.MakeObstacle(RandomLine(), blockLength * lastAdded - blockLength));
	}
	private void AddTwoObstacles()
	{
		int line1 = RandomLine();
        int line2 = (line1 + 2) % 3 - 1;
        obstacleBlockQueue.Enqueue(behaviour.MakeObstacle(line1, blockLength * lastAdded - blockLength));
        obstacleBlockQueue.Enqueue(behaviour.MakeObstacle(line2, blockLength * lastAdded - blockLength));
	}
	private void AddThreeObstacles()
	{
		obstacleBlockQueue.Enqueue(behaviour.MakeObstacle(-1, blockLength * lastAdded - blockLength));
	    obstacleBlockQueue.Enqueue(behaviour.MakeObstacle(0, blockLength * lastAdded - blockLength));
		obstacleBlockQueue.Enqueue(behaviour.MakeObstacle(1, blockLength * lastAdded - blockLength));
	}

    /// <summary>
    /// Add two new obstacles to the game environment
    /// </summary>
    private void AddObstacles()
    {
		Random r = new Random();
        double chance = r.NextDouble();
		if (lastAdded < 5)
		{
			if(chance < 0.4)
				AddOneObstacle();
			else
				AddTwoObstacles();
		}
		else if (lastAdded < 15)
		{
			AddTwoObstacles();
		}
		else
		{
			if(chance<0.8)
			{
				AddTwoObstacles();
			}
			else
			{
				AddThreeObstacles();
			}
		}
		
    }

    /// <summary>
    /// Add a new powerup to the game environment
    /// </summary>
    private void AddPowerUp()
    {
        powerUpQueue.Enqueue(behaviour.MakePowerUp(RandomLine(), blockLength * lastAdded - blockLength / 2));
    }

    /// <summary>
    /// Add decorations to the game environment
    /// </summary>
    private void AddDecoration()
    {
        for (int i = 0; i < 50; i++)
        {
            decorationQueue.Enqueue(behaviour.MakeDecoration(i % 2 == 0, blockLength * lastAdded - i * 3, RandomLine()));
        }
    }

    /// <summary>
    /// Destroy the decorations
    /// </summary>
    private void DestroyDecoration()
    {
        for (int i = 0; i < 50; i++)
        {
            behaviour.DestroyObject(decorationQueue.Dequeue());
        }
    }
    /// <summary>
    /// Gives the number of a line, at ramdom
    /// </summary>
    /// <returns>The number of the line</returns>
    private int RandomLine()
    {
        Random r = new Random();
        int output = r.Next(-1, 2);
        return output;
    }
}
