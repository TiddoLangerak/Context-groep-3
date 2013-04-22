using UnityEngine;
using System.Collections;

/// <summary>
/// Level generator.
/// </summary>
public class LevelGenerator : MonoBehaviour {
	
	public int blockAmount = 4;
	public float blockLength = 50.0f;
	public GameObject levelBlock;
	private int lastAdded = 0;
	public Queue levelBlockQueue = new Queue();
	
	/// <summary>
	/// Adds a levelblock to the Queue.
	/// </summary>
	void AddLevelblock() {
		levelBlockQueue.Enqueue(Instantiate(levelBlock, new Vector3(0.0f, 4.875f, blockLength*lastAdded), Quaternion.identity));
		lastAdded++;
	}

	/// <summary>
	/// Fills the queue with levelBlocks.
	/// </summary>
	void Start () {
		for(int i=0; i<blockAmount; i++)
		{
			AddLevelblock();
		}
		levelBlockQueue.TrimToSize();
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		if((int)transform.position.z > (lastAdded-(blockAmount-1))*blockLength)
		{
			Destroy((Object)levelBlockQueue.Dequeue());
			AddLevelblock();
		}
	}
}
