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
	public float moveSpeed { get; set; }
	
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

        userInput.Initialize();
        this._avatarBehaviour = avatarBehaviour;
        this._userInput = userInput;

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

            switch (this._userInput.CurrentMovement())
            {
                case AvatarMovement.Left:
                    this.Left();
                    break;
                case AvatarMovement.Right:
                    this.Right();
                    break;
				case AvatarMovement.Jump:
					this.Up();
					break;
				case AvatarMovement.Increase:
					Logger.Log("increase: " + moveSpeed);
					moveSpeed+= 2.0f;
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
	/// Up this instance.
	/// </summary>
	public void Up()
	{
		if(StateManager.Instance.isPlaying())
		{
			this._avatarBehaviour.Up();	
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
