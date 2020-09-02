// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ProxyAssemblyNotLoadedException
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation
{
  public class ProxyAssemblyNotLoadedException : Exception
  {
    private static readonly int ElementException = -2147220991;

    public ProxyAssemblyNotLoadedException()
      : base("ElementNotAvailable")
      => this.HResult = ProxyAssemblyNotLoadedException.ElementException;

    public ProxyAssemblyNotLoadedException(Exception innerException)
      : base("ElementNotAvailable", innerException)
      => this.HResult = ProxyAssemblyNotLoadedException.ElementException;

    public ProxyAssemblyNotLoadedException(string message)
      : base(message)
      => this.HResult = ProxyAssemblyNotLoadedException.ElementException;

    public ProxyAssemblyNotLoadedException(string message, Exception innerException)
      : base(message, innerException)
      => this.HResult = ProxyAssemblyNotLoadedException.ElementException;
  }
}
