// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.Provider.ISelectionItemProvider
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Runtime.InteropServices;
using UIAutomationClient;

namespace System.Windows.Automation.Provider {
    [Guid(guid: "2acad808-b2d4-452d-a407-91ff1ad167b2"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
    public interface ISelectionItemProvider {
        void Select();

        void AddToSelection();

        void RemoveFromSelection();

        bool IsSelected { get; }

        IRawElementProviderSimple SelectionContainer { get; }
    }
}