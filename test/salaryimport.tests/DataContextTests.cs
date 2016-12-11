using Xunit;
using SalaryImport;
using SalaryImport.Model;
using System;
using System.Linq;

namespace Tests
{
  public class DataContextTests
  {
    readonly DataContext _dataContext;
    readonly EmployeeWithSalaryComponents[] _employeeData;

    public DataContextTests()
    {
      _dataContext = DataContext.FromDataSeed(new DataSeed());
      _employeeData = _dataContext.GetEmployeesWithSalaryComponents();
    }

    [Theory]
    [InlineData("Anna")]
    [InlineData("Bertil")]
    [InlineData("Cecilia")]
    [InlineData("David")]
    [InlineData("Emma")]
    public void CanGetSeededEmployeeData( string employeeName )
    {
      var employeeData = _employeeData.FirstOrDefault( e => e.Name == employeeName );

      Assert.NotNull(employeeData);
    }

    [Fact]
    public void CanGetSalaryComponentsByEmployeeId()
    {
      var components = _dataContext.GetSalaryComponentsByEmployee(TestData.EmployeeIdForAnna);
      Assert.NotNull(components);
      Assert.Equal(32000m, components[0].Amount);
      Assert.Equal(new DateTime(2014,3,1), components[0].FromDate);
      Assert.Equal("1", components[0].Type);
    }

    [Fact]
    public void CanAddSalaryComponentsForEmployee()
    {
      var newComponent = new SalaryComponent { 
        Id = "12345",
        FromDate = new DateTime(2018,1,1),
        Amount = 14000,
        Type = "2" 
      };
      
      _dataContext.AddSalaryComponent(TestData.EmployeeIdForAnna, newComponent);
      
      var added = _dataContext
        .GetSalaryComponentsByEmployee(TestData.EmployeeIdForAnna)
        .SingleOrDefault( sc => sc.Type == "2");
      Assert.NotNull(added);
      Assert.Equal(14000m, added.Amount);
      Assert.Equal(new DateTime(2018,1,1), added.FromDate);
    }

    [Fact]
    public void CanRemoveSalaryComponentsFromEmployee()
    {
      _dataContext.RemoveSalaryComponentFromEmployee(TestData.EmployeeIdForAnna, TestData.SalaryComponentIdForAnna);
      
      var components = _dataContext
        .GetSalaryComponentsByEmployee(TestData.EmployeeIdForAnna)
        .ToArray();
      Assert.NotNull(components);
      Assert.Equal(0, components.Length);
    }
  }
}
