// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.SelectionItemPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class SelectionItemPattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = SelectionItemPatternIdentifiers.Pattern;
    public static readonly AutomationProperty SelectionContainerProperty = SelectionItemPatternIdentifiers.SelectionContainerProperty;
    public static readonly AutomationProperty IsSelectedProperty = SelectionItemPatternIdentifiers.IsSelectedProperty;
    public static readonly AutomationEvent ElementAddedToSelectionEvent = SelectionItemPatternIdentifiers.ElementAddedToSelectionEvent;
    public static readonly AutomationEvent ElementRemovedFromSelectionEvent = SelectionItemPatternIdentifiers.ElementRemovedFromSelectionEvent;
    public static readonly AutomationEvent ElementSelectedEvent = SelectionItemPatternIdentifiers.ElementSelectedEvent;
    private readonly IUIAutomationSelectionItemPattern _selectionItemPattern;

    private SelectionItemPattern(
      AutomationElement element,
      IUIAutomationSelectionItemPattern selectionItemPattern)
      : base(element)
      => this._selectionItemPattern = selectionItemPattern;

    internal static SelectionItemPattern Wrap(
      AutomationElement element,
      IUIAutomationSelectionItemPattern selectionItemPattern) => new SelectionItemPattern(element, selectionItemPattern);

    public void Select() => this._selectionItemPattern.Select();

    public void AddToSelection() => this._selectionItemPattern.AddToSelection();

    public void RemoveFromSelection() => this._selectionItemPattern.RemoveFromSelection();

    public SelectionItemPattern.SelectionItemPatternInformation Cached => new SelectionItemPattern.SelectionItemPatternInformation(this._el, true);

    public SelectionItemPattern.SelectionItemPatternInformation Current => new SelectionItemPattern.SelectionItemPatternInformation(this._el, false);

    public struct SelectionItemPatternInformation
    {
      private AutomationElement _el;
      private bool _useCache;

      internal SelectionItemPatternInformation(AutomationElement el, bool useCache)
      {
        this._el = el;
        this._useCache = useCache;
      }

      public bool IsSelected => (bool) this._el.GetPatternPropertyValue(SelectionItemPattern.IsSelectedProperty, this._useCache);

      public AutomationElement SelectionContainer => (AutomationElement) this._el.GetPatternPropertyValue(SelectionItemPattern.SelectionContainerProperty, this._useCache);
    }
  }
}
