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
        //EventHandler defines the type of methods that cna subscribe - it is a built in delegate. The OnAlarmRing here is the name of the event.
        //EventHandler expects methods with this signature (void SomeMethod(object? sender, AlarmEventArgs e)

        public void Ring()
        {
            var args = new AlarmEventArgs(DateTime.Now); //this creates the event data.
            OnAlarmRing?.Invoke(this, args); //this raises the event. It says the alarm has rung and anyone who subscribed, execute your methods.
            //this is the object that raised the event. args is the info about the event.
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
