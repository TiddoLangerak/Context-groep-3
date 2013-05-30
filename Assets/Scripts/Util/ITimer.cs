using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

/// <summary>
/// Defines an interface for a timer. This interface is useful for testing purposes.
/// (As you can use a mocked 'implementation' for this interface)
/// </summary>
public interface ITimer
{
    /// <summary>
    /// Start the timer
    /// </summary>
    void Start();

    /// <summary>
    /// Stop the timer
    /// </summary>
    void Stop();

    /// <summary>
    /// The time after which the timer expires
    /// </summary>
    double Interval { get; set; }

    /// <summary>
    /// Event handler that is called when the timer expires
    /// </summary>
    event ElapsedEventHandler Elapsed;
}