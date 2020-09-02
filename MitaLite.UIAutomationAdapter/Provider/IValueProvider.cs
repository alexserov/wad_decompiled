// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.Provider.IValueProvider
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
  [Guid("c7935180-6fb3-4201-b174-7df73adbf64a")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IValueProvider
  {
    void SetValue(string value);

    string Value { get; }

    bool IsReadOnly { get; }
  }
}
