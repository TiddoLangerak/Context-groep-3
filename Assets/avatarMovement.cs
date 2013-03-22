using UnityEngine;
using System.Collections;

public class avatarMovement : MonoBehaviour {
	public float moveSpeed = 1;
	public int track = 2;
	public StateManager stateM;
	// Use this for initialization
	void Start () {
		StartCoroutine(sideMovement());
		stateM = new StateManager();
		stateM.pauseOrUnpause();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward * 4 * Time.smoothDeltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.forward * -2);
		if (Input.GetKey(KeyCode.Space))
			stateM.pauseOrUnpause();
	}
	
	IEnumerator sideMovement(){
		while(true){
			if (Input.GetKey(KeyCode.A) && track > 1){
				track--;
				transform.Translate(Vector3.left * 5 * stateM.right());
				yield return new WaitForSeconds(0.2f);
			} else if (Input.GetKey(KeyCode.D) && track < 3) {
				track++;
				transform.Translate(Vector3.left * 5 * stateM.left());
				yield return new WaitForSeconds(0.2f);
			} else {
				yield return 0;
			}
		}
	}
}
