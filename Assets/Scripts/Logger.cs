#if UNITY_EDITOR
using UnityEngine;
public class Logger
{
    public static void Log(object message)
    {
        Debug.Log(message);
    }
}

#else


using System;
public class Logger {
    public static void Log(object message)
    {
        Console.WriteLine((string)message);
    }
}

#endif