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

    public AvatarMovement CurrentMovement()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            return AvatarMovement.Left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            return AvatarMovement.Right;
        }
		else if(Input.GetKeyDown(KeyCode.W))
		{
			return Movement.Up;	
		}
        else
        {
            return AvatarMovement.None;
        }
    }

    public void Destroy()
    {
    }
}