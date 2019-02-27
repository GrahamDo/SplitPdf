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

    public void ProcessArguments(string[] arguments)
    {
      if (arguments == null || arguments.Length == 0)
        throw new ArgumentValidationException(UsageMessage);
    }
  }
}
