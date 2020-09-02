// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationEvent
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation {
    public class AutomationEvent : AutomationIdentifier {
        internal AutomationEvent(int id, string programmaticName)
            : base(type: AutomationIdType.Event, id: id, programmaticName: programmaticName) {
        }

        public static AutomationEvent LookupById(int id) {
            return LookupById<AutomationEvent>(id: id);
        }
    }
}