// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationElementArray
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), Guid(guid: "14314595-B4BC-4055-95F2-58F2E42C9855"), ComImport]
    public interface IUIAutomationElementArray {
        [DispId(dispId: 1610678272)]
        int Length {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement GetElement([In] int index);
    }
}