using System;
using UnityEngine;

class KeyboardUserInput : IUserInput
{
    public void Initialize()
    {
    }

    public Movement CurrentMovement()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            return Movement.Left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            return Movement.Right;
        }
        else
        {
            return Movement.None;
        }
    }

    public void Destroy()
    {
    }
}