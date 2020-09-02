// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.Provider.ISelectionProvider
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Runtime.InteropServices;
using UIAutomationClient;

namespace System.Windows.Automation.Provider {
    [Guid(guid: "fb8b03af-3bdf-48d4-bd36-1a65793be168"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
    public interface ISelectionProvider {
        IRawElementProviderSimple[] GetSelection();

        bool CanSelectMultiple {
            [return: MarshalAs(unmanagedType: UnmanagedType.Bool)]
            get;
        }

        bool IsSelectionRequired {
            [return: MarshalAs(unmanagedType: UnmanagedType.Bool)]
            get;
        }
    }
}