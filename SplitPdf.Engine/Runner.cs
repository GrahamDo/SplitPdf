using System.Collections.Generic;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace SplitPdf.Engine
{
  public class Runner
  {
    private readonly ArgumentsValidator _argumentsValidator;

    public Runner(ArgumentsValidator argumentsValidator)
    {
      _argumentsValidator = argumentsValidator;
    }

    public delegate void RunnerProgressHandler(object sender, RunnerProgressEventArgs e);

    public event RunnerProgressHandler Progress;

    public void Run(List<string> inputFiles) => Run(inputFiles, null);
    public void Run(List<string> inputFiles, string mergeOutputFile)
    {
      _argumentsValidator.Validate(inputFiles, mergeOutputFile);

      if (string.IsNullOrEmpty(mergeOutputFile))
        foreach (var file in inputFiles)
          DoSplit(file);
      else
        DoMerge(inputFiles, mergeOutputFile);
    }

    private void DoSplit(string file)
    {
      var inputDocument = OpenPdfFile(file);
      var destFolder = Path.GetDirectoryName(inputDocument.FullPath);
      var destFileName = Path.GetFileNameWithoutExtension(file);
      var destFileExtension = Path.GetExtension(file);
      for (var i = 0; i < inputDocument.PageCount; i++)
      {
        var destFileNameFinal = $"{destFileName}-Page{i + 1}of{inputDocument.PageCount}{destFileExtension}";
        Progress?.Invoke(this, new RunnerProgressEventArgs
        {
          ProgressMessage = $"Creating file: {destFileNameFinal}"
        });
        var outputDocument = new PdfDocument { Version = inputDocument.Version };
        outputDocument.AddPage(inputDocument.Pages[i]);
        outputDocument.Save($"{destFolder}\\{destFileNameFinal}");
      }
    }

    private PdfDocument OpenPdfFile(string file)
    {
      try
      {
        return PdfReader.Open(file, PdfDocumentOpenMode.Import);
      }
      catch (PdfReaderException exception)
      {
        if (exception.Message != "The PDF document is protected with an encryption not " +
                                 "supported by PDFsharp.")
          throw;

        throw new EncryptedPdfException(file);
      }
    }

    private void DoMerge(List<string> inputFiles, string mergeOutputFile)
    {
      var outputDocument = new PdfDocument();
      inputFiles.ForEach(file =>
      {
        Progress?.Invoke(this, new RunnerProgressEventArgs
        {
          ProgressMessage = $"Processing {file}"
        });
        var inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);
        var count = inputDocument.PageCount;
        for (var idx = 0; idx < count; idx++)
        {
          var page = inputDocument.Pages[idx];
          outputDocument.AddPage(page);
        }
      });
      Progress?.Invoke(this, new RunnerProgressEventArgs
      {
        ProgressMessage = $"Creating output file {mergeOutputFile}"
      });
      outputDocument.Save(mergeOutputFile);

    }
  }
}
