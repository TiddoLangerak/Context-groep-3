using UnityEngine;
using System.Collections;

public class WorldBehaviour : MonoBehaviour
{
    /// <summary>
    /// Used for initialization
    /// </summary>
    void Start()
    {
        StartCoroutine(onKey());
    }

    /// <summary>
    /// Update is called once per frame and is used to
    /// increment the score as time elapses.
    /// </summary>
    void Update()
    {
        if (!StateManager.Instance.isPausing())
            StateManager.Instance.score += (Time.deltaTime * 10 * StateManager.Instance.NumberOfPlayers);
    }

    /// <summary>
    /// OnGUI is called for rendering and handling GUI
    /// events. It draws the score and draws a textarea
    /// if the game is paused.
    /// </summary>
    void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
        guiStyle.fontSize = 50;
        GUI.contentColor = Color.white;
        GUI.backgroundColor = Color.clear;
        GUI.TextArea(new Rect(10, 10, 350, 60), "Score: " + Mathf.Round(StateManager.Instance.score),guiStyle);

        if (StateManager.Instance.isPausing() && !StateManager.Instance.isDead())
            GUI.TextArea(new Rect(Screen.width / 2 - 110, Screen.height / 2 - 41, 350, 60), "Paused",guiStyle);
        if (StateManager.Instance.isDead())
            GUI.TextArea(new Rect(Screen.width / 2 - 170, Screen.height / 2 - 41, 350, 60), "Game Over",guiStyle);
        
        GUI.TextArea(new Rect(10, 70, 350, 60), "Multiplier: " + StateManager.Instance.NumberOfPlayers, guiStyle);
            
    }

    /// <summary>
    /// Provides ability to pause the game (on 'P')
    /// and to close the game (on 'ESC').
    /// </summary>
    IEnumerator onKey()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.P))
            {
                StateManager.Instance.pauseOrUnpause();
                yield return new WaitForSeconds(0.2f);
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                // Close the application
                // Using Application.Quit will result in "Not Responding"
                yield return 0;
            }
            else
            {
                yield return 0;
            }
        }
    }
}
