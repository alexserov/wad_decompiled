// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ScrollItemPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class ScrollItemPattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = ScrollItemPatternIdentifiers.Pattern;
    private readonly IUIAutomationScrollItemPattern _scrollItemPattern;

    private ScrollItemPattern(
      AutomationElement element,
      IUIAutomationScrollItemPattern scrollItemPattern)
      : base(element)
      => this._scrollItemPattern = scrollItemPattern;

    internal static ScrollItemPattern Wrap(
      AutomationElement element,
      IUIAutomationScrollItemPattern scrollItemPattern) => new ScrollItemPattern(element, scrollItemPattern);

    public void ScrollIntoView() => this._scrollItemPattern.ScrollIntoView();
  }
}
