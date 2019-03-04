using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SplitPdf.Engine;

namespace SplitPdf.UnitTests
{
  [TestClass]
  public class RunnerTests
  {
    [TestMethod]
    public void Run_PassPdfFile1_Should_Create4Files()
    {
      var progressMessages = new List<string>();

      var inputFiles = new List<string> {"PDF-File1.pdf"};
      var runner = new Runner(new ArgumentsValidator());
      runner.Progress += (sender, e) => { progressMessages.Add(e.ProgressMessage); };
      runner.Run(inputFiles);

      Assert.AreEqual(4, progressMessages.Count);
      for (var i = 0; i < 4; i++)
      {
        var fileName = $"PDF-File1-Page{i + 1}of4.pdf";
        Assert.AreEqual($"Creating file: {fileName}", progressMessages[i]);
        Assert.IsTrue(File.Exists(fileName), $"{fileName} does not exist.");
        File.Delete(fileName);
      }
    }

    [TestMethod]
    public void Run_PassPdfFile1AndPdfFile2_Should_Create8Files()
    {
      var progressMessages = new List<string>();

      var inputFiles = new List<string> { "PDF-File1.pdf", "PDF-File2.pdf" };
      var runner = new Runner(new ArgumentsValidator());
      runner.Progress += (sender, e) => { progressMessages.Add(e.ProgressMessage); };
      runner.Run(inputFiles);

      Assert.AreEqual(8, progressMessages.Count);
      for (var i = 0; i < 4; i++)
      {
        var fileName = $"PDF-File1-Page{i + 1}of4.pdf";
        Assert.AreEqual($"Creating file: {fileName}", progressMessages[i]);
        Assert.IsTrue(File.Exists(fileName), $"{fileName} does not exist.");
        File.Delete(fileName);
      }

      for (var i = 0; i < 4; i++)
      {
        var fileName = $"PDF-File2-Page{i + 1}of4.pdf";
        Assert.AreEqual($"Creating file: {fileName}", progressMessages[i + 4]);
        Assert.IsTrue(File.Exists(fileName), $"{fileName} does not exist.");
        File.Delete(fileName);
      }
    }
  }
}
