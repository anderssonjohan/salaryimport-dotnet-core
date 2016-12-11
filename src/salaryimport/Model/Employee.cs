using System;

namespace SalaryImport.Model 
{
  public class Employee 
  {
    public string Type { get; set; }
    public string PersonId { get; set; }
    public string CompanyId { get; set; }
    public DateTime StartDate { get; set; }
    public string[] SalaryComponentIds { get; set; }
    public string Id { get; set; }
  }
}