// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomation
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient
{
  [Guid("30CBE57D-D9D0-452A-AB13-7AC5AC4825EE")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [ComConversionLoss]
  [ComImport]
  public interface IUIAutomation
  {
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    int CompareElements([MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement el1, [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement el2);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    int CompareRuntimeIds([MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In] int[] runtimeId1, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In] int[] runtimeId2);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement GetRootElement();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement ElementFromHandle([In] IntPtr hwnd);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement ElementFromPoint([In] tagPOINT pt);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement GetFocusedElement();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement GetRootElementBuildCache(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement ElementFromHandleBuildCache(
      [In] IntPtr hwnd,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement ElementFromPointBuildCache(
      [In] tagPOINT pt,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement GetFocusedElementBuildCache(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationTreeWalker CreateTreeWalker(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCondition pCondition);

    [DispId(1610678283)]
    IUIAutomationTreeWalker ControlViewWalker { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [DispId(1610678284)]
    IUIAutomationTreeWalker ContentViewWalker { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [DispId(1610678285)]
    IUIAutomationTreeWalker RawViewWalker { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [DispId(1610678286)]
    IUIAutomationCondition RawViewCondition { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [DispId(1610678287)]
    IUIAutomationCondition ControlViewCondition { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [DispId(1610678288)]
    IUIAutomationCondition ContentViewCondition { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationCacheRequest CreateCacheRequest();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationCondition CreateTrueCondition();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationCondition CreateFalseCondition();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationCondition CreatePropertyCondition(
      [In] int propertyId,
      [MarshalAs(UnmanagedType.Struct), In] Variant value);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationCondition CreatePropertyConditionEx(
      [In] int propertyId,
      [MarshalAs(UnmanagedType.Struct), In] Variant value,
      [In] PropertyConditionFlags flags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationCondition CreateAndCondition(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCondition condition1,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCondition condition2);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationCondition CreateAndConditionFromArray(
      [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN), In] IUIAutomationCondition[] conditions);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationCondition CreateAndConditionFromNativeArray(
      [MarshalAs(UnmanagedType.Interface), In] ref IUIAutomationCondition conditions,
      [In] int conditionCount);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationCondition CreateOrCondition(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCondition condition1,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCondition condition2);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationCondition CreateOrConditionFromArray(
      [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN), In] IUIAutomationCondition[] conditions);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationCondition CreateOrConditionFromNativeArray(
      [MarshalAs(UnmanagedType.Interface), In] ref IUIAutomationCondition conditions,
      [In] int conditionCount);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationCondition CreateNotCondition([MarshalAs(UnmanagedType.Interface), In] IUIAutomationCondition condition);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void AddAutomationEventHandler(
      [In] int eventId,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [In] TreeScope scope,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationEventHandler handler);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void RemoveAutomationEventHandler(
      [In] int eventId,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationEventHandler handler);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void AddPropertyChangedEventHandlerNativeArray(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [In] TreeScope scope,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationPropertyChangedEventHandler handler,
      [In] ref int propertyArray,
      [In] int propertyCount);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void AddPropertyChangedEventHandler(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [In] TreeScope scope,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationPropertyChangedEventHandler handler,
      [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In] int[] propertyArray);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void RemovePropertyChangedEventHandler(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationPropertyChangedEventHandler handler);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void AddStructureChangedEventHandler(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [In] TreeScope scope,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationStructureChangedEventHandler handler);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void RemoveStructureChangedEventHandler(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationStructureChangedEventHandler handler);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void AddFocusChangedEventHandler(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationFocusChangedEventHandler handler);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void RemoveFocusChangedEventHandler([MarshalAs(UnmanagedType.Interface), In] IUIAutomationFocusChangedEventHandler handler);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void RemoveAllEventHandlers();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT)]
    int[] IntNativeArrayToSafeArray([In] ref int array, [In] int arrayCount);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    int IntSafeArrayToNativeArray([MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In] int[] intArray, [Out] IntPtr array);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Struct)]
    Variant RectToVariant([In] tagRECT rc);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    tagRECT VariantToRect([MarshalAs(UnmanagedType.Struct), In] Variant var);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    int SafeArrayToRectNativeArray([MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_R8), In] double[] rects, [Out] IntPtr rectArray);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationProxyFactoryEntry CreateProxyFactoryEntry(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationProxyFactory factory);

    [DispId(1610678317)]
    IUIAutomationProxyFactoryMapping ProxyFactoryMapping { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string GetPropertyProgrammaticName([In] int property);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string GetPatternProgrammaticName([In] int pattern);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void PollForPotentialSupportedPatterns(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement pElement,
      [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT)] out int[] patternIds,
      [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] patternNames);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void PollForPotentialSupportedProperties(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement pElement,
      [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT)] out int[] propertyIds,
      [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] propertyNames);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    int CheckNotSupported([MarshalAs(UnmanagedType.Struct), In] Variant value);

    [DispId(1610678323)]
    object ReservedNotSupportedValue { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.IUnknown)] get; }

    [DispId(1610678324)]
    object ReservedMixedAttributeValue { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.IUnknown)] get; }

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement ElementFromIAccessible(
      [MarshalAs(UnmanagedType.Interface), In] IAccessible accessible,
      [In] int childId);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement ElementFromIAccessibleBuildCache(
      [MarshalAs(UnmanagedType.Interface), In] IAccessible accessible,
      [In] int childId,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest);
  }
}
