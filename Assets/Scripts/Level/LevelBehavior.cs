using UnityEngine;
using System.Collections;

/// <summary>
/// Level generator.
/// </summary>
public class LevelBehavior : MonoBehaviour, ILevelBehavior
{

    public int blockAmount = 4;
    public float blockLength = 50.0f;
	public int powerupOffset = 2;
    public GameObject levelBlock;
	public GameObject endLevelBlockPrefab;
    public GameObject[] obstacles;
	public GameObject[] powerups;
	public GameObject[] decoration;
    public Vector3 offset = new Vector3(0.0f, 4.875f, 0);
    //private int blockAdded = 0;
	private GameObject endLevelBlock;

    Level level;

    /// <summary>
    /// Makes a level
    /// </summary>
    void Start()
    {
		endLevelBlock = (GameObject)Instantiate(endLevelBlockPrefab, offset, endLevelBlockPrefab.transform.localRotation);
        level = new Level(blockAmount, powerupOffset, blockLength, this);
    }

    /// <summary>
    /// Updates the level.
    /// </summary>
    void Update()
    {
        level.update((int)transform.position.z);
    }

    public object makeLevelBlock(float position)
    {
		Vector3 pos = offset + new Vector3(0, 0, position);
		endLevelBlock.transform.position = pos + new Vector3(0, 0, blockLength);
        return Instantiate(levelBlock, pos, Quaternion.identity);
    }

    public void destroyObject(object gameObject)
    {
        Destroy((Object)gameObject);
    }

    public object makeObstacle(int line, float position)
    {
        return Instantiate(randomObject(), new Vector3(0.0f + 5 * line, 2.875f, position), randomRotation());
    }
	
	public object makePowerUp(int line, float position)
	{
		GameObject powerUpGo = randomPowerUp();
		
		return Instantiate(powerUpGo, new Vector3(0.0f + 5 * line, 2.875f, position), powerUpGo.transform.rotation); // Quaternion.identity);
	}
	
	public object makeDecoration(bool left, float position, int height)
	{
		float posX = 10f;
		if(left)
		{
			posX = -10f;	
		}
		return Instantiate(randomDecoration(), new Vector3(posX, 6.75f + 2*height, position), Quaternion.identity);
	}
		

    private GameObject randomObject()
    {
		return obstacles[Random.Range(0, obstacles.Length)];
    }
	
	private GameObject randomPowerUp()
	{
		return powerups[Random.Range(0, powerups.Length)];	
	}
	
	private GameObject randomDecoration()
	{
		return decoration[Random.Range(0, decoration.Length)];
	}

    private Quaternion randomRotation()
    {
        return Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

}