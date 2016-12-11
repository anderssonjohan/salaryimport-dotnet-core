using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalaryImport.Model;

namespace SalaryImport
{
  public class SalaryComponentImporter
  {
    readonly DataContext _dataContext;
    Action<string> _onSuccess = _ => { };
    Action<string> _onError = _ => { };

    public SalaryComponentImporter (DataContext dataContext)
    {
      if( null == dataContext )
        throw new ArgumentNullException(nameof(dataContext));
      _dataContext = dataContext;
    }

    public SalaryComponentImporter OnSuccess( Action<string> action )
    {
      _onSuccess = action;
      return this;
    }

    public SalaryComponentImporter OnError( Action<string> action )
    {
      _onError = action;
      return this;
    }

    public bool ImportFromFile( string path )
    {
      try
      {
        ImportData(JsonConvert.DeserializeObject<SalaryComponentImport>(File.ReadAllText(path)));
        _onSuccess($"Successfully imported file '{path}'");
        return true;
      }
      catch( Exception ex )
      {
        _onError($"Failed to import data from file '{path}': {ex.Message}");
      }
      return false;
    }

    public void ImportData(SalaryComponentImport importData) =>
      importData.Items.ToList().ForEach(ImportDataItem);

    static readonly string[] ValidComponentTypes = new[] { "1", "2", "3" };
    
    public void ImportDataItem(SalaryComponentImportItem importDataItem)
    {
      var currentComponents = _dataContext.GetSalaryComponentsByEmployee(importDataItem.EmployeeId);
      foreach (var importComponent in importDataItem.SalaryComponents)
      {
        if( !ValidComponentTypes.Contains(importComponent.Type) )
          continue;

        var toRemove = currentComponents
          .Where(sc => sc.Type == importComponent.Type && sc.FromDate >= importComponent.FromDate);
        foreach (var componentToRemove in toRemove)
        {
          _dataContext.RemoveSalaryComponentFromEmployee(importDataItem.EmployeeId, componentToRemove.Id);
        }
        _dataContext.AddSalaryComponent(importDataItem.EmployeeId, importComponent);
      }
    }
  }
}