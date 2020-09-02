// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.DockPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class DockPattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = DockPatternIdentifiers.Pattern;
    public static readonly AutomationProperty DockPositionProperty = DockPatternIdentifiers.DockPositionProperty;
    private readonly IUIAutomationDockPattern _dockPattern;

    private DockPattern(AutomationElement element, IUIAutomationDockPattern dockPattern)
      : base(element)
      => this._dockPattern = dockPattern;

    internal static DockPattern Wrap(
      AutomationElement element,
      IUIAutomationDockPattern dockPattern) => new DockPattern(element, dockPattern);

    public void SetDockPosition(DockPosition dockPosition) => this._dockPattern.SetDockPosition(UiaConvert.Convert(dockPosition));

    public DockPattern.DockPatternInformation Cached => new DockPattern.DockPatternInformation(this._el, true);

    public DockPattern.DockPatternInformation Current => new DockPattern.DockPatternInformation(this._el, false);

    public struct DockPatternInformation
    {
      private AutomationElement _el;
      private bool _useCache;

      internal DockPatternInformation(AutomationElement el, bool useCache)
      {
        this._el = el;
        this._useCache = useCache;
      }

      public DockPosition DockPosition => (DockPosition) this._el.GetPatternPropertyValue(DockPattern.DockPositionProperty, this._useCache);
    }
  }
}
