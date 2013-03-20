using UnityEngine;
using System.Collections;

public class avatarMovement : MonoBehaviour {
	private int linewidth = 5;
	
	public float moveSpeed = 1;
	public StateManager stateM;
	// Use this for initialization
	void Start () {
		StartCoroutine(sideMovement());
		stateM = new StateManager();
		stateM.start();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * 1);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.forward * -1);
		if (Input.GetKey(KeyCode.Space))
			stateM.pauseOrUnpause();
	}
	
	IEnumerator sideMovement(){
		while(true){
			if (Input.GetKey(KeyCode.A)){
				transform.Translate(Vector3.left * linewidth * stateM.left());
				yield return new WaitForSeconds(0.5f);
			} else if (Input.GetKey(KeyCode.D)) {
				transform.Translate(Vector3.left * linewidth * stateM.right());
				yield return new WaitForSeconds(0.5f);
			} else {
				yield return 0;
			}
		}
	}
}
