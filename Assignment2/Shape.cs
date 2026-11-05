using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2
{
    public abstract class Shape
    {
        //abstract classes can contain both abstract methods and a concrete method.
        
        public abstract double CalculateArea();

        public void DisplayArea()
        {
            Console.WriteLine($"Area: {CalculateArea()}");
        }
    }

    public class Circle : Shape
    {
        public double rad;
        public Circle(double Radius)
        {
            rad = Radius;
        }

        public override double CalculateArea()
        {
            return 3.14 * Math.Pow(rad,2);
        }
    }

    public class Rectangle : Shape
    {
        public double len;
        public double wid;
        public Rectangle(double length, double width)
        {
            len = length;
            wid = width;
        }

        public override double CalculateArea()
        {
            return len * wid;
        }
    }
}
