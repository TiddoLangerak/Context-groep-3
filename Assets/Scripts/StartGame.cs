using UnityEngine;
using Kinect;
using System.Collections;

/// <summary>
/// This class is responsible for the application setup, such as providing
/// a game menu. It  is invoked in the start-scene, before any levels have
/// been started.
/// </summary>
public class StartGame : MonoBehaviour
{
    /// <summary>
    /// Shows a GUI button named "PLAY" and loads the real level
    /// when clicked.
    /// </summary>
	void OnGUI()
	{
		if (GUI.Button(new Rect(10,10,150,100), "PLAY"))
		{
        	Application.LoadLevel("level");
		}
	}
}