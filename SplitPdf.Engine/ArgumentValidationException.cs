using System;
using System.Text;

namespace SplitPdf.Engine
{
  public class ArgumentValidationException : ApplicationException
  {
    public ArgumentValidationException(string message) : base(message) { }

    public static void ThrowWithUsageMessage()
      => ThrowWithUsageMessage(null);

    public static void ThrowWithUsageMessage(string message)
    {
      var finalMessage = new StringBuilder(2);
      if (message != null)
      {
        finalMessage.AppendLine(message);
        finalMessage.AppendLine();
      }

      finalMessage.Append(ArgumentsInterpreter.UsageMessage);
      throw new ArgumentValidationException(finalMessage.ToString());
    }
  }
}
