using System;
using System.Reflection;
using System.Threading;
using SplitPdf.Engine;
using SplitPdf.UpgradeChecker;

namespace SplitPdf
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var argumentsInterpreter = new ArgumentsInterpreter();
      var runner = new Runner(new ArgumentsValidator());

      try
      {
        argumentsInterpreter.ProcessArguments(args);
        var doUpgradeCheck = 
          argumentsInterpreter.IsUpgradeCheckRequested;

        var upgradeChecker = new UpgradeRequiredChecker();
        if (!doUpgradeCheck)
        {
          // If it's time for a check, but it wasn't specifically 
          // requested by the user, we should still run (and validate
          // args) first.
          runner.Progress += (sender, e) => Console.WriteLine(e.ProgressMessage);
          runner.Run(argumentsInterpreter.InputFiles, argumentsInterpreter.MergeOutputFile);

          if (Settings.Default.DaysBetweenUpgradeCheck == -1)
            return;

          var lastChecked = upgradeChecker.GetLastChecked();
          doUpgradeCheck =
            DateTime.Now.Subtract(lastChecked).TotalDays >= Settings.Default.DaysBetweenUpgradeCheck;
        }

        if (doUpgradeCheck)
          CheckForUpgrades(upgradeChecker);
      }
      catch (ArgumentValidationException exception)
      {
        Console.WriteLine(exception.Message);
      }
    }

    private static void CheckForUpgrades(
      UpgradeRequiredChecker upgradeChecker)
    {
      Console.WriteLine();
      Console.WriteLine("Checking for upgrades...");
      var versionInfo = upgradeChecker.GetLatestVersionInfoFromUrl(Settings.Default.UpgradeCheckUrl);
      var currentVersion = Assembly.GetExecutingAssembly().GetName()
        .Version;
      var isRequired = upgradeChecker.IsUpgradeRequired(currentVersion.Major, currentVersion.Minor,
        currentVersion.Build, currentVersion.Revision, versionInfo);
      if (!isRequired)
      {
        upgradeChecker.SetDateLastChecked();
        // We only want to STOP checking, if there are no upgrades
        // available. Otherwise, keep bugging the user until they 
        // upgrade.

        Console.WriteLine("The application is up to date.");
        return;
      }

      Console.WriteLine("There is a new version available:");
      Console.WriteLine($"\tYour version: {currentVersion}");
      Console.WriteLine("\tLatest version: " + 
                        versionInfo.VersionString);
      Console.WriteLine("\tFor more info, visit: " + 
                        versionInfo.ReleaseUrl);
      Console.WriteLine();
      Console.WriteLine("Left unattended, the application will terminate after " + 
                        $"{Settings.Default.SecondsDelayAfterFindingUpgrades} seconds...");
      Thread.Sleep(Settings.Default.SecondsDelayAfterFindingUpgrades * 1000);
    }
  }
}