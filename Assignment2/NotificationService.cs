using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationAssignment
{
    public class NotificationEvent : EventArgs
    {
        public string Message { get; }

        public NotificationEvent(string message)
        {
            Message = message;
        }
    }

    public class NotificationService
    {
        
        public event EventHandler<NotificationEvent>? OnNotificationSent;

        public delegate void NotificationDelegate(string message);
        public void Notify(NotificationDelegate sender, string message)
        {
            sender(message);
            var args = new NotificationEvent(message);
            OnNotificationSent?.Invoke(this, args);
        }
    }

    public class Logger
    {
        public void LoggingEvent(object? sender, NotificationEvent n)
        {
            Console.WriteLine($"Logging that a notification took place with message: {n.Message}");
        }
    }

    public class EmailNotif
    {
        public void SendEmail(string message)
        {
            Console.WriteLine($"Email sent with message: {message}");
        }
    }

    public class SMSNotif
    {
        public void SendSMS(string message)
        {
            Console.WriteLine($"SMS sent with message: {message}");
        }
    }

    public class PushNotif
    {
        public void SendPush(string message)
        {
            Console.WriteLine($"Push notification sent: {message}");
        }
    }
}
