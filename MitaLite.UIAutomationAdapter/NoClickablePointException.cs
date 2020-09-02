// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.NoClickablePointException
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation
{
  public class NoClickablePointException : Exception
  {
    public NoClickablePointException()
    {
    }

    public NoClickablePointException(Exception innerException)
      : base(nameof (NoClickablePointException), innerException)
    {
    }

    public NoClickablePointException(string message)
      : base(message)
    {
    }

    public NoClickablePointException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
