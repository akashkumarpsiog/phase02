using Assignment2;
using BankAccountInfo;
using MathOps;
using System;
using System.Collections;
using System.Collections.Generic;
using VehicleInfo;
using System.Linq;
using System.ComponentModel;
using NotificationAssignment;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using LibraryManagement;
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

        Assignment2.Logger logger = new Assignment2.Logger();
        FileLogger filelogger = new FileLogger();
        //this gives expected output
        logger.Log();
        filelogger.Log();

        //but C# chooses which method to call based on the variables declared type.
        //So if we use Logger logger = new FileLogger() then use logger.log() we would still get Logger.log()
        //override would use runtime polymorphism.
        Assignment2.Logger l = new FileLogger();
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
        //we can do normal squaring and even methods. but what if we want the future list processor to filter odd numbers, double them, then we would have ot modify the method.
        //with delegates we dont have to rewrite the processing method, just the delegates.
        List<int> numbers = new() { 1, 2, 3, 4, 5, 6 };
        Predicate<int> isEven = n => n % 2 == 0; //accepts int, return bool
        Func<int, int> square = n => n * n; //accepts two int, returns int
        Action<string> print = message => Console.WriteLine(message); //accepts text, return nothing
        ListProcessor processor = new();
        processor.ProcessList(numbers, isEven, square, print);

        // First pattern with delegates: 
        // 1. Declare central method that accepts delegates
        // 2. define behavior/delegates outside the method.
        // 3. pass the delegate/behavior to the central method.

        // Events:
        // 1. define event data. (can inherit from EventArgs) - this is so subscribers can have info about the event.
        // 2. Declare the event. This public event EventHandler<> x; establishes the event that subscribers can register with.
        // the EventHandler<a> is a built in delegate type. It describes the handler it recieves.
        // 3. Raise the event using Invoke (call the methods currently registered with this delegate/event - passing them these arguments).
        // 4. write subscriber methods
        // 5. subscribe and trigger - add the method to the event. The subscriber uses the signature.
        // 6. when the event happens (that method is called) - the data of the event object is created and passed with the object in Invoke.
        // EventArgs is used when there is no event data: Ex:
        // public event EventHandler? OnAlarmRing; 
        // public void Ring() { OnAlarmRing?Invoke(this,EventArgs.Empty); }


        //task 2.11
        // When we apply the same logic, but the only difference is what object is given, we can use Generics -> we write the logic once and make the type configurable.
        // T is the type parameter and it is a placeholder for a type that will be specified when we use repository later.
        // A repository(arbitrary class name) is a commonly used name for a class that manages data access.

        Repository<Student> students = new();
        students.Add(new Student(1, "Vishvalingam", 23));
        students.Add(new Student(2, "abijith", 23));
        students.Add(new Student(3, "Akash", 24));

        foreach (Student student in students.GetAll())
        {
            Console.WriteLine(student);
        }

        students.Update(1, new Student(2, "marlin", 23));
        foreach (Student student in students.GetAll())
        {
            Console.WriteLine(student);
        }

        students.Delete(0);
        foreach (Student student in students.GetAll())
        {
            Console.WriteLine(student);
        }
        Student EmptyStudent = students.CreateNew();
        Console.WriteLine(EmptyStudent);


        Repository<Product> products = new();
        products.Add(new Product(1, "guitar", 2300m));
        products.Add(new Product(2, "drums", 2000m));

        foreach (Product product in products.GetAll())
        {
            Console.WriteLine(product);
        }

        products.Update(1, new Product(2, "keyboard", 3000m));
        foreach (Product product in products.GetAll())
        {
            Console.WriteLine(product);
        }

        products.Delete(0);
        foreach (Product product in products.GetAll())
        {
            Console.WriteLine(product);
        }
        Product EmptyProduct = products.CreateNew();
        Console.WriteLine(EmptyProduct);

        //task 2.12
        // IEnumerable<T> is a promise that this object can provide a sequence of T values that we can iterate over.
        // instead of creating a list, adding data to it and then returning those numbers we can use yield return. It produces one value and pauses execution of the method. 
        // When the caller requests the next value, the method resumes from where it paused. It is lazy execution because it produces output one at a time.

        Console.WriteLine("Before foreach");
        foreach (int num in GetEvenNumbers(10))
        {
            Console.WriteLine($"Consumed:{num}");
        }
        Console.WriteLine("After foreach");

        var books = new BookCollection();
        books.Add(new Book("abc book", "Mark"));
        books.Add(new Book("Atomic Habits", "James Clear"));
        books.Add(new Book("xyz book", "George RR Martin"));
        books.Add(new Book("1984", "George Orwell"));

        foreach (Book book in books)
        {
            Console.WriteLine($"{book.Title} - {book.Author}");
        }

        //task 2.13
        // LINQ - Language Integrated Query
        // without linq we would write multiple foreach, if temporary lists and dictionaries. Query syntax is translated by c# compiler into method calls.
        // We can write with query syntax which is like SQL or method syntax which uses methods nad lambda expressions.

        EmployeeManager empMan = new EmployeeManager();
        empMan.HighSalary();
        empMan.GetDepartmentSummary();
        empMan.EmployeeInfo();
        empMan.FilteredQuerySalary();

        //task 2.14
        // this keyword tells the compiler that this static method can also be called using instance-method syntax on a string object.
        //extension methods do not add methods to the original class. We dont modify System.string for example. We sare using the compiler supported syntax to make a static method look like an instance method. 

        string name1 = "aKASH KUMAR";
        string name2 = "hello WORLD";

        Console.WriteLine(name1.ToTitleCase());
        Console.WriteLine(name2.ToTitleCase());

        List<int> emptyList = new();
        List<int> populatedList = new() { 10, 20, 30 };
        List<int>? nullList = null;

        Console.WriteLine(emptyList.IsNullOrEmpty());      // True
        Console.WriteLine(populatedList.IsNullOrEmpty());  // False
        Console.WriteLine(nullList.IsNullOrEmpty());       // True

        Console.WriteLine(0.ToWords());    // Zero
        Console.WriteLine(20.ToWords());   // Twenty
        Console.WriteLine(42.ToWords());   // Forty-two
        Console.WriteLine(101.ToWords());  // One Hundred and One

        int[] invalidNumbers = { -1, 1000 };

        foreach (int number in invalidNumbers)
        {
            try
            {
                Console.WriteLine(number.ToWords());
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Invalid input: {number}");
            }
        }

        EmployeeManager manager = new EmployeeManager();

        manager.EmployeeAnnualSalary();



        //Task Mini Q4 Employee Payroll

        Console.WriteLine("Employee Payroll");
        List<Employees.Employee> EmployeeList = new List<Employees.Employee>
        {
            new Employees.FullTime("Alice", "HR", 5000, 1000),
            new Employees.FullTime("Alan", "HR", 4000, 1000),
            new Employees.FullTime("Bob", "IT", 7000, 1000),

            new Employees.PartTime("Meera", "IT", 4000, 1000),
            new Employees.PartTime("Poorvika", "HR", 4000, 1000),
            new Employees.PartTime("Xavier", "Management", 10000, 1000),

            new Employees.Contractor("Tanmay", "Management", 12000),
            new Employees.Contractor("Katy", "Sales", 15000),
            new Employees.Contractor("Hannah", "IT", 10000)
        };

        var GroupedEmployees = EmployeeList.GroupBy(e => e.Department);
        foreach (var group in GroupedEmployees)
        {
            Console.WriteLine($"Department: {group.Key}");
            double DepartmentPayroll = group.Sum(e => e.CalculateSalary());
            
            foreach (var employee in group)
            {
                Console.WriteLine($"Employee: {employee.Name}, Salary: {employee.CalculateSalary()}");
            }
            Console.WriteLine($"Department Payroll: {DepartmentPayroll}");
        }

        // total payroll
        double TotalPayroll = EmployeeList.Sum(e => e.CalculateSalary());
        Console.WriteLine($"Total Payroll: {TotalPayroll}");

        //task Mini Q5 - delegates
        NotificationService service = new NotificationService();

        NotificationAssignment.Logger notifLogger = new NotificationAssignment.Logger();

        EmailNotif emailObj = new EmailNotif();
        SMSNotif smsObj = new SMSNotif();
        PushNotif pushObj = new PushNotif();

        service.OnNotificationSent += notifLogger.LoggingEvent;

        service.Notify(emailObj.SendEmail, "Hi email");
        service.Notify(smsObj.SendSMS, "hi sms");
        service.Notify(pushObj.SendPush, "hi push");


        // Task Mini Q6 - Library Management LINQ

        List<LibraryManagement.Book> Library = new()
        {
            new("The Hobbit", "J.R.R. Tolkien", "Fantasy", 1937, true),
            new("1984", "George Orwell", "Dystopian", 1949, false),
            new("Animal Farm", "George Orwell", "Satire", 1945, true),
            new("The Great Gatsby", "F. Scott Fitzgerald", "Classic", 1925, true),
            new("To Kill a Mockingbird", "Harper Lee", "Classic", 1960, false),
            new("Dune", "Frank Herbert", "Science Fiction", 1965, true),
            new("Foundation", "Isaac Asimov", "Science Fiction", 1951, true),
            new("Harry Potter and the Philosopher's Stone", "J.K. Rowling", "Fantasy", 1997, true),
            new("The Da Vinci Code", "Dan Brown", "Thriller", 2003, false),
            new("The Alchemist", "Paulo Coelho", "Fiction", 1988, true),
            new("The Hunger Games", "Suzanne Collins", "Dystopian", 2008, true),
            new("The Fault in Our Stars", "John Green", "Romance", 2012, false),
            new("The Martian", "Andy Weir", "Science Fiction", 2011, true),
            new("Educated", "Tara Westover", "Memoir", 2018, true),
            new("Project Hail Mary", "Andy Weir", "Science Fiction", 2021, true),
            new("Atomic Habits", "James Clear", "Self-Help", 2018, true)
        };

        // 1. Find the oldest book
        var oldestBook = LibraryManagement.BookQueries.OldestBook(Library);

        if (oldestBook != null)
        {
            Console.WriteLine($"Oldest Book: {oldestBook.Title}, Year: {oldestBook.Year}");
        }

        // 2. Find books published after 2010
        var booksAfter2010 = LibraryManagement.BookQueries.BooksAfterYear(Library, 2010);

        foreach (var book in booksAfter2010)
        {
            Console.WriteLine($"Book: {book.Title}, Year: {book.Year}");
        }

        // 3. Find books by author
        var booksByAuthor = LibraryManagement.BookQueries.BooksByAuthor("Suzanne Collins", Library);

        foreach (var book in booksByAuthor)
        {
            Console.WriteLine(book.Title);
        }

        // 4. Group books by genre
        var booksByGenre = LibraryManagement.BookQueries.BooksByGenre(Library);

        foreach (var genre in booksByGenre)
        {
            Console.WriteLine($"Genre Name: {genre.Genre}");
            Console.WriteLine($"Number of books: {genre.Count}");
        }

    }




    public static IEnumerable<int> GetEvenNumbers(int max)
    {
        Console.WriteLine("Iterator started");
        for (int i = 2; i <= max; i++)
        {
            if (i % 2 == 0)
                Console.WriteLine($"Producing: {i}");
                yield return i;
        }
    }

    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public Book(string title, string author)
        {
            Title = title;
            Author = author;
        }
    }

    public class BookCollection : IEnumerable<Book> //this shows that BookCollection can be enumerated.
    {
        private readonly List<Book> _books = new(); //internal list

        public void Add(Book book)
        {
            _books.Add(book);
        }

        public IEnumerator<Book> GetEnumerator()
        {
            foreach (Book book in _books.OrderBy(b => b.Title))
            {
                yield return book;
            }
        } //this is the generic implementation.

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        //before generics were introduced, collections used non generic interfaces. 
        // With that, the enumerable could contain different types. So it was less type safe and required casting. 
        //Generics fixedthis. But .NET retained the original non-generic IEnumerable interface for backward compatibility.
    }



}

// method hiding is compile time. method overriding is runtime polymorphism - it uses virtual dispatch.
// method overloading is compile time.
// compile time binding first - based on reference type. then we switch to virtual dispatch for runtime polymorphism if virtual or abstract is used (runtime/ dynamic dispatch).
// EventHandler - its signature is:
// public delegate void EventHandler (object? sender, EventArgs e); - sender is object raising event, e is the event data. EventArgs is a base class for event data. It can be used when there is no event data.
// we can declare public event EventHandler<x>? OnX; Then we can raise the event with OnX?.Invoke(this, new x{ a = B });
// having yield return makes a method an iterator method. Its body doesnt start executing until someone requests elements from that sequence.




//Generate code coverage data:
//dotnet test --collect:"XPlat Code Coverage"
//Generate HTML coverage report
//reportgenerator "-reports:Assignment2.Tests/TestResults/**/coverage.cobertura.xml" "-targetdir:coveragereport" "-reporttypes:Html;TextSummary"