using System;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Collections.Generic;

namespace SplitPdf
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length == 0)
      {
        Console.WriteLine("You must specify the input file as a parameter!");
        return;
      }
      else if (args[0].ToUpper() == "-M")
      {
        if (args.Length < 4)
        {
          //-M Source1 Source2 Target
          Console.WriteLine("You must specify at least two files to merge!");
          return;
        }

        string outputFile = args[args.Length - 1];
        if (File.Exists(outputFile))
        {
          Console.WriteLine(string.Format("{0} already exists! Will not overwrite it.", outputFile));
          return;
        }

        Console.WriteLine(string.Format("Generating output file {0}", outputFile));
        List<string> files = new List<string>();
        foreach (string arg in args)
        {
          if (arg.ToUpper() != "-M" && arg.ToUpper() != outputFile.ToUpper())
            files.Add(arg);
        }
        ConcatenatePdfs(files, outputFile);
      }
      else
      {
        foreach (string file in args)
          if (!File.Exists(file))
          {
            Console.WriteLine("File not found: {0}", args[0]);
            return;
          }

        foreach (string file in args)
          SplitFile(file);
      }
    }

    private static void ConcatenatePdfs(List<string> files, string outputFile)
    {
      PdfDocument outputDocument = new PdfDocument();
      foreach (string file in files)
      {
        Console.WriteLine(string.Format("Processing {0}", file));
        PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);
        int count = inputDocument.PageCount;
        for (int idx = 0; idx < count; idx++)
        {
          PdfPage page = inputDocument.Pages[idx];
          outputDocument.AddPage(page);
        }
      }
      Console.WriteLine(string.Format("Creating output file {0}", outputFile));
      outputDocument.Save(outputFile);
    }

    private static void SplitFile(string sourceFile)
    {
      try
      {
        PdfDocument inputDocument = PdfReader.Open(sourceFile, PdfDocumentOpenMode.Import);
        string destFolder = Path.GetDirectoryName(inputDocument.FullPath);
        string destFileName = Path.GetFileNameWithoutExtension(sourceFile);
        string destFileExtension = Path.GetExtension(sourceFile);
        for (int i = 0; i < inputDocument.PageCount; i++)
        {
          string destFileNameFinal = string.Format("{0}-Page{1}of{2}{3}", destFileName, 
            i + 1, inputDocument.PageCount, destFileExtension);
          Console.WriteLine("Creating file: {0}", destFileNameFinal);
          PdfDocument outputDocument = new PdfDocument { Version = inputDocument.Version };
          outputDocument.AddPage(inputDocument.Pages[i]);
          outputDocument.Save(String.Format("{0}\\{1}", destFolder, destFileNameFinal));
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}