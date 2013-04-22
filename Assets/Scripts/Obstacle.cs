using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour 
{
	
	// Use this for initialization
	void Start() 
	{
	
	}
	
	// Update is called once per frame
	void Update() 
	{
		
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.name == "Avatar")
		{
			StartCoroutine(Wait(0.5f));
			
		}
	}
	
	
    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
		StateManager.Instance.die();
    }
}
