using System;
using System.Net;

namespace SplitPdf.UpgradeChecker
{
    public class UpgradeRequiredChecker
    {
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

          return new LatestVersionInfo(major, minor, build, revision, 
            elements[1]);
        }
    }
    }
}
