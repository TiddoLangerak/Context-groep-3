using UnityEngine;
using System.Collections;

public class avatarMovement : MonoBehaviour {
	public float moveSpeed = 1;
	public int track = 2;
	public stateManager stateM;
	// Use this for initialization
	void Start () {
		StartCoroutine(sideMovement());
		stateM = new stateManager();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * 1);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.forward * -1);
		if (Input.GetKey(KeyCode.Escape))
			stateM.restart();
		if (Input.GetKey(KeyCode.Space))
			stateM.pause();
	}
	
	IEnumerator sideMovement(){
		while(true){
			if (Input.GetKey(KeyCode.A) && track > 1){
				track--;
				transform.Translate(Vector3.left * 5);
				yield return new WaitForSeconds(0.5f);
			} else if (Input.GetKey(KeyCode.D) && track < 3) {
				track++;
				transform.Translate(Vector3.left * -5);
				yield return new WaitForSeconds(0.5f);
			} else {
				yield return 0;
			}
		}
	}
}
