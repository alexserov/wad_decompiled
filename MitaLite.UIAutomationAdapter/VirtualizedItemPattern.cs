// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.VirtualizedItemPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class VirtualizedItemPattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<VirtualizedItemPattern, IUIAutomationVirtualizedItemPattern>(10020, "VirtualizedItemPatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationVirtualizedItemPattern, VirtualizedItemPattern>(VirtualizedItemPattern.Wrap));
    private readonly IUIAutomationVirtualizedItemPattern _virtualizedItemPattern;

    private VirtualizedItemPattern(
      AutomationElement element,
      IUIAutomationVirtualizedItemPattern virtualizedItemPattern)
      : base(element)
      => this._virtualizedItemPattern = virtualizedItemPattern;

    internal static VirtualizedItemPattern Wrap(
      AutomationElement element,
      IUIAutomationVirtualizedItemPattern virtualizedItemPattern) => new VirtualizedItemPattern(element, virtualizedItemPattern);

    public void Realize() => this._virtualizedItemPattern.Realize();
  }
}
