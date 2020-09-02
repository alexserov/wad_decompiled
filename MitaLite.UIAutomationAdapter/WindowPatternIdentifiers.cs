// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.WindowPatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public static class WindowPatternIdentifiers {
        public static readonly AutomationPattern Pattern = new AutomationPattern<WindowPattern, IUIAutomationWindowPattern>(id: 10009, programmaticName: "WindowPatternIdentifiers.Pattern", wrap: WindowPattern.Wrap);
        public static readonly AutomationEvent WindowClosedEvent = new AutomationEvent(id: 20017, programmaticName: "WindowPatternIdentifiers.WindowClosedProperty");
        public static readonly AutomationEvent WindowOpenedEvent = new AutomationEvent(id: 20016, programmaticName: "WindowPatternIdentifiers.WindowOpenedProperty");
        public static readonly AutomationProperty CanMaximizeProperty = new AutomationProperty(id: 30073, programmaticName: "WindowPatternIdentifiers.CanMaximizeProperty");
        public static readonly AutomationProperty CanMinimizeProperty = new AutomationProperty(id: 30074, programmaticName: "WindowPatternIdentifiers.CanMinimizeProperty");
        public static readonly AutomationProperty IsModalProperty = new AutomationProperty(id: 30077, programmaticName: "WindowPatternIdentifiers.IsModalProperty");
        public static readonly AutomationProperty IsTopmostProperty = new AutomationProperty(id: 30078, programmaticName: "WindowPatternIdentifiers.IsTopmostProperty");
        public static readonly AutomationProperty WindowInteractionStateProperty = new AutomationProperty(id: 30076, programmaticName: "WindowPatternIdentifiers.WindowInteractionStateProperty");
        public static readonly AutomationProperty WindowVisualStateProperty = new AutomationProperty(id: 30075, programmaticName: "WindowPatternIdentifiers.WindowVisualStateProperty");
    }
}