using System;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Collections.Generic;
using SplitPdf.Engine;

namespace SplitPdf
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var argumentsInterpreter = new ArgumentsInterpreter();
      try
      {
        argumentsInterpreter.ProcessArguments(args);
      }
      catch (ArgumentValidationException exception)
      {
        Console.WriteLine(exception.Message);
        return;
      }

      if (!CheckInputFilesExist(argumentsInterpreter.InputFiles))
        return;

      if (argumentsInterpreter.IsMergeEnabled)
      {
        if (CheckOutputFileDoesNotExist(argumentsInterpreter.MergeOutputFile))
          ConcatenatePdfs(argumentsInterpreter.InputFiles, argumentsInterpreter.MergeOutputFile);
      }
      else
        foreach (var file in argumentsInterpreter.InputFiles)
          SplitFile(file);

    }

    private static bool CheckOutputFileDoesNotExist(string outputFile)
    {
      if (!File.Exists(outputFile))
        return true;

      Console.WriteLine($"The output file you specified, {outputFile}, already exists.");
      return false;

    }

    private static bool CheckInputFilesExist(IEnumerable<string> inputFiles)
    {
      foreach (var file in inputFiles)
      {
        if (File.Exists(file))
          continue;

        Console.WriteLine($"File not found: {file}");
        return false;
      }

      return true;
    }

    private static void ConcatenatePdfs(List<string> files, string outputFile)
    {
      var outputDocument = new PdfDocument();
      files.ForEach(file =>
      {
        Console.WriteLine($"Processing {file}");
        var inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);
        var count = inputDocument.PageCount;
        for (var idx = 0; idx < count; idx++)
        {
          var page = inputDocument.Pages[idx];
          outputDocument.AddPage(page);
        }
      });
      Console.WriteLine($"Creating output file {outputFile}");
      outputDocument.Save(outputFile);
    }

    private static void SplitFile(string sourceFile)
    {
      try
      {
        var inputDocument = PdfReader.Open(sourceFile, PdfDocumentOpenMode.Import);
        var destFolder = Path.GetDirectoryName(inputDocument.FullPath);
        var destFileName = Path.GetFileNameWithoutExtension(sourceFile);
        var destFileExtension = Path.GetExtension(sourceFile);
        for (var i = 0; i < inputDocument.PageCount; i++)
        {
          var destFileNameFinal = $"{destFileName}-Page{i + 1}of{inputDocument.PageCount}{destFileExtension}";
          Console.WriteLine("Creating file: {0}", destFileNameFinal);
          var outputDocument = new PdfDocument { Version = inputDocument.Version };
          outputDocument.AddPage(inputDocument.Pages[i]);
          outputDocument.Save($"{destFolder}\\{destFileNameFinal}");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}