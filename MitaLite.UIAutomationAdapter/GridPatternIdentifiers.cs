// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.GridPatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public static class GridPatternIdentifiers
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<GridPattern, IUIAutomationGridPattern>(10006, "GridPatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationGridPattern, GridPattern>(GridPattern.Wrap));
    public static readonly AutomationProperty ColumnCountProperty = new AutomationProperty(30063, "GridPatternIdentifiers.ColumnCountProperty");
    public static readonly AutomationProperty RowCountProperty = new AutomationProperty(30062, "GridPatternIdentifiers.RowCountProperty");
  }
}
