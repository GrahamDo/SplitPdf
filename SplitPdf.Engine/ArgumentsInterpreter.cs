using System.Collections.Generic;
using System.IO;
using System.Net;

namespace SplitPdf.Engine
{
  public class ArgumentsInterpreter
  {
    public const string UsageMessage = "Usage\r\n:" +
                                       "=====\r\n" +
                                       "\r\n" +
                                       "Split a single PDF file into many:\r\n" +
                                       "\tSplitPdf.exe <File>\r\n" +
                                       "Split multiple PDF files into many (batching):\r\n" +
                                       "\tSplitPdf.exe <File1> <File2> <...> <FileN>\r\n" +
                                       "Merge multiple PDF files into one (creates <OutputFile> at the end):\r\n" +
                                       "\tSplitPdf.exe -m <File1> <File2> <...> <FileN> <OutputFile>";

    public List<string> InputFiles { get; private set; }

    public void ProcessArguments(string[] arguments)
      => ProcessArguments(arguments, false);
    public void ProcessArguments(string[] arguments, bool bypassFileExistsCheck)
    {
      if (arguments == null || arguments.Length == 0)
        ArgumentValidationException.ThrowWithUsageMessage();

      InputFiles = new List<string>();
      // ReSharper disable once PossibleNullReferenceException
      foreach (var argument in arguments)
      {
        if (!bypassFileExistsCheck)
          if (!File.Exists(argument))
            ArgumentValidationException.ThrowWithUsageMessage($"File not found: {argument}");

        InputFiles.Add(argument);
      }
    }
  }
}
