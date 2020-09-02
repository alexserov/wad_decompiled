// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.Provider.IObjectModelProvider
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
  [Guid("3ad86ebd-f5ef-483d-bb18-b1042a475d64")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IObjectModelProvider
  {
    [return: MarshalAs(UnmanagedType.Interface)]
    object GetUnderlyingObjectModel();
  }
}
