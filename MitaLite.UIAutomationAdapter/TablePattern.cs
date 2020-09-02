// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TablePattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class TablePattern : GridPattern
  {
    public new static readonly AutomationPattern Pattern = TablePatternIdentifiers.Pattern;
    public static readonly AutomationProperty ColumnHeadersProperty = TablePatternIdentifiers.ColumnHeadersProperty;
    public static readonly AutomationProperty RowHeadersProperty = TablePatternIdentifiers.RowHeadersProperty;
    public static readonly AutomationProperty RowOrColumnMajorProperty = TablePatternIdentifiers.RowOrColumnMajorProperty;
    private readonly IUIAutomationTablePattern _tablePattern;

    private TablePattern(
      AutomationElement element,
      IUIAutomationTablePattern tablePattern,
      IUIAutomationGridPattern gridPattern)
      : base(element, gridPattern)
      => this._tablePattern = tablePattern;

    internal static TablePattern Wrap(
      AutomationElement element,
      IUIAutomationTablePattern tablePattern)
    {
      object obj = (object) null;
      return new TablePattern(element, tablePattern, (IUIAutomationGridPattern) obj);
    }

    public TablePattern.TablePatternInformation Cached => new TablePattern.TablePatternInformation(this._el, true);

    public TablePattern.TablePatternInformation Current => new TablePattern.TablePatternInformation(this._el, false);

    public struct TablePatternInformation
    {
      private AutomationElement _el;
      private bool _useCache;

      internal TablePatternInformation(AutomationElement el, bool useCache)
      {
        this._el = el;
        this._useCache = useCache;
      }

      public AutomationElement[] GetRowHeaders() => (AutomationElement[]) (AutomationElementCollection) this._el.GetPatternPropertyValue(TablePattern.RowHeadersProperty, this._useCache);

      public AutomationElement[] GetColumnHeaders() => (AutomationElement[]) (AutomationElementCollection) this._el.GetPatternPropertyValue(TablePattern.ColumnHeadersProperty, this._useCache);

      public int RowCount => (int) this._el.GetPatternPropertyValue(GridPattern.RowCountProperty, this._useCache);

      public int ColumnCount => (int) this._el.GetPatternPropertyValue(GridPattern.ColumnCountProperty, this._useCache);

      public RowOrColumnMajor RowOrColumnMajor => (RowOrColumnMajor) this._el.GetPatternPropertyValue(TablePattern.RowOrColumnMajorProperty, this._useCache);
    }
  }
}
