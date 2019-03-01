using System.Collections.Generic;

namespace SplitPdf.Engine
{
  // Note: This class can be unit tested separately, but in practice will not be used that way. 
  // It will be injected into the runner, and the runner will validate automatically. This is
  // in preparation for a future enhancement, where we will have both a GUI and command line version
  // of the application.
  public class ArgumentsValidator
  {
    public void Validate(List<string> inputFiles, string outputPdf)
    {
      if (!string.IsNullOrEmpty(outputPdf) && inputFiles.Count < 2)
        ArgumentValidationException.ThrowWithUsageMessage(
          "Merge requires at least two input files and an output file.");
    }
  }
}
