using System;
using System.Collections.Generic;
using System.Text;

namespace ShapeInt
{
    public interface IShape
    {
        public double CalculateArea();
        public double CalculatePerimeter();
    }

    public interface IDrawable
    {
        public string Draw();
    }


    public class Shape: IShape, IDrawable
    {
        private double l;
        private double w;

        public double Len
        {
            get { return l; }
            private set { l = value; }
        }

        public double Wid
        {
            get { return w; }
            private set { w = value; }
        }
        public Shape(double len, double wid)
        {
            Len = len;
            Wid = wid;
        }
        public double CalculateArea()
        {
            return Len * Wid;
        }

        public double CalculatePerimeter()
        {
            return 2 * (Len + Wid);
        }
        
        public string Draw()
        {
            string drawing = "This is a drawing of a shape";
            return drawing;
        }
    }
}

// Interfaces tells what the object can do. Interface cant declare instance fields.
// Abstract classes can provide shared data and implementations. A class can inherit only from one class.
