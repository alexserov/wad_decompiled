// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationEventArgs
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation
{
  public class AutomationEventArgs : EventArgs
  {
    private AutomationEvent _eventId;

    public AutomationEventArgs(AutomationEvent eventId) => this._eventId = eventId;

    public AutomationEvent EventId => this._eventId;

    public override string ToString() => string.Format("{0} {{ {1} = {2} }}", (object) this.GetType().Name, (object) "EventId", (object) this.EventId);
  }
}
