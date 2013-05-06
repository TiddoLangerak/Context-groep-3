using UnityEngine;
using System.Collections;

/// <summary>
/// Level generator.
/// </summary>
public class LevelBehavior : MonoBehaviour, ILevelBehavior
{

    public int blockAmount = 4;
    public float blockLength = 50.0f;
    public GameObject levelBlock;
    public GameObject[] obstacles;
    public Vector3 offset = new Vector3(0.0f, 4.875f, 0);
    private int blockAdded = 0;

    Level level;

    /// <summary>
    /// Makes a level
    /// </summary>
    void Start()
    {
        level = new Level(blockAmount, blockLength, this);
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
        return Instantiate(levelBlock, offset + new Vector3(0, 0, position), Quaternion.identity);
    }

    public void destroyObject(object gameObject)
    {
        Destroy((Object)gameObject);
    }

    public object makeObstacle(int line, float position)
    {
        return Instantiate(randomObject(), new Vector3(0.0f + 5 * line, 4.875f, position), randomRotation());
    }

    private GameObject randomObject()
    {
        blockAdded++;
        if (blockAdded % 10 == 0)
        {
            return obstacles[2];
        }
        else
        {
            return obstacles[Random.Range(0, 2)];
        }
    }

    private Quaternion randomRotation()
    {
        return Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

}
