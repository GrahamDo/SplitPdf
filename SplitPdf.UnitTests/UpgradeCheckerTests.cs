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

    [TestMethod]
    public void IsUpgradeRequired_Should_ReturnFalseForReallyHighMajorVersionNumber()
    {
      var checker = new UpgradeRequiredChecker();
      var versionInfo = checker.GetLatestVersionInfoFromUrl(
        "https://raw.githubusercontent.com/GrahamDo/SplitPdf/master/VERSION");

      Assert.IsFalse(checker.IsUpgradeRequired(uint.MaxValue,
          versionInfo.VersionMinor, versionInfo.VersionBuild,
          versionInfo.VersionRevision, versionInfo),
        "Failed to correctly identify that a new version is " +
        "NOT required.");
    }

    [TestMethod]
    public void IsUpgradeRequired_Should_ReturnFalseForReallyHighMinorVersionNumber()
    {
      var checker = new UpgradeRequiredChecker();
      var versionInfo = checker.GetLatestVersionInfoFromUrl(
        "https://raw.githubusercontent.com/GrahamDo/SplitPdf/master/VERSION");

      Assert.IsFalse(checker.IsUpgradeRequired(versionInfo.VersionMajor,
          uint.MaxValue, versionInfo.VersionBuild,
          versionInfo.VersionRevision, versionInfo),
        "Failed to correctly identify that a new version is " +
        "NOT required.");
    }

    [TestMethod]
    public void IsUpgradeRequired_Should_ReturnFalseForReallyHighBuildVersionNumber()
    {
      var checker = new UpgradeRequiredChecker();
      var versionInfo = checker.GetLatestVersionInfoFromUrl(
        "https://raw.githubusercontent.com/GrahamDo/SplitPdf/master/VERSION");

      Assert.IsFalse(checker.IsUpgradeRequired(versionInfo.VersionMajor,
          versionInfo.VersionMinor, uint.MaxValue,
          versionInfo.VersionRevision, versionInfo),
        "Failed to correctly identify that a new version is " +
        "NOT required.");
    }

    [TestMethod]
    public void IsUpgradeRequired_Should_ReturnFalseForReallyHighRevisionVersionNumber()
    {
      var checker = new UpgradeRequiredChecker();
      var versionInfo = checker.GetLatestVersionInfoFromUrl(
        "https://raw.githubusercontent.com/GrahamDo/SplitPdf/master/VERSION");

      Assert.IsFalse(checker.IsUpgradeRequired(versionInfo.VersionMajor,
          versionInfo.VersionMinor, versionInfo.VersionBuild,
          uint.MaxValue, versionInfo),
        "Failed to correctly identify that a new version is " +
        "NOT required.");
    }

    [TestMethod]
    public void IsUpgradeRequired_Should_ReturnTrueForLowerMajorVersionNumber()
    {
      var checker = new UpgradeRequiredChecker();
      var versionInfo = checker.GetLatestVersionInfoFromUrl(
        "https://raw.githubusercontent.com/GrahamDo/SplitPdf/master/VERSION");

      Assert.IsTrue(checker.IsUpgradeRequired(
          versionInfo.VersionMajor - 1,
          versionInfo.VersionMinor, versionInfo.VersionBuild,
          versionInfo.VersionRevision, versionInfo),
        "Failed to correctly identify that a new version is " +
        "required.");
    }

    [TestMethod]
    public void IsUpgradeRequired_Should_ReturnTrueForLowerMinorVersionNumber()
    {
      var checker = new UpgradeRequiredChecker();
      var versionInfo = checker.GetLatestVersionInfoFromUrl(
        "https://raw.githubusercontent.com/GrahamDo/SplitPdf/master/VERSION");

      Assert.IsTrue(checker.IsUpgradeRequired(versionInfo.VersionMajor,
          versionInfo.VersionMinor - 1,
          versionInfo.VersionBuild, versionInfo.VersionRevision,
          versionInfo),
        "Failed to correctly identify that a new version is " +
        "required.");
    }

    [TestMethod]
    public void IsUpgradeRequired_Should_ReturnTrueForLowerBuildVersionNumber()
    {
      var checker = new UpgradeRequiredChecker();
      var versionInfo = checker.GetLatestVersionInfoFromUrl(
        "https://raw.githubusercontent.com/GrahamDo/SplitPdf/master/VERSION");

      Assert.IsTrue(checker.IsUpgradeRequired(versionInfo.VersionMajor,
          versionInfo.VersionMinor,          versionInfo.VersionBuild - 1,
          versionInfo.VersionRevision, versionInfo),
        "Failed to correctly identify that a new version is " +
        "required.");
    }
  }
}
