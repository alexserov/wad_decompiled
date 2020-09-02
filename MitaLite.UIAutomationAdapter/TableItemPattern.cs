// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TableItemPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class TableItemPattern : GridItemPattern
  {
    public new static readonly AutomationPattern Pattern = TableItemPatternIdentifiers.Pattern;
    public static readonly AutomationProperty ColumnHeaderItemsProperty = TableItemPatternIdentifiers.ColumnHeaderItemsProperty;
    public static readonly AutomationProperty RowHeaderItemsProperty = TableItemPatternIdentifiers.RowHeaderItemsProperty;
    private readonly IUIAutomationTableItemPattern _tableItemPattern;

    private TableItemPattern(
      AutomationElement element,
      IUIAutomationTableItemPattern tableItemPattern)
      : base(element, (object) tableItemPattern)
      => this._tableItemPattern = tableItemPattern;

    internal static TableItemPattern Wrap(
      AutomationElement element,
      IUIAutomationTableItemPattern pattern) => new TableItemPattern(element, pattern);

    public TableItemPattern.TableItemPatternInformation Cached => new TableItemPattern.TableItemPatternInformation(this._el, true);

    public TableItemPattern.TableItemPatternInformation Current => new TableItemPattern.TableItemPatternInformation(this._el, false);

    public struct TableItemPatternInformation
    {
      private AutomationElement _el;
      private bool _useCache;

      internal TableItemPatternInformation(AutomationElement el, bool useCache)
      {
        this._el = el;
        this._useCache = useCache;
      }

      public int Row => (int) this._el.GetPatternPropertyValue(GridItemPattern.RowProperty, this._useCache);

      public int Column => (int) this._el.GetPatternPropertyValue(GridItemPattern.ColumnProperty, this._useCache);

      public int RowSpan => (int) this._el.GetPatternPropertyValue(GridItemPattern.RowSpanProperty, this._useCache);

      public int ColumnSpan => (int) this._el.GetPatternPropertyValue(GridItemPattern.ColumnSpanProperty, this._useCache);

      public AutomationElement ContainingGrid => (AutomationElement) this._el.GetPatternPropertyValue(GridItemPattern.ContainingGridProperty, this._useCache);

      public AutomationElement[] GetRowHeaderItems() => (AutomationElement[]) (AutomationElementCollection) this._el.GetPatternPropertyValue(TableItemPattern.RowHeaderItemsProperty, this._useCache);

      public AutomationElement[] GetColumnHeaderItems() => (AutomationElement[]) (AutomationElementCollection) this._el.GetPatternPropertyValue(TableItemPattern.ColumnHeaderItemsProperty, this._useCache);
    }
  }
}
