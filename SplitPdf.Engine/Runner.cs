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
      foreach (var file in inputFiles)
        DoSplit(file);
    }

    private void DoSplit(string file)
    {
      var inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);
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
  }
}
