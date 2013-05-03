#if UNITY_EDITOR
using UnityEngine;
#else
using System;
#endif

/// <summary>
/// Logger class which can be used for logging both in and outside of unity.
/// </summary>
public class Logger
{
    /// <summary>
    /// Logs a message
    /// </summary>
    /// <param name="message">
    /// An object which can be converted to a string. The string representation of the object will be printed.
    /// </param>
    public static void Log(object message)
    {
        #if UNITY_EDITOR
            Debug.Log(message);
        #else
            Console.WriteLine((string)message);
        #endif
    }
}
