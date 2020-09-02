// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ScrollPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class ScrollPattern : BasePattern
  {
    public const double NoScroll = -1.0;
    public static readonly AutomationPattern Pattern = ScrollPatternIdentifiers.Pattern;
    public static readonly AutomationProperty HorizontallyScrollableProperty = ScrollPatternIdentifiers.HorizontallyScrollableProperty;
    public static readonly AutomationProperty HorizontalScrollPercentProperty = ScrollPatternIdentifiers.HorizontalScrollPercentProperty;
    public static readonly AutomationProperty HorizontalViewSizeProperty = ScrollPatternIdentifiers.HorizontalViewSizeProperty;
    public static readonly AutomationProperty VerticallyScrollableProperty = ScrollPatternIdentifiers.VerticallyScrollableProperty;
    public static readonly AutomationProperty VerticalScrollPercentProperty = ScrollPatternIdentifiers.VerticalScrollPercentProperty;
    public static readonly AutomationProperty VerticalViewSizeProperty = ScrollPatternIdentifiers.VerticalViewSizeProperty;
    private readonly IUIAutomationScrollPattern _scrollPattern;

    private ScrollPattern(AutomationElement element, IUIAutomationScrollPattern scrollPattern)
      : base(element)
      => this._scrollPattern = scrollPattern;

    internal static ScrollPattern Wrap(
      AutomationElement element,
      IUIAutomationScrollPattern scrollPattern) => new ScrollPattern(element, scrollPattern);

    public void SetScrollPercent(double horizontalPercent, double verticalPercent) => this._scrollPattern.SetScrollPercent(horizontalPercent, verticalPercent);

    public void Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount) => this._scrollPattern.Scroll(UiaConvert.Convert(horizontalAmount), UiaConvert.Convert(verticalAmount));

    public void ScrollHorizontal(ScrollAmount amount) => this._scrollPattern.Scroll(UiaConvert.Convert(amount), UiaConvert.Convert(ScrollAmount.NoAmount));

    public void ScrollVertical(ScrollAmount amount) => this._scrollPattern.Scroll(UiaConvert.Convert(ScrollAmount.NoAmount), UiaConvert.Convert(amount));

    public ScrollPattern.ScrollPatternInformation Cached => new ScrollPattern.ScrollPatternInformation(this._el, true);

    public ScrollPattern.ScrollPatternInformation Current => new ScrollPattern.ScrollPatternInformation(this._el, false);

    public struct ScrollPatternInformation
    {
      private AutomationElement _el;
      private bool _useCache;

      internal ScrollPatternInformation(AutomationElement el, bool useCache)
      {
        this._el = el;
        this._useCache = useCache;
      }

      public double HorizontalScrollPercent => (double) this._el.GetPatternPropertyValue(ScrollPattern.HorizontalScrollPercentProperty, this._useCache);

      public double VerticalScrollPercent => (double) this._el.GetPatternPropertyValue(ScrollPattern.VerticalScrollPercentProperty, this._useCache);

      public double HorizontalViewSize => (double) this._el.GetPatternPropertyValue(ScrollPattern.HorizontalViewSizeProperty, this._useCache);

      public double VerticalViewSize => (double) this._el.GetPatternPropertyValue(ScrollPattern.VerticalViewSizeProperty, this._useCache);

      public bool HorizontallyScrollable => (bool) this._el.GetPatternPropertyValue(ScrollPattern.HorizontallyScrollableProperty, this._useCache);

      public bool VerticallyScrollable => (bool) this._el.GetPatternPropertyValue(ScrollPattern.VerticallyScrollableProperty, this._useCache);
    }
  }
}
