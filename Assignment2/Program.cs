using System;
using Assignment2;
using System.Collections.Generic;
using BankAccountInfo;
using VehicleInfo;
using MathOps;
class Program
{
    static void Main()
    {
        //task 2.1
        BankAccount account = new BankAccount(1000m);

        Console.WriteLine(account.GetBalance());

        account.Deposit(500m);
        account.Withdraw(200m);
        Console.WriteLine(account.GetBalance());

        try { account.Withdraw(2000m); }
        catch (InvalidOperationException) { Console.WriteLine("Overdraw rejected"); }

        try { account.Deposit(-100m); }
        catch (ArgumentOutOfRangeException) { Console.WriteLine("Negative deposit rejected"); } 

        try { account.Withdraw(-100m); }
        catch (InvalidOperationException) { Console.WriteLine("Negative withdrawal rejected"); }

        try { account.Withdraw(0m); }
        catch (InvalidOperationException) { Console.WriteLine("Zero withdrawal rejected"); }

        account.Withdraw(account.GetBalance());
        Console.WriteLine(account.GetBalance());

        account.PrintHistory();

        //task 2.2
        Vehicle vehicle = new Vehicle("Honda", "City", 2024);
        Car car = new Car("Mercedez", "e class", 2026, 4);
        Bike bike = new Bike("Yamaha", "Fz", 2026, false);
        ElectricCar electriccar = new ElectricCar("BYD", "newest", 2026, 4, "105 kwh");
        vehicle.DisplayInfo();
        car.DisplayInfo();
        bike.DisplayInfo();
        electriccar.DisplayInfo();

        //task 2.3
        Notification notif = new Notification();
        notif.Send();
        EmailNotification Emailnotif = new EmailNotification();
        Emailnotif.Send();
        SMSNotification SMSnotif = new SMSNotification();
        SMSnotif.Send();

        //task 2.4
        Circle circle = new Circle(5);
        Rectangle rectangle = new Rectangle(4, 6);

        Console.WriteLine("Circle:");
        circle.DisplayArea();

        Console.WriteLine("Rectangle:");
        rectangle.DisplayArea();

        //Shape shape = new Shape(); 
        // cannot create an instance of the abstract type 

        //task 2.5 - interfaces
        ShapeInt.Shape shape = new ShapeInt.Shape(5, 4);
        Console.WriteLine(shape.CalculateArea());
        Console.WriteLine(shape.CalculatePerimeter());
        Console.WriteLine(shape.Draw());

        //task 2.6 - overloading, runtime polymorphism and method hiding

        List<Assignment2.Shape> shapes = new()
        {
            new Assignment2.Circle(5),
            new Assignment2.Rectangle(4,6)

        };

        foreach (Assignment2.Shape s in shapes)
        {
            s.DisplayArea();
        }

        Logger logger = new Logger();
        FileLogger filelogger = new FileLogger();
        //this gives expected output
        logger.Log();
        filelogger.Log();

        //but C# chooses which method to call based on the variables declared type.
        //So if we use Logger logger = new FileLogger() then use logger.log() we would still get Logger.log()
        //override would use runtime polymorphism.
        Logger l = new FileLogger();
        l.Log();

        //task 2.7

        Money a = new Money(100m, "INR");
        Money b = new Money(50m, "INR");
        Money c = new Money(100m, "INR");

        // Addition
        Money total = a + b;
        Console.WriteLine(total.amount == 150m); // True

        // Equality
        Console.WriteLine(a == c); // True
        Console.WriteLine(a != b); // True

        // Comparisons
        Console.WriteLine(a > b);  // True
        Console.WriteLine(a < b);  // False

        // Different currencies
        Money dollars = new Money(20m, "USD");

        //if we dont use a try catch here, then the remaining statements after the exception is not run. 

        try
        {
            Money invalid = a + dollars;
            Console.WriteLine("Unexpected success");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
            // Cannot add different currencies
        }

        // Different currencies should not be considered equal
        Console.WriteLine(a == dollars); // False

        // Equals and GetHashCode consistency
        Console.WriteLine(a.Equals(c)); // True
        Console.WriteLine(a.GetHashCode() == c.GetHashCode()); // True

        //task 2.8
        //MathOperations.MathOperation operation = MathOperations.Add;
        //built in func delegate
        Func<double, double, double> operation = MathOperations.Add;
        Console.WriteLine(operation(10, 5));

        operation = MathOperations.Subtract;
        Console.WriteLine(operation(10, 5));

        operation = MathOperations.Add;
        operation += MathOperations.Multiply;
        operation += MathOperations.Divide;

        Console.WriteLine(operation(10, 5));
        //for mulicast delegates, all of the methods execute (here add, multiple divide all execute), but the output only gives you the return value of the last method. So with void methods, all of those methods happen. 
        // Ex: with notify, we can call sms, email and notification method. If one method throws an exception, the remaining in the invocation list wont execute unless its handled within the method.

        //task 2.9
        AlarmClock alarmclock = new();
        Person person = new();
        CoffeeMachine coffeeMachine = new();

        alarmclock.OnAlarmRing += person.WakeUp;
        alarmclock.OnAlarmRing += coffeeMachine.MakeCoffee;
        alarmclock.Ring();

        //task 2.10
        List<int> numbers = new() { 1, 2, 3, 4, 5, 6 };
        Predicate<int> isEven = n => n % 2 == 0;
        Func<int, int> square = n => n * n;
        Action<string> print = message => Console.WriteLine(message);
        ListProcessor processor = new();
        processor.ProcessList(numbers, isEven, square, print);
    }
}