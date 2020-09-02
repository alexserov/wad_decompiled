// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ClientSettings
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Reflection;

namespace System.Windows.Automation
{
  public static class ClientSettings
  {
    public static void RegisterClientSideProviderAssembly(AssemblyName assemblyName) => throw new NotImplementedException();

    public static void RegisterClientSideProviders(
      ClientSideProviderDescription[] clientSideProviderDescription)
    {
      foreach (ClientSideProviderDescription proxyDescription in clientSideProviderDescription)
        new AdapterProxyFactory(proxyDescription).Register();
    }
  }
}
