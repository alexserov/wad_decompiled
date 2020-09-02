// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationStylesPattern
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), ComConversionLoss, Guid(guid: "85B5F0A2-BD79-484A-AD2B-388C9838D5FB"), ComImport]
    public interface IUIAutomationStylesPattern {
        [DispId(dispId: 1610678272)]
        int CurrentStyleId {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(dispId: 1610678273)]
        string CurrentStyleName {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
            get;
        }

        [DispId(dispId: 1610678274)]
        int CurrentFillColor {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(dispId: 1610678275)]
        string CurrentFillPatternStyle {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
            get;
        }

        [DispId(dispId: 1610678276)]
        string CurrentShape {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
            get;
        }

        [DispId(dispId: 1610678277)]
        int CurrentFillPatternColor {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(dispId: 1610678278)]
        string CurrentExtendedProperties {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
            get;
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetCurrentExtendedPropertiesAsArray([Out] IntPtr propertyArray, out int propertyCount);

        [DispId(dispId: 1610678280)]
        int CachedStyleId {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(dispId: 1610678281)]
        string CachedStyleName {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
            get;
        }

        [DispId(dispId: 1610678282)]
        int CachedFillColor {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(dispId: 1610678283)]
        string CachedFillPatternStyle {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
            get;
        }

        [DispId(dispId: 1610678284)]
        string CachedShape {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
            get;
        }

        [DispId(dispId: 1610678285)]
        int CachedFillPatternColor {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(dispId: 1610678286)]
        string CachedExtendedProperties {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
            get;
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetCachedExtendedPropertiesAsArray([Out] IntPtr propertyArray, out int propertyCount);
    }
}