// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationProxyFactoryEntry
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [Guid(guid: "D50E472E-B64B-490C-BCA1-D30696F9F289"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), ComImport]
    public interface IUIAutomationProxyFactoryEntry {
        [DispId(dispId: 1610678272)]
        IUIAutomationProxyFactory ProxyFactory {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678273)]
        string ClassName {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
            get;
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(unmanagedType: UnmanagedType.LPWStr), In]
            set;
        }

        [DispId(dispId: 1610678274)]
        string ImageName {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
            get;
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(unmanagedType: UnmanagedType.LPWStr), In]
            set;
        }

        [DispId(dispId: 1610678275)]
        int AllowSubstringMatch {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In]
            set;
        }

        [DispId(dispId: 1610678276)]
        int CanCheckBaseClass {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In]
            set;
        }

        [DispId(dispId: 1610678277)]
        int NeedsAdviseEvents {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In]
            set;
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetWinEventsForAutomationEvent([In] int eventId, [In] int propertyId, [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UINT), In]
                                            uint[] winEvents);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UINT)]
        uint[] GetWinEventsForAutomationEvent([In] int eventId, [In] int propertyId);
    }
}