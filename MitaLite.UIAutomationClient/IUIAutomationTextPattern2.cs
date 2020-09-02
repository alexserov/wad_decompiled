// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationTextPattern2
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [Guid(guid: "506A921A-FCC9-409F-B23B-37EB74106872"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), ComImport]
    public interface IUIAutomationTextPattern2 : IUIAutomationTextPattern {
        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationTextRange RangeFromPoint([In] tagPOINT pt);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationTextRange RangeFromChild([MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                                                  IUIAutomationElement child);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationTextRangeArray GetSelection();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationTextRangeArray GetVisibleRanges();

        [DispId(dispId: 1610678276)]
        new IUIAutomationTextRange DocumentRange {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678277)]
        new SupportedTextSelection SupportedTextSelection {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationTextRange RangeFromAnnotation([MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                                                   IUIAutomationElement annotation);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationTextRange GetCaretRange(out int isActive);
    }
}