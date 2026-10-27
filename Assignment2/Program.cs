using System;
using System.Collections.Generic;
using BankAccountInfo;
using VehicleInfo;
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


    }
}

