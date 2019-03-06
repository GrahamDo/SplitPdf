using System;
using System.IO;
using System.Net;

namespace SplitPdf.UpgradeChecker
{
  public class UpgradeRequiredChecker
  {
    public DateTime GetLastChecked()
    {
      var path = Path.Combine(Environment.GetFolderPath(
        Environment.SpecialFolder.ApplicationData), "GrahamDo");
      if (!Directory.Exists(path))
        return DateTime.MinValue;

      path = Path.Combine(path, "SplitPdf");
      if (!Directory.Exists(path))
        return DateTime.MinValue;

      var fileName = $"{path}{Path.DirectorySeparatorChar}" +
                     "LastUpdateCheck";

      return !File.Exists(fileName) ? DateTime.MinValue : DateTime.Parse(File.ReadAllText(fileName));
    }

    private static void SetDateLastChecked()
    {
      var path = Path.Combine(Environment.GetFolderPath(
        Environment.SpecialFolder.ApplicationData), "GrahamDo");
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);

      path = Path.Combine(path, "SplitPdf");
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);

      var fileName = $"{path}{Path.DirectorySeparatorChar}" +
                     "LastUpdateCheck";
      File.WriteAllText(fileName, 
        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }

    public LatestVersionInfo GetLatestVersionInfoFromUrl(string url)
    {
      using (var client = new WebClient())
      {
        var rawString = client.DownloadString(url);

        var elements = rawString.Split('|');
        var versionElements = elements[0].Split('.');
        var major = Convert.ToUInt32(versionElements[0]);
        var minor = Convert.ToUInt32(versionElements[1]);
        var build = Convert.ToUInt32(versionElements[2]);
        var revision = Convert.ToUInt32(versionElements[3]);

        SetDateLastChecked();
        return new LatestVersionInfo(major, minor, build, revision,
          elements[1]);
      }
    }

    public bool IsUpgradeRequired(uint actualMajor, uint actualMinor, 
      uint actualBuild, uint actualRevision, 
      LatestVersionInfo versionInfo)
    {
      if (versionInfo.VersionMajor > actualMajor)
        return true;
      if (versionInfo.VersionMajor == actualMajor && 
          versionInfo.VersionMinor > actualMinor)
        return true;
      if (versionInfo.VersionMajor == actualMajor &&
          versionInfo.VersionMinor == actualMinor &&
          versionInfo.VersionBuild > actualBuild)
        return true;

      return versionInfo.VersionMajor == actualMajor &&
             versionInfo.VersionMinor == actualMinor &&
             versionInfo.VersionBuild == actualBuild &&
             versionInfo.VersionRevision > actualRevision;
    }
  }
}
