// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.MultipleViewPatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public static class MultipleViewPatternIdentifiers {
        public static readonly AutomationPattern Pattern = new AutomationPattern<MultipleViewPattern, IUIAutomationMultipleViewPattern>(id: 10008, programmaticName: "MultipleViewPatternIdentifiers.Pattern", wrap: MultipleViewPattern.Wrap);
        public static readonly AutomationProperty CurrentViewProperty = new AutomationProperty(id: 30071, programmaticName: "MultipleViewPatternIdentifiers.CurrentViewProperty");
        public static readonly AutomationProperty SupportedViewsProperty = new AutomationProperty(id: 30072, programmaticName: "MultipleViewPatternIdentifiers.SupportedViewsProperty");
    }
}