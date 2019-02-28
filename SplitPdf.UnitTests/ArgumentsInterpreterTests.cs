using Microsoft.VisualStudio.TestTools.UnitTesting;
using SplitPdf.Engine;

namespace SplitPdf.UnitTests
{
  [TestClass]
  public class ArgumentsInterpreterTests
  {
    [TestMethod]
    public void ProcessArguments_PassingNull_Should_ThrowException()
    {
      ArgumentValidationException expectedException = null;

      var interpreter = new ArgumentsInterpreter();
      try
      {
        interpreter.ProcessArguments(null);
      }
      catch (ArgumentValidationException e)
      {
        expectedException = e;
      }

      Assert.IsInstanceOfType(expectedException, typeof(ArgumentValidationException));
      // ReSharper disable once PossibleNullReferenceException
      Assert.AreEqual(ArgumentsInterpreter.UsageMessage, expectedException.Message);
    }

    [TestMethod]
    public void ProcessArguments_PassingEmptyArray_Should_ThrowException()
    {
      ArgumentValidationException expectedException = null;

      var interpreter = new ArgumentsInterpreter();

      try
      {
        var args = new string[] { };
        interpreter.ProcessArguments(args);
      }
      catch (ArgumentValidationException e)
      {
        expectedException = e;
      }
      Assert.IsInstanceOfType(expectedException, typeof(ArgumentValidationException));
      // ReSharper disable once PossibleNullReferenceException
      Assert.AreEqual(ArgumentsInterpreter.UsageMessage, expectedException.Message);
    }

    [TestMethod]
    public void ProcessArguments_PassingOnlyMergeArgument_Should_ThrowException()
    {
      ArgumentValidationException expectedException = null;

      var interpreter = new ArgumentsInterpreter();

      try
      {
        var args = new [] { "-m" };
        interpreter.ProcessArguments(args);
      }
      catch (ArgumentValidationException e)
      {
        expectedException = e;
      }
      Assert.IsInstanceOfType(expectedException, typeof(ArgumentValidationException));
      var expectedMessage = $"Nothing to merge.\r\n\r\n{ArgumentsInterpreter.UsageMessage}";
      // ReSharper disable once PossibleNullReferenceException
      Assert.AreEqual(expectedMessage, expectedException.Message);
    }

    [TestMethod]
    public void ProcessArguments_PassingMergeWithOnly1File_Should_ThrowException()
    {
      ArgumentValidationException expectedException = null;

      var interpreter = new ArgumentsInterpreter();

      try
      {
        var args = new[] { "-m", "File1" };
        interpreter.ProcessArguments(args);
      }
      catch (ArgumentValidationException e)
      {
        expectedException = e;
      }
      Assert.IsInstanceOfType(expectedException, typeof(ArgumentValidationException));
      var expectedMessage = "Merge requires at least two input files and an output file.\r\n\r\n" + 
                            ArgumentsInterpreter.UsageMessage;
      // ReSharper disable once PossibleNullReferenceException
      Assert.AreEqual(expectedMessage, expectedException.Message);
    }

    [TestMethod]
    public void ProcessArguments_PassingMergeWithOnly2Files_Should_ThrowException()
    {
      ArgumentValidationException expectedException = null;

      var interpreter = new ArgumentsInterpreter();

      try
      {
        var args = new[] { "-m", "File1", "File2" };
        interpreter.ProcessArguments(args);
      }
      catch (ArgumentValidationException e)
      {
        expectedException = e;
      }
      Assert.IsInstanceOfType(expectedException, typeof(ArgumentValidationException));
      var expectedMessage = "Merge requires at least two input files and an output file.\r\n\r\n" +
                            ArgumentsInterpreter.UsageMessage;
      // ReSharper disable once PossibleNullReferenceException
      Assert.AreEqual(expectedMessage, expectedException.Message);
    }

    [TestMethod]
    public void ProcessArguments_PassingMergeWithSameOutputFileAsInputFile_Should_ThrowException()
    {
      ArgumentValidationException expectedException = null;

      var interpreter = new ArgumentsInterpreter();

      try
      {
        var args = new[] { "-m", "File1", "File2", "File1" };
        interpreter.ProcessArguments(args);
      }
      catch (ArgumentValidationException e)
      {
        expectedException = e;
      }
      Assert.IsInstanceOfType(expectedException, typeof(ArgumentValidationException));
      var expectedMessage = "Merge output file cannot be the same as one of the input files.\r\n\r\n" +
                            ArgumentsInterpreter.UsageMessage;
      // ReSharper disable once PossibleNullReferenceException
      Assert.AreEqual(expectedMessage, expectedException.Message);
    }

    [TestMethod]
    public void ProcessArguments_PassingMergeWithOnly3Files_Should_PopulateFileNamesAndOutputFile()
    {
      var interpreter = new ArgumentsInterpreter();
      const string fileName1 = "File1";
      const string fileName2 = "File2";
      const string outputFileName = "Output";
      var args = new[] { "-m", fileName1,fileName2,outputFileName };
      interpreter.ProcessArguments(args);
      Assert.IsTrue(interpreter.IsMergeEnabled);
      Assert.AreEqual(2, interpreter.InputFiles.Count);
      Assert.AreEqual(interpreter.InputFiles[0], fileName1);
      Assert.AreEqual(interpreter.InputFiles[1], fileName2);
      Assert.AreEqual(interpreter.MergeOutputFile, outputFileName);
    }

    [TestMethod]
    public void ProcessArguments_PassSingleFileName_Should_PopulateFiles()
    {
      var interpreter = new ArgumentsInterpreter();
      const string fileName = "C:\\Temp\\File.pdf";
      var args = new[] { fileName };
      interpreter.ProcessArguments(args);
      Assert.AreEqual(1, interpreter.InputFiles.Count);
      Assert.AreEqual(interpreter.InputFiles[0], fileName);
    }

    [TestMethod]
    public void ProcessArguments_PassMultipleFileNames_Should_PopulateFiles()
    {
      var interpreter = new ArgumentsInterpreter();
      const string fileName1 = "C:\\Temp\\File1.pdf";
      const string fileName2 = "C:\\Temp\\File2.pdf";
      const string fileName3 = "C:\\Temp\\File3.pdf";

      var args = new[] { fileName1, fileName2, fileName3 };
      interpreter.ProcessArguments(args);
      Assert.AreEqual(3, interpreter.InputFiles.Count);
      Assert.AreEqual(interpreter.InputFiles[0], fileName1);
      Assert.AreEqual(interpreter.InputFiles[1], fileName2);
      Assert.AreEqual(interpreter.InputFiles[2], fileName3);
    }
  }
}
