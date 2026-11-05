using System;
using System.Collections.Generic;
using System.Text;

namespace Employees
{
    public interface ITaxable
    {
        public double CalculateTax();
    }

    public abstract class Employee
    {
        public string Name { get; set; }
        public string Department { get; set; }

        public Employee(string name, string department)
        {
            Name = name;
            Department = department;
        }
        public abstract double CalculateSalary();

    }

    public class  FullTime : Employee, ITaxable
    {
        public double MonthlySalary { get; set; }
        public double Bonus { get; set; }

        public FullTime(string name, string department, double monthlySalary, double bonus) : base(name, department)
        {
            MonthlySalary = monthlySalary;
            Bonus = bonus;
        }
        public double CalculateTax()
        {
            return 0.2 * CalculateSalary();
        }
        public override double CalculateSalary()
        {
            return MonthlySalary + Bonus;
        }
    }

    public class PartTime : Employee
    {
        public int HoursWorked { get; set; }
        public double HourlyRate { get; set; }

        public PartTime(string name, string department, int hoursWorked, double hourlyRate) : base(name, department)
        {
            HoursWorked = hoursWorked;
            HourlyRate = hourlyRate;
        }

        public override double CalculateSalary()
        {
            return HoursWorked * HourlyRate;
        }
    }

    public class Contractor : Employee
    {
        public double ContractAmount { get; set; }

        public Contractor(string name, string department, double contractAmount ) : base(name, department)
        {
            ContractAmount = contractAmount;
        }

        public override double CalculateSalary()
        {
            return ContractAmount;
        }
    }
}
