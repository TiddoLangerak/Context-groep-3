using UnityEngine;

/// <summary>
/// Implements the behaviour of the start screen
/// </summary>
public class StartScreenBehaviour : MonoBehaviour
{
    /// <summary>
    /// The cup a soup texture
    /// </summary>
    public Texture cupASoup;

    /// <summary>
    /// The beer texture
    /// </summary>
    public Texture beer;

    /// <summary>
    /// The page texture
    /// </summary>
    public Texture page;

    /// <summary>
    /// The money texture
    /// </summary>
    public Texture money;

    /// <summary>
    /// The star texture
    /// </summary>
    public Texture star;

    /// <summary>
    /// The texture of the line
    /// </summary>
    public Texture line;

    /// <summary>
    /// Show the startscreen on startup, called automatically by Unity
    /// </summary>
    void Start()
    {
        StateManager.Instance.ShowStartScreen = true;
    }

    /// <summary>
    /// Shows the startcreen if it should be shown. (automatically called by Unity)
    /// </summary>
    void OnGUI()
    {
        if (StateManager.Instance.ShowStartScreen)
        {
            GUI.Window(0, new Rect((Screen.width / 2) - 550, (Screen.height / 2) - 400, 1100, 800), CreateStartScreen, "");
        }
    }

    /// <summary>
    /// Creates/shows the start screen
    /// </summary>
    /// <param name="windowID">The id of the start screen window</param>
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

    /// <summary>
    /// Show the title
    /// </summary>
    private void ShowTitle()
    {
        GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
        guiStyle.fontSize = 45;
        GUI.Label(new Rect(300, 20, 600, 100), "Welcome to \'The Chase\'!", guiStyle);
        GUI.Label(new Rect(0, 50, Screen.width, 5), "", guiStyle);
    }

    /// <summary>
    /// Show the line of the title
    /// </summary>
    private void ShowLine()
    {
        GUI.DrawTexture(new Rect(100, 90, 900, 2), line);
    }

    /// <summary>
    /// Show the first text area
    /// </summary>
    private void ShowFirstTextArea()
    {
        GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
        guiStyle.fontSize = 24;
        string text = "The purpose of this game is to get the highest possible score. This is done by avoiding the obstacles as long as possible. " +
        "The obstacles can be avoided by leaning your bodies to the left or to the right. " +
        "Obstacles can also be avoided by jumping over them. The three obstacles used in the game are:";
        GUI.Label(new Rect(200, 125, 700, 200), text, guiStyle);
    }

    /// <summary>
    /// Show the images of the obstacles
    /// </summary>
    private void ShowObstacleImages()
    {
        GUI.DrawTexture(new Rect(200, 280, 100, 100), cupASoup);
        GUI.DrawTexture(new Rect(310, 280, 100, 100), page);
        GUI.DrawTexture(new Rect(420, 280, 100, 100), beer);
    }

    /// <summary>
    /// Show the second text area
    /// </summary>
    private void ShowSecondTextArea()
    {
        GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
        guiStyle.fontSize = 24;
        string text = "The group's score can be increased by picking up money and by increasing the multiplier. " +
            "The multiplier is based on the number of players, so more players means a higher score. " +
            "The game also has a star powerup which makes you invincible for a while. The money and stars are shown in the following way:";
        GUI.Label(new Rect(200, 390, 700, 200), text, guiStyle);
    }

    /// <summary>
    /// Show the images of the powerup
    /// </summary>
    private void ShowPowerupImages()
    {
        GUI.DrawTexture(new Rect(200, 545, 100, 100), money);
        GUI.DrawTexture(new Rect(310, 545, 100, 100), star);
    }

    /// <summary>
    /// Show the footer based on the nr. of players
    /// </summary>
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

