﻿#if UNITY_EDITOR
using UnityEngine;
public class Logger
{
    public static void Log(string message)
    {
        Debug.Log(message);
    }
}

#else


using System;
public class Logger {
    public static void Log(string message)
    {
        Console.WriteLine(message);
    }
}

#endif