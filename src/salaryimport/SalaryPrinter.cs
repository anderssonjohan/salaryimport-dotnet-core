using System;
using System.IO;

namespace SalaryImport
{
  public class SalaryPrinter 
  {
    readonly SalaryCalculator _calculator = new SalaryCalculator();
    readonly TextWriter _outputWriter;

    public static SalaryPrinter ForConsole() =>
      new SalaryPrinter(Console.Out);
    
    public SalaryPrinter( TextWriter outputWriter ) 
    {
      _outputWriter = outputWriter;
    }

    public virtual void PrintSalaries(DataContext dataContext)
    {
      var employees = dataContext.GetEmployeesWithSalaryComponents();
      foreach( var employee in employees )
      {
        var result = _calculator.CalculateSalary(employee.Type, employee.SalaryComponents);
        foreach (var salary in result)
        {
          _outputWriter.WriteLine($"{employee.Name},{salary.AnnualSalary:f0},{salary.PensionableSalary:f0},{salary.FromDate:yyyy-MM-dd}");
        }
      }
    }
  }
}