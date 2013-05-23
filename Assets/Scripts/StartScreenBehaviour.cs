using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class StartScreenBehaviour: MonoBehaviour
{
    public Texture cupASoup;
    public Texture beer;
    public Texture page;
    public Texture money;
    public Texture star;
    public Texture lineColor;

    void Start()
    {
        StateManager.Instance.pauseOrUnpause();
        StateManager.Instance.ShowStartScreen = true;
    }

    void OnGUI()
    {
        if (StateManager.Instance.ShowStartScreen)
        {
            GUI.Window(0, new Rect((Screen.width / 2) - 550, (Screen.height / 2) - 400, 1100, 800), CreateStartScreen, "The Chase");
        }
    }

    private void CreateStartScreen(int windowID)
    {
        ShowTitle();
        ShowLine();
        ShowFirstTextArea();
        ShowObstacleImages();
        ShowSecondTextArea();
        ShowPowerupImages();
        ShowStatusFooter();
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
        string text = "The purpose of this game is to get the highest possible score. This is done by avoiding the obstacles as long as possible. "+
        "The obstacles can be avoided by leaning your bodies to the left or to the right. "+
        "Obstacles can also be avoided by jumping over them. The three obstacles used in the game are:";
        GUI.Label(new Rect(200, 125, 700, 200), text, guiStyle);
    }

    private void ShowObstacleImages()
    {
        GUI.DrawTexture(new Rect(200, 280, 100, 100), cupASoup);
        GUI.DrawTexture(new Rect(310, 280, 100, 100), page);
        GUI.DrawTexture(new Rect(420, 280, 100, 100), beer);
    }

    private void ShowSecondTextArea()
    {
        GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
        guiStyle.fontSize = 24;
        string text = "The group's score can be increased by picking up money and by increasing the multiplier. "+
            "The multiplier is based on the number of players, so more players means a higher score. "+
            "The game also has a star powerup which makes you invincible for a while. The money and stars are shown in the following way:";
        GUI.Label(new Rect(200, 390, 700, 200), text, guiStyle);
    }
    
    private void ShowPowerupImages()
    {
        GUI.DrawTexture(new Rect(200, 545, 100, 100), money);
        GUI.DrawTexture(new Rect(310, 545, 100, 100), star);
    }

    private void ShowStatusFooter()
    {
        GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
        guiStyle.fontSize = 40;
        if (StateManager.Instance.NumberOfPlayers > 0)
        {
            GUI.Label(new Rect(360, 680, 600, 100), "Jump to start the game", guiStyle);
        }
        else
        {
            GUI.Label(new Rect(175, 680, 800, 100), "The game needs at least one player to start", guiStyle);
        }
    }
}

