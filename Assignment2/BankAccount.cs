using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccountInfo
{
    public class BankAccount
    {
        private decimal _balance;
        private readonly List<string> _history = new();
        
        public decimal Balance 
        {
            get { return _balance; }
            private set { _balance = value; }
        }

        public BankAccount(decimal initialBalance)
        {
            if (initialBalance <0)
            {
                throw new ArgumentOutOfRangeException(nameof(initialBalance), "initial balance cannot be negative");
            }
            _balance = initialBalance;
            _history.Add($"Account created with balance: {_balance}");
        }

        public decimal Deposit(decimal amt)
        {
            if (amt > 0)
            {
                Balance = Balance + amt;
                _history.Add($"{amt} deposited");
            }
            else
            {
                _history.Add($"{amt} deposit rejected, amount must be positive.");
                throw new ArgumentOutOfRangeException("Insufficient Deposit Amount");
            }

            return Balance;
        }
        public decimal Withdraw(decimal wamt)
        {
            if (wamt > 0 && Balance - wamt >= 0)
            {
                Balance = Balance - wamt;
                _history.Add($"{wamt} withdrawn");
            }
            else if (wamt <=0)
            {
                _history.Add($"{wamt} withdraw rejected: amount has to be positive.");
                throw new InvalidOperationException("Withdraw amount must be positive.");
            }
            else if (wamt > Balance)
            {
                _history.Add($"{wamt} withdraw rejected as its greater than balance.");
                throw new InvalidOperationException("Withdraw amount must be lower than balance."); 
            }
            return Balance;
        }

        public decimal GetBalance()
        {
            return Balance;
        }

        public void PrintHistory()
        {
            foreach (string hist in _history)
            {
                Console.WriteLine($"{hist}");
            }
        }

    }
}
