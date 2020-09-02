// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.NativeMethods
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Runtime.InteropServices;
using System.Security;
using UIAutomationClient;

namespace System.Windows.Automation {
    internal static class NativeMethods {
        internal const uint EVENT_MIN = 1;
        internal const uint EVENT_MAX = 2147483647;

        [SecurityCritical, DllImport(dllName: "UIAutomationCore.dll")]
        internal static extern bool UiaClientsAreListening();

        [SecurityCritical, DllImport(dllName: "UIAutomationCore.dll")]
        internal static extern int UiaHostProviderFromHwnd(
            IntPtr hwnd,
            [MarshalAs(unmanagedType: UnmanagedType.Interface)]
            out IRawElementProviderSimple provider);

        [SecurityCritical, DllImport(dllName: "UIAutomationCore.dll")]
        internal static extern int UiaRaiseAsyncContentLoadedEvent(
            [MarshalAs(unmanagedType: UnmanagedType.Interface)]
            IRawElementProviderSimple provider,
            AsyncContentLoadedState asyncContentLoadedState,
            double PercentComplete);

        [SecurityCritical, DllImport(dllName: "UIAutomationCore.dll")]
        internal static extern int UiaRaiseAutomationEvent([MarshalAs(unmanagedType: UnmanagedType.Interface)]
                                                           IRawElementProviderSimple provider, int id);

        [SecurityCritical, DllImport(dllName: "UIAutomationCore.dll")]
        internal static extern int UiaRaiseAutomationPropertyChangedEvent(
            [MarshalAs(unmanagedType: UnmanagedType.Interface)]
            IRawElementProviderSimple provider,
            int id,
            object oldValue,
            object newValue);

        [SecurityCritical, DllImport(dllName: "UIAutomationCore.dll")]
        public static extern int UiaRaiseStructureChangedEvent(
            [MarshalAs(unmanagedType: UnmanagedType.Interface)]
            IRawElementProviderSimple provider,
            StructureChangeType structureChangeType,
            int[] runtimeId,
            int runtimeIdLen);

        [SecurityCritical, DllImport(dllName: "UIAutomationCore.dll")]
        internal static extern IntPtr UiaReturnRawElementProvider(
            IntPtr hwnd,
            IntPtr wParam,
            IntPtr lParam,
            [MarshalAs(unmanagedType: UnmanagedType.Interface)]
            IRawElementProviderSimple el);

        [SecurityCritical, DllImport(dllName: "UIAutomationCore.dll")]
        public static extern int UiaGetReservedNotSupportedValue([MarshalAs(unmanagedType: UnmanagedType.IUnknown)]
                                                                 out object punkNotSupportedValue);
    }
}