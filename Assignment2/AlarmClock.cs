using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2
{
    //public class EventArgs
    //{ public static readonly EventArgs Empty = new EventArgs();}
    //it represents data associated with an event. 
    public class AlarmEventArgs : EventArgs
    {
        public DateTime AlarmTime { get; }
        public AlarmEventArgs(DateTime alarmTime)
        {
            AlarmTime = alarmTime;
        }
    }

    public class AlarmClock
    {
        public event EventHandler<AlarmEventArgs>? OnAlarmRing;

        public void Ring()
        {
            var args = new AlarmEventArgs(DateTime.Now);
            OnAlarmRing?.Invoke(this, args);
        }
    }

    public class Person
    {
        public void WakeUp(object? sender, AlarmEventArgs e)
        {
            Console.WriteLine($"Person woke up at {e.AlarmTime:T}");
        }
    }

    public class CoffeeMachine
    {
        public void MakeCoffee(object? sender, AlarmEventArgs e)
        {
            Console.WriteLine($"Coffee machine started at {e.AlarmTime:T}");
        }
    }
}
