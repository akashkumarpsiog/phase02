using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleInfo
{
    public class Vehicle
    {
        protected string make;
        protected string model;
        protected int year;
        public Vehicle(string Make, string Model, int Year)
        {
            make = Make;
            model = Model;
            year = Year;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Make: {make}");
            Console.WriteLine($"Model: {model}");
            Console.WriteLine($"Year: {year}");
        }
    }

    public class Car : Vehicle
    {
        public int numberOfDoors;
        public Car(string make, string model, int year, int NumberOfDoors) : base(make,model, year)
        {
            numberOfDoors = NumberOfDoors;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine("This is a car: ");
            base.DisplayInfo();
            
            Console.WriteLine($"Number of Doors: {numberOfDoors}");
        }

    }

    public class Bike : Vehicle
    {
        public bool hassideCar;
        public Bike (string make, string model, int year, bool HasSideCar) : base(make, model, year)
        {
            hassideCar = HasSideCar;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine("This is a bike: ");
            base.DisplayInfo();
            
            Console.WriteLine($"Has a side Car: {hassideCar}");
        }
    }

    public class ElectricCar : Car
    {
        public string batteryCapacity;

        public ElectricCar(string make, string model, int year, int NumberOfDoors, string BatteryCapacity) : base(make, model, year, NumberOfDoors)
        {
            batteryCapacity = BatteryCapacity;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine("This is an electric car: ");
            base.DisplayInfo();
            
            Console.WriteLine($"Has a battery capacity of: {batteryCapacity}");
        }

    }
}
