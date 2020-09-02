// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.SelectionPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class SelectionPattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = SelectionPatternIdentifiers.Pattern;
    public static readonly AutomationProperty CanSelectMultipleProperty = SelectionPatternIdentifiers.CanSelectMultipleProperty;
    public static readonly AutomationProperty IsSelectionRequiredProperty = SelectionPatternIdentifiers.IsSelectionRequiredProperty;
    public static readonly AutomationProperty SelectionProperty = SelectionPatternIdentifiers.SelectionProperty;
    public static readonly AutomationEvent InvalidatedEvent = SelectionPatternIdentifiers.InvalidatedEvent;
    private readonly IUIAutomationSelectionPattern _selectionPattern;

    private SelectionPattern(
      AutomationElement element,
      IUIAutomationSelectionPattern selectionPattern)
      : base(element)
      => this._selectionPattern = selectionPattern;

    internal static SelectionPattern Wrap(
      AutomationElement element,
      IUIAutomationSelectionPattern selectionPattern) => new SelectionPattern(element, selectionPattern);

    public SelectionPattern.SelectionPatternInformation Cached => new SelectionPattern.SelectionPatternInformation(this._el, true);

    public SelectionPattern.SelectionPatternInformation Current => new SelectionPattern.SelectionPatternInformation(this._el, false);

    public struct SelectionPatternInformation
    {
      private AutomationElement _el;
      private bool _useCache;

      internal SelectionPatternInformation(AutomationElement el, bool useCache)
      {
        this._el = el;
        this._useCache = useCache;
      }

      public AutomationElement[] GetSelection() => (AutomationElement[]) (AutomationElementCollection) this._el.GetPatternPropertyValue(SelectionPattern.SelectionProperty, this._useCache);

      public bool CanSelectMultiple => (bool) this._el.GetPatternPropertyValue(SelectionPattern.CanSelectMultipleProperty, this._useCache);

      public bool IsSelectionRequired => (bool) this._el.GetPatternPropertyValue(SelectionPattern.IsSelectionRequiredProperty, this._useCache);
    }
  }
}
