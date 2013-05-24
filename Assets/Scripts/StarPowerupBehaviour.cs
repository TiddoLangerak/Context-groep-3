using UnityEngine;
using System.Collections;
using System.Timers;

public class StarPowerupBehaviour : MonoBehaviour, IStarPowerupBehaviour
{
	protected StarPowerup powerup;
	private GameObject particles;
	public GameObject particlePrefab;
	public float duration = 5;

	// Use this for initialization
	void Start ()
	{
        var timer = new TimerAdapter();
        timer.Interval = (int)(duration * 1000);

		powerup = new StarPowerup(this, timer);
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
	public void AddParticles()
	{
		GameObject avatar = GameObject.Find("Avatar");
		particles = (GameObject) Instantiate(particlePrefab, avatar.transform.position + particlePrefab.transform.position, Quaternion.identity);
		particles.transform.parent = avatar.transform;
	}
}