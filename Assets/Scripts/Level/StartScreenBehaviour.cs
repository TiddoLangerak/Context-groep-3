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
    /// The texture of the movement image
    /// </summary>
    public Texture movement;

    /// <summary>
    /// The texture for the multiplier image
    /// </summary>
    public Texture users;

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
        ShowMovementExplanation();
        ShowMultiplierExplanation();
        ShowStatusFooter();
    }

    /// <summary>
    /// Show the multiplier explanation text and image
    /// </summary>
    private void ShowMultiplierExplanation()
    {
        GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
        guiStyle.fontSize = 24;
        string text = "More players is more fun (=points):";
        GUI.Label(new Rect(600, 330, 700, 200), text, guiStyle);
        GUI.DrawTexture(new Rect(600, 365, 200, 200), users);
    }

    /// <summary>
    /// Show the movement explanation image incl. text
    /// </summary>
    private void ShowMovementExplanation()
    {
        GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
        guiStyle.fontSize = 24;
        string text = "Move like this:";
        GUI.Label(new Rect(200, 330, 700, 200), text, guiStyle);
        GUI.DrawTexture(new Rect(200, 365, 100, 200), movement);
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
        string text = "Avoid these:";
        GUI.Label(new Rect(200, 125, 700, 200), text, guiStyle);
    }

    /// <summary>
    /// Show the images of the obstacles
    /// </summary>
    private void ShowObstacleImages()
    {
        GUI.DrawTexture(new Rect(200, 160, 100, 100), cupASoup);
        GUI.DrawTexture(new Rect(310, 160, 100, 100), page);
        GUI.DrawTexture(new Rect(420, 160, 100, 100), beer);
    }

    /// <summary>
    /// Show the second text area
    /// </summary>
    private void ShowSecondTextArea()
    {
        GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
        guiStyle.fontSize = 24;
        string text = "Grab these:";
        GUI.Label(new Rect(600, 125, 700, 200), text, guiStyle);
    }

    /// <summary>
    /// Show the images of the powerup
    /// </summary>
    private void ShowPowerupImages()
    {
        GUI.DrawTexture(new Rect(600, 160, 100, 100), money);
        GUI.DrawTexture(new Rect(710, 160, 100, 100), star);
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
            GUI.Label(new Rect(350, 650, 600, 100), "Jump to start the game", guiStyle);
        }
        else
        {
            GUI.Label(new Rect(175, 650, 800, 100), "The game needs at least one player to start", guiStyle);
        }
    }
}

