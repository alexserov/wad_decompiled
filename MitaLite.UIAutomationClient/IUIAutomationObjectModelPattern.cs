// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationObjectModelPattern
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [Guid(guid: "71C284B3-C14D-4D14-981E-19751B0D756D")]
    [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual)]
    [ComImport]
    public interface IUIAutomationObjectModelPattern {
        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.IUnknown)]
        object GetUnderlyingObjectModel();
    }
}