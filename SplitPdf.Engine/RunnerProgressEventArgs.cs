using System;

namespace SplitPdf.Engine
{
  public class RunnerProgressEventArgs : EventArgs
  {
    public string ProgressMessage { get; set; }
  }
}
