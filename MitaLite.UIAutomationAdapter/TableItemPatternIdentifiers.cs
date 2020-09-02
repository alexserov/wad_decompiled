// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TableItemPatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public static class TableItemPatternIdentifiers
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<TableItemPattern, IUIAutomationTableItemPattern>(10013, "TableItemPatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationTableItemPattern, TableItemPattern>(TableItemPattern.Wrap));
    public static readonly AutomationProperty ColumnHeaderItemsProperty = new AutomationProperty(30085, "TableItemPatternIdentifiers.ColumnHeaderItemsProperty");
    public static readonly AutomationProperty RowHeaderItemsProperty = new AutomationProperty(30084, "TableItemPatternIdentifiers.RowHeaderItemsProperty");
  }
}
