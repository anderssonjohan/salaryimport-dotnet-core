using Xunit;
using SalaryImport;
using SalaryImport.Model;
using System;
using System.Linq;

namespace Tests
{
  public class SalaryComponentImporterTests
  {
    readonly DataContext _dataContext;
    readonly SalaryComponentImporter _importer;

    public SalaryComponentImporterTests()
    {
      _dataContext = DataContext.FromDataSeed(new DataSeed());
      _importer = new SalaryComponentImporter(_dataContext);
    }

    [Fact]
    public void CanDoImportThatReplacesCurrentSalary() 
    {
      var newFromDate = new DateTime(2014, 1, 1);
      var importData = new SalaryComponentImport
      {
        Items = new[] {
          new SalaryComponentImportItem {
            EmployeeId = TestData.EmployeeIdForAnna,
            SalaryComponents = new[] {
              new SalaryComponent {
                Id = "1234",
                FromDate = newFromDate,
                Type = "1",
                Amount = 10000
              }
            }
          }
        }
      };

      _importer.ImportData(importData);

      var employeeData = _dataContext
        .GetEmployeesWithSalaryComponents()
        .FirstOrDefault( e => e.Name == "Anna" );
      var type1Component = employeeData?
        .SalaryComponents
        .Where(sc => sc.Type == "1")
        .SingleOrDefault();

      Assert.NotNull(type1Component);
      Assert.Equal(10000, type1Component.Amount);
      Assert.Equal(newFromDate, type1Component.FromDate);
    }

    [Fact]
    public void UnknownComponentTypesAreIgnored() 
    {
      var importData = new SalaryComponentImport
      {
        Items = new[] {
          new SalaryComponentImportItem {
            EmployeeId = TestData.EmployeeIdForAnna,
            SalaryComponents = new[] {
              new SalaryComponent {
                Id = "bonus1234",
                FromDate = new DateTime(2014, 1, 1),
                Type = "bonus",
                Amount = 10000
              }
            }
          }
        }
      };

      _importer.ImportData(importData);

      var bonusComponent = _dataContext
        .GetEmployeesWithSalaryComponents()
        .SelectMany( e => e.SalaryComponents )
        .FirstOrDefault( sc => sc.Type == "bonus" );

      Assert.Null(bonusComponent);
    }
  }
}
