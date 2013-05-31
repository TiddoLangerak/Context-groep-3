using UnityEngine;

/// <summary>
/// Implements the behaviour of the star powerup
/// </summary>
public class StarPowerupBehaviour : MonoBehaviour, IStarPowerupBehaviour
{
    /// <summary>
    /// The StarPowerup associated with this behaviour
    /// </summary>
    protected StarPowerup powerup;

    /// <summary>
    /// The particles for the particle effect
    /// </summary>
    private GameObject particles;

    /// <summary>
    /// The prefab used for the particle effect
    /// </summary>
    public GameObject particlePrefab;

    /// <summary>
    /// The duration of the particle effect
    /// </summary>
    public float duration = 5;

    /// <summary>
    /// Initializes the instance variables of the object. 
    /// (This function is called by Unity automatically)
    /// </summary>
    void Start()
    {
        var timer = new TimerAdapter();
        timer.Interval = (int)(duration * 1000);
        powerup = new StarPowerup(this, timer);
    }

    /// <summary>
    /// Implements the behaviour of this object when it collides.
    /// When it collides with the avatar, the particle effect is started.
    /// (This function is called by Unity automatically)
    /// </summary>
    /// <param name="collision">The data of the collision</param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "ShoppingCart")
        {
            powerup.Collision();
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Adds particles to the avatar to create a particle effect when the avatar collides with a star powerup.
    /// </summary>
    public void AddParticles()
    {
        GameObject avatar = GameObject.Find("Avatar");
        particles = (GameObject)Instantiate(particlePrefab, avatar.transform.position + particlePrefab.transform.position, Quaternion.identity);
        particles.transform.parent = avatar.transform;
    }
}