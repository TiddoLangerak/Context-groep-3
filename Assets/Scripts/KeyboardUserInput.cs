using System;
using UnityEngine;

/// <summary>
/// This class is responsible for user input from the keyboard.
/// </summary>
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
		else if(Input.GetKeyDown(KeyCode.W))
		{
			return Movement.Up;	
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