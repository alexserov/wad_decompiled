// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.StylesPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class StylesPattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<StylesPattern, IUIAutomationStylesPattern>(10025, "StylesPatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationStylesPattern, StylesPattern>(StylesPattern.Wrap));
    public static readonly AutomationProperty StyleIdProperty = new AutomationProperty(30120, "StylesPatternIdentifiers.StyleIdProperty");
    public static readonly AutomationProperty StyleNameProperty = new AutomationProperty(30121, "StylesPatternIdentifiers.StyleNameProperty");
    public static readonly AutomationProperty FillColorProperty = new AutomationProperty(30122, "StylesPatternIdentifiers.FillColorProperty");
    public static readonly AutomationProperty FillPatternStyleProperty = new AutomationProperty(30123, "StylesPatternIdentifiers.FillPatternStyleProperty");
    public static readonly AutomationProperty ShapeProperty = new AutomationProperty(30124, "StylesPatternIdentifiers.ShapeProperty");
    public static readonly AutomationProperty FillPatternColorProperty = new AutomationProperty(30125, "StylesPatternIdentifiers.FillPatternColorProperty");
    public static readonly AutomationProperty ExtendedPropertiesProperty = new AutomationProperty(30126, "StylesPatternIdentifiers.ExtendedPropertiesProperty");
    private readonly IUIAutomationStylesPattern _stylesPattern;

    private StylesPattern(AutomationElement element, IUIAutomationStylesPattern stylesPattern)
      : base(element)
      => this._stylesPattern = stylesPattern;

    internal static StylesPattern Wrap(
      AutomationElement element,
      IUIAutomationStylesPattern stylesPattern) => new StylesPattern(element, stylesPattern);
  }
}
