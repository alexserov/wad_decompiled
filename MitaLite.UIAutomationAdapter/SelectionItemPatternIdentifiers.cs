// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.SelectionItemPatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public static class SelectionItemPatternIdentifiers {
        public static readonly AutomationPattern Pattern = new AutomationPattern<SelectionItemPattern, IUIAutomationSelectionItemPattern>(id: 10010, programmaticName: "SelectionItemPatternIdentifiers.Pattern", wrap: SelectionItemPattern.Wrap);
        public static readonly AutomationEvent ElementAddedToSelectionEvent = new AutomationEvent(id: 20010, programmaticName: "SelectionItemPatternIdentifiers.ElementAddedToSelectionEvent");
        public static readonly AutomationEvent ElementRemovedFromSelectionEvent = new AutomationEvent(id: 20011, programmaticName: "SelectionItemPatternIdentifiers.ElementRemovedFromSelectionEvent");
        public static readonly AutomationEvent ElementSelectedEvent = new AutomationEvent(id: 20012, programmaticName: "SelectionItemPatternIdentifiers.ElementSelectedEvent");
        public static readonly AutomationProperty IsSelectedProperty = new AutomationProperty(id: 30079, programmaticName: "SelectionItemPatternIdentifiers.IsSelectedProperty");
        public static readonly AutomationProperty SelectionContainerProperty = new AutomationProperty(id: 30080, programmaticName: "SelectionItemPatternIdentifiers.SelectionContainerProperty");
    }
}