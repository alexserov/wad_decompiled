﻿// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.AutomationEventSource
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters {
    public class AutomationEventSource : EventSource {
        readonly AutomationEvent _eventId;
        AutomationEventHandler _handlingDelegate;
        readonly UIObject _root;
        readonly Scope _scope;
        WeakReference _sinkReference;

        public AutomationEventSource(AutomationEvent eventId, UIObject root, Scope scope) {
            Validate.ArgumentNotNull(parameter: eventId, parameterName: nameof(eventId));
            Validate.ArgumentNotNull(parameter: root, parameterName: nameof(root));
            Validate.ArgumentNotNull(parameter: scope, parameterName: nameof(scope));
            this._eventId = eventId;
            this._root = Cache.PopulateDefaultCache(uiObject: root);
            this._scope = scope;
        }

        public override bool IsStarted {
            get { return this._handlingDelegate != null; }
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing: disposing);
        }

        public override void Start(IEventSink sink) {
            Validate.ArgumentNotNull(parameter: sink, parameterName: nameof(sink));
            Stop();
            Log.Out(msg: "{0} Start", (object) ToString());
            this._sinkReference = new WeakReference(target: sink);
            this._handlingDelegate = Handler;
            Automation.AddAutomationEventHandler(eventId: this._eventId, element: this._root.AutomationElement, scope: (TreeScope) this._scope, eventHandler: this._handlingDelegate);
        }

        public override void Stop() {
            if (!IsStarted)
                return;
            Log.Out(msg: "{0} Stop", (object) ToString());
            Automation.RemoveAutomationEventHandler(eventId: this._eventId, element: this._root.AutomationElement, eventHandler: this._handlingDelegate);
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