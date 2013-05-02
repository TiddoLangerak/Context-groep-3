//using Kinect;
using System;
using System.Collections;

/// <summary>
/// This class represents the avatar as domain object. Therefore, it
/// is a plain old C# object.
/// </summary>
public class Avatar
{
    /// <summary>
    /// Reference to the kinect thread
    /// </summary>
    //private KinectReaderThread kinectThread;

    /// <summary>
    /// Reference to IAvatarBehaviour
    /// </summary>
    private IAvatarBehaviour _avatarBehaviour;

    /// <summary>
    /// The speed (an integer in the range [1, 10])
    /// </summary>
    private int _moveSpeed = 4;

    /// <summary>
    /// Gets or sets the move speed. The move speed should be
    /// specified as an integer in the range [1, 10].
    /// </summary>
    /// <value>
    /// The move speed.
    /// </value>
    public int moveSpeed
    {
        get { return _moveSpeed; }
        set
        {
            if (_moveSpeed < 1 || _moveSpeed > 10)
            {
                throw new ArgumentOutOfRangeException("moveSpeed", "The move speed should be in range [1, 10]");
            }
            else
            {
                _moveSpeed = value;
            }
        }
    }

    /// <summary>
    /// The track (an integer in range [1, 3])
    /// </summary>
    private int _track = 2;

    /// <summary>
    /// Gets or sets the track. The track should be
    /// specified as an integer in the range [1, 3].
    /// </summary>
    /// <value>
    /// The track.
    /// </value>
    public int track
    {
        get { return _track; }
        set
        {
            if (_track < 1 || _track > 3)
            {
                throw new ArgumentOutOfRangeException("track", "The track should be in range [1, 3]");
            }
            else
            {
                _track = value;
            }
        }
    }


    /// <summary>
    /// Initialize avatar. It is dependend on an IAvatarBehaviour. Also
    /// sets up the Kinect thread and the state manager.
    /// </summary>
    public Avatar(IAvatarBehaviour avatarBehaviour)
    {
        this._avatarBehaviour = avatarBehaviour;

        /*
        this.kinectThread = new KinectReaderThread(new KinectManager());
        this.kinectThread.Start();
        */

        StateManager.Instance.pauseOrUnpause();
    }

    /// <summary>
    /// Update is called once per frame. It moves the avatar
    /// forward by a constant value. If the 'S' key is pressed,
    /// the avatar is moved backwards.
    /// </summary>
    public void Update()
    {
        if (!StateManager.Instance.isPausing())
        {
            this._avatarBehaviour.Forward(this.moveSpeed);
        }
    }

    /// <summary>
    /// Move player to the left track.
    /// </summary>
    /// <remarks>
    /// Shouldn't we just throw a custom exception, such as
    /// AvatarOutOfLaneException, if the avatar tries to move out of lane?
    /// </remarks>
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
    /// <remarks>
    /// Shouldn't we just throw a custom exception, such as
    /// AvatarOutOfLaneException, if the avatar tries to move out of lane?
    /// </remarks>
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