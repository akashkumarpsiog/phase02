using NotificationAssignment;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Assignment2.Tests
{
    public class NotificationTests
    {
        [Fact]
        public void Notify_InvokesSender()
        {
            //Arrange
            NotificationService service = new NotificationService();
            string? receivedMessage = null;

            NotificationService.NotificationDelegate sender = message => receivedMessage = message;

            //Act
            service.Notify(sender, "hello");

            //Assert
            Assert.Equal("hello", receivedMessage);
        }

        [Fact]
        public void Notify_RaisesEvent()
        {
            //arrange
            NotificationService service = new NotificationService();
            bool eventRaised = false;

            service.OnNotificationSent += (sender, args) => eventRaised = true;

            //Act
            service.Notify(message => { }, "hello");
            // message => {} is equivalent to a method that takes string as input and returns void.

            //Assert
            Assert.True(eventRaised);
        }

        [Fact]
        public void Notify_ExecutesSenderBeforeEvent()
        {
            // Arrange
            NotificationService service = new NotificationService();

            List<string> executionOrder = new List<string>();

            // Act
            service.OnNotificationSent += (sender, args) =>
            {
                executionOrder.Add("Event");
            }; //recording the event happened. This just subscribes to the event. It doesnt execute the lambda.

            service.Notify(message =>
            {
                executionOrder.Add("Sender");
            }, "Hello!"); //the method inputs string (message) and returns nothing. just adds info to a list.
            //this actually calls the notify method.


            // Assert
            Assert.Equal(
                new[] { "Sender", "Event" },
                executionOrder
            );
        }

        [Fact]
        public void Logger_LogsNotification()
        {
            // Arrange
            NotificationService service = new();
            NotificationAssignment.Logger logger = new();

            using StringWriter output = new(); //using makes it so the stringwriter will be disposed automatically when execution leaves the scope.
            TextWriter originalOutput = Console.Out;

            //when we write console.writeline the text goes to standard output stream which is represented by Console.Out
            //Console.Out is a property of the console class and its type is TextWriter.
            //TextWriter is an abstract base class designed for writing text to different destinations. Text can be written to console, a string in memory, a file.
            //Console.WriteLine() writes to whatever the Console.Out points to and we can change that.
            //StringWriter is a class that inherits from TextWriter. It captures text and stores it in a string buffer in memory.
            // we can write to the stringwriter object and retrieve it using output.ToString().
            //originaloutput is the standard output location (actual console).

            try
            {
                Console.SetOut(output); //changes destination used by Console.WriteLine.

                service.OnNotificationSent += logger.LoggingEvent;

                // Act
                service.Notify(message => { }, "Hello!");
            }
            finally
            {
                Console.SetOut(originalOutput); //we have to restore since its application level.
            }

            // Assert
            Assert.Contains("Logging that a notification took place", output.ToString());
            Assert.Contains("Hello!", output.ToString());
        }

        [Fact]
        public void Notify_SupportsAllThreeSenders()
        {
            // Arrange
            NotificationService service = new NotificationService();

            EmailNotif email = new EmailNotif();
            SMSNotif sms = new SMSNotif();
            PushNotif push = new PushNotif();

            using StringWriter output = new StringWriter();
            TextWriter originalOutput = Console.Out;

            try
            {
                Console.SetOut(output);

                // Act
                service.Notify(email.SendEmail, "Email message");
                service.Notify(sms.SendSMS, "SMS message");
                service.Notify(push.SendPush, "Push message");
            }
            finally
            {
                Console.SetOut(originalOutput);
            }

            // Assert
            string result = output.ToString();

            Assert.Contains("Email message", result);
            Assert.Contains("SMS message", result);
            Assert.Contains("Push message", result);
        }
    }
}
