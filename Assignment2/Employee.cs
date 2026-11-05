using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2
{
    public class Employee
    {
        public string name;
        public string department;
        public double salary;
        public DateTime joiningDate;
        public Employee(string Name, string Department, double Salary, DateTime JoiningDate)
        {
            name = Name;
            department = Department;
            salary = Salary;
            joiningDate = JoiningDate;
        }
    }

    public class EmployeeManager
    {
        List<Employee> employees1 = new List<Employee>
            {
                new Employee("Alice", "IT", 85000, new DateTime(2020, 3, 15)),
                new Employee("Bob", "HR", 45000, new DateTime(2022, 6, 10)),
                new Employee("Charlie", "IT", 75000, new DateTime(2019, 1, 20)),
                new Employee("David", "Finance", 60000, new DateTime(2021, 8, 5)),
                new Employee("Eve", "HR", 55000, new DateTime(2020, 11, 12)),
                new Employee("Frank", "IT", 95000, new DateTime(2018, 4, 25)),
                new Employee("Grace", "Finance", 70000, new DateTime(2022, 2, 14)),
                new Employee("Henry", "Marketing", 40000, new DateTime(2023, 5, 1)),
                new Employee("Ivy", "Marketing", 65000, new DateTime(2021, 9, 18)),
                new Employee("Jack", "Finance", 80000, new DateTime(2017, 7, 30))
            };

        public IEnumerable<Employee> HighSalary()
        {
            IEnumerable<Employee> HighSalary = employees1.Where(x => x.salary > 50000).GroupBy(e => e.department).SelectMany(group => group).OrderByDescending(e => e.salary);
            //after groupby it is no longer a list. It is IEnumerable<IGrouping<string, Employe>>
            //this is a group identified by a string key containing employee objects.
            // C# doesnt allow reference to another non-static instance field in a field initializer. Cant call an instance method inside a field initializer.
            return HighSalary;
        }

        public void GetDepartmentSummary()
        {
            var result = employees1.GroupBy(e => e.department).Select(group => new
            {
                Department = group.Key,
                EmployeeCount = group.Count(),
                AverageSalary = group.Average(e => e.salary)
                // this is an anonymous type. It is a class that is created by the compiler. It has no name and is used to hold data temporarily.
                // we create an object that has these properties and the compiler generates a type behind the scenes.
            });

            foreach (var item in result)
            {
                Console.WriteLine(item.Department);
                Console.WriteLine(item.EmployeeCount);
                Console.WriteLine(item.AverageSalary);
            }
        }

        public void EmployeeInfo()
        {
            var EmployeeExperience = employees1.Select(e => new
            {
                Name = e.name,
                Experience = DateTime.Today.Year - e.joiningDate.Year
            });

            foreach (var item in EmployeeExperience)
            {
                Console.WriteLine($"Name: {item.Name}, Experience: {item.Experience} years");
            }
        }

        public void FilteredQuerySalary()
        {
            var filteredQuery = from e in employees1
                                where e.salary > 50000
                                group e by e.department into depargroup
                                from employee in depargroup
                                orderby employee.salary descending
                                select employee;

            var departmentQuery = from e in employees1
                                  group e by e.department into departmentGroup
                                  select new
                                  {
                                      Department = departmentGroup.Key,
                                      EmployeeCount = departmentGroup.Count(),
                                      AvgSalary = departmentGroup.Average(e => e.salary)
                                  };

            var EmployeeeQuery = from e in employees1
                                 select new
                                 {
                                     Name = e.name,
                                     Experience = DateTime.Today.Year - e.joiningDate.Year
                                 };

            foreach (var item in filteredQuery)
            {
                Console.WriteLine($"Name: {item.name}, Department: {item.department}, Salary: {item.salary}, Joining Date: {item.joiningDate.ToShortDateString()}");
            }

            foreach (var item in departmentQuery)
            {
                Console.WriteLine($"Department: {item.Department}, EmployeeCount: {item.EmployeeCount}, AvgSalary: {item.AvgSalary}");
            }

            foreach (var item in EmployeeeQuery)
            {
                Console.WriteLine($"Name: {item.Name}, Experience: {item.Experience} years");
            }
        }

        public void EmployeeAnnualSalary()
        {
            var annualSalaryData = employees1.Select(e => new
            {
                Name = e.name,
                AnnualSalary = e.salary * 12
            });

            foreach (var employee in annualSalaryData)
            {
                Console.WriteLine($"Name: {employee.Name}");
                Console.WriteLine($"Annual Salary: {employee.AnnualSalary}");
            }
        }
    }
}

// ANONYMOUS TYPE RETURN LIMITATION:
// Anonymous types are types generated automatically by the C# compiler.
// They do not have an explicitly declared name that we can reference in our code.
//
// We can use 'var' to store an anonymous type in a local variable because the compiler automatically infers its type.
//
// However, we cannot directly declare an anonymous type as a method's return type because we cannot reference its generated type name.
//
// For example, the following is invalid:
//
// public IEnumerable<AnonymousType> GetEmployeeData()
//
// We also cannot use 'var' as a method's return type.
//
// If we need to return this data from a method, we should create a
// named class or record containing Name and AnnualSalary.
//
// In this implementation, our method returns void because we only
// consume and print the anonymous objects locally.