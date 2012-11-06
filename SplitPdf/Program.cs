using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace SplitPdf
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length != 1)
        Console.WriteLine("You must specify the input file as a parameter!");
      else if (!File.Exists(args[0]))
        Console.WriteLine("File not found: {0}", args[0]);
      else
        ProcessFile(args[0]);
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
          PdfDocument outputDocument = new PdfDocument();
          outputDocument.Version = inputDocument.Version;
          outputDocument.Info.Title = string.Format("Page {0} of {1}", i + 1, inputDocument.Info.Title);
          outputDocument.Info.Creator = inputDocument.Info.Creator;
          outputDocument.AddPage(inputDocument.Pages[i]);
          outputDocument.Save(destFolder + "\\" + destFileNameFinal);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}
