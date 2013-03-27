using UnityEngine;
using System.Collections;

public class WorldBehaviour : MonoBehaviour
{
    /// <summary>
    /// Variable keeping track of current score
    /// </summary>
	private float score;
	
    /// <summary>
    /// Used for initialization
    /// </summary>
	void Start()
    {
		StartCoroutine(onKey());
		score = 0f;
	}
	
	/// <summary>
    /// Update is called once per frame and is used to
    /// increment the score as time elapses.
	/// </summary>
	void Update()
    {
		if (!StateManager.Instance.isPausing())
			score += Time.deltaTime*10;
	}
	
    /// <summary>
    /// OnGUI is called for rendering and handling GUI
    /// events. It draws the score and draws a textarea
    /// if the game is paused.
    /// </summary>
	void OnGUI()
	{
		int width = 65 + 10*((int) Mathf.Round(score)).ToString().Length;
		GUI.TextArea(new Rect(10,10, width ,22), "POINTS: " + Mathf.Round(score));
		
        if (StateManager.Instance.isPausing())
			GUI.TextArea(new Rect(Screen.width/2 - 30, Screen.height/2 - 11, 60, 22), "PAUSED");
	}
	
    /// <summary>
    /// Provides ability to pause the game (on 'P')
    /// and to close the game (on 'ESC').
    /// </summary>
	IEnumerator onKey()
    {
		while (true) {
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
