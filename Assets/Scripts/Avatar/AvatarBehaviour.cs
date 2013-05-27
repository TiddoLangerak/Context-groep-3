// #define INPUT_KINECT
#define INPUT_KINECT

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

    bool jumping = false;
    AudioBehaviour audioManager;

    bool kinectFailed = false;

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
            this.avatar = new Avatar(this, GameObject.Find("Kinect(Clone)").GetComponent<KinectUserInput>());
#elif INPUT_KEYBOARD
            this.avatar = new Avatar(this, new KeyboardUserInput());
#else
                throw System.Exception("No input specified");
#endif
        }
        catch (System.Exception e)
        {
            Logger.Log("Input initialization failed! Please check if your controller is connected properly.");
            Logger.Log("Type: " + e.GetType() + "; Message: " + e.Message.ToString() + "\nStacktrace: " + e.StackTrace.ToString());

            Logger.Log("Fallback to keyboard input");
            this.avatar = new Avatar(this, new KeyboardUserInput());
            this.kinectFailed = true;
        }
        finally
        {
            StartCoroutine(SideMovement());
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
        if (Input.GetKey(KeyCode.S))
		{
            transform.Translate(Vector3.forward * -2);
		}
		if(!StateManager.Instance.isPausing())
		{
			this.avatar.moveSpeed += Time.smoothDeltaTime / 10;	
		}
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

    public void OnGUI() {
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
        StartCoroutine(MoveAnimation(Vector3.right * 6, 100));
    }

    /// <summary>
    /// Move player to the left track.
    /// </summary>
    /// <returns>1 iff a movement is possible</returns>
    public void Left()
    {
        StartCoroutine(MoveAnimation(Vector3.left * 6, 100));
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
        while (true)
        {
            if (this.avatar.MovementHandler())
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
        for (int i = 0; i < x; i++)
        {
            transform.Translate(targetlocation / x);
            yield return new WaitForSeconds(0.025f);
        }
        yield return 0;
    }

}