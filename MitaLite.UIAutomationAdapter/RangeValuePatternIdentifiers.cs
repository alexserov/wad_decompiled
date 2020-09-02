// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.RangeValuePatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public static class RangeValuePatternIdentifiers {
        public static readonly AutomationPattern Pattern = new AutomationPattern<RangeValuePattern, IUIAutomationRangeValuePattern>(id: 10003, programmaticName: "RangeValuePatternIdentifiers.Pattern", wrap: RangeValuePattern.Wrap);
        public static readonly AutomationProperty IsReadOnlyProperty = new AutomationProperty(id: 30048, programmaticName: "RangeValuePatternIdentifiers.IsReadOnlyProperty");
        public static readonly AutomationProperty LargeChangeProperty = new AutomationProperty(id: 30051, programmaticName: "RangeValuePatternIdentifiers.LargeChangeProperty");
        public static readonly AutomationProperty MaximumProperty = new AutomationProperty(id: 30050, programmaticName: "RangeValuePatternIdentifiers.MaximumProperty");
        public static readonly AutomationProperty MinimumProperty = new AutomationProperty(id: 30049, programmaticName: "RangeValuePatternIdentifiers.MinimumProperty");
        public static readonly AutomationProperty SmallChangeProperty = new AutomationProperty(id: 30052, programmaticName: "RangeValuePatternIdentifiers.SmallChangeProperty");
        public static readonly AutomationProperty ValueProperty = new AutomationProperty(id: 30047, programmaticName: "RangeValuePatternIdentifiers.ValueProperty");
    }
}