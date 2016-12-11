using System;
using System.IO;
using System.Linq;

namespace SalaryImport
{
  public class Program
  {
    readonly SalaryPrinter _printer;

    public Program() : this(SalaryPrinter.ForConsole()) { }
    public Program(SalaryPrinter printer)
    {
      if (null == printer)
      {
        throw new ArgumentNullException(nameof(printer));
      }
      _printer = printer;
    }

    public static int Main(string[] args)
    {
      var app = new Microsoft.Extensions.CommandLineUtils.CommandLineApplication();
      app.Name = "SalaryImport";
      var files = app.Argument("file", "Files to import", true);
      app.HelpOption("-? | -h | --help");
      app.OnExecute( () => new Program().Run(files.Values.ToArray()) );
      return app.Execute(args);
    }

    public int Run( params string[] filesToImport )
    {
      var dataContext = DataContext.FromDataSeed(new DataSeed());

      if (filesToImport.Length == 0)
      {
        _printer.PrintSalaries(dataContext);
        return 0;
      }

      if (!filesToImport.All(File.Exists))
      {
        Console.Error.WriteLine("Some of the given files could not be found");
        return 1;
      }

      var importer = new SalaryComponentImporter(dataContext);
      importer.OnSuccess( message =>  {
        Console.WriteLine(message);
        _printer.PrintSalaries(dataContext); 
      });
      importer.OnError( message => Console.Error.WriteLine(message) );
      if (!filesToImport.All(filePath => importer.ImportFromFile(filePath)))
      {
        Console.Error.WriteLine("Some of the given files could not be imported");
        return 2;
      }

      return 0;
    }
  }
}
