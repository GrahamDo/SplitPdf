using SplitPdf.UpgradeChecker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SplitPdf.UnitTests
{
  [TestClass]
  public class UpgradeCheckerTests
  {
    [TestMethod]
    public void GetLatestVersionInfo_Should_ReturnValidVersionInfo()
    {
      var checker = new UpgradeRequiredChecker();
      var versionInfo = checker.GetLatestVersionInfoFromUrl(
        "https://raw.githubusercontent.com/GrahamDo/SplitPdf/master/VERSION");

      Assert.IsTrue(versionInfo.VersionMajor > 0, 
        "Failed to get major version number");
      Assert.IsTrue(versionInfo.ReleaseUrl.StartsWith(
          "https://github.com/GrahamDo/SplitPdf/releases/tag/"),
        "Failed to get a valid release URL.");
    }

    [TestMethod]
    public void IsUpgradeRequired_Should_ReturnTrueForVersion1dot0dot0dot0()
    {
      var checker = new UpgradeRequiredChecker();
      var versionInfo = checker.GetLatestVersionInfoFromUrl(
        "https://raw.githubusercontent.com/GrahamDo/SplitPdf/master/VERSION");

      Assert.IsTrue(checker.IsUpgradeRequired(1, 0, 0, 0, versionInfo),
        "Failed to correctly identify that a new version is " + 
        "required.");
    }
  }
}
