using Kinect;
using System;
using System.Collections;

/// <summary>
/// This class represents the avatar as domain object. Therefore, it
/// is a plain old C# object.
/// </summary>
public class Avatar
{
	private KinectReaderThread kinectThread;
	
	/// <summary>
	/// Gets or sets the move speed.
	/// </summary>
	/// <value>
	/// The move speed.
	/// </value>
	public int moveSpeed { get; set; }
	
	/// <summary>
	/// Gets or sets the track.
	/// </summary>
	/// <value>
	/// The track.
	/// </value>
	public int track { get; set; }
	
	/// <summary>
	/// Gets or sets the _avatar behaviour.
	/// </summary>
	/// <value>
	/// The _avatar behaviour.
	/// </value>
	private IAvatarBehaviour _avatarBehaviour { get; set; }

	/// <summary>
    /// Used for initialization. The Start method is called just
    /// before any of the Update methods is called the first time.
	/// </summary>
	public Avatar(IAvatarBehaviour avatarBehaviour)
    {
		this.track = 2;
		this.moveSpeed = 4;
		
        try
        {
            //KinectManager kinectMgr = new KinectManager();
            //kinectThread = new KinectReaderThread(kinectMgr);
            //kinectThread.Start();
        }
        catch (System.Exception)
        {
            //Debug.Log("Kinect initiliazation failed! Maybe it's not connected.");
        }
        finally
        {
			this._avatarBehaviour = avatarBehaviour;
            StateManager.Instance.pauseOrUnpause();
        }
	}
	
	/// <summary>
    /// Update is called once per frame. It moves the avatar
    /// forward by a constant value. If the 'S' key is pressed,
    /// the avatar is moved backwards.
	/// </summary>
	public void Update ()
    {
        if (!StateManager.Instance.isPausing())
        {
			//moveSpeed += Time.smoothDeltaTime/5;
			
            this._avatarBehaviour.Forward(this.moveSpeed);
        }
    }
	
    void OnDestroy()
    {
        if(kinectThread != null)
            kinectThread.Stop();
    }
	
    /// <summary>
    /// Move player to the left track.
    /// </summary>
    public void Left()
    {
        if (StateManager.Instance.isPlaying() && track > 1)
        {
            this.track--;
            this._avatarBehaviour.Left();
        }
    }

    /// <summary>
    /// Move avatar one track to the right. As precondition we assume that
    /// the player is allowed to move right (e.g. not already on the
    /// rightmost lane)
    /// </summary>
    public void Right()
    {
        if (StateManager.Instance.isPlaying() && track < 3)
        {
            this.track++;
            this._avatarBehaviour.Right();
        }
	}

    /// <summary>
    /// This destructor is responsible for cleaning up resources, such
    /// as the kinect thread.
    /// </summary>
    ~Avatar()
    {
        //kinectThread.Stop();
    }
}