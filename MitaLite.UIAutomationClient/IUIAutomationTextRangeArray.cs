// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationTextRangeArray
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient
{
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [Guid("CE4AE76A-E717-4C98-81EA-47371D028EB6")]
  [ComImport]
  public interface IUIAutomationTextRangeArray
  {
    [DispId(1610678272)]
    int Length { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationTextRange GetElement([In] int index);
  }
}
