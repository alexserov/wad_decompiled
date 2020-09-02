// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.SelectionPatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public static class SelectionPatternIdentifiers {
        public static readonly AutomationPattern Pattern = new AutomationPattern<SelectionPattern, IUIAutomationSelectionPattern>(id: 10001, programmaticName: "SelectionPatternIdentifiers.Pattern", wrap: SelectionPattern.Wrap);
        public static readonly AutomationEvent InvalidatedEvent = new AutomationEvent(id: 20013, programmaticName: "SelectionPatternIdentifiers.InvalidatedEvent");
        public static readonly AutomationProperty CanSelectMultipleProperty = new AutomationProperty(id: 30060, programmaticName: "SelectionPatternIdentifiers.CanSelectMultipleProperty");
        public static readonly AutomationProperty IsSelectionRequiredProperty = new AutomationProperty(id: 30061, programmaticName: "SelectionPatternIdentifiers.IsSelectionRequiredProperty");
        public static readonly AutomationProperty SelectionProperty = new AutomationProperty(id: 30059, programmaticName: "SelectionPatternIdentifiers.SelectionProperty");
    }
}