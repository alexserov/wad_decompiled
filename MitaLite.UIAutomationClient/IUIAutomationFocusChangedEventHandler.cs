// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationFocusChangedEventHandler
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), Guid(guid: "C270F6B5-5C69-4290-9745-7A7F97169468"), TypeLibType(flags: TypeLibTypeFlags.FHidden), ComImport]
    public interface IUIAutomationFocusChangedEventHandler {
        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void HandleFocusChangedEvent([MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                                     IUIAutomationElement sender);
    }
}