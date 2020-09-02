// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.PropertyChangedEventSource
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters {
    public class PropertyChangedEventSource : EventSource {
        AutomationPropertyChangedEventHandler _handlingDelegate;
        readonly AutomationProperty[] _properties;
        readonly UIObject _root;
        readonly Scope _scope;
        WeakReference _sinkReference;

        public PropertyChangedEventSource(UIObject root, Scope scope) {
            Validate.ArgumentNotNull(parameter: root, parameterName: nameof(root));
            Validate.ArgumentNotNull(parameter: scope, parameterName: nameof(scope));
            this._root = Cache.PopulateDefaultCache(uiObject: root);
            this._scope = scope;
            var automationPropertyList = new List<AutomationProperty>();
            foreach (var allProperty in UIProperty.AllProperties)
                automationPropertyList.Add(item: allProperty.Property);
            this._properties = new AutomationProperty[automationPropertyList.Count];
            automationPropertyList.CopyTo(array: this._properties);
        }

        public PropertyChangedEventSource(UIObject root, Scope scope, params UIProperty[] uiProperties) {
            Validate.ArgumentNotNull(parameter: root, parameterName: nameof(root));
            Validate.ArgumentNotNull(parameter: scope, parameterName: nameof(scope));
            Validate.ArgumentNotNull(parameter: uiProperties, parameterName: nameof(uiProperties));
            this._root = root;
            this._scope = scope;
            this._properties = new AutomationProperty[uiProperties.Length];
            for (var index = 0; index < uiProperties.Length; ++index)
                if (uiProperties[index] != null && uiProperties[index].Property != null)
                    this._properties[index] = uiProperties[index].Property;
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
            this._sinkReference = new WeakReference(target: sink);
            this._sinkReference.Target = sink;
            this._handlingDelegate = Handler;
            Automation.AddAutomationPropertyChangedEventHandler(element: this._root.AutomationElement, scope: (TreeScope) this._scope, eventHandler: this._handlingDelegate, properties: this._properties);
            Log.Out(msg: "{0} Started", (object) ToString());
        }

        public override void Stop() {
            if (!IsStarted)
                return;
            Automation.RemoveAutomationPropertyChangedEventHandler(element: this._root.AutomationElement, eventHandler: this._handlingDelegate);
            this._handlingDelegate = null;
            this._sinkReference = null;
            Log.Out(msg: "{0} Stopped", (object) ToString());
        }

        void Handler(object sender, EventArgs e) {
            Log.Out(msg: "{0} saw event {1}", (object) ToString(), e != null ? (object) e.ToString() : (object) "null");
            if (this._sinkReference == null || !(this._sinkReference.Target is IEventSink target))
                return;
            target.SinkEvent(eventArgs: new WaiterEventArgs(sender: sender, eventArgs: e));
        }
    }
}