using UnityEngine;
using Kinect;
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
    /// Used for initialization by Unity. The Start method is called just
    /// before any of the Update methods is called the first time.
    /// 
    /// This method starts the SideMovement coroutine, which is used to
    /// handle user inputs.
    /// </summary>
    void Start()
    {
        this.avatar = new Avatar(this);

        StartCoroutine(SideMovement());
    }

    /// <summary>
    /// The Destroy method is called when the MonoBehaviour will be destroyed.
    /// OnDestroy will only be called on game objects that have previously
    /// been active.
    /// </summary>
    void OnDestroy()
    {
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
            transform.Translate(Vector3.forward * -2);
    }

    /// <summary>
    /// Move the avatar forward by the given move speed.
    /// </summary>
    public void Forward(int moveSpeed)
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
    /// <returns>1 iff a movement is possible</returns>
    public void Left()
    {
		StartCoroutine(MoveAnimation(Vector3.left * 5));
    }

    /// <summary>
    /// A coroutine responsible for moving the avatar. Yields a
    /// WaitForSeconds to pause execution and prevent moving
    /// over multiple tracks at a time.
    /// </summary>
    IEnumerator SideMovement()
    {
        while (true) {
            if (Input.GetKey(KeyCode.A)) {
                avatar.Left();
				
                yield return new WaitForSeconds(0.2f);
            } else if (Input.GetKey(KeyCode.D)) {
                avatar.Right();
				
                yield return new WaitForSeconds(0.2f);
            } else {
                yield return 0;
            }
        }

		/*
        while (true)
        {
            switch (kinectThread.CurrentMovement)
            {
                case KinectReaderThread.Movement.LEFT:
                    avatar.Left();
                    yield return new WaitForSeconds(0.2f);
                    break;
                case KinectReaderThread.Movement.RIGHT:
                    avatar.Right();
                    yield return new WaitForSeconds(0.2f);
                    break;
                default:
                    yield return 0;
                    break;
            }
        }
		*/
    }
	IEnumerator MoveAnimation(Vector3 targetlocation)
	{
		for(int i=0; i<20; i++) 
		{
			transform.Translate(targetlocation/20);
			
			yield return new WaitForSeconds(0.008f);
		}
		
		yield return 0;
	}
}