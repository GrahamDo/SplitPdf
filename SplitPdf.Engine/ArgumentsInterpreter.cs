using System.Collections.Generic;

namespace SplitPdf.Engine
{
  public class ArgumentsInterpreter
  {
    public const string UsageMessage = "Usage:\r\n" +
                                       "======\r\n" +
                                       "\r\n" +
                                       "Split a single PDF file into many:\r\n" +
                                       "\tSplitPdf.exe <File>\r\n" +
                                       "Split multiple PDF files into many (batching):\r\n" +
                                       "\tSplitPdf.exe <File1> <File2> <...> <FileN>\r\n" +
                                       "Merge multiple PDF files into one (creates <OutputFile> at the end):\r\n" +
                                       "\tSplitPdf.exe -m <File1> <File2> <...> <FileN> <OutputFile>";

    public List<string> InputFiles { get; private set; }
    public bool IsMergeEnabled { get; private set; }
    public string MergeOutputFile { get; private set; }

    public void ProcessArguments(string[] arguments)
    {
      if (arguments == null || arguments.Length == 0)
        ArgumentValidationException.ThrowWithUsageMessage();

      var firstFileNameIndex = 0;
      // ReSharper disable once PossibleNullReferenceException
      if (arguments[0].ToUpper() == "-M")
      {
        if (arguments.Length < 2)
          ArgumentValidationException.ThrowWithUsageMessage("Nothing to merge.");

        IsMergeEnabled = true;
        firstFileNameIndex = 1;
      }

      InputFiles = new List<string>();
      // ReSharper disable once PossibleNullReferenceException
      for (var i = firstFileNameIndex; i < arguments.Length; i++)
      {
        if (IsMergeEnabled && i == arguments.Length - 1)
          // Last argument is the Output File
          MergeOutputFile = arguments[i];
        else
          InputFiles.Add(arguments[i]);
      }

      if (IsMergeEnabled && InputFiles.Contains(MergeOutputFile))
        ArgumentValidationException.ThrowWithUsageMessage("Merge output file cannot be the same as one " +
                                                          "of the input files.");
    }
  }
}
