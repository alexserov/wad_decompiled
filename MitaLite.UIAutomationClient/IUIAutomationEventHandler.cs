// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationEventHandler
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient
{
  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [Guid("146C3C17-F12E-4E22-8C27-F894B9B79C69")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [ComImport]
  public interface IUIAutomationEventHandler
  {
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void HandleAutomationEvent([MarshalAs(UnmanagedType.Interface), In] IUIAutomationElement sender, [In] int eventId);
  }
}
