using System.IO;
using System.Text;
using Xunit;
using SalaryImport;
using SalaryImport.Model;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
  public class ProgramTests
  {
    readonly TestSalaryPrinter _printer = new TestSalaryPrinter();

    [Theory]
    [InlineData("Anna,384000,390400,2014-03-01")]
    [InlineData("Bertil,480000,488000,2012-05-01")]
    [InlineData("Cecilia,0,360000,2014-07-01")]
    [InlineData("Cecilia,0,364000,2014-09-17")]
    [InlineData("Cecilia,612000,364000,2015-07-01")]
    [InlineData("David,0,8000,2014-03-01")]
    [InlineData("David,336000,8000,2014-03-15")]
    [InlineData("David,336000,452000,2015-08-01")]
    [InlineData("David,336000,488000,2016-03-01")]
    [InlineData("Emma,492000,500200,2011-10-12")]
    public void CanPrintSeededData( string expectedOutput )
    {
      var program = new Program(_printer);
      program.Run();

      Assert.Contains(expectedOutput, _printer.Output);
    }

    [Fact]
    public void CanImportFromFileThatReplacesCurrentSalary() 
    {
      var importData = new SalaryComponentImport 
      {
        Items = new[] { 
          new SalaryComponentImportItem {
            EmployeeId = TestData.EmployeeIdForAnna,
            SalaryComponents = new[] {
              new SalaryComponent {
                Id = "1234",
                FromDate = new DateTime(2014,1,1),
                Type = "1",
                Amount = 10000
              }
            }
          }
        }
      };

      using (var file = CreateImportFile(importData))
      {
        var program = new Program(_printer);
        program.Run(file.Path);
      }

      var notExpectedOutput = "Anna,384000,390400,2014-03-01";
      var expectedOutput = "Anna,120000,122000,2014-01-01";
      Assert.Contains(expectedOutput, _printer.Output);
      Assert.DoesNotContain(notExpectedOutput, _printer.Output);
    }

    [Fact]
    public void CanImportFromMultipleFilesAndPrintAfterEachFile() 
    {
      var importData1 = new SalaryComponentImport 
      {
        Items = new[] { 
          new SalaryComponentImportItem {
            EmployeeId = TestData.EmployeeIdForAnna,
            SalaryComponents = new[] {
              new SalaryComponent {
                Id = "1234",
                FromDate = new DateTime(2014,1,1),
                Type = "1",
                Amount = 20000
              }
            }
          }
        }
      };
      var importData2 = new SalaryComponentImport 
      {
        Items = new[] { 
          new SalaryComponentImportItem {
            EmployeeId = TestData.EmployeeIdForAnna,
            SalaryComponents = new[] {
              new SalaryComponent {
                Id = "5678",
                FromDate = new DateTime(2014,1,1),
                Type = "1",
                Amount = 25000
              }
            }
          }
        }
      };

      using (var file1 = CreateImportFile(importData1))
      using (var file2 = CreateImportFile(importData2))
      {
        var program = new Program(_printer);
        program.Run(file1.Path, file2.Path);
      }

      var expectedOutput1 = "Anna,240000,244000,2014-01-01";
      var expectedOutput2 = "Anna,300000,305000,2014-01-01";
      Assert.Contains(expectedOutput1, _printer.Outputs[0]);
      Assert.Contains(expectedOutput2, _printer.Outputs[1]);
    }

    TempFile CreateImportFile( SalaryComponentImport importData ) 
    {
      var file = new TempFile();
      File.WriteAllText(file.Path, JsonConvert.SerializeObject(importData));
      return file;
    }
  }

  public class TestSalaryPrinter : SalaryPrinter 
  {
    readonly StringBuilder _buffer;
    readonly List<string> _outputs = new List<string>();

    public TestSalaryPrinter() : this( new StringBuilder() ) {}
    TestSalaryPrinter(StringBuilder buffer) : base(new StringWriter(buffer))
    {
      _buffer = buffer;
    }

    public string Output => _outputs.LastOrDefault();
    public string[] Outputs => _outputs.ToArray();

    public override void PrintSalaries(DataContext dataContext)
    {
      base.PrintSalaries(dataContext);
      _outputs.Add(_buffer.ToString());
      _buffer.Clear();
    }
  }

  public class TempFile : IDisposable
  {
    readonly string _path;
    bool _disposed = false;

    public string Path => _path;

    public TempFile() 
    {
      _path = System.IO.Path.GetTempFileName();
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!_disposed)
      {
        File.Delete(_path);
        _disposed = true;
      }
    }

    ~TempFile()
    {
      Dispose(false);
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }
}
