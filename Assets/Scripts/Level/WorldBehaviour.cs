using UnityEngine;
using System.Collections;
using System.Timers;
using System;

/// <summary>
/// Implements the behaviour of the world
/// </summary>
public class WorldBehaviour : MonoBehaviour
{
    /// <summary>
    /// The input game object
    /// </summary>
    public GameObject inputObject;

    /// <summary>
    /// The duration for the reload timer
    /// </summary>
    public float reloadDuration = 3;

    /// <summary>
    /// The multiplier of the score
    /// </summary>
    public float scoreMultiplier = 5;

    /// <summary>
    /// Indicates if the scene needs to be reloaded
    /// </summary>
    private bool sceneNeedsReloading = false;

    /// <summary>
    /// The timer used for reloading the scene
    /// </summary>
    private ITimer timer = new TimerAdapter();

    /// <summary>
    /// Initializes the input if needed and reloads the scene if needed
    /// </summary>
    void Start()
    {
        StartCoroutine(OnKey());
        if (GameObject.Find("Kinect(Clone)") == null)
        {
            Instantiate(inputObject);
        }
        this.timer.Interval = (int)(reloadDuration * 1000);
        this.timer.Elapsed += new ElapsedEventHandler(ReloadSceneTimerElapsed);
    }

    /// <summary>
    /// Update is called once per frame and is used to
    /// increment the score as time elapses.
    /// </summary>
    void Update()
    {
        if (!StateManager.Instance.IsPausing())
            StateManager.Instance.Score += (Time.deltaTime * scoreMultiplier * StateManager.Instance.NumberOfPlayers);
        if (sceneNeedsReloading)
            ResetGame();
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
        ShowMultiplier(guiStyle);
        ChangeScoreColorIfNeeded();
        GUI.TextArea(new Rect(10, 10, 350, 60), "Score: " + Mathf.Round(StateManager.Instance.Score), guiStyle);
        GUI.contentColor = Color.red;
        ShowGameState(guiStyle);
    }

    /// <summary>
    /// Changes the color of the score label if the money powerup is active
    /// </summary>
    private static void ChangeScoreColorIfNeeded()
    {
        if (StateManager.Instance.NewMoneyPowerup)
        {
            GUI.contentColor = Color.green;
        }
    }

    /// <summary>
    /// Show the multiplier in the top left corner of the screen
    /// </summary>
    /// <param name="guiStyle">The style used for the label</param>
    private static void ShowMultiplier(GUIStyle guiStyle)
    {
        GUI.contentColor = Color.white;
        GUI.backgroundColor = Color.clear;
        GUI.TextArea(new Rect(10, 70, 350, 60), "Multiplier: " + StateManager.Instance.NumberOfPlayers, guiStyle);
    }

    /// <summary>
    /// Shows 'Paused', 'Game Over' or nothing in the top left corner, depending on the gamestate
    /// </summary>
    /// <param name="guiStyle">The style used for the labels</param>
    private static void ShowGameState(GUIStyle guiStyle)
    {
        if (StateManager.Instance.IsPausing() && !StateManager.Instance.IsDead())
        {
            GUI.TextArea(new Rect(Screen.width / 2 - 110, Screen.height / 2 - 41, 350, 60), "Paused", guiStyle);
        }

        if (StateManager.Instance.IsDead())
        {
            GUI.TextArea(new Rect(Screen.width / 2 - 170, Screen.height / 2 - 41, 350, 60), "Game Over", guiStyle);
        }
    }

    /// <summary>
    /// Provides ability to pause the game (on 'P')
    /// </summary>
    IEnumerator OnKey()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.P) && !StateManager.Instance.IsDead())
            {
                StateManager.Instance.PauseOrUnpause();
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                yield return 0;
            }
        }
    }

    /// <summary>
    /// Start the timer for reloading the scene
    /// </summary>
    public void ReloadScene()
    {
        timer.Start();
    }

    /// <summary>
    /// Reload the scene
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The arguments of the event</param>
    private void ReloadSceneTimerElapsed(object sender, EventArgs e)
    {
        timer.Stop();
        sceneNeedsReloading = true;
    }

    /// <summary>
    /// Reset the game, i.e. reload sceen, show start screen and reset score
    /// </summary>
    public void ResetGame()
    {
        Application.LoadLevel("level");
        StateManager.Instance.ShowStartScreen = true;
    }
}
