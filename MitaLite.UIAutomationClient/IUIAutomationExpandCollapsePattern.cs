// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationExpandCollapsePattern
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), Guid(guid: "619BE086-1F4E-4EE4-BAFA-210128738730"), ComImport]
    public interface IUIAutomationExpandCollapsePattern {
        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Expand();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Collapse();

        [DispId(dispId: 1610678274)]
        ExpandCollapseState CurrentExpandCollapseState {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(dispId: 1610678275)]
        ExpandCollapseState CachedExpandCollapseState {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }
    }
}