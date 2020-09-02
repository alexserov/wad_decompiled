// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ScrollPatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public static class ScrollPatternIdentifiers
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<ScrollPattern, IUIAutomationScrollPattern>(10004, "ScrollPatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationScrollPattern, ScrollPattern>(ScrollPattern.Wrap));
    public static readonly AutomationProperty HorizontallyScrollableProperty = new AutomationProperty(30057, "ScrollPatternIdentifiers.HorizontallyScrollableProperty");
    public static readonly AutomationProperty HorizontalScrollPercentProperty = new AutomationProperty(30053, "ScrollPatternIdentifiers.HorizontalScrollPercentProperty");
    public static readonly AutomationProperty HorizontalViewSizeProperty = new AutomationProperty(30054, "ScrollPatternIdentifiers.HorizontalViewSizeProperty");
    public static readonly AutomationProperty VerticallyScrollableProperty = new AutomationProperty(30058, "ScrollPatternIdentifiers.VerticallyScrollableProperty");
    public static readonly AutomationProperty VerticalScrollPercentProperty = new AutomationProperty(30055, "ScrollPatternIdentifiers.VerticalScrollPercentProperty");
    public static readonly AutomationProperty VerticalViewSizeProperty = new AutomationProperty(30056, "ScrollPatternIdentifiers.VerticalViewSizeProperty");
    public const double NoScroll = -1.0;
  }
}
