using UnityEngine;

/// <summary>
/// The behaviour of an obstacle
/// </summary>
class ObstacleBehaviour : MonoBehaviour, IObstacleBehaviour
{
    /// <summary>
    /// The domain-specific obstacle instance.
    /// </summary>
    private Obstacle obstacle;

    /// <summary>
    /// The game object containing the destroy effect
    /// </summary>
    public GameObject destroyEffect = null;

    /// <summary>
    /// The world of the game
    /// </summary>
    private WorldBehaviour world;

    /// <summary>
    /// Used for initialization by Unity. The Start method is called just
    /// before any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        this.obstacle = new Obstacle(this);
        world = GameObject.Find("World").GetComponent<WorldBehaviour>();
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    /// </summary>
    /// <param name="collision">Collision information</param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Avatar")
        {
            Instantiate(destroyEffect, this.transform.position, destroyEffect.transform.localRotation);
            DestroySelf();
        }

        if (collision.gameObject.name != "LevelPart(Clone)" && audio && audio.enabled && !audio.isPlaying)
        {
            audio.Play();
        }
    }

    /// <summary>
    /// Destroys the obstacle
    /// </summary>
    public void DestroySelf()
    {
        this.obstacle.Collision();
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Reloads the scene
    /// </summary>
    public void ReloadScene()
    {
        world.ReloadScene();
    }
}
