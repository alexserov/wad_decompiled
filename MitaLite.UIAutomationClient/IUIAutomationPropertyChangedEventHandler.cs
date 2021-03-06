﻿// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationPropertyChangedEventHandler
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [Guid(guid: "40CD37D4-C756-4B0C-8C6F-BDDFEEB13B50"), TypeLibType(flags: TypeLibTypeFlags.FHidden), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), ComImport]
    public interface IUIAutomationPropertyChangedEventHandler {
        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void HandlePropertyChangedEvent([MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                                        IUIAutomationElement sender, [In] int propertyId, [MarshalAs(unmanagedType: UnmanagedType.Struct), In]
                                        Variant newValue);
    }
}