using System;
using System.Collections.Generic;
using System.Linq;
using SalaryImport.Model;

namespace SalaryImport
{
  public class DataContext 
  {
    IDictionary<string, Person> _persons = new Dictionary<string, Person>();
    IDictionary<string, Company> _companies = new Dictionary<string, Company>();
    IDictionary<string, SalaryComponent> _salaryComponents = new Dictionary<string, SalaryComponent>();
    IDictionary<string, Employee> _employees = new Dictionary<string, Employee>();

    public static DataContext FromDataSeed(DataSeed dataSeed) =>
      new DataContext(dataSeed);

    public DataContext() {}
    DataContext( DataSeed dataSeed ) 
    {
      _persons = dataSeed.Persons.ToDictionary(p => p.Id);
      _companies = dataSeed.Companies.ToDictionary(c => c.Id);
      _salaryComponents = dataSeed.SalaryComponents.ToDictionary(sc => sc.Id);
      _employees = dataSeed.Employees.ToDictionary(e => e.Id);
    }

    public EmployeeWithSalaryComponents[] GetEmployeesWithSalaryComponents() =>
      (
        from employee in _employees.Values
        let salaryComponents = GetSalaryComponentsByEmployee(employee.Id)
        let name = _persons[employee.PersonId].Name
        select new EmployeeWithSalaryComponents( name, employee.Type, salaryComponents )
      ).ToArray();

    void CheckEmployeeIdArgument( string employeeId )
    {
      if (null == employeeId)
        throw new ArgumentNullException(nameof(employeeId));
      if (!_employees.ContainsKey(employeeId))
        throw new ArgumentException("Non-existing employee", nameof(employeeId));
    }

    public SalaryComponent[] GetSalaryComponentsByEmployee(string employeeId)
    {
      CheckEmployeeIdArgument(employeeId);
      return (
        from salaryComponentId in _employees[employeeId].SalaryComponentIds
        select _salaryComponents[salaryComponentId]
      ).ToArray();
    }

    public void AddSalaryComponent( string employeeId, SalaryComponent salaryComponent )
    {
      CheckEmployeeIdArgument(employeeId);
      if (null == salaryComponent)
        throw new ArgumentNullException(nameof(salaryComponent));
      if (_salaryComponents.ContainsKey(salaryComponent.Id))
        throw new ArgumentException("Duplicate salary component ID", nameof(salaryComponent));

      _salaryComponents.Add(salaryComponent.Id, salaryComponent);
      var employee = _employees[employeeId];
      employee.SalaryComponentIds = employee
        .SalaryComponentIds
        .Concat(new[] { salaryComponent.Id })
        .ToArray();
    }

    public void RemoveSalaryComponentFromEmployee( string employeeId, string salaryComponentId )
    {
      CheckEmployeeIdArgument(employeeId);
      if (null == salaryComponentId)
        throw new ArgumentNullException(nameof(salaryComponentId));
      if (!_salaryComponents.ContainsKey(salaryComponentId))
        throw new ArgumentException("Non-existing salary component ID", nameof(salaryComponentId));
      
      var employee = _employees[employeeId];
      employee.SalaryComponentIds = employee
        .SalaryComponentIds
        .Where( id => id != salaryComponentId )
        .ToArray();
    }
  }
}