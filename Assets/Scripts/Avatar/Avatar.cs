/// <summary>
/// This class represents the avatar as domain object. Therefore, it
/// is a plain old C# object.
/// </summary>
public class Avatar
{
    /// <summary>
    /// Gets or sets the move speed.
    /// </summary>
    /// <value>The move speed</value>
    public float MoveSpeed { get; set; }

    /// <summary>
    /// Gets or sets the track.
    /// </summary>
    /// <value>The track</value>
    public int Track { get; set; }

    /// <summary>
    /// Gets or sets the _avatar behaviour.
    /// </summary>
    /// <value>The avatar behaviour</value>
    private IAvatarBehaviour AvatarBehaviour { get; set; }
	
	/// <summary>
	/// Gets or sets the previous angle.
	/// </summary>
	/// <value>
	/// The previous angle.
	/// </value>/
	private double previousAngle { get; set; }
	
	/// <summary>
	/// Gets or sets the current angle.
	/// </summary>
	/// <value>
	/// The current angle.
	/// </value>
	private double currentAngle { get; set; }

    /// <summary>
    /// Gets or sets the user input
    /// </summary>
    /// <value>The user input</value>
    private IUserInput UserInput { get; set; }

    /// <summary>
    /// Used for initialization. The Start method is called just
    /// before any of the Update methods is called the first time.
    /// </summary>
    public Avatar(IAvatarBehaviour avatarBehaviour, IUserInput userInput)
    {
        this.Track = 2;
        this.MoveSpeed = 15;

        userInput.Initialize();
        this.AvatarBehaviour = avatarBehaviour;
        this.UserInput = userInput;

        StateManager.Instance.PauseOrUnpause();
    }

    /// <summary>
    /// Update is called once per frame. It moves the avatar
    /// forward by a constant value. If the 'S' key is pressed,
    /// the avatar is moved backwards.
    /// </summary>
    public void Update()
    {
        if (!StateManager.Instance.IsPausing())
        {
            AvatarBehaviour.Forward(this.MoveSpeed);
        }
		previousAngle = currentAngle;
		currentAngle = UserInput.CurrentAverageAngle() * 20;
		AvatarBehaviour.SetRotation((previousAngle + currentAngle)/2);
    }

    /// <summary>
    /// Handles the movement
    /// </summary>
    /// <returns></returns>
    public bool MovementHandler()
    {
        switch (this.UserInput.CurrentMovement())
        {
            case AvatarMovement.Left:
                return this.Left();
            case AvatarMovement.Right:
                return this.Right();
            case AvatarMovement.Jump:
                return this.Up();
            case AvatarMovement.Increase:
                MoveSpeed += 2.0f;
                break;
            case AvatarMovement.Decrease:
                MoveSpeed -= 1.0f;
                break;
            default:
                break;
        }
        return false;
    }

    /// <summary>
    /// Move player one track to the left
    /// </summary>
    public bool Left()
    {
        if (StateManager.Instance.IsPlaying() && Track > 1)
        {
            this.Track--;
            this.AvatarBehaviour.Left();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Move avatar one track to the right
    /// </summary>
    public bool Right()
    {
        if (StateManager.Instance.IsPlaying() && Track < 3)
        {
            this.Track++;
            this.AvatarBehaviour.Right();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Let the avatar jump and hide the startscreen when the game should be started
    /// </summary>
    public bool Up()
    {
        if (StateManager.Instance.IsPlaying() && StateManager.Instance.JumpingEnabled)
        {
            this.AvatarBehaviour.Up();
            return true;
        }
        else if (!StateManager.Instance.IsDead())
        {
            StateManager.Instance.ShowStartScreen = false;
            StateManager.Instance.Score = 0;
            StateManager.Instance.PauseOrUnpause();
        }
        return false;
    }
}
