using System;
using UnityEngine;

class KeyboardUserInput : IUserInput
{
    public void Initialize()
    {
    }

    public int CurrentMovement()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            return 1;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }

    public void Destroy()
    {
    }
}