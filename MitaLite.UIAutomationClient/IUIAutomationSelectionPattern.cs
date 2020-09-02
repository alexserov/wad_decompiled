﻿// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationSelectionPattern
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient
{
  [Guid("5ED5202E-B2AC-47A6-B638-4B0BF140D78E")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [ComImport]
  public interface IUIAutomationSelectionPattern
  {
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElementArray GetCurrentSelection();

    [DispId(1610678273)]
    int CurrentCanSelectMultiple { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

    [DispId(1610678274)]
    int CurrentIsSelectionRequired { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationElementArray GetCachedSelection();

    [DispId(1610678276)]
    int CachedCanSelectMultiple { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

    [DispId(1610678277)]
    int CachedIsSelectionRequired { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
  }
}
