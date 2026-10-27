using Assignment2;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
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

    }
}

