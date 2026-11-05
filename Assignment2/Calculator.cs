using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Assignment2
{
    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
        public double Add(double a, double b)
        {
            return a + b;
        }
        public int Add(int a, int b, int c)
        {
            return a + b + c;
        }
        public int Add(params int[] numbers)
        {
            int sum = 0;
            foreach (int number in numbers)
            {
                sum += number;
            }
            return sum;
        }

    }

    public class Logger
    {
        public void Log()
        {
            Console.WriteLine("Logger.Log()");
        }
    }

    public class FileLogger : Logger
    {
        public new void Log()
        {
            Console.WriteLine("FileLogger.Log()");
        }
    }
}
