﻿// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.AutomationEventSource
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public class AutomationEventSource : EventSource
  {
    private AutomationEvent _eventId;
    private UIObject _root;
    private Scope _scope;
    private WeakReference _sinkReference;
    private AutomationEventHandler _handlingDelegate;

    public AutomationEventSource(AutomationEvent eventId, UIObject root, Scope scope)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) eventId, nameof (eventId));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) root, nameof (root));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) scope, nameof (scope));
      this._eventId = eventId;
      this._root = Cache.PopulateDefaultCache(root);
      this._scope = scope;
    }

    protected override void Dispose(bool disposing) => base.Dispose(disposing);

    public override void Start(IEventSink sink)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) sink, nameof (sink));
      this.Stop();
      Log.Out("{0} Start", (object) this.ToString());
      this._sinkReference = new WeakReference((object) sink);
      this._handlingDelegate = new AutomationEventHandler(this.Handler);
      System.Windows.Automation.Automation.AddAutomationEventHandler(this._eventId, this._root.AutomationElement, (TreeScope) this._scope, this._handlingDelegate);
    }

    public override void Stop()
    {
      if (!this.IsStarted)
        return;
      Log.Out("{0} Stop", (object) this.ToString());
      System.Windows.Automation.Automation.RemoveAutomationEventHandler(this._eventId, this._root.AutomationElement, this._handlingDelegate);
      this._handlingDelegate = (AutomationEventHandler) null;
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
