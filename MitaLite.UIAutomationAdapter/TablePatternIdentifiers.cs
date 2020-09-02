// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TablePatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public static class TablePatternIdentifiers
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<TablePattern, IUIAutomationTablePattern>(10012, "TablePatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationTablePattern, TablePattern>(TablePattern.Wrap));
    public static readonly AutomationProperty ColumnHeadersProperty = new AutomationProperty(30082, "TablePatternIdentifiers.ColumnHeadersProperty");
    public static readonly AutomationProperty RowHeadersProperty = new AutomationProperty(30081, "TablePatternIdentifiers.RowHeadersProperty");
    public static readonly AutomationProperty RowOrColumnMajorProperty = new AutomationProperty(30083, "TablePatternIdentifiers.RowOrColumnMajorProperty");
  }
}
