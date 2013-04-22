using UnityEngine;
using System.Collections;

/// <summary>
/// Level generator.
/// </summary>
public class LevelBehavior : MonoBehaviour, ILevelBlockFactory{
	
	public int blockAmount = 4;
	public float blockLength = 50.0f;
	public GameObject levelBlock;
	public Vector3 offset = new Vector3(0.0f, 4.875f, 0);
	public Queue levelBlockQueue = new Queue();
	
	LevelGenerator generator;

	/// <summary>
	/// Makes a levelGenerator
	/// </summary>
	void Start () {
		generator = new LevelGenerator(blockAmount, blockLength, this);
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		generator.update((int)transform.position.z);
	}
	
	/// <summary>
	/// Makes a level block.
	/// </summary>
	/// <returns>
	/// The level block.
	/// </returns>
	/// <param name='position'>
	/// The position on the z axis of the levelBlock.
	/// </param>
	public object makeLevelBlock(float position)
	{
		return Instantiate(levelBlock, offset + new Vector3(0,0,position), Quaternion.identity);
	}
	public void destroyLevelBlock(object levelBlock)
	{
		Destroy((Object)levelBlock);
	}
}
