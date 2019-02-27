using System;

namespace SplitPdf.Engine
{
  public class ArgumentValidationException : ApplicationException
  {
    public ArgumentValidationException(string message) : base(message) { }
  }
}
