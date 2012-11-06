using System;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

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
      else
      {
        foreach (string file in args)
          if (!File.Exists(file))
          {
            Console.WriteLine("File not found: {0}", args[0]);
            return;
          }
      }

      foreach (string file in args)
        ProcessFile(file);
    }

    private static void ProcessFile(string sourceFile)
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