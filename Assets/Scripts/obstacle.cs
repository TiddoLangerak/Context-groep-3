using UnityEngine;
using System.Collections;

public class obstacle : MonoBehaviour {
	public float moveSpeed = 20;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float moveForward = moveSpeed * Time.smoothDeltaTime;
		transform.Translate(Vector3.forward * -moveForward);
	}
}
