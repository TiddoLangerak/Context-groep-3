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
<<<<<<< arch-refactor
    private KinectReaderThread kinectThread;
=======
    //private KinectReaderThread kinectThread;
>>>>>>> local

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
<<<<<<< arch-refactor
        set {
=======
        set
        {
>>>>>>> local
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

<<<<<<< arch-refactor
    
=======

>>>>>>> local

    /// <summary>
    /// Initialize avatar. It is dependend on an IAvatarBehaviour. Also
    /// sets up the Kinect thread and the state manager.
    /// </summary>
    public Avatar(IAvatarBehaviour avatarBehaviour)
    {
        this._avatarBehaviour = avatarBehaviour;
<<<<<<< arch-refactor

        this.kinectThread = new KinectReaderThread(new KinectManager());
        this.kinectThread.Start();

        this.StateManager.Instance.pauseOrUnpause();
    }
=======

        /*
        this.kinectThread = new KinectReaderThread(new KinectManager());
        this.kinectThread.Start();
        */

        StateManager.Instance.pauseOrUnpause();
    }


>>>>>>> local



    /// <summary>
    /// Update is called once per frame. It moves the avatar
    /// forward by a constant value. If the 'S' key is pressed,
    /// the avatar is moved backwards.
    /// </summary>
    void Update()
    {
        if (!StateManager.Instance.isPausing())
<<<<<<< arch-refactor
            this.avatarBehaviour.Forward(this.moveSpeed);
=======
        {
            this._avatarBehaviour.Forward(this.moveSpeed);
        }
>>>>>>> local
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
<<<<<<< arch-refactor
            this.avatarBehaviour.Left();
=======
            this._avatarBehaviour.Left();
>>>>>>> local
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
<<<<<<< arch-refactor
            this.avatarBehaviour.Right();
=======
            this._avatarBehaviour.Right();
>>>>>>> local
        }
    }

    /// <summary>
    /// A coroutine responsible for moving the avatar. Yields a
    /// WaitForSeconds to pause execution and prevent moving
    /// over multiple tracks at a time.
    /// </summary>
    IEnumerator SideMovement()
    {
        //while (true) {
        //    if (Input.GetKey(KeyCode.A)) {
        //        Left();
        //        yield return new WaitForSeconds(0.2f);
        //    } else if (Input.GetKey(KeyCode.D)) {
        //        Right();
        //        yield return new WaitForSeconds(0.2f);
        //    } else {
        //        yield return 0;
        //    }
        //}

        while (true)
        {
            /*
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
            */
        }
<<<<<<< arch-refactor
	}
=======
    }
>>>>>>> local

    /// <summary>
    /// This destructor is responsible for cleaning up resources, such
    /// as the kinect thread.
    /// </summary>
    ~Avatar()
    {
<<<<<<< arch-refactor
        kinectThread.Stop();
=======
        //kinectThread.Stop();
>>>>>>> local
    }
}