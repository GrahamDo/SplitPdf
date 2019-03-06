namespace SplitPdf.UpgradeChecker
{
  public class LatestVersionInfo
  {
    public uint VersionMajor { get; }
    public uint VersionMinor { get; }
    public uint VersionBuild { get; }
    public uint VersionRevision { get; }
    public string ReleaseUrl { get; }

    public LatestVersionInfo(uint major, uint minor, uint build, 
      uint revision, string releaseUrl)
    {
      VersionMajor = major;
      VersionMinor = minor;
      VersionBuild = build;
      VersionRevision = revision;
      ReleaseUrl = releaseUrl;
    }
  }
}
