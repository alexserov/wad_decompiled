﻿// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationSpreadsheetPattern
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), Guid(guid: "7517A7C8-FAAE-4DE9-9F08-29B91E8595C1"), ComImport]
    public interface IUIAutomationSpreadsheetPattern {
        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
        IUIAutomationElement GetItemByName([MarshalAs(unmanagedType: UnmanagedType.BStr), In]
                                           string name);
    }
}