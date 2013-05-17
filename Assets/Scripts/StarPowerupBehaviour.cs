using UnityEngine;
using System.Collections;

public class StarPowerupBehaviour : MonoBehaviour, IStarPowerupBehaviour
{
	protected StarPowerup powerup;
	private GameObject particles;
	public GameObject particlePrefab;
	public float duration = 5;

	// Use this for initialization
	void Start ()
	{
		powerup = new StarPowerup(this, new TimerAdapter(), duration);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "ShoppingCart")
        {
            powerup.Collision();
			Destroy(this.gameObject);
        }
    }
	public void addParticles()
	{
		GameObject avatar = GameObject.Find("Avatar");
		particles = (GameObject) Instantiate(particlePrefab, avatar.transform.position + particlePrefab.transform.position, Quaternion.identity);
		particles.transform.parent = avatar.transform;
	}
}