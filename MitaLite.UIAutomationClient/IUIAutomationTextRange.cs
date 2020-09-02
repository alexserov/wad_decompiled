// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationTextRange
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [Guid(guid: "A543CC6A-F4AE-494B-8239-C814481187A8"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), ComImport]
    public interface IUIAutomationTextRange {
        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationTextRange Clone();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int Compare([MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                    IUIAutomationTextRange range);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int CompareEndpoints(
            [In] TextPatternRangeEndpoint srcEndPoint,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationTextRange range,
            [In] TextPatternRangeEndpoint targetEndPoint);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void ExpandToEnclosingUnit([In] TextUnit TextUnit);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationTextRange FindAttribute([In] int attr, [MarshalAs(unmanagedType: UnmanagedType.Struct), In]
                                             Variant val, [In] int backward);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationTextRange FindText(
            [MarshalAs(unmanagedType: UnmanagedType.BStr), In]
            string text,
            [In] int backward,
            [In] int ignoreCase);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Struct)]
        Variant GetAttributeValue([In] int attr);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_R8)]
        double[] GetBoundingRectangles();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement GetEnclosingElement();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
        string GetText([In] int maxLength);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int Move([In] TextUnit unit, [In] int count);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int MoveEndpointByUnit([In] TextPatternRangeEndpoint endpoint, [In] TextUnit unit, [In] int count);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void MoveEndpointByRange(
            [In] TextPatternRangeEndpoint srcEndPoint,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationTextRange range,
            [In] TextPatternRangeEndpoint targetEndPoint);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Select();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddToSelection();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void RemoveFromSelection();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void ScrollIntoView([In] int alignToTop);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElementArray GetChildren();
    }
}