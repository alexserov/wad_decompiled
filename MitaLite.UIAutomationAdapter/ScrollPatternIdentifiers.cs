// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ScrollPatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public static class ScrollPatternIdentifiers {
        public const double NoScroll = -1.0;
        public static readonly AutomationPattern Pattern = new AutomationPattern<ScrollPattern, IUIAutomationScrollPattern>(id: 10004, programmaticName: "ScrollPatternIdentifiers.Pattern", wrap: ScrollPattern.Wrap);
        public static readonly AutomationProperty HorizontallyScrollableProperty = new AutomationProperty(id: 30057, programmaticName: "ScrollPatternIdentifiers.HorizontallyScrollableProperty");
        public static readonly AutomationProperty HorizontalScrollPercentProperty = new AutomationProperty(id: 30053, programmaticName: "ScrollPatternIdentifiers.HorizontalScrollPercentProperty");
        public static readonly AutomationProperty HorizontalViewSizeProperty = new AutomationProperty(id: 30054, programmaticName: "ScrollPatternIdentifiers.HorizontalViewSizeProperty");
        public static readonly AutomationProperty VerticallyScrollableProperty = new AutomationProperty(id: 30058, programmaticName: "ScrollPatternIdentifiers.VerticallyScrollableProperty");
        public static readonly AutomationProperty VerticalScrollPercentProperty = new AutomationProperty(id: 30055, programmaticName: "ScrollPatternIdentifiers.VerticalScrollPercentProperty");
        public static readonly AutomationProperty VerticalViewSizeProperty = new AutomationProperty(id: 30056, programmaticName: "ScrollPatternIdentifiers.VerticalViewSizeProperty");
    }
}