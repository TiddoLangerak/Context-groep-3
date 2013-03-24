using UnityEngine;
using System.Collections;

public class WorldBehaviour : MonoBehaviour {
	private float score;
	// Use this for initialization
	void Start () {
		StartCoroutine(onKey());
		score = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(!StateManager.Instance.isPausing())
			score += Time.deltaTime*10;
	}
	
	void OnGUI()
	{
		int width = 65 + 10*((int) Mathf.Round(score)).ToString().Length;
		GUI.TextArea(new Rect(10,10, width ,22), "POINTS: " + Mathf.Round(score));
		if(StateManager.Instance.isPausing())
		{
			GUI.TextArea(new Rect(Screen.width/2 - 30, Screen.height/2 - 11, 60, 22), "PAUSED");
		}
	}
	
	IEnumerator onKey(){
		while(true){
			if (Input.GetKey(KeyCode.P)) {
				StateManager.Instance.pauseOrUnpause();
				yield return new WaitForSeconds(0.2f);
			} else if (Input.GetKey(KeyCode.Escape)) {
				// Close the application
				// Using Application.Quit will result in "Not Responding"
			} else {
				yield return 0;
			}
		}
	}
}
