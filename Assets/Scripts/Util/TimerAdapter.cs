using System.Timers;

/// <summary>
/// Implementation of the ITimer interface. The implementation is the same as in
/// in the native System Timer. This adapter is useful for testing. (as it implements the interface)
/// </summary>
public class TimerAdapter : Timer, ITimer
{
}