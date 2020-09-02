// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IRawElementProviderSimple
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient
{
  [Guid("D6DD68D1-86FD-4332-8666-9ABEDEA2D24C")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [ComImport]
  public interface IRawElementProviderSimple
  {
    [DispId(1610678272)]
    ProviderOptions ProviderOptions { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.IUnknown)]
    object GetPatternProvider([In] int patternId);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Struct)]
    Variant GetPropertyValue([In] int propertyId);

    [DispId(1610678275)]
    IRawElementProviderSimple HostRawElementProvider { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }
  }
}
