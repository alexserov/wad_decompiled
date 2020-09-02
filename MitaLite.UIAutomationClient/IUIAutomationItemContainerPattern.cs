// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationItemContainerPattern
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [Guid(guid: "C690FDB2-27A8-423C-812D-429773C9084E"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), ComImport]
    public interface IUIAutomationItemContainerPattern {
        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement FindItemByProperty(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement pStartAfter,
            [In] int propertyId,
            [MarshalAs(unmanagedType: UnmanagedType.Struct), In]
            Variant value);
    }
}