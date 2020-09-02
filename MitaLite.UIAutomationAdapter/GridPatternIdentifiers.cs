// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.GridPatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public static class GridPatternIdentifiers {
        public static readonly AutomationPattern Pattern = new AutomationPattern<GridPattern, IUIAutomationGridPattern>(id: 10006, programmaticName: "GridPatternIdentifiers.Pattern", wrap: GridPattern.Wrap);
        public static readonly AutomationProperty ColumnCountProperty = new AutomationProperty(id: 30063, programmaticName: "GridPatternIdentifiers.ColumnCountProperty");
        public static readonly AutomationProperty RowCountProperty = new AutomationProperty(id: 30062, programmaticName: "GridPatternIdentifiers.RowCountProperty");
    }
}