// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.GridItemPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class GridItemPattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = GridItemPatternIdentifiers.Pattern;
    public static readonly AutomationProperty RowProperty = GridItemPatternIdentifiers.RowProperty;
    public static readonly AutomationProperty RowSpanProperty = GridItemPatternIdentifiers.RowSpanProperty;
    public static readonly AutomationProperty ColumnProperty = GridItemPatternIdentifiers.ColumnProperty;
    public static readonly AutomationProperty ColumnSpanProperty = GridItemPatternIdentifiers.ColumnSpanProperty;
    public static readonly AutomationProperty ContainingGridProperty = GridItemPatternIdentifiers.ContainingGridProperty;
    private readonly IUIAutomationGridItemPattern _gridItemPattern;

    internal GridItemPattern(AutomationElement element, object gridItemPattern)
      : base(element)
      => this._gridItemPattern = (IUIAutomationGridItemPattern) gridItemPattern;

    internal static GridItemPattern Wrap(
      AutomationElement element,
      IUIAutomationGridItemPattern gridItemPattern) => new GridItemPattern(element, (object) gridItemPattern);

    public GridItemPattern.GridItemPatternInformation Cached => new GridItemPattern.GridItemPatternInformation(this._el, true);

    public GridItemPattern.GridItemPatternInformation Current => new GridItemPattern.GridItemPatternInformation(this._el, false);

    public struct GridItemPatternInformation
    {
      private AutomationElement _el;
      private bool _useCache;

      internal GridItemPatternInformation(AutomationElement el, bool useCache)
      {
        this._el = el;
        this._useCache = useCache;
      }

      public int Row => (int) this._el.GetPatternPropertyValue(GridItemPattern.RowProperty, this._useCache);

      public int Column => (int) this._el.GetPatternPropertyValue(GridItemPattern.ColumnProperty, this._useCache);

      public int RowSpan => (int) this._el.GetPatternPropertyValue(GridItemPattern.RowSpanProperty, this._useCache);

      public int ColumnSpan => (int) this._el.GetPatternPropertyValue(GridItemPattern.ColumnSpanProperty, this._useCache);

      public AutomationElement ContainingGrid => (AutomationElement) this._el.GetPatternPropertyValue(GridItemPattern.ContainingGridProperty, this._useCache);
    }
  }
}
