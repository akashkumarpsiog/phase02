using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2
{
    public class Notification
    {
        public virtual void Send()
        {
            Console.WriteLine("Sending generic Notification");
        }
    }
    public class EmailNotification : Notification
    {
        public sealed override void Send()
        {
            Console.WriteLine("sending an email");
        }
    }

    public class SMSNotification : Notification
    {
        public override void Send()
        {
            Console.WriteLine("Sending an SMS");
        }
    }
    //public class MarketingEmailNotification : EmailNotification
    //{
    //    public override void Send()
    //    {
    //        Console.WriteLine("Sending a marketing email.");
    //    }
    //}
    //compile error produced because we marked sealed override in the email class. 
}
