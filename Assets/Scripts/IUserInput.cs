using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// The interface for user input.
/// </summary>
public interface IUserInput
{
    /// <summary>
    /// Setup the input
    /// </summary>
    void Initialize();
    /// <summary>
    /// Get the current movement
    /// </summary>
    /// <returns>
    /// The current movement
    /// </returns>
    Movement CurrentMovement();
    /// <summary>
    /// Stops processing input.
    /// </summary>
    void Destroy();
}