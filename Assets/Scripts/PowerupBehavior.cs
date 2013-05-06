using UnityEngine;
using System.Collections;

public class PowerupBehavior : MonoBehaviour, IPowerupBehavior
{
	public IPowerup powerup;

	// Use this for initialization
	void Start ()
	{
		powerup = new PointsPowerup();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
	
	void OnCollisionEnter(Collision collision)
    {
		Logger.Log ("ABC");
        if (collision.gameObject.name == "Avatar")
        {
            powerup.Collision();
			Destroy(this);
			Logger.Log ("DEF");
        }
    }
}
