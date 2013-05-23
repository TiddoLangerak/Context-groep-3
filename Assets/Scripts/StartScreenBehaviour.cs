using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class StartScreenBehaviour: MonoBehaviour
{
    public bool ShowStartScreen { get; set; } 
    public Texture cupASoup;
    public Texture beer;
    public Texture page;
    public Texture money;
    public Texture star;
    public Texture lineColor;

    void Start()
    {
        StateManager.Instance.pauseOrUnpause();
        ShowStartScreen = true;
    }

    void OnGUI()
    {
        if (ShowStartScreen)
        {
            GUI.Window(0, new Rect((Screen.width / 2) - 550, (Screen.height / 2) - 400, 1100, 800), CreateStartScreen, "The Chase");
        }
    }

    private void CreateStartScreen(int windowID)
    {
        CheckIfStartScreenShouldBeShown();
        ShowTitle();
        ShowLine();
        ShowFirstTextArea();
        ShowObstacleImages();
        ShowSecondTextArea();
        ShowPowerupImages();
        ShowStatusFooter();
    }

    private void CheckIfStartScreenShouldBeShown()
    {
        if (Input.GetKey(KeyCode.Q))/*if (jumping) DOES NOT WORK!*/
        {
            ShowStartScreen = false;
            StateManager.Instance.pauseOrUnpause();
        }
    }

    private void ShowTitle()
    {
        GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
        guiStyle.fontSize = 45;
        GUI.Label(new Rect(300, 20, 600, 100), "Welcome to \'The Chase\'!", guiStyle);
        GUI.Label(new Rect(0,50,Screen.width,5),"",guiStyle);
    }

    private void ShowLine()
    {
        GUI.DrawTexture(new Rect(100, 90, 900, 2), lineColor);
    }

    private void ShowFirstTextArea()
    {
        GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
        guiStyle.fontSize = 24;
        string text = "The purpose of this game is to avoid the obstacles as long as possible. The obstacles can be avoided by leaning your bodies to the "+
        "left or to the right. Obstacles can also be avoided by jumping over them. The three obstacles used in the game are shown below.";
        GUI.Label(new Rect(200, 125, 700, 200), text, guiStyle);
    }

    private void ShowObstacleImages()
    {
        GUI.DrawTexture(new Rect(200, 300, 100, 100), cupASoup);
        GUI.DrawTexture(new Rect(310, 300, 100, 100), page);
        GUI.DrawTexture(new Rect(420, 300, 100, 100), beer);
    }

    private void ShowSecondTextArea()
    {
        GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
        guiStyle.fontSize = 24;
        string text = "The purpose of this game is to avoid the obstacles as long as possible. The obstacles can be avoided by leaning your bodies to the " +
        "left or to the right. Obstacles can also be avoided by jumping over them. The three obstacles used in the game are shown below.";
        GUI.Label(new Rect(200, 125, 700, 200), text, guiStyle);
    }
    
    private void ShowPowerupImages()
    {
        GUI.DrawTexture(new Rect(300, 600, 100, 100), money);
        GUI.DrawTexture(new Rect(410, 600, 100, 100), star);
    }

    private void ShowStatusFooter()
    {
        GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
        guiStyle.fontSize = 30;
        if (StateManager.Instance.NumberOfPlayers > 0)
        {
            GUI.Label(new Rect(400, 750, 600, 100), "Jump to start the game", guiStyle);
        }
        else
        {
            GUI.Label(new Rect(270, 750, 600, 100), "The game needs at least one player to start", guiStyle);
        }
    }
}

