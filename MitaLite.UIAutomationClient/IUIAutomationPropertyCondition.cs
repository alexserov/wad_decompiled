// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationPropertyCondition
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient
{
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [Guid("99EBF2CB-5578-4267-9AD4-AFD6EA77E94B")]
  [ComImport]
  public interface IUIAutomationPropertyCondition : IUIAutomationCondition
  {
    [DispId(1610743808)]
    int propertyId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

    [DispId(1610743809)]
    Variant PropertyValue { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }

    [DispId(1610743810)]
    PropertyConditionFlags PropertyConditionFlags { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
  }
}
