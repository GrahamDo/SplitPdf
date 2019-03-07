PdfSplit is a .NET console application for splitting PDF files into multiple files. Pass a PDF file to this app on the command line (or drag a PDF file onto the executable in Windows Explorer), and it will be split into multiple documents, one document per page.

Note: It was developed in Microsoft Visual Studio 2008 (although it currently needs Visual Studio 2017 to build), and targeting the .NET Framework 3.5. This is because the lady who I originally developed it for, to use at work, only has .NET 3.5 on her PC and the company policy didn't permit her to upgrade. Hence it needs to be left targetting that framework version.

Note 2: Although the executable only requires the .NET Framework 3.5 to run, there is a Unit Test project in the solution. In order to execute tests in that project, you require .NET Framework 4.6.1 on your machine. This, of course, shouldn't be a problem if you're running it from within Visual Studio 2017.

<h3>Usage</h3>
<ul><li>Split a single PDF file into many:<br/>
  <code>SplitPdf.exe &lt;File&gt;</code></li>
<li>Split multiple PDF files into many (batching):<br/>
  <code>SplitPdf.exe &lt;File1&gt; &lt;File2&gt; &lt;...&gt; &lt;FileN&gt;</code></li>
<li>Merge multiple PDF files into one (creates &lt;OutputFile&gt; at the end):<br/>
<code>SplitPdf.exe -m &lt;File1&gt; &lt;File2&gt; &lt;...&gt; &lt;FileN&gt; &lt;OutputFile&gt;</code></li></ul>

<h3>Checking for Upgrades</h3>
From version 2.5.6.0 onwards, SplitPdf will automatically check for upgrades every 14 days. You can configure this behaviour by editing SplitPdf.exe.config, and changing the following settings:
<ul><li>DaysBetweenUpgradeCheck (default: 14) - Set this to -1 to disable automatic checking for upgrades.</li>
<li>UpgradeCheckUrl - Don't modify this value, or the application will become unstable.</li>
<li>SecondsDelayAfterFindingUpgrades - If the application finds an upgrade, it pauses to give you time to copy the URL. This is the number of seconds it should wait for.</li></ul>

At any time, you can force a manual upgrade check by executing:
  <code>SplitPdf.exe -uc</code></li>
