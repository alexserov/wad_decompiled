// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.NativeMethods
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Runtime.InteropServices;
using System.Security;
using UIAutomationClient;

namespace System.Windows.Automation
{
  internal static class NativeMethods
  {
    internal const uint EVENT_MIN = 1;
    internal const uint EVENT_MAX = 2147483647;

    [SecurityCritical]
    [DllImport("UIAutomationCore.dll")]
    internal static extern bool UiaClientsAreListening();

    [SecurityCritical]
    [DllImport("UIAutomationCore.dll")]
    internal static extern int UiaHostProviderFromHwnd(
      IntPtr hwnd,
      [MarshalAs(UnmanagedType.Interface)] out IRawElementProviderSimple provider);

    [SecurityCritical]
    [DllImport("UIAutomationCore.dll")]
    internal static extern int UiaRaiseAsyncContentLoadedEvent(
      [MarshalAs(UnmanagedType.Interface)] IRawElementProviderSimple provider,
      AsyncContentLoadedState asyncContentLoadedState,
      double PercentComplete);

    [SecurityCritical]
    [DllImport("UIAutomationCore.dll")]
    internal static extern int UiaRaiseAutomationEvent([MarshalAs(UnmanagedType.Interface)] IRawElementProviderSimple provider, int id);

    [SecurityCritical]
    [DllImport("UIAutomationCore.dll")]
    internal static extern int UiaRaiseAutomationPropertyChangedEvent(
      [MarshalAs(UnmanagedType.Interface)] IRawElementProviderSimple provider,
      int id,
      object oldValue,
      object newValue);

    [SecurityCritical]
    [DllImport("UIAutomationCore.dll")]
    public static extern int UiaRaiseStructureChangedEvent(
      [MarshalAs(UnmanagedType.Interface)] IRawElementProviderSimple provider,
      StructureChangeType structureChangeType,
      int[] runtimeId,
      int runtimeIdLen);

    [SecurityCritical]
    [DllImport("UIAutomationCore.dll")]
    internal static extern IntPtr UiaReturnRawElementProvider(
      IntPtr hwnd,
      IntPtr wParam,
      IntPtr lParam,
      [MarshalAs(UnmanagedType.Interface)] IRawElementProviderSimple el);

    [SecurityCritical]
    [DllImport("UIAutomationCore.dll")]
    public static extern int UiaGetReservedNotSupportedValue([MarshalAs(UnmanagedType.IUnknown)] out object punkNotSupportedValue);
  }
}
