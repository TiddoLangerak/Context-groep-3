using UnityEngine;
using System.Collections;

/// <summary>
/// Level generator.
/// </summary>
public class LevelGenerator : MonoBehaviour {
	
	public int blockAmount = 4;
	public float blockLength = 50.0f;
	public GameObject levelBlock;
	private GameObject[] obstacles;
	private int lastAdded = 0;
	private int blockAdded = 0;
	public Queue levelBlockQueue = new Queue();
	public Queue obstacleBlockQ = new Queue();
	
	/// <summary>
	/// Adds a levelblock to the Queue.
	/// </summary>
	void AddLevelblock()
	{
		levelBlockQueue.Enqueue(Instantiate(levelBlock, new Vector3(0.0f, 4.875f, blockLength*lastAdded), Quaternion.identity));
		lastAdded++;
	}
	
	int randomLine()
	{
		return Random.Range(-1,2);	
	}
	
	GameObject randomObject() 
	{
		blockAdded++;
		Logger.Log(blockAdded % 10);
		if(blockAdded % 10 == 0)
		{
			Logger.Log("Bier");
			return obstacles[2];
		}
		else 
		{
			return obstacles[Random.Range(0,2)];
		}
	}
	
	/// <summary>
	/// Adds the obstacle.
	/// </summary>
	void AddObstacle()
	{
		
		obstacleBlockQ.Enqueue(Instantiate(randomObject(), 
			new Vector3(0.0f + 5*randomLine(), 4.875f, blockLength*lastAdded - blockLength + 10 * randomLine()), randomObject().transform.rotation));
		obstacleBlockQ.Enqueue(Instantiate(randomObject(), 
			new Vector3(0.0f + 5*randomLine(), 4.875f, blockLength*lastAdded - blockLength + 10 * randomLine()), randomObject().transform.rotation));
	}
	
	/// <summary>
	/// Removes the obstacle.
	/// </summary>
	void RemoveObstacle()
	{
		Destroy((Object)obstacleBlockQ.Dequeue());
		Destroy((Object)obstacleBlockQ.Dequeue());
		
	}
	
	/// <summary>
	/// Fills the queue with levelBlocks.
	/// </summary>
	void Start ()
	{
		obstacles = new GameObject[3];
		obstacles[0] = GameObject.Find("PagePapier");
		obstacles[1] = GameObject.Find("CupASoup");
		obstacles[2] = GameObject.Find("BeerStack");
		for(int i=0; i<blockAmount; i++)
		{
			AddLevelblock();
			AddObstacle();
		}
		levelBlockQueue.TrimToSize();
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () 
	{
		if((int)transform.position.z > (lastAdded-(blockAmount-1))*blockLength)
		{
			Destroy((Object)levelBlockQueue.Dequeue());
			RemoveObstacle();
			AddObstacle();
			AddLevelblock();
		}
	}
}
