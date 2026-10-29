using System;
using System.Collections.Generic;
using System.Text;

namespace MathOps
{
    public class MathOperations
    {
        //custom delegate type.
        //public delegate double MathOperation(double a, double b);

        
        public static double Add(double a, double b)
        {
            return a + b;
        }

        public static double Multiply(double a, double b)
        {
            return a * b;
        }

        public static double Divide(double a, double b)
        {
            return a / b;
        }

        public static double Subtract(double a, double b)
        {
            return a - b;
        }
    }
}
