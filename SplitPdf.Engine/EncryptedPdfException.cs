using System;

namespace SplitPdf.Engine
{
  public class EncryptedPdfException : Exception
  {
    public string FileName { get; set; }

    public EncryptedPdfException(string fileName)
      : base($"The PDF document, \"{fileName}\", was encrypted using an algorithm not yet " +
              "supported by this program.\r\n" +
              "\r\n" +
              "Open the file and \"PDF Print\" it. Then try this operation again.")
    {
      FileName = fileName;
    }
  }
}
