// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.FocusChangedEventSource
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters {
    public class FocusChangedEventSource : EventSource {
        AutomationFocusChangedEventHandler _handlingDelegate;
        WeakReference _sinkReference;

        public override bool IsStarted {
            get { return this._handlingDelegate != null; }
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing: disposing);
        }

        public override void Start(IEventSink sink) {
            Validate.ArgumentNotNull(parameter: sink, parameterName: nameof(sink));
            Stop();
            this._sinkReference = new WeakReference(target: sink);
            this._sinkReference.Target = sink;
            this._handlingDelegate = Handler;
            Automation.AddAutomationFocusChangedEventHandler(eventHandler: this._handlingDelegate);
        }

        public override void Stop() {
            if (!IsStarted)
                return;
            Automation.RemoveAutomationFocusChangedEventHandler(eventHandler: this._handlingDelegate);
            this._handlingDelegate = null;
            this._sinkReference = null;
        }

        void Handler(object sender, EventArgs e) {
            Log.Out(msg: "{0} saw event {1}", (object) ToString(), e != null ? (object) e.ToString() : (object) "null");
            if (this._sinkReference == null || !(this._sinkReference.Target is IEventSink target))
                return;
            target.SinkEvent(eventArgs: new WaiterEventArgs(sender: sender, eventArgs: e));
        }
    }
}