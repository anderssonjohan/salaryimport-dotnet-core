namespace SalaryImport.Model 
{
  public class SalaryComponentImportItem
  {
    public string EmployeeId { get; set; }
    public SalaryComponent[] SalaryComponents { get; set; }
  }
}