// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ExpandCollapsePattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class ExpandCollapsePattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = ExpandCollapsePatternIdentifiers.Pattern;
    public static readonly AutomationProperty ExpandCollapseStateProperty = ExpandCollapsePatternIdentifiers.ExpandCollapseStateProperty;
    private readonly IUIAutomationExpandCollapsePattern _expandCollapsePattern;

    private ExpandCollapsePattern(
      AutomationElement element,
      IUIAutomationExpandCollapsePattern expandCollapsePattern)
      : base(element)
      => this._expandCollapsePattern = expandCollapsePattern;

    internal static ExpandCollapsePattern Wrap(
      AutomationElement element,
      IUIAutomationExpandCollapsePattern expandCollapsePattern) => new ExpandCollapsePattern(element, expandCollapsePattern);

    public void Expand() => this._expandCollapsePattern.Expand();

    public void Collapse() => this._expandCollapsePattern.Collapse();

    public ExpandCollapsePattern.ExpandCollapsePatternInformation Cached => new ExpandCollapsePattern.ExpandCollapsePatternInformation(this._el, true);

    public ExpandCollapsePattern.ExpandCollapsePatternInformation Current => new ExpandCollapsePattern.ExpandCollapsePatternInformation(this._el, false);

    public struct ExpandCollapsePatternInformation
    {
      private AutomationElement _el;
      private bool _useCache;

      internal ExpandCollapsePatternInformation(AutomationElement el, bool useCache)
      {
        this._el = el;
        this._useCache = useCache;
      }

      public ExpandCollapseState ExpandCollapseState => (ExpandCollapseState) this._el.GetPatternPropertyValue(ExpandCollapsePattern.ExpandCollapseStateProperty, this._useCache);
    }
  }
}
