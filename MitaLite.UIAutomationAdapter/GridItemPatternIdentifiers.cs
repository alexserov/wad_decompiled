// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.GridItemPatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public static class GridItemPatternIdentifiers {
        public static readonly AutomationPattern Pattern = new AutomationPattern<GridItemPattern, IUIAutomationGridItemPattern>(id: 10007, programmaticName: "GridItemPatternIdentifiers.Pattern", wrap: GridItemPattern.Wrap);
        public static readonly AutomationProperty ColumnProperty = new AutomationProperty(id: 30065, programmaticName: "GridItemPatternIdentifiers.ColumnProperty");
        public static readonly AutomationProperty ColumnSpanProperty = new AutomationProperty(id: 30067, programmaticName: "GridItemPatternIdentifiers.ColumnSpanProperty");
        public static readonly AutomationProperty ContainingGridProperty = new AutomationProperty(id: 30068, programmaticName: "GridItemPatternIdentifiers.ContainingGridProperty");
        public static readonly AutomationProperty RowProperty = new AutomationProperty(id: 30064, programmaticName: "GridItemPatternIdentifiers.RowProperty");
        public static readonly AutomationProperty RowSpanProperty = new AutomationProperty(id: 30066, programmaticName: "GridItemPatternIdentifiers.RowSpanProperty");
    }
}