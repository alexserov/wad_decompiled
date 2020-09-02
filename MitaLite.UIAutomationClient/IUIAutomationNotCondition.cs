// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationNotCondition
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient
{
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [Guid("F528B657-847B-498C-8896-D52B565407A1")]
  [ComImport]
  public interface IUIAutomationNotCondition : IUIAutomationCondition
  {
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IUIAutomationCondition GetChild();
  }
}
