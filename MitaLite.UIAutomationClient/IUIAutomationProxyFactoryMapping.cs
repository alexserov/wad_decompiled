// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationProxyFactoryMapping
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [Guid(guid: "09E31E18-872D-4873-93D1-1E541EC133FD"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), ComImport]
    public interface IUIAutomationProxyFactoryMapping {
        [DispId(dispId: 1610678272)]
        uint count {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)]
        IUIAutomationProxyFactoryEntry[] GetTable();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationProxyFactoryEntry GetEntry([In] uint index);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetTable([MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN), In]
                      IUIAutomationProxyFactoryEntry[] factoryList);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void InsertEntries([In] uint before, [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN), In]
                           IUIAutomationProxyFactoryEntry[] factoryList);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void InsertEntry([In] uint before, [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                         IUIAutomationProxyFactoryEntry factory);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void RemoveEntry([In] uint index);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void ClearTable();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void RestoreDefaultTable();
    }
}