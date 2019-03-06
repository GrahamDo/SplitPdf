namespace SplitPdf.UpgradeChecker
{
  public class LatestVersionInfo
  {
    public int VersionMajor { get; }
    public int VersionMinor { get; }
    public int VersionBuild { get; }
    public int VersionRevision { get; }
    public string VersionString =>
      $"{VersionMajor}.{VersionMinor}.{VersionBuild}.{VersionRevision}";

    public string ReleaseUrl { get; }

    public LatestVersionInfo(int major, int minor, int build, 
      int revision, string releaseUrl)
    {
      VersionMajor = major;
      VersionMinor = minor;
      VersionBuild = build;
      VersionRevision = revision;
      ReleaseUrl = releaseUrl;
    }
  }
}
