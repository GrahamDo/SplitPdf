using System;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Collections.Generic;

namespace SplitPdf
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      if (args.Length == 0)
      {
        Console.WriteLine("You must specify the input file as a parameter!");
      }
      else if (args[0].ToUpper() == "-M")
      {
        if (args.Length < 4)
        {
          //-M Source1 Source2 Target
          Console.WriteLine("You must specify at least two files to merge!");
          return;
        }

        var outputFile = args[args.Length - 1];
        if (File.Exists(outputFile))
        {
          Console.WriteLine($"{outputFile} already exists! Will not overwrite it.");
          return;
        }

        var files = new List<string>();
        foreach (var arg in args)
        {
          if (arg.ToUpper() != "-M" && arg.ToUpper() != outputFile.ToUpper())
            files.Add(arg);
        }

        var allFilesFound = true;
        files.ForEach(s => 
        {
          if (File.Exists(s))
              return;

          Console.WriteLine($"File not found: {s}");
          allFilesFound = false;
        });

        if (allFilesFound)
          ConcatenatePdfs(files, outputFile);
      }
      else
      {
        foreach (var file in args)
          if (!File.Exists(file))
          {
            Console.WriteLine($"File not found: {args[0]}");
            return;
          }

        foreach (var file in args)
          SplitFile(file);
      }
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