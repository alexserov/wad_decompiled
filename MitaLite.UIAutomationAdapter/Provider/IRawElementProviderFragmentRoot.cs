// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.Provider.IRawElementProviderFragmentRoot
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Runtime.InteropServices;
using UIAutomationClient;

namespace System.Windows.Automation.Provider {
    [Guid(guid: "620ce2a5-ab8f-40a9-86cb-de3c75599b58"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
    public interface IRawElementProviderFragmentRoot : IRawElementProviderFragment, IRawElementProviderSimple {
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IRawElementProviderFragment ElementProviderFromPoint(
            double x,
            double y);

        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IRawElementProviderFragment GetFocus();
    }
}