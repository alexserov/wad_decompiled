// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.WindowPatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public static class WindowPatternIdentifiers
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<WindowPattern, IUIAutomationWindowPattern>(10009, "WindowPatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationWindowPattern, WindowPattern>(WindowPattern.Wrap));
    public static readonly AutomationEvent WindowClosedEvent = new AutomationEvent(20017, "WindowPatternIdentifiers.WindowClosedProperty");
    public static readonly AutomationEvent WindowOpenedEvent = new AutomationEvent(20016, "WindowPatternIdentifiers.WindowOpenedProperty");
    public static readonly AutomationProperty CanMaximizeProperty = new AutomationProperty(30073, "WindowPatternIdentifiers.CanMaximizeProperty");
    public static readonly AutomationProperty CanMinimizeProperty = new AutomationProperty(30074, "WindowPatternIdentifiers.CanMinimizeProperty");
    public static readonly AutomationProperty IsModalProperty = new AutomationProperty(30077, "WindowPatternIdentifiers.IsModalProperty");
    public static readonly AutomationProperty IsTopmostProperty = new AutomationProperty(30078, "WindowPatternIdentifiers.IsTopmostProperty");
    public static readonly AutomationProperty WindowInteractionStateProperty = new AutomationProperty(30076, "WindowPatternIdentifiers.WindowInteractionStateProperty");
    public static readonly AutomationProperty WindowVisualStateProperty = new AutomationProperty(30075, "WindowPatternIdentifiers.WindowVisualStateProperty");
  }
}
