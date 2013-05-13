#define INPUT_KINECT
//#define INPUT_KEYBOARD

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

    /// <summary>
    /// Flag indicating if the avatar is currenly moving
    /// </summary>
    public bool IsMoving { get; private set; }

    /// <summary>
    /// Used for initialization by Unity. The Start method is called just
    /// before any of the Update methods is called the first time.
    /// 
    /// This method starts the SideMovement coroutine, which is used to
    /// handle user inputs.
    /// </summary>
    void Start()
    {
        this.IsMoving = false;
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
		this.avatar.moveSpeed += Time.smoothDeltaTime/5;
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.forward * -2);
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
        StartCoroutine(MoveAnimation(Vector3.right * 5));
    }

    /// <summary>
    /// Move player to the left track.
    /// </summary>
    public void Left()
    {
        StartCoroutine(MoveAnimation(Vector3.left * 5));
    }

    
    IEnumerator MoveAnimation(Vector3 targetlocation)
    {
        this.IsMoving = true;
        for (int i = 0; i < 20; i++)
        {
            transform.Translate(targetlocation / 20);
            yield return new WaitForSeconds(0.012f);
        }
        yield return new WaitForSeconds(0.2f);
        this.IsMoving = false;
        yield return 0;
    }
}