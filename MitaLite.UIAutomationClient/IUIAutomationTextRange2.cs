// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationTextRange2
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient
{
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [Guid("BB9B40E0-5E04-46BD-9BE0-4B601B9AFAD4")]
  [ComImport]
  public interface IUIAutomationTextRange2 : IUIAutomationTextRange
  {
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    new IUIAutomationTextRange Clone();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    new int Compare([MarshalAs(UnmanagedType.Interface), In] IUIAutomationTextRange range);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    new int CompareEndpoints(
      [In] TextPatternRangeEndpoint srcEndPoint,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationTextRange range,
      [In] TextPatternRangeEndpoint targetEndPoint);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    new void ExpandToEnclosingUnit([In] TextUnit TextUnit);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    new IUIAutomationTextRange FindAttribute(
      [In] int attr,
      [MarshalAs(UnmanagedType.Struct), In] Variant val,
      [In] int backward);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    new IUIAutomationTextRange FindText(
      [MarshalAs(UnmanagedType.BStr), In] string text,
      [In] int backward,
      [In] int ignoreCase);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Struct)]
    new Variant GetAttributeValue([In] int attr);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_R8)]
    new double[] GetBoundingRectangles();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    new IUIAutomationElement GetEnclosingElement();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.BStr)]
    new string GetText([In] int maxLength);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    new int Move([In] TextUnit unit, [In] int count);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    new int MoveEndpointByUnit([In] TextPatternRangeEndpoint endpoint, [In] TextUnit unit, [In] int count);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    new void MoveEndpointByRange(
      [In] TextPatternRangeEndpoint srcEndPoint,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationTextRange range,
      [In] TextPatternRangeEndpoint targetEndPoint);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    new void Select();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    new void AddToSelection();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    new void RemoveFromSelection();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    new void ScrollIntoView([In] int alignToTop);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    new IUIAutomationElementArray GetChildren();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void ShowContextMenu();
  }
}
