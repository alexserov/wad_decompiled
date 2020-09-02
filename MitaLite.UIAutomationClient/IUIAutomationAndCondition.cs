// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationAndCondition
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), ComConversionLoss, Guid(guid: "A7D0AF36-B912-45FE-9855-091DDC174AEC"), ComImport]
    public interface IUIAutomationAndCondition : IUIAutomationCondition {
        [DispId(dispId: 1610743808)]
        int ChildCount {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetChildrenAsNativeArray([Out] IntPtr childArray, out int childArrayCount);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)]
        IUIAutomationCondition[] GetChildren();
    }
}