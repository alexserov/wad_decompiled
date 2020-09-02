// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationStructureChangedEventHandler
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient
{
  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [Guid("E81D1B4E-11C5-42F8-9754-E7036C79F054")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [ComImport]
  public interface IUIAutomationStructureChangedEventHandler
  {
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void HandleStructureChangedEvent(
      [MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement sender,
      [In] StructureChangeType changeType,
      [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_INT), In] int[] runtimeId);
  }
}
