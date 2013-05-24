using UnityEngine;
using System.Collections;

public class PointsPowerupBehaviour : MonoBehaviour, IPointsPowerupBehaviour
{
	protected PointsPowerup powerup;
    private AudioBehaviour audioManager;

	// Use this for initialization
	void Start ()
	{
		powerup = new PointsPowerup();
        this.audioManager = GameObject.Find("2DAudio").GetComponent<AudioBehaviour>();
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
            this.audioManager.Play("money");
			Destroy(this.gameObject);
        }
    }
}
