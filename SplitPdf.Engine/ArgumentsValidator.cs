using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SplitPdf.Engine
{
  // Note: This class can be unit tested separately, but in practice will not be used that way. 
  // It will be injected into the runner, and the runner will validate automatically. This is
  // in preparation for a future enhancement, where we will have both a GUI and command line version
  // of the application.
  public class ArgumentsValidator
  {
    public void Validate(List<string> inputFiles)
      => Validate(inputFiles, null);
    public void Validate(List<string> inputFiles, string outputPdf)
    {
      if (inputFiles.Count < 1)
        ArgumentValidationException.ThrowWithUsageMessage(
          "Please pass at least one input file (two if merging).");

      if (!string.IsNullOrEmpty(outputPdf))
      {
        ThrowExceptionIfLessThan2InputFiles(inputFiles);
      }
      else
      {
        ThrowExceptionIfDuplicateInputFiles(inputFiles);
      }
    }

    private static void ThrowExceptionIfDuplicateInputFiles(ICollection<string> inputFiles)
    {
      var distinctList = inputFiles.Distinct();
      if (distinctList.Count() != inputFiles.Count)
        ArgumentValidationException.ThrowWithUsageMessage("Each file to split must be unique.");
    }

    public void ThrowExceptionIfLessThan2InputFiles(ICollection inputFiles)
    {
      if (inputFiles.Count < 2)
        ArgumentValidationException.ThrowWithUsageMessage(
          "Merge requires at least two input files and an output file.");
    }
  }
}
