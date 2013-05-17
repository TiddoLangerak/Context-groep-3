using UnityEngine;
using System.Collections;

class ObstacleBehaviour : MonoBehaviour, IObstacleBehaviour
{
    /// <summary>
    /// The domain-specific obstacle instance.
    /// </summary>
    private Obstacle obstacle;
	public GameObject destroyEffect = null;

    /// <summary>
    /// Used for initialization by Unity. The Start method is called just
    /// before any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {	
        this.obstacle = new Obstacle(this);
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    /// </summary>
    /// <param name="collision">Collision information</param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "ShoppingCart")
		{
			Instantiate(destroyEffect, this.transform.position, destroyEffect.transform.localRotation);
            DestroySelf();
        } 
        if (collision.gameObject.name != "LevelPart(Clone)" && audio.enabled && !audio.isPlaying)
        {
            audio.Play();
        }
    }
	
	public void DestroySelf()
	{
		this.obstacle.Collision();
		Destroy(this.gameObject);
	}
}
