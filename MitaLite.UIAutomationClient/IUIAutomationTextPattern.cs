// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationTextPattern
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), Guid(guid: "32EBA289-3583-42C9-9C59-3B6D9A1E9B6A"), ComImport]
    public interface IUIAutomationTextPattern {
        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationTextRange RangeFromPoint([In] tagPOINT pt);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationTextRange RangeFromChild([MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                                              IUIAutomationElement child);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationTextRangeArray GetSelection();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationTextRangeArray GetVisibleRanges();

        [DispId(dispId: 1610678276)]
        IUIAutomationTextRange DocumentRange {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678277)]
        SupportedTextSelection SupportedTextSelection {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }
    }
}