using System;
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
      Assert.AreEqual(ArgumentsInterpreter.UsageMessage, expectedException.Message);
    }
  }
}
