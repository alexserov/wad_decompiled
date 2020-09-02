// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.MultipleViewPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class MultipleViewPattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = MultipleViewPatternIdentifiers.Pattern;
    public static readonly AutomationProperty CurrentViewProperty = MultipleViewPatternIdentifiers.CurrentViewProperty;
    public static readonly AutomationProperty SupportedViewsProperty = MultipleViewPatternIdentifiers.SupportedViewsProperty;
    private readonly IUIAutomationMultipleViewPattern _multipleViewPattern;

    private MultipleViewPattern(
      AutomationElement element,
      IUIAutomationMultipleViewPattern multipleViewPattern)
      : base(element)
      => this._multipleViewPattern = multipleViewPattern;

    internal static MultipleViewPattern Wrap(
      AutomationElement element,
      IUIAutomationMultipleViewPattern multipleViewPattern) => new MultipleViewPattern(element, multipleViewPattern);

    public string GetViewName(int viewId) => this._multipleViewPattern.GetViewName(viewId);

    public void SetCurrentView(int viewId) => this._multipleViewPattern.SetCurrentView(viewId);

    public MultipleViewPattern.MultipleViewPatternInformation Cached => new MultipleViewPattern.MultipleViewPatternInformation(this._el, true);

    public MultipleViewPattern.MultipleViewPatternInformation Current => new MultipleViewPattern.MultipleViewPatternInformation(this._el, false);

    public struct MultipleViewPatternInformation
    {
      private AutomationElement _el;
      private bool _useCache;

      internal MultipleViewPatternInformation(AutomationElement el, bool useCache)
      {
        this._el = el;
        this._useCache = useCache;
      }

      public int CurrentView => (int) this._el.GetPatternPropertyValue(MultipleViewPattern.CurrentViewProperty, this._useCache);

      public int[] GetSupportedViews() => (int[]) this._el.GetPatternPropertyValue(MultipleViewPattern.SupportedViewsProperty, this._useCache);
    }
  }
}
