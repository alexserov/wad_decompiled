// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationEventArgs
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation {
    public class AutomationEventArgs : EventArgs {
        public AutomationEventArgs(AutomationEvent eventId) {
            EventId = eventId;
        }

        public AutomationEvent EventId { get; }

        public override string ToString() {
            return string.Format(format: "{0} {{ {1} = {2} }}", arg0: GetType().Name, arg1: "EventId", arg2: EventId);
        }
    }
}