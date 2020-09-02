// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.CUIAutomationClass
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [ComConversionLoss, Guid(guid: "FF48DBA4-60EF-4201-AA87-54103EEF594E"), ClassInterface(classInterfaceType: ClassInterfaceType.AutoDispatch), TypeLibType(flags: TypeLibTypeFlags.FHidden), ComImport]
    public class CUIAutomationClass : IUIAutomation, CUIAutomation {
        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern int CompareElements([MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                                                  IUIAutomationElement el1, [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                                                  IUIAutomationElement el2);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern int CompareRuntimeIds([MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In]
                                                    int[] runtimeId1, [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In]
                                                    int[] runtimeId2);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationElement GetRootElement();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationElement ElementFromHandle([In] IntPtr hwnd);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationElement ElementFromPoint([In] tagPOINT pt);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationElement GetFocusedElement();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationElement GetRootElementBuildCache(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationElement ElementFromHandleBuildCache(
            [In] IntPtr hwnd,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationElement ElementFromPointBuildCache(
            [In] tagPOINT pt,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationElement GetFocusedElementBuildCache(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationTreeWalker CreateTreeWalker(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCondition pCondition);

        [DispId(dispId: 1610678283)]
        public virtual extern IUIAutomationTreeWalker ControlViewWalker {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678284)]
        public virtual extern IUIAutomationTreeWalker ContentViewWalker {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678285)]
        public virtual extern IUIAutomationTreeWalker RawViewWalker {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678286)]
        public virtual extern IUIAutomationCondition RawViewCondition {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678287)]
        public virtual extern IUIAutomationCondition ControlViewCondition {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678288)]
        public virtual extern IUIAutomationCondition ContentViewCondition {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationCacheRequest CreateCacheRequest();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationCondition CreateTrueCondition();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationCondition CreateFalseCondition();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationCondition CreatePropertyCondition(
            [In] int propertyId,
            [MarshalAs(unmanagedType: UnmanagedType.Struct), In]
            Variant value);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationCondition CreatePropertyConditionEx(
            [In] int propertyId,
            [MarshalAs(unmanagedType: UnmanagedType.Struct), In]
            Variant value,
            [In] PropertyConditionFlags flags);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationCondition CreateAndCondition(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCondition condition1,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCondition condition2);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationCondition CreateAndConditionFromArray(
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN), In]
            IUIAutomationCondition[] conditions);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationCondition CreateAndConditionFromNativeArray(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            ref IUIAutomationCondition conditions,
            [In] int conditionCount);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationCondition CreateOrCondition(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCondition condition1,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCondition condition2);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationCondition CreateOrConditionFromArray(
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN), In]
            IUIAutomationCondition[] conditions);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationCondition CreateOrConditionFromNativeArray(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            ref IUIAutomationCondition conditions,
            [In] int conditionCount);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationCondition CreateNotCondition(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCondition condition);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void AddAutomationEventHandler(
            [In] int eventId,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [In] TreeScope scope,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void RemoveAutomationEventHandler(
            [In] int eventId,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void AddPropertyChangedEventHandlerNativeArray(
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
        public virtual extern void AddPropertyChangedEventHandler(
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
        public virtual extern void RemovePropertyChangedEventHandler(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationPropertyChangedEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void AddStructureChangedEventHandler(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [In] TreeScope scope,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationStructureChangedEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void RemoveStructureChangedEventHandler(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationStructureChangedEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void AddFocusChangedEventHandler(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationFocusChangedEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void RemoveFocusChangedEventHandler(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationFocusChangedEventHandler handler);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void RemoveAllEventHandlers();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT)]
        public virtual extern int[] IntNativeArrayToSafeArray([In] ref int array, [In] int arrayCount);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern int IntSafeArrayToNativeArray([MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In]
                                                            int[] intArray, [Out] IntPtr array);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Struct)]
        public virtual extern Variant RectToVariant([In] tagRECT rc);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern tagRECT VariantToRect([MarshalAs(unmanagedType: UnmanagedType.Struct), In]
                                                    Variant var);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern int SafeArrayToRectNativeArray([MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_R8), In]
                                                             double[] rects, [Out] IntPtr rectArray);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationProxyFactoryEntry CreateProxyFactoryEntry(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationProxyFactory factory);

        [DispId(dispId: 1610678317)]
        public virtual extern IUIAutomationProxyFactoryMapping ProxyFactoryMapping {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
        public virtual extern string GetPropertyProgrammaticName([In] int property);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
        public virtual extern string GetPatternProgrammaticName([In] int pattern);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void PollForPotentialSupportedPatterns(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement pElement,
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT)]
            out int[] patternIds,
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
            out string[] patternNames);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void PollForPotentialSupportedProperties(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement pElement,
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT)]
            out int[] propertyIds,
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
            out string[] propertyNames);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern int CheckNotSupported([MarshalAs(unmanagedType: UnmanagedType.Struct), In]
                                                    Variant value);

        [DispId(dispId: 1610678323)]
        public virtual extern object ReservedNotSupportedValue {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.IUnknown)]
            get;
        }

        [DispId(dispId: 1610678324)]
        public virtual extern object ReservedMixedAttributeValue {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.IUnknown)]
            get;
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationElement ElementFromIAccessible(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IAccessible accessible,
            [In] int childId);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        public virtual extern IUIAutomationElement ElementFromIAccessibleBuildCache(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IAccessible accessible,
            [In] int childId,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);
    }
}