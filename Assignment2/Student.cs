using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; //when we dont initialize reference types, they are null.
        public int Age { get; set; }

        public Student() { }
        public Student(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        public override string ToString()
        {
            return $"Students: {Id}, {Name}, Age: {Age}";
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Product() { }
        public Product(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
        public override string ToString()
        {
            return $"Product: {Id}, {Name}, Price: {Price}";
        }


        //when we call WriteLine(student) it calls ToString automatically before printing it. Now we override it.
    }
}
