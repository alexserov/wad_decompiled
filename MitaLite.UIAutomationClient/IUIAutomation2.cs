// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomation2
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [Guid(guid: "34723AFF-0C9D-49D0-9896-7AB52DF8CD8A"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), ComImport]
    public interface IUIAutomation2 : IUIAutomation {
        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new int CompareElements([MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                                IUIAutomationElement el1, [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                                IUIAutomationElement el2);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new int CompareRuntimeIds([MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In]
                                  int[] runtimeId1, [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In]
                                  int[] runtimeId2);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationElement GetRootElement();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationElement ElementFromHandle([In] IntPtr hwnd);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationElement ElementFromPoint([In] tagPOINT pt);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationElement GetFocusedElement();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationElement GetRootElementBuildCache(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationElement ElementFromHandleBuildCache(
            [In] IntPtr hwnd,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationElement ElementFromPointBuildCache(
            [In] tagPOINT pt,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationElement GetFocusedElementBuildCache(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationTreeWalker CreateTreeWalker(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCondition pCondition);

        [DispId(dispId: 1610678283)]
        new IUIAutomationTreeWalker ControlViewWalker {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678284)]
        new IUIAutomationTreeWalker ContentViewWalker {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678285)]
        new IUIAutomationTreeWalker RawViewWalker {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678286)]
        new IUIAutomationCondition RawViewCondition {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678287)]
        new IUIAutomationCondition ControlViewCondition {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678288)]
        new IUIAutomationCondition ContentViewCondition {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationCacheRequest CreateCacheRequest();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationCondition CreateTrueCondition();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationCondition CreateFalseCondition();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationCondition CreatePropertyCondition(
            [In] int propertyId,
            [MarshalAs(unmanagedType: UnmanagedType.Struct), In]
            Variant value);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationCondition CreatePropertyConditionEx(
            [In] int propertyId,
            [MarshalAs(unmanagedType: UnmanagedType.Struct), In]
            Variant value,
            [In] PropertyConditionFlags flags);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationCondition CreateAndCondition(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCondition condition1,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCondition condition2);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationCondition CreateAndConditionFromArray(
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN), In]
            IUIAutomationCondition[] conditions);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationCondition CreateAndConditionFromNativeArray(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            ref IUIAutomationCondition conditions,
            [In] int conditionCount);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationCondition CreateOrCondition(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCondition condition1,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCondition condition2);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationCondition CreateOrConditionFromArray(
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN), In]
            IUIAutomationCondition[] conditions);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationCondition CreateOrConditionFromNativeArray(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            ref IUIAutomationCondition conditions,
            [In] int conditionCount);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationCondition CreateNotCondition(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCondition condition);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void AddAutomationEventHandler(
            [In] int eventId,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [In] TreeScope scope,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void RemoveAutomationEventHandler(
            [In] int eventId,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void AddPropertyChangedEventHandlerNativeArray(
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
        new void AddPropertyChangedEventHandler(
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
        new void RemovePropertyChangedEventHandler(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationPropertyChangedEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void AddStructureChangedEventHandler(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [In] TreeScope scope,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationStructureChangedEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void RemoveStructureChangedEventHandler(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationStructureChangedEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void AddFocusChangedEventHandler(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationFocusChangedEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void RemoveFocusChangedEventHandler([MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                                                IUIAutomationFocusChangedEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void RemoveAllEventHandlers();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT)]
        new int[] IntNativeArrayToSafeArray([In] ref int array, [In] int arrayCount);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new int IntSafeArrayToNativeArray([MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In]
                                          int[] intArray, [Out] IntPtr array);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Struct)]
        new Variant RectToVariant([In] tagRECT rc);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new tagRECT VariantToRect([MarshalAs(unmanagedType: UnmanagedType.Struct), In]
                                  Variant var);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new int SafeArrayToRectNativeArray([MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_R8), In]
                                           double[] rects, [Out] IntPtr rectArray);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationProxyFactoryEntry CreateProxyFactoryEntry(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationProxyFactory factory);

        [DispId(dispId: 1610678317)]
        new IUIAutomationProxyFactoryMapping ProxyFactoryMapping {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
        new string GetPropertyProgrammaticName([In] int property);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
        new string GetPatternProgrammaticName([In] int pattern);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void PollForPotentialSupportedPatterns(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement pElement,
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT)]
            out int[] patternIds,
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
            out string[] patternNames);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void PollForPotentialSupportedProperties(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement pElement,
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT)]
            out int[] propertyIds,
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
            out string[] propertyNames);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new int CheckNotSupported([MarshalAs(unmanagedType: UnmanagedType.Struct), In]
                                  Variant value);

        [DispId(dispId: 1610678323)]
        new object ReservedNotSupportedValue {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.IUnknown)]
            get;
        }

        [DispId(dispId: 1610678324)]
        new object ReservedMixedAttributeValue {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.IUnknown)]
            get;
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationElement ElementFromIAccessible(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IAccessible accessible,
            [In] int childId);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        new IUIAutomationElement ElementFromIAccessibleBuildCache(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IAccessible accessible,
            [In] int childId,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [DispId(dispId: 1610743808)]
        int AutoSetFocus {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In]
            set;
        }

        [DispId(dispId: 1610743810)]
        uint ConnectionTimeout {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In]
            set;
        }

        [DispId(dispId: 1610743812)]
        uint TransactionTimeout {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In]
            set;
        }
    }
}