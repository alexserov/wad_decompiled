// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TablePatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public static class TablePatternIdentifiers {
        public static readonly AutomationPattern Pattern = new AutomationPattern<TablePattern, IUIAutomationTablePattern>(id: 10012, programmaticName: "TablePatternIdentifiers.Pattern", wrap: TablePattern.Wrap);
        public static readonly AutomationProperty ColumnHeadersProperty = new AutomationProperty(id: 30082, programmaticName: "TablePatternIdentifiers.ColumnHeadersProperty");
        public static readonly AutomationProperty RowHeadersProperty = new AutomationProperty(id: 30081, programmaticName: "TablePatternIdentifiers.RowHeadersProperty");
        public static readonly AutomationProperty RowOrColumnMajorProperty = new AutomationProperty(id: 30083, programmaticName: "TablePatternIdentifiers.RowOrColumnMajorProperty");
    }
}