// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.FocusChangedEventSource
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public class FocusChangedEventSource : EventSource
  {
    private WeakReference _sinkReference;
    private AutomationFocusChangedEventHandler _handlingDelegate;

    protected override void Dispose(bool disposing) => base.Dispose(disposing);

    public override void Start(IEventSink sink)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) sink, nameof (sink));
      this.Stop();
      this._sinkReference = new WeakReference((object) sink);
      this._sinkReference.Target = (object) sink;
      this._handlingDelegate = new AutomationFocusChangedEventHandler(this.Handler);
      System.Windows.Automation.Automation.AddAutomationFocusChangedEventHandler(this._handlingDelegate);
    }

    public override void Stop()
    {
      if (!this.IsStarted)
        return;
      System.Windows.Automation.Automation.RemoveAutomationFocusChangedEventHandler(this._handlingDelegate);
      this._handlingDelegate = (AutomationFocusChangedEventHandler) null;
      this._sinkReference = (WeakReference) null;
    }

    public override bool IsStarted => this._handlingDelegate != null;

    private void Handler(object sender, EventArgs e)
    {
      Log.Out("{0} saw event {1}", (object) this.ToString(), e != null ? (object) e.ToString() : (object) "null");
      if (this._sinkReference == null || !(this._sinkReference.Target is IEventSink target))
        return;
      target.SinkEvent(new WaiterEventArgs(sender, e));
    }
  }
}
