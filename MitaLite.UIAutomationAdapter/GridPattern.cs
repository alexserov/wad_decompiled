// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.GridPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class GridPattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = GridPatternIdentifiers.Pattern;
    public static readonly AutomationProperty ColumnCountProperty = GridPatternIdentifiers.ColumnCountProperty;
    public static readonly AutomationProperty RowCountProperty = GridPatternIdentifiers.RowCountProperty;
    private readonly IUIAutomationGridPattern _gridPattern;

    internal GridPattern(AutomationElement element, IUIAutomationGridPattern gridPattern)
      : base(element)
      => this._gridPattern = gridPattern;

    internal static GridPattern Wrap(
      AutomationElement element,
      IUIAutomationGridPattern gridPattern) => new GridPattern(element, gridPattern);

    public AutomationElement GetItem(int row, int column) => new AutomationElement(this._gridPattern.GetItem(row, column));

    public GridPattern.GridPatternInformation Cached => new GridPattern.GridPatternInformation(this._el, true);

    public GridPattern.GridPatternInformation Current => new GridPattern.GridPatternInformation(this._el, false);

    public struct GridPatternInformation
    {
      private AutomationElement _el;
      private bool _useCache;

      internal GridPatternInformation(AutomationElement el, bool useCache)
      {
        this._el = el;
        this._useCache = useCache;
      }

      public int RowCount => (int) this._el.GetPatternPropertyValue(GridPattern.RowCountProperty, this._useCache);

      public int ColumnCount => (int) this._el.GetPatternPropertyValue(GridPattern.ColumnCountProperty, this._useCache);
    }
  }
}
