// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.SpreadsheetItemPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class SpreadsheetItemPattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<SpreadsheetItemPattern, IUIAutomationSpreadsheetItemPattern>(10027, "SpreadsheetItemPatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationSpreadsheetItemPattern, SpreadsheetItemPattern>(SpreadsheetItemPattern.Wrap));
    public static readonly AutomationProperty FormulaProperty = new AutomationProperty(30129, "SpreadsheetItemPatternIdentifiers.FormulaProperty");
    public static readonly AutomationProperty AnnotationObjectsProperty = new AutomationProperty(30130, "SpreadsheetItemPatternIdentifiers.AnnotationObjectsProperty");
    public static readonly AutomationProperty AnnotationTypesProperty = new AutomationProperty(30131, "SpreadsheetItemPatternIdentifiers.AnnotationTypesProperty");
    private readonly IUIAutomationSpreadsheetItemPattern _spreadsheetItemPattern;

    private SpreadsheetItemPattern(
      AutomationElement element,
      IUIAutomationSpreadsheetItemPattern spreadsheetItemPattern)
      : base(element)
      => this._spreadsheetItemPattern = spreadsheetItemPattern;

    internal static SpreadsheetItemPattern Wrap(
      AutomationElement element,
      IUIAutomationSpreadsheetItemPattern spreadsheetItemPattern) => new SpreadsheetItemPattern(element, spreadsheetItemPattern);
  }
}
