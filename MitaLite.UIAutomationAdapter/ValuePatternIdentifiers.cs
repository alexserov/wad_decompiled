// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ValuePatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public static class ValuePatternIdentifiers {
        public static readonly AutomationPattern Pattern = new AutomationPattern<ValuePattern, IUIAutomationValuePattern>(id: 10002, programmaticName: "ValuePatternIdentifiers.Pattern", wrap: ValuePattern.Wrap);
        public static readonly AutomationProperty IsReadOnlyProperty = new AutomationProperty(id: 30046, programmaticName: "ValuePatternIdentifiers.IsReadOnlyProperty");
        public static readonly AutomationProperty ValueProperty = new AutomationProperty(id: 30045, programmaticName: "ValuePatternIdentifiers.ValueProperty");
    }
}