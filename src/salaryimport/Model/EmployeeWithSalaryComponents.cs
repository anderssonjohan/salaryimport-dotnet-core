namespace SalaryImport.Model 
{
  public class EmployeeWithSalaryComponents 
  {
    public EmployeeWithSalaryComponents( 
      string employeeName, 
      string employeeType, 
      SalaryComponent[] salaryComponents )
    {
      Name = employeeName;
      Type = employeeType;
      SalaryComponents = salaryComponents;
    }

    public string Name { get; private set; }
    public string Type { get; private set; }
    public SalaryComponent[] SalaryComponents { get; private set; }
  }
}