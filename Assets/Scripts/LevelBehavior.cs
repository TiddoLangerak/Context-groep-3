using UnityEngine;
using System.Collections;

/// <summary>
/// Level generator.
/// </summary>
public class LevelBehavior : MonoBehaviour, ILevelBehavior{
	
	public int blockAmount = 4;
	public float blockLength = 50.0f;
	public GameObject levelBlock;
	public Vector3 offset = new Vector3(0.0f, 4.875f, 0);
	Level level;

	/// <summary>
	/// Makes a level
	/// </summary>
	void Start () {
		level = new Level(blockAmount, blockLength, this);
	}
	
	/// <summary>
	/// Updates the level.
	/// </summary>
	void Update () {
		level.update((int)transform.position.z);
	}
	
	public object makeLevelBlock(float position)
	{
		return Instantiate(levelBlock, offset + new Vector3(0,0,position), Quaternion.identity);
	}

	public void destroyLevelBlock(object levelBlock)
	{
		Destroy((Object)levelBlock);
	}
}
