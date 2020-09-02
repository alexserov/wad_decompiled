// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationTreeWalker
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [Guid(guid: "4042C624-389C-4AFC-A630-9DF854A541FC"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), ComImport]
    public interface IUIAutomationTreeWalker {
        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement GetParentElement([MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                                              IUIAutomationElement element);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement GetFirstChildElement([MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                                                  IUIAutomationElement element);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement GetLastChildElement([MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                                                 IUIAutomationElement element);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement GetNextSiblingElement([MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                                                   IUIAutomationElement element);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement GetPreviousSiblingElement([MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                                                       IUIAutomationElement element);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement NormalizeElement([MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                                              IUIAutomationElement element);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement GetParentElementBuildCache(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement GetFirstChildElementBuildCache(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement GetLastChildElementBuildCache(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement GetNextSiblingElementBuildCache(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement GetPreviousSiblingElementBuildCache(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement NormalizeElementBuildCache(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement element,
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationCacheRequest cacheRequest);

        [DispId(dispId: 1610678284)]
        IUIAutomationCondition condition {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }
    }
}