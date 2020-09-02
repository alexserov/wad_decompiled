// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationTextChildPattern
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [Guid(guid: "6552B038-AE05-40C8-ABFD-AA08352AAB86"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), ComImport]
    public interface IUIAutomationTextChildPattern {
        [DispId(dispId: 1610678272)]
        IUIAutomationElement TextContainer {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }

        [DispId(dispId: 1610678273)]
        IUIAutomationTextRange TextRange {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Interface)]
            get;
        }
    }
}