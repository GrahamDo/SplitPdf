using System;
using SplitPdf.Engine;

namespace SplitPdf
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var argumentsInterpreter = new ArgumentsInterpreter();
      var runner = new Runner(new ArgumentsValidator());

      try
      {
        argumentsInterpreter.ProcessArguments(args);
        runner.Progress += (sender, e) => Console.WriteLine(e.ProgressMessage);
        runner.Run(argumentsInterpreter.InputFiles, argumentsInterpreter.MergeOutputFile);
      }
      catch (ArgumentValidationException exception)
      {
        Console.WriteLine(exception.Message);
      }
    }
  }
}