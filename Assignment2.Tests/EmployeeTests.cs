using Employees;

namespace Assignment2.Tests;

public class EmployeePayrollTests
{
    [Fact]
    public void FullTime_CalculateSalary_ReturnsSalaryPlusBonus()
    {
        //Arrange
        var employee = new Employees.FullTime("John", "HR", 5000, 1000);

        //Act
        double result = employee.CalculateSalary();

        //Assert
        Assert.Equal(6000, result);
    }

    [Fact]
    public void FullTime_CalculateTax_Returns20Percent()
    {
        //Arrange
        var employee = new Employees.FullTime("John", "HR", 5000, 1000);

        //Act
        double result = employee.CalculateTax();

        //Assert
        Assert.Equal(1200, result);
    }

    [Fact]
    public void PartTime_CalculateSalary_ReturnsHoursTimesRate()
    {
        //Arrange
        var employee = new Employees.PartTime("Jane", "IT", 20, 15);

        //Act
        double result = employee.CalculateSalary();

        //Assert

        Assert.Equal(300, result);
    }

    [Fact]
    public void Contractor_CalculateSalary_ReturnsContractAmount()
    {
        // Arrange
        var employee = new Contractor("Charlie", "Finance", 15000);

        // Act
        double result = employee.CalculateSalary();

        // Assert
        Assert.Equal(15000, result);
    }

    [Fact]
    public void EmployeeList_CalculateTotalPayroll_ReturnsCorrectTotal()
    {
        // Arrange
        List<Employees.Employee> employees = new()
            {
                new Employees.FullTime("Alice", "HR", 5000, 1000),
                new Employees.PartTime("Bob", "IT", 40, 300),
                new Employees.Contractor("Charlie", "Finance", 15000)
            };

        // Act
        double totalPayroll =
            employees.Sum(e => e.CalculateSalary());

        // Assert
        Assert.Equal(33000, totalPayroll);
    }

    [Fact]
    public void EmployeeList_GroupByDepartment_ReturnsCorrectGroups()
    {
        //Arrange
        List<Employees.Employee> employees = new()
            {
                new Employees.FullTime("Alice", "HR", 5000, 1000),
                new Employees.FullTime("Alan", "HR", 4000, 1000),
                new Employees.PartTime("Bob", "IT", 10, 300),
                new Employees.Contractor("Charlie", "IT", 5000)
            };

        //Act
        var groups = employees.GroupBy(e => e.Department).ToList();

        //Assert
        Assert.Equal(2, groups.Count);

        var hrGroup = groups.Single(g => g.Key == "HR");
        var itGroup = groups.Single(g => g.Key == "IT");

        Assert.Equal(2, hrGroup.Count());
        Assert.Equal(2, itGroup.Count());

        Assert.Equal(11000,hrGroup.Sum(e=>e.CalculateSalary()));
        Assert.Equal(8000, itGroup.Sum(e => e.CalculateSalary()));
    }

    [Fact]
    public void Employee_Constructor_InitializesNameAndDepartment()
    {
        //Arrange
        var employee = new Employees.FullTime("sidharth", "IT", 5000, 1000);

        //Assert 
        Assert.Equal("sidharth", employee.Name);
        Assert.Equal("IT", employee.Department);


    }

}
