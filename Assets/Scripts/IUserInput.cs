using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IUserInput
{
    void Initialize();
    int CurrentMovement();
    void Destroy();
}