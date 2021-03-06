﻿// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationDropTargetPattern
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual)]
    [Guid(guid: "69A095F7-EEE4-430E-A46B-FB73B1AE39A5")]
    [ComImport]
    public interface IUIAutomationDropTargetPattern {
        [DispId(dispId: 1610678272)]
        string CurrentDropTargetEffect {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
            get;
        }

        [DispId(dispId: 1610678273)]
        string CachedDropTargetEffect {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
            get;
        }

        [DispId(dispId: 1610678274)]
        string[] CurrentDropTargetEffects {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
            get;
        }

        [DispId(dispId: 1610678275)]
        string[] CachedDropTargetEffects {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
            get;
        }
    }
}