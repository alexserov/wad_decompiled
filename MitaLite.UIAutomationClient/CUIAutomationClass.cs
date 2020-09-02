// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.CUIAutomationClass
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient
{
  [ComConversionLoss]
  [Guid("FF48DBA4-60EF-4201-AA87-54103EEF594E")]
  [ClassInterface(ClassInterfaceType.AutoDispatch)]
  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [ComImport]
  public class CUIAutomationClass : IUIAutomation, CUIAutomation
  {
    
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern int CompareElements([MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement el1, [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement el2);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern int CompareRuntimeIds([MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In] int[] runtimeId1, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In] int[] runtimeId2);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationElement GetRootElement();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationElement ElementFromHandle([In] IntPtr hwnd);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationElement ElementFromPoint([In] tagPOINT pt);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationElement GetFocusedElement();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationElement GetRootElementBuildCache(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationElement ElementFromHandleBuildCache(
      [In] IntPtr hwnd,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationElement ElementFromPointBuildCache(
      [In] tagPOINT pt,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationElement GetFocusedElementBuildCache(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationTreeWalker CreateTreeWalker(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCondition pCondition);

    [DispId(1610678283)]
    public virtual extern IUIAutomationTreeWalker ControlViewWalker { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [DispId(1610678284)]
    public virtual extern IUIAutomationTreeWalker ContentViewWalker { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [DispId(1610678285)]
    public virtual extern IUIAutomationTreeWalker RawViewWalker { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [DispId(1610678286)]
    public virtual extern IUIAutomationCondition RawViewCondition { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [DispId(1610678287)]
    public virtual extern IUIAutomationCondition ControlViewCondition { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [DispId(1610678288)]
    public virtual extern IUIAutomationCondition ContentViewCondition { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationCacheRequest CreateCacheRequest();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationCondition CreateTrueCondition();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationCondition CreateFalseCondition();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationCondition CreatePropertyCondition(
      [In] int propertyId,
      [MarshalAs(UnmanagedType.Struct), In] Variant value);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationCondition CreatePropertyConditionEx(
      [In] int propertyId,
      [MarshalAs(UnmanagedType.Struct), In] Variant value,
      [In] PropertyConditionFlags flags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationCondition CreateAndCondition(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCondition condition1,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCondition condition2);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationCondition CreateAndConditionFromArray(
      [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN), In] IUIAutomationCondition[] conditions);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationCondition CreateAndConditionFromNativeArray(
      [MarshalAs(UnmanagedType.Interface), In] ref IUIAutomationCondition conditions,
      [In] int conditionCount);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationCondition CreateOrCondition(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCondition condition1,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCondition condition2);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationCondition CreateOrConditionFromArray(
      [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN), In] IUIAutomationCondition[] conditions);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationCondition CreateOrConditionFromNativeArray(
      [MarshalAs(UnmanagedType.Interface), In] ref IUIAutomationCondition conditions,
      [In] int conditionCount);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationCondition CreateNotCondition(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCondition condition);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern void AddAutomationEventHandler(
      [In] int eventId,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [In] TreeScope scope,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationEventHandler handler);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern void RemoveAutomationEventHandler(
      [In] int eventId,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationEventHandler handler);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern void AddPropertyChangedEventHandlerNativeArray(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [In] TreeScope scope,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationPropertyChangedEventHandler handler,
      [In] ref int propertyArray,
      [In] int propertyCount);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern void AddPropertyChangedEventHandler(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [In] TreeScope scope,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationPropertyChangedEventHandler handler,
      [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In] int[] propertyArray);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern void RemovePropertyChangedEventHandler(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationPropertyChangedEventHandler handler);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern void AddStructureChangedEventHandler(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [In] TreeScope scope,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationStructureChangedEventHandler handler);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern void RemoveStructureChangedEventHandler(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationStructureChangedEventHandler handler);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern void AddFocusChangedEventHandler(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationFocusChangedEventHandler handler);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern void RemoveFocusChangedEventHandler(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationFocusChangedEventHandler handler);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern void RemoveAllEventHandlers();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT)]
    public virtual extern int[] IntNativeArrayToSafeArray([In] ref int array, [In] int arrayCount);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern int IntSafeArrayToNativeArray([MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In] int[] intArray, [Out] IntPtr array);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Struct)]
    public virtual extern Variant RectToVariant([In] tagRECT rc);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern tagRECT VariantToRect([MarshalAs(UnmanagedType.Struct), In] Variant var);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern int SafeArrayToRectNativeArray([MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_R8), In] double[] rects, [Out] IntPtr rectArray);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationProxyFactoryEntry CreateProxyFactoryEntry(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationProxyFactory factory);

    [DispId(1610678317)]
    public virtual extern IUIAutomationProxyFactoryMapping ProxyFactoryMapping { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.BStr)]
    public virtual extern string GetPropertyProgrammaticName([In] int property);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.BStr)]
    public virtual extern string GetPatternProgrammaticName([In] int pattern);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern void PollForPotentialSupportedPatterns(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement pElement,
      [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT)] out int[] patternIds,
      [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] patternNames);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern void PollForPotentialSupportedProperties(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement pElement,
      [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT)] out int[] propertyIds,
      [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] propertyNames);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern int CheckNotSupported([MarshalAs(UnmanagedType.Struct), In] Variant value);

    [DispId(1610678323)]
    public virtual extern object ReservedNotSupportedValue { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.IUnknown)] get; }

    [DispId(1610678324)]
    public virtual extern object ReservedMixedAttributeValue { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.IUnknown)] get; }

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationElement ElementFromIAccessible(
      [MarshalAs(UnmanagedType.Interface), In] IAccessible accessible,
      [In] int childId);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public virtual extern IUIAutomationElement ElementFromIAccessibleBuildCache(
      [MarshalAs(UnmanagedType.Interface), In] IAccessible accessible,
      [In] int childId,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest);
  }
}
