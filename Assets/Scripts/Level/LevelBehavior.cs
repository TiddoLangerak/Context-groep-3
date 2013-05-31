using UnityEngine;

/// <summary>
/// The behaviour of the level
/// </summary>
public class LevelBehavior : MonoBehaviour, ILevelBehaviour
{
    /// <summary>
    /// The amount of level blocks
    /// </summary>
    public int blockAmount = 4;

    /// <summary>
    /// The length of a level block
    /// </summary>
    public float blockLength = 50.0f;

    /// <summary>
    /// The offset of powerups
    /// </summary>
    public int powerupOffset = 2;

    /// <summary>
    /// The game object belonging to the level block
    /// </summary>
    public GameObject levelBlock;

    /// <summary>
    /// The game object containing the prefab of the end level block
    /// </summary>
    public GameObject endLevelBlockPrefab;

    /// <summary>
    /// The obstacle game objects
    /// </summary>
    public GameObject[] obstacles;

    /// <summary>
    /// The powerup game objects
    /// </summary>
    public GameObject[] powerups;

    /// <summary>
    /// The decoration game objects
    /// </summary>
    public GameObject[] decoration;

    /// <summary>
    /// The offset vector
    /// </summary>
    public Vector3 offset = new Vector3(0.0f, 4.875f, 0);

    /// <summary>
    /// The end level block game object
    /// </summary>
    private GameObject endLevelBlock;

    /// <summary>
    /// The level business logic associated with this behaviour
    /// </summary>
    private Level level;

    /// <summary>
    /// Initializes the end level block and the level.
    /// (This function is called automatically by Unity at startup
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
        level.Update((int)transform.position.z);
    }

    /// <summary>
    /// Creates a level block
    /// </summary>
    /// <param name="position">The position of the level block</param>
    /// <returns>The new level block</returns>
    public object MakeLevelBlock(float position)
    {
        Vector3 pos = offset + new Vector3(0, 0, position);
        endLevelBlock.transform.position = pos + new Vector3(0, 0, blockLength);
        return Instantiate(levelBlock, pos, Quaternion.identity);
    }

    /// <summary>
    /// Destroy a game objects
    /// </summary>
    /// <param name="gameObject">The game objects to be destroyed</param>
    public void DestroyObject(object gameObject)
    {
        Destroy((Object)gameObject);
    }

    /// <summary>
    /// Create a new obstacle
    /// </summary>
    /// <param name="line">The line on which the obstacle is placed</param>
    /// <param name="position">The position of the new obstacle</param>
    /// <returns>A new obstacle</returns>
    public object MakeObstacle(int line, float position)
    {
        return Instantiate(RandomObstacle(), new Vector3(0.0f + 5 * line, 2.875f, position), RandomRotation());
    }

    /// <summary>
    /// Creates a new powerup
    /// </summary>
    /// <param name="line">The line on which the powerup is placed</param>
    /// <param name="position">The position of the new powerup </param>
    /// <returns>A new powerup</returns>
    public object MakePowerUp(int line, float position)
    {
        GameObject powerUpGo = RandomPowerUp();
        return Instantiate(powerUpGo, new Vector3(0.0f + 5 * line, 2.875f, position), powerUpGo.transform.rotation); // Quaternion.identity);
    }

    /// <summary>
    /// Creates a new decoration
    /// </summary>
    /// <param name="left">Place the decoration left or right</param>
    /// <param name="position">The position of the decoration</param>
    /// <param name="height">The height of the decoration</param>
    /// <returns>A new decoration</returns>
    public object MakeDecoration(bool left, float position, int height)
    {
        float posX = 10f;
        if (left)
        {
            posX = -10f;
        }
        return Instantiate(RandomDecoration(), new Vector3(posX, 6.75f + 2 * height, position), Quaternion.identity);
    }

    /// <summary>
    /// Returns the game object of a random obstacle
    /// </summary>
    /// <returns>A random obstacle game object</returns>
    private GameObject RandomObstacle()
    {
        return obstacles[Random.Range(0, obstacles.Length)];
    }

    /// <summary>
    /// Returns the game object of a random powerup
    /// </summary>
    /// <returns>A random powerup game object</returns>
    private GameObject RandomPowerUp()
    {
        return powerups[Random.Range(0, powerups.Length)];
    }

    /// <summary>
    /// Returns the game object of a random decoration
    /// </summary>
    /// <returns>A random decoration game object</returns>
    private GameObject RandomDecoration()
    {
        return decoration[Random.Range(0, decoration.Length)];
    }

    /// <summary>
    /// Returns a Quaternion with a random rotation
    /// </summary>
    /// <returns>A quaternion with random rotation</returns>
    private Quaternion RandomRotation()
    {
        return Quaternion.Euler(0, Random.Range(0, 360), 0);
    }
}
