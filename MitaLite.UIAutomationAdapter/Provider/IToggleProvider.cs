// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.Provider.IToggleProvider
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider {
    [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), Guid(guid: "56d00bd0-c4f4-433c-a836-1a52a57e0892"), ComImport]
    public interface IToggleProvider {
        void Toggle();

        ToggleState ToggleState { get; }
    }
}