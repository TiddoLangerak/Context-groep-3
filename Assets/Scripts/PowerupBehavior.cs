using UnityEngine;
using System.Collections;

public class PowerupBehavior : MonoBehaviour, IPowerupBehavior
{
	protected IPowerup powerup;
	
	public enum PowerType
    {
        MONEY,
        STAR
    };
	
	public PowerType powerType;

	// Use this for initialization
	void Start ()
	{
		switch (powerType)
		{
			case PowerType.MONEY:
			{
				powerup = new PointsPowerup();
				break;
			}
			case PowerType.STAR:
			{
				powerup = null;
				break;
			}
		}
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
}
