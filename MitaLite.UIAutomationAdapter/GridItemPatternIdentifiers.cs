// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.GridItemPatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public static class GridItemPatternIdentifiers
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<GridItemPattern, IUIAutomationGridItemPattern>(10007, "GridItemPatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationGridItemPattern, GridItemPattern>(GridItemPattern.Wrap));
    public static readonly AutomationProperty ColumnProperty = new AutomationProperty(30065, "GridItemPatternIdentifiers.ColumnProperty");
    public static readonly AutomationProperty ColumnSpanProperty = new AutomationProperty(30067, "GridItemPatternIdentifiers.ColumnSpanProperty");
    public static readonly AutomationProperty ContainingGridProperty = new AutomationProperty(30068, "GridItemPatternIdentifiers.ContainingGridProperty");
    public static readonly AutomationProperty RowProperty = new AutomationProperty(30064, "GridItemPatternIdentifiers.RowProperty");
    public static readonly AutomationProperty RowSpanProperty = new AutomationProperty(30066, "GridItemPatternIdentifiers.RowSpanProperty");
  }
}
