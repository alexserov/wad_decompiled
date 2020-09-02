// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ElementNotAvailableException
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation
{
  public class ElementNotAvailableException : Exception
  {
    public ElementNotAvailableException()
      : base("ElementNotAvailable")
      => this.HResult = -2147220991;

    public ElementNotAvailableException(Exception innerException)
      : base("ElementNotAvailable", innerException)
      => this.HResult = -2147220991;

    public ElementNotAvailableException(string message)
      : base(message)
      => this.HResult = -2147220991;

    public ElementNotAvailableException(string message, Exception innerException)
      : base(message, innerException)
      => this.HResult = -2147220991;
  }
}
