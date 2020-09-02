// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.InvokePatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public static class InvokePatternIdentifiers {
        public static readonly AutomationPattern Pattern = new AutomationPattern<InvokePattern, IUIAutomationInvokePattern>(id: 10000, programmaticName: "InvokePatternIdentifiers.Pattern", wrap: InvokePattern.Wrap);
        public static readonly AutomationEvent InvokedEvent = new AutomationEvent(id: 20009, programmaticName: "InvokePatternIdentifiers.InvokedEvent");
    }
}