using System;

namespace SalaryImport.Model 
{
  public class SalaryComponent 
  {
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime FromDate { get; set; }
    public string Id { get; set; }
  }
}