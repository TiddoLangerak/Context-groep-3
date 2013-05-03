using Kinect;
using System;
using System.Collections;

/// <summary>
/// This class represents the avatar as domain object. Therefore, it
/// is a plain old C# object.
/// </summary>
public class Avatar
{
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
    /// Gets or sets the user input
    /// </summary>
    /// <value>
    /// The user input
    /// </value>
    private IUserInput _userInput { get; set; }

	/// <summary>
    /// Used for initialization. The Start method is called just
    /// before any of the Update methods is called the first time.
	/// </summary>
	public Avatar(IAvatarBehaviour avatarBehaviour, IUserInput userInput)
    {
		this.track = 2;
		this.moveSpeed = 4;
		
        try
        {
            userInput.Initialize();
	        this._avatarBehaviour = avatarBehaviour;
            this._userInput = userInput;

            StateManager.Instance.pauseOrUnpause();
        }
        catch (System.Exception)
        {
            Debug.Log("Input initialization failed! Please check if your controller is connected properly.");
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

            switch (this._userInput.CurrentMovement())
            {
                case Movement.Left:
                    this.Left();
                    break;
                case Movement.Right:
                    this.Right();
                    break;
                default:
                    break;
            }
        }
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
        if (this._userInput != null)
            this._userInput.Destroy();
    }
}
