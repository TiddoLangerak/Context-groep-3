using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour
{
	void OnGUI()
	{
		if (GUI.Button(new Rect(10,10,150,100), "PLAY"))
		{
        	Application.LoadLevel("level");
		}
	}
}
