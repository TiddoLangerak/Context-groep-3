using System;

public interface IAvatarBehaviour
{
    bool IsMoving { get; }
    void Forward(int moveSpeed);
    void Left();
    void Right();
}