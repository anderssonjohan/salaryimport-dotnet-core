using Xunit;
using SalaryImport;
using SalaryImport.Model;
using System;
using System.Linq;

namespace Tests
{
  public class SalaryCalculatorTests
  {
    readonly SalaryCalculator _calculator = new SalaryCalculator();

    [Theory]
    [InlineData(40000)]
    [InlineData(41000)]
    [InlineData(8000)]
    public void AnnualSalary_Type1Component( decimal component1Amount )
    {
      var components = new[] {
        new SalaryComponent { Id = "6789", Type = "3", Amount = 4356 },
        new SalaryComponent { Id = "4567", Type = "2", Amount = 1234 },
        new SalaryComponent { Id = "1234", Type = "1", Amount = component1Amount },
      };
      var result = _calculator.CalculateSalary( "A", components).ToArray();
      var expected = 12 * component1Amount;
      Assert.Equal(expected, result[0].AnnualSalary);
    }

    [Theory]
    [InlineData(40000)]
    [InlineData(41000)]
    [InlineData(8000)]
    public void PensionableSalary_EmployeeTypeA( decimal component1Amount )
    {
      var components = new[] {
        new SalaryComponent { Id = "6789", Type = "3", Amount = 4356 },
        new SalaryComponent { Id = "4567", Type = "2", Amount = 1234 },
        new SalaryComponent { Id = "1234", Type = "1", Amount = component1Amount },
      };
      var result = _calculator.CalculateSalary( "A", components).ToArray();
      var expected = 12.2m * component1Amount;
      Assert.Equal(expected, result[0].PensionableSalary);
    }

    [Theory]
    [InlineData(40000, 8000)]
    [InlineData(41000, 4000)]
    public void PensionableSalary_EmployeeTypeB( decimal component2Amount, decimal component3Amount )
    {
      var components = new[] {
        new SalaryComponent { Id = "6789", Type = "3", Amount = component3Amount },
        new SalaryComponent { Id = "4567", Type = "2", Amount = component2Amount },
        new SalaryComponent { Id = "1234", Type = "1", Amount = 38000 },
      };
      var result = _calculator.CalculateSalary( "B", components).ToArray();
      var expected = 12 * component2Amount + component3Amount;
      Assert.Equal(expected, result[0].PensionableSalary);
    }

    [Fact]
    public void TwoType1ComponentsReturnsTwoSalaries() 
    {
      var date1 = new DateTime(2000, 1, 1);
      var date2 = new DateTime(2001, 1, 1);
      var components = new[] {
        new SalaryComponent { Id = "1234", FromDate = date1, Type = "1", Amount = 38000 },
        new SalaryComponent { Id = "4567", FromDate = date2, Type = "1", Amount = 40000 },
      };
      var result = _calculator.CalculateSalary( "A", components).ToArray();

      Assert.Equal(2, result.Length);
      Assert.Equal(456000, result[0].AnnualSalary);
      Assert.Equal(date1, result[0].FromDate);
      Assert.Equal(480000, result[1].AnnualSalary);
      Assert.Equal(date2, result[1].FromDate);
    }
  }
}
