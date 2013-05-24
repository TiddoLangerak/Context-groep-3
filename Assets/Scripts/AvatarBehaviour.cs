﻿//#define INPUT_KINECT
#define INPUT_KEYBOARD

using UnityEngine;

#if INPUT_KINECT
using Kinect;
#endif

using System.Collections;

/// <summary>
/// This class represents the avatar in the game environment.
/// </summary>
public class AvatarBehaviour : MonoBehaviour, IAvatarBehaviour
{
    /// <summary>
    /// The domain-specific avatar instance.
    /// </summary>
    private Avatar avatar;

    private bool showStartScreen = true;
    bool jumping = false;
    AudioBehaviour audioManager;
	
    /// <summary>
    /// Used for initialization by Unity. The Start method is called just
    /// before any of the Update methods is called the first time.
    /// 
    /// This method starts the SideMovement coroutine, which is used to
    /// handle user inputs.
    /// </summary>
    void Start()
    {
        this.audioManager = transform.Find("2DAudio").GetComponent<AudioBehaviour>();
        StateManager.Instance.pauseOrUnpause();
        try
        {
            //Try to initialize the input
#if INPUT_KINECT
            this.avatar = new Avatar(this, new KinectUserInput());
#elif INPUT_KEYBOARD
            this.avatar = new Avatar(this, new KeyboardUserInput());
#else
                throw System.Exception("No input specified");
#endif
            StartCoroutine(SideMovement());
        }
        catch (System.Exception)
        {
            Logger.Log("Input initialization failed! Please check if your controller is connected properly.");
            Application.Quit();
            //In the unity editor the application doesn't quit using Application.quit, so we just break using the debugger
            //to prevent further execution of code
#if UNITY_EDITOR
            Debug.Break();
#endif
        }

    }

    void OnGUI()
    {
        if (showStartScreen)
        {
            GUI.Window(0, new Rect((Screen.width / 2) - 550, (Screen.height / 2) - 400, 1100, 800), StartWindowHandler, "The Chase");
        }
    }

    private void StartWindowHandler(int windowID)
    {
        GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
        guiStyle.fontSize = 30;
        if (StateManager.Instance.NumberOfPlayers > 0)
        {
            GUI.Label(new Rect(400, 650, 600, 100), "Jump to start the game", guiStyle);
        }
        else
        {
            GUI.Label(new Rect(270, 650, 600, 100), "The game needs at least one player to start", guiStyle);
        }
        if (Input.anyKey)/*if (jumping) DOES NOT WORK!*/
        {
            showStartScreen = false;
            StateManager.Instance.pauseOrUnpause();
        }
    }

    /// <summary>
    /// The Destroy method is called when the MonoBehaviour will be destroyed.
    /// OnDestroy will only be called on game objects that have previously
    /// been active.
    /// </summary>
    void OnDestroy()
    {
        //GUI.TextArea(new Rect(10, 40, 200, 220), avatar.ToString());
    }

    /// <summary>
    /// Update is called once per frame. It moves the avatar
    /// forward by a constant value. If the 'S' key is pressed,
    /// the avatar is moved backwards.
    /// </summary>
    public void Update()
    {
	this.avatar.Update();
	this.avatar.moveSpeed += Time.smoothDeltaTime/20;

        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.forward * -2);

        if (StateManager.Instance.isDead())
        {
            this.audioManager.CrashEnding("soundtrack", 2500);
        }
        else if (StateManager.Instance.invincible)
        {
            if (!this.audioManager.IsPlaying("powerup"))
            {
                this.audioManager.StopPlaying("soundtrack");
                this.audioManager.Play("powerup");
            }
        }
        else if (!StateManager.Instance.invincible && !this.audioManager.IsPlaying("soundtrack"))
        {
            this.audioManager.StopPlaying("powerup");
            this.audioManager.Play("soundtrack");
        }
        
    }

    /// <summary>
    /// Move the avatar forward by the given move speed.
    /// </summary>
    public void Forward(float moveSpeed)
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.smoothDeltaTime);
    }

    /// <summary>
    /// Move player to the right track.
    /// </summary>
    public void Right()
    {
        StartCoroutine(MoveAnimation(Vector3.right * 5, 100));
    }

    /// <summary>
    /// Move player to the left track.
    /// </summary>
    /// <returns>1 iff a movement is possible</returns>
    public void Left()
    {
        StartCoroutine(MoveAnimation(Vector3.left * 5, 100));
    }

    public void Up()
    {
        StartCoroutine(UpAndDownAnimation());
    }

    /// <summary>
    /// A coroutine responsible for moving the avatar. Yields a
    /// WaitForSeconds to pause execution and prevent moving
    /// over multiple tracks at a time.
    /// </summary>
    IEnumerator SideMovement()
    {
		while(true) 
		{
			if(this.avatar.MovementHandler())
			{
				yield return new WaitForSeconds(0.5f);
			}
			yield return new WaitForSeconds(Time.deltaTime);
		}
    }

    IEnumerator UpAndDownAnimation()
    {
        if (!jumping)
        {
            jumping = true;
            yield return StartCoroutine(MoveAnimation(Vector3.up * 8, 400));
            yield return StartCoroutine(MoveAnimation(Vector3.down * 8, 400));
            jumping = false;
        }
    }

    IEnumerator MoveAnimation(Vector3 targetlocation, int quick)
    {
        int x = (int)Mathf.Round(quick / avatar.moveSpeed) + 1;
        Debug.Log("wait time: " + (x));
        for (int i = 0; i < x; i++)
        {
            transform.Translate(targetlocation / x);
            yield return new WaitForSeconds(0.025f);
        }
        yield return 0;
    }
}