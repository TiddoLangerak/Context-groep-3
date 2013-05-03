using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IUserInput
{
    public enum Movement
    {
        None,
        Left,
        Right,
    };
    void Initialize();
    Movement CurrentMovement();
    void Destroy();
}