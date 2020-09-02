// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.Provider.IInvokeProvider
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider {
    [Guid(guid: "54fcb24b-e18e-47a2-b4d3-eccbe77599a2"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
    public interface IInvokeProvider {
        void Invoke();
    }
}