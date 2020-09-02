// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationTreeWalker
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient
{
  [Guid("4042C624-389C-4AFC-A630-9DF854A541FC")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [ComImport]
  public interface IUIAutomationTreeWalker
  {
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement GetParentElement([MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement GetFirstChildElement([MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement GetLastChildElement([MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement GetNextSiblingElement([MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement GetPreviousSiblingElement([MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement NormalizeElement([MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement GetParentElementBuildCache(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement GetFirstChildElementBuildCache(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement GetLastChildElementBuildCache(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement GetNextSiblingElementBuildCache(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement GetPreviousSiblingElementBuildCache(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElement NormalizeElementBuildCache(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement element,
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationCacheRequest cacheRequest);

    [DispId(1610678284)]
    IUIAutomationCondition condition { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }
  }
}
