using System;
using UnityEngine;

/// <summary>
/// This class is responsible for user input from the keyboard.
/// </summary>
class KeyboardUserInput : IUserInput
{
    public void Initialize()
    {
        StateManager.Instance.NumberOfPlayers = 1;
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
			return AvatarMovement.Jump;	
		} else if(Input.GetKeyDown(KeyCode.I))
		{
			return AvatarMovement.Increase;
		}
		else if(Input.GetKeyDown(KeyCode.U))
		{
			return AvatarMovement.Decrease;
		}
        else
        {
            return AvatarMovement.None;
        }
    }

    public void OnDestroy()
    {
        StateManager.Instance.NumberOfPlayers = 0;
    }
}