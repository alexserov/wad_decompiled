// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.ProviderOptions
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

namespace UIAutomationClient
{
  public enum ProviderOptions
  {
    ProviderOptions_ClientSideProvider = 1,
    ProviderOptions_ServerSideProvider = 2,
    ProviderOptions_NonClientAreaProvider = 4,
    ProviderOptions_OverrideProvider = 8,
    ProviderOptions_ProviderOwnsSetFocus = 16, // 0x00000010
    ProviderOptions_UseComThreading = 32, // 0x00000020
    ProviderOptions_RefuseNonClientSupport = 64, // 0x00000040
    ProviderOptions_HasNativeIAccessible = 128, // 0x00000080
    ProviderOptions_UseClientCoordinates = 256, // 0x00000100
  }
}
