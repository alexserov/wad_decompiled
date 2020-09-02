// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomation
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [Guid(guid: "30CBE57D-D9D0-452A-AB13-7AC5AC4825EE"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), ComConversionLoss, ComImport]
    public interface IUIAutomation {
        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int CompareElements([MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                            IUIAutomationElement el1, [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                            IUIAutomationElement el2);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int CompareRuntimeIds([MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In]
                              int[] runtimeId1, [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In]
                              int[] runtimeId2);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement GetRootElement();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement ElementFromHandle([In] IntPtr hwnd);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement ElementFromPoint([In] tagPOINT pt);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement GetFocusedElement();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement GetRootElementBuildCache(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement ElementFromHandleBuildCache(
            [In] IntPtr hwnd,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement ElementFromPointBuildCache(
            [In] tagPOINT pt,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement GetFocusedElementBuildCache(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationTreeWalker CreateTreeWalker(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCondition pCondition);

        [DispId(dispId: 1610678283)]
        IUIAutomationTreeWalker ControlViewWalker {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678284)]
        IUIAutomationTreeWalker ContentViewWalker {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678285)]
        IUIAutomationTreeWalker RawViewWalker {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678286)]
        IUIAutomationCondition RawViewCondition {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678287)]
        IUIAutomationCondition ControlViewCondition {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678288)]
        IUIAutomationCondition ContentViewCondition {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationCacheRequest CreateCacheRequest();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationCondition CreateTrueCondition();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationCondition CreateFalseCondition();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationCondition CreatePropertyCondition(
            [In] int propertyId,
            [MarshalAs(unmanagedType: UnmanagedType.Struct), In]
            Variant value);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationCondition CreatePropertyConditionEx(
            [In] int propertyId,
            [MarshalAs(unmanagedType: UnmanagedType.Struct), In]
            Variant value,
            [In] PropertyConditionFlags flags);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationCondition CreateAndCondition(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCondition condition1,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCondition condition2);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationCondition CreateAndConditionFromArray(
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN), In]
            IUIAutomationCondition[] conditions);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationCondition CreateAndConditionFromNativeArray(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            ref IUIAutomationCondition conditions,
            [In] int conditionCount);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationCondition CreateOrCondition(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCondition condition1,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCondition condition2);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationCondition CreateOrConditionFromArray(
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN), In]
            IUIAutomationCondition[] conditions);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationCondition CreateOrConditionFromNativeArray(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            ref IUIAutomationCondition conditions,
            [In] int conditionCount);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationCondition CreateNotCondition([MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                                                  IUIAutomationCondition condition);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddAutomationEventHandler(
            [In] int eventId,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [In] TreeScope scope,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void RemoveAutomationEventHandler(
            [In] int eventId,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddPropertyChangedEventHandlerNativeArray(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [In] TreeScope scope,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationPropertyChangedEventHandler handler,
            [In] ref int propertyArray,
            [In] int propertyCount);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddPropertyChangedEventHandler(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [In] TreeScope scope,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationPropertyChangedEventHandler handler,
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In]
            int[] propertyArray);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void RemovePropertyChangedEventHandler(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationPropertyChangedEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddStructureChangedEventHandler(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [In] TreeScope scope,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationStructureChangedEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void RemoveStructureChangedEventHandler(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationStructureChangedEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddFocusChangedEventHandler(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationFocusChangedEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void RemoveFocusChangedEventHandler([MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                                            IUIAutomationFocusChangedEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void RemoveAllEventHandlers();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT)]
        int[] IntNativeArrayToSafeArray([In] ref int array, [In] int arrayCount);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int IntSafeArrayToNativeArray([MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In]
                                      int[] intArray, [Out] IntPtr array);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Struct)]
        Variant RectToVariant([In] tagRECT rc);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        tagRECT VariantToRect([MarshalAs(unmanagedType: UnmanagedType.Struct), In]
                              Variant var);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int SafeArrayToRectNativeArray([MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_R8), In]
                                       double[] rects, [Out] IntPtr rectArray);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationProxyFactoryEntry CreateProxyFactoryEntry(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationProxyFactory factory);

        [DispId(dispId: 1610678317)]
        IUIAutomationProxyFactoryMapping ProxyFactoryMapping {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
        string GetPropertyProgrammaticName([In] int property);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
        string GetPatternProgrammaticName([In] int pattern);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void PollForPotentialSupportedPatterns(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement pElement,
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT)]
            out int[] patternIds,
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
            out string[] patternNames);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void PollForPotentialSupportedProperties(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement pElement,
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT)]
            out int[] propertyIds,
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
            out string[] propertyNames);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int CheckNotSupported([MarshalAs(unmanagedType: UnmanagedType.Struct), In]
                              Variant value);

        [DispId(dispId: 1610678323)]
        object ReservedNotSupportedValue {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.IUnknown)]
            get;
        }

        [DispId(dispId: 1610678324)]
        object ReservedMixedAttributeValue {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.IUnknown)]
            get;
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement ElementFromIAccessible(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IAccessible accessible,
            [In] int childId);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement ElementFromIAccessibleBuildCache(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IAccessible accessible,
            [In] int childId,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);
    }
}