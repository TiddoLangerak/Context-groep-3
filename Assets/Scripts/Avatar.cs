using UnityEngine;
using Kinect;
using System.Collections;

/// <summary>
/// This class is responsible for keeping track of the
/// avatar movement.
/// </summary>
public class Avatar : MonoBehaviour
{
	private KinectReaderThread kinectThread;
	/// <summary>
	/// The move speed.
	/// </summary>
	private float _moveSpeed = 10;
	
	/// <summary>
	/// Gets or sets the move speed.
	/// </summary>
	/// <value>
	/// The move speed.
	/// </value>
	public float moveSpeed
	{
		get { return _moveSpeed; }
		set { _moveSpeed = value; }
	}
	
	/// <summary>
	/// The track.
	/// </summary>
	private int _track = 2;
	
	/// <summary>
	/// Gets or sets the track.
	/// </summary>
	/// <value>
	/// The track.
	/// </value>
	public int track
	{
		get { return _track; }
		set { _track = value; }
	}

	/// <summary>
    /// Used for initialization. The Start method is called just
    /// before any of the Update methods is called the first time.
	/// </summary>
	public void Start()
    {
        try
        {
            KinectManager kinectMgr = new KinectManager();
            kinectThread = new KinectReaderThread(kinectMgr);
            kinectThread.Start();
        }
        catch (System.Exception)
        {
            Debug.Log("Kinect initiliazation failed! Maybe it's not connected.");
        }
        finally
        {
            StartCoroutine(SideMovement());
            StateManager.Instance.pauseOrUnpause();
        }
	}
	
	/// <summary>
    /// Update is called once per frame. It moves the avatar
    /// forward by a constant value. If the 'S' key is pressed,
    /// the avatar is moved backwards.
	/// </summary>
	void Update ()
    {
		// _moveSpeed += Time.smoothDeltaTime/5;
        if (!StateManager.Instance.isPausing()) {	
		    transform.Translate(Vector3.forward * this.moveSpeed * Time.smoothDeltaTime);
			moveSpeed+=Time.smoothDeltaTime/2;
		}
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.forward * -2);
    }
	
	
    void OnDestroy()
    {
        if(kinectThread != null)
            kinectThread.Stop();
    }
	
    /// <summary>
    /// Move player to the right track.
    /// </summary>
	public void Right()
	{
		if (StateManager.Instance.isPlaying() && _track > 1) {
			track--;
			StartCoroutine(MoveAnimation(Vector3.left * -6));
		}
	}

    /// <summary>
    /// Move player to the left track.
    /// </summary>
    /// <returns>1 iff a movement is possible</returns>
	public void Left()
	{
		if (StateManager.Instance.isPlaying() && _track < 3) {
			track++;
			StartCoroutine(MoveAnimation(Vector3.left * 6));
		}
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
            if (Input.GetKey(KeyCode.A))
            {
                Left();
                yield return new WaitForSeconds(0.2f);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Right();
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                if (kinectThread != null)
                {
                    switch (kinectThread.CurrentMovement)
                    {
                        case KinectReaderThread.Movement.LEFT:
                            Left();
                            yield return new WaitForSeconds(0.2f);
                            break;
                        case KinectReaderThread.Movement.RIGHT:
                            Right();
                            yield return new WaitForSeconds(0.2f);
                            break;
                        default:
                            yield return 0;
                            break;
                    }
                }
                else
                    yield return 0;
            }
        }
    }
	
	
	IEnumerator MoveAnimation(Vector3 targetlocation)
	{
		for(int i=0; i<20; i++) 
		{
			transform.Translate(targetlocation/20);
			yield return new WaitForSeconds(0.005f);
		}
		yield return 0;
	}
}