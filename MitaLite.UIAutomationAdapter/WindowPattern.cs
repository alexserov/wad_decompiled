// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.WindowPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class WindowPattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = WindowPatternIdentifiers.Pattern;
    public static readonly AutomationProperty CanMaximizeProperty = WindowPatternIdentifiers.CanMaximizeProperty;
    public static readonly AutomationProperty CanMinimizeProperty = WindowPatternIdentifiers.CanMinimizeProperty;
    public static readonly AutomationProperty IsModalProperty = WindowPatternIdentifiers.IsModalProperty;
    public static readonly AutomationProperty IsTopmostProperty = WindowPatternIdentifiers.IsTopmostProperty;
    public static readonly AutomationProperty WindowInteractionStateProperty = WindowPatternIdentifiers.WindowInteractionStateProperty;
    public static readonly AutomationProperty WindowVisualStateProperty = WindowPatternIdentifiers.WindowVisualStateProperty;
    public static readonly AutomationEvent WindowClosedEvent = WindowPatternIdentifiers.WindowClosedEvent;
    public static readonly AutomationEvent WindowOpenedEvent = WindowPatternIdentifiers.WindowOpenedEvent;
    private readonly IUIAutomationWindowPattern _windowPattern;

    private WindowPattern(AutomationElement element, IUIAutomationWindowPattern windowPattern)
      : base(element)
      => this._windowPattern = windowPattern;

    internal static WindowPattern Wrap(
      AutomationElement element,
      IUIAutomationWindowPattern windowPattern) => new WindowPattern(element, windowPattern);

    public void SetWindowVisualState(WindowVisualState state) => this._windowPattern.SetWindowVisualState(UiaConvert.Convert(state));

    public void Close() => this._windowPattern.Close();

    public bool WaitForInputIdle(int milliseconds) => this._windowPattern.WaitForInputIdle(milliseconds) != 0;

    public WindowPattern.WindowPatternInformation Cached => new WindowPattern.WindowPatternInformation(this._el, true);

    public WindowPattern.WindowPatternInformation Current => new WindowPattern.WindowPatternInformation(this._el, false);

    public struct WindowPatternInformation
    {
      private AutomationElement _el;
      private bool _useCache;

      internal WindowPatternInformation(AutomationElement el, bool useCache)
      {
        this._el = el;
        this._useCache = useCache;
      }

      public bool CanMaximize => (bool) this._el.GetPatternPropertyValue(WindowPattern.CanMaximizeProperty, this._useCache);

      public bool CanMinimize => (bool) this._el.GetPatternPropertyValue(WindowPattern.CanMinimizeProperty, this._useCache);

      public bool IsModal => (bool) this._el.GetPatternPropertyValue(WindowPattern.IsModalProperty, this._useCache);

      public WindowVisualState WindowVisualState => (WindowVisualState) this._el.GetPatternPropertyValue(WindowPattern.WindowVisualStateProperty, this._useCache);

      public WindowInteractionState WindowInteractionState => (WindowInteractionState) this._el.GetPatternPropertyValue(WindowPattern.WindowInteractionStateProperty, this._useCache);

      public bool IsTopmost => (bool) this._el.GetPatternPropertyValue(WindowPattern.IsTopmostProperty, this._useCache);
    }
  }
}
