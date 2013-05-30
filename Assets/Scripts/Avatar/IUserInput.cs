/// <summary>
/// The interface for user input.
/// </summary>
public interface IUserInput
{
    /// <summary>
    /// Initialize the input
    /// </summary>
    void Initialize();

    /// <summary>
    /// Get the current movement
    /// </summary>
    /// <returns>The current movement</returns>
    AvatarMovement CurrentMovement();
}