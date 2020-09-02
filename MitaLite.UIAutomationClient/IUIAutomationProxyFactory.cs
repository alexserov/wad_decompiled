// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationProxyFactory
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), Guid(guid: "85B94ECD-849D-42B6-B94D-D6DB23FDF5A4"), ComImport]
    public interface IUIAutomationProxyFactory {
        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IRawElementProviderSimple CreateProvider(
            [In] IntPtr hwnd,
            [In] int idObject,
            [In] int idChild);

        [DispId(dispId: 1610678273)]
        string ProxyFactoryId {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
            get;
        }
    }
}