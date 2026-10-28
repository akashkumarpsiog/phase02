using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2
{
    internal class Money
    {
        public decimal amount;
        public string currency;
        public Money(decimal Amount, string Currency)
        {
            amount = Amount;
            currency = Currency;
        }

        //operator overloading is when we teach c# what an operator should do when used with objects of our own class. like here what would it do if we add money1 and money2.
        // this is the general syntax:
        //public static ReturnType operator +(ParameterType left, ParameterType right) - here we are defining what + should do and returntype is the return type of the operator. It is static because the operator belongs to the type not one particular instance.

        public static Money operator +(Money left, Money right)
        {
            if (left.currency != right.currency)
            {
                throw new InvalidOperationException("Cannot add different currencies");
            }
            return new Money(left.amount + right.amount, left.currency);
        }

        public static bool operator ==(Money left, Money right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }
            if (left is null || right is null)
            {
                return false;
            }

            return left.amount == right.amount && left.currency == right.currency;
        }

        public static bool operator !=(Money left, Money right)
        {
            return !(left == right);
        }

        public static bool operator >(Money left, Money right)
        {
            EnsureSameCurrency(left, right);
            return left.amount > right.amount;
        }
        public static bool operator <(Money left, Money right)
        {
            EnsureSameCurrency(left, right);
            return left.amount < right.amount;
        }

        private static void EnsureSameCurrency(Money left, Money right)
        {
            if (left.currency != right.currency)
            {
                throw new InvalidOperationException("Cannot compare different currencies");
            }
        }

        //When we do a.Equals(b) where a and b are two different money objects with the same amount and currency it still returns false by default because the objects are different in memory. This is why we override Equals.

        public override bool Equals(object? obj) //type object means it can recieve any type or null.
        {
            return obj is Money other && this == other;
        }

        //hash code helps us compare every object one by one. It is fast.
        //in case hash code collides and two objects have the same one we can use equals which is one by one o(n).
        //A hashSet<> uses hash code to narrow down where to look. This is O(1).
        //a dictionary basically uses a hashcode for hte key and equals to check if objects are equal.
        //hash set (stores unique values), Hash map (stores key value pairs - dictionary). Both use a hash table which is the underlying data structure.

        public override int GetHashCode()
        {
            return HashCode.Combine(amount, currency);
        }
    }
}
