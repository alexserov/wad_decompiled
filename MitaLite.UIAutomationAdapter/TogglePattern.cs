// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TogglePattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class TogglePattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = TogglePatternIdentifiers.Pattern;
    public static readonly AutomationProperty ToggleStateProperty = TogglePatternIdentifiers.ToggleStateProperty;
    private readonly IUIAutomationTogglePattern _togglePattern;

    private TogglePattern(AutomationElement element, IUIAutomationTogglePattern togglePattern)
      : base(element)
      => this._togglePattern = togglePattern;

    internal static TogglePattern Wrap(
      AutomationElement element,
      IUIAutomationTogglePattern pattern) => new TogglePattern(element, pattern);

    public void Toggle() => this._togglePattern.Toggle();

    public TogglePattern.TogglePatternInformation Cached => new TogglePattern.TogglePatternInformation(this._el, true);

    public TogglePattern.TogglePatternInformation Current => new TogglePattern.TogglePatternInformation(this._el, false);

    public struct TogglePatternInformation
    {
      private AutomationElement _el;
      private bool _useCache;

      internal TogglePatternInformation(AutomationElement el, bool useCache)
      {
        this._el = el;
        this._useCache = useCache;
      }

      public ToggleState ToggleState => (ToggleState) this._el.GetPatternPropertyValue(TogglePattern.ToggleStateProperty, this._useCache);
    }
  }
}
