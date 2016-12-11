using System;
using System.Collections.Generic;
using System.Linq;
using SalaryImport.Model;

namespace SalaryImport
{
  public class SalaryCalculator
  {
    public IEnumerable<CalculationResult> CalculateSalary(string employeeType, SalaryComponent[] salaryComponents)
    {
      var amountsByType = new Dictionary<string, decimal>{
        { "1", 0 },
        { "2", 0 },
        { "3", 0 }
      };
      var salaryChangesByDate = salaryComponents
        .OrderBy(c => c.FromDate)
        .GroupBy(c => c.FromDate);

      foreach (var dateGroup in salaryChangesByDate)
      {
        UpdateAmountsByType(dateGroup, amountsByType);

        var annualSalary = amountsByType["1"] * 12;
        var pensionableSalary = employeeType == "A" ?
          amountsByType["1"] * 12.2m :
          amountsByType["2"] * 12 + amountsByType["3"];

        yield return new CalculationResult
        {
          FromDate = dateGroup.Key,
          AnnualSalary = annualSalary,
          PensionableSalary = pensionableSalary
        };
      }
    }

    void UpdateAmountsByType( IEnumerable<SalaryComponent> components, IDictionary<string, decimal> amountsByType )
    {
      foreach (var sc in components.Where( c => amountsByType.ContainsKey( c.Type ) ) )
      {
        amountsByType[sc.Type] = sc.Amount;
      }
    }
  }

  public struct CalculationResult
  {
    public DateTime FromDate { get; set; }
    public decimal AnnualSalary;
    public decimal PensionableSalary;
  }
}