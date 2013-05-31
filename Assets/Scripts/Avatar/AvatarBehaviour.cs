using UnityEngine;
using Kinect;
using System.Collections;

/// <summary>
/// This class represents behaviour of the avatar in the game environment.
/// </summary>
public class AvatarBehaviour : MonoBehaviour, IAvatarBehaviour
{
    /// <summary>
    /// The domain-specific avatar instance.
    /// </summary>
    private Avatar avatar;

    /// <summary>
    /// Indicates if the avatar is jumping
    /// </summary>
    private bool jumping = false;

    /// <summary>
    /// The sound generator
    /// </summary>
    AudioBehaviour audioManager;

    /// <summary>
    /// Indicates if the kinect initialization failed 
    /// </summary>
    private bool kinectFailed = false;

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
        StateManager.Instance.PauseOrUnpause();
        try
        {
            this.avatar = new Avatar(this, GameObject.Find("Kinect(Clone)").GetComponent<KinectUserInput>());
        }
        catch (OpenNI.GeneralException)
        {
            HandleKinectInitializationFailure();
        }
        catch (System.DllNotFoundException)
        {
            Logger.Log("OpenNI DLL not found. Check if OpenNI is installed properly");
            HandleKinectInitializationFailure();
        }
        finally
        {
            StartCoroutine(SideMovement());
        }
    }

    /// <summary>
    /// Logs that the kinect initialization failed and sets the user input to the keyboard
    /// </summary>
    private void HandleKinectInitializationFailure()
    {
        Logger.Log("Input initialization failed! Please check if your controller is connected properly.");
        Logger.Log("Fallback to keyboard input");
        this.avatar = new Avatar(this, new KeyboardUserInput());
        this.kinectFailed = true;
    }

    /// <summary>
    /// Update is called once per frame. It moves the avatar
    /// forward by a constant value. If the 'S' key is pressed,
    /// the avatar is moved backwards.
    /// </summary>
    public void Update()
    {
        this.avatar.Update();
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * -2);
        }
        IncreaseMoveSpeed();
        UpdateAudio();
    }

    /// <summary>
    /// Increases the movement speed when the game isn't paused
    /// </summary>
    private void IncreaseMoveSpeed()
    {
        if (!StateManager.Instance.IsPausing())
        {
            this.avatar.MoveSpeed += Time.smoothDeltaTime / 10;
        }
    }

    /// <summary>
    /// Plays the correct audio
    /// </summary>
    private void UpdateAudio()
    {
        if (StateManager.Instance.IsDead())
        {
            this.audioManager.CrashEnding("soundtrack", 2500);
        }
        else if (StateManager.Instance.Invincible)
        {
            PlayPowerupAudio();
        }
        else if (!StateManager.Instance.Invincible && !this.audioManager.IsPlaying("soundtrack"))
        {
            this.audioManager.StopPlaying("powerup");
            this.audioManager.Play("soundtrack");
        }
    }

    /// <summary>
    /// Starts playing the powerup audio if it isn't playing already
    /// </summary>
    private void PlayPowerupAudio()
    {
        if (!this.audioManager.IsPlaying("powerup"))
        {
            this.audioManager.StopPlaying("soundtrack");
            this.audioManager.Play("powerup");
        }
    }

    /// <summary>
    /// Show a message on the screen if the kinect initialization failed
    /// </summary>
    public void OnGUI()
    {
        if (this.kinectFailed)
        {
            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.fontSize = 50;
            GUI.contentColor = Color.red;
            GUI.backgroundColor = Color.clear;
            GUI.TextArea(new Rect(Screen.width / 2 - 350, Screen.height / 2 - 120, 700, 60), "KINECT IS NOT CONNECTED", guiStyle);
        }
    }

    /// <summary>
    /// Move the avatar forward by the given move speed
    /// </summary>
    public void Forward(float moveSpeed)
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.smoothDeltaTime);
    }

    /// <summary>
    /// Move avatar to the right track
    /// </summary>
    public void Right()
    {
        StartCoroutine(MoveAnimation(Vector3.right * 6, 100));
    }

    /// <summary>
    /// Move avatar to the left track
    /// </summary>
    public void Left()
    {
        StartCoroutine(MoveAnimation(Vector3.left * 6, 100));
    }

    /// <summary>
    /// Move avatar up
    /// </summary>
    public void Up()
    {
        StartCoroutine(UpAndDownAnimation());
    }

    /// <summary>
    /// A coroutine responsible for moving the avatar sideways. Yields a
    /// WaitForSeconds to pause execution and prevent moving
    /// over multiple tracks at a time.
    /// </summary>
    IEnumerator SideMovement()
    {
        while (true)
        {
            if (this.avatar.MovementHandler())
            {
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    /// <summary>
    /// A coroutine responsible for moving the avatar up.
    /// </summary>
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

    /// <summary>
    /// Moves the avatar with an animation
    /// </summary>
    /// <param name="targetlocation">The end location of the animation</param>
    /// <param name="quick">The speed of the animation</param>
    IEnumerator MoveAnimation(Vector3 targetlocation, int quick)
    {
        int x = (int)Mathf.Round(quick / avatar.MoveSpeed) + 1;
        for (int i = 0; i < x; i++)
        {
            transform.Translate(targetlocation / x);
            yield return new WaitForSeconds(0.025f);
        }
        yield return 0;
    }
}