using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SplitPdf.Engine;

namespace SplitPdf.UnitTests
{
  [TestClass]
  public class ArgumentsValidatorTests
  {
    [TestMethod]
    public void Validate_PassingMergeOutputFileWithOnly1InputFile_Should_ThrowException()
    {
      ArgumentValidationException expectedException = null;

      var validator = new ArgumentsValidator();

      try
      {
        var inputFiles = new List<string> {"File1.pdf"};
        validator.Validate(inputFiles, "output.pdf");
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
    public void Validate_PassDuplicateFileNamesWithoutMerge_Should_ThrowException()
    {
      ArgumentValidationException expectedException = null;

      var validator = new ArgumentsValidator();

      try
      {
        var inputFiles = new List<string> {"File1.pdf", "File1.pdf"};
        validator.Validate(inputFiles);
      }
      catch (ArgumentValidationException e)
      {
        expectedException = e;
      }
      Assert.IsInstanceOfType(expectedException, typeof(ArgumentValidationException));
      var expectedMessage = "Each file to split must be unique.\r\n\r\n" +
                            ArgumentsInterpreter.UsageMessage;
      // ReSharper disable once PossibleNullReferenceException
      Assert.AreEqual(expectedMessage, expectedException.Message);
    }

    [TestMethod]
    public void Validate_PassZeroInputFiles_Should_ThrowException()
    {
      ArgumentValidationException expectedException = null;

      var validator = new ArgumentsValidator();

      try
      {
        var inputFiles = new List<string>();
        validator.Validate(inputFiles);
      }
      catch (ArgumentValidationException e)
      {
        expectedException = e;
      }
      Assert.IsInstanceOfType(expectedException, typeof(ArgumentValidationException));
      var expectedMessage = "Please pass at least one input file (two if merging).\r\n\r\n" +
                            ArgumentsInterpreter.UsageMessage;
      // ReSharper disable once PossibleNullReferenceException
      Assert.AreEqual(expectedMessage, expectedException.Message);
    }

    [TestMethod]
    public void Validate_PassNullInputFiles_Should_ThrowException()
    {
      ArgumentValidationException expectedException = null;

      var validator = new ArgumentsValidator();

      try
      {
        validator.Validate(null);
      }
      catch (ArgumentValidationException e)
      {
        expectedException = e;
      }
      Assert.IsInstanceOfType(expectedException, typeof(ArgumentValidationException));
      var expectedMessage = "Please pass at least one input file (two if merging).\r\n\r\n" +
                            ArgumentsInterpreter.UsageMessage;
      // ReSharper disable once PossibleNullReferenceException
      Assert.AreEqual(expectedMessage, expectedException.Message);
    }

    [TestMethod]
    public void ValidateArguments_PassingMergeWithSameOutputFileAsInputFile_Should_ThrowException()
    {
      ArgumentValidationException expectedException = null;

      var interpreter = new ArgumentsValidator();

      try
      {
        var inputFiles = new List<string> {"File1", "File2"};        
        interpreter.Validate(inputFiles, "File1");
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
  }
}
