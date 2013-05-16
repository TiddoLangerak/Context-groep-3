using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

public interface ITimer
{
    void Start();
    void Stop();
    
    double Interval { get; set; }
    event ElapsedEventHandler Elapsed;
}