// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationTextEditTextChangedEventHandler
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [TypeLibType(flags: TypeLibTypeFlags.FHidden), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), Guid(guid: "92FAA680-E704-4156-931A-E32D5BB38F3F"), ComImport]
    public interface IUIAutomationTextEditTextChangedEventHandler {
        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void HandleTextEditTextChangedEvent(
            [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
            IUIAutomationElement sender,
            [In] TextEditChangeType TextEditChangeType,
            [MarshalAs(unmanagedType: UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR), In]
            string[] eventStrings);
    }
}