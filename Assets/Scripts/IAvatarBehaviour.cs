using System;

public interface IAvatarBehaviour
{
    bool IsMoving { get; }
    void Forward(float moveSpeed);
    void Left();
    void Right();
}