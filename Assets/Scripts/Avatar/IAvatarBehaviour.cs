/// <summary>
/// Interface for the behaviour of the avatar
/// </summary>
public interface IAvatarBehaviour
{
    /// <summary>
    /// Move the avatar forward
    /// </summary>
    /// <param name="moveSpeed">The speed at which the avatar is forwarded</param>
    void Forward(float moveSpeed);

    /// <summary>
    /// Move the avatar one track to the left
    /// </summary>
    void Left();

    /// <summary>
    /// Move the avatar one track to the right
    /// </summary>
    void Right();

    /// <summary>
    /// Move the avatar up
    /// </summary>
    void Up();

    void SetRotation(double angle);
}