// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.UIEventWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Threading;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters {
    public abstract class UIEventWaiter : CompositableWaiter, IDisposable, IEventSink {
        readonly object _lockObject = new object();
        ManualResetEvent _blockingEvent;
        bool _disposed;
        IEventSource _eventSource;
        IFactory<UIObject> _factory;
        Predicate<WaiterEventArgs> _filterDelegate;

        protected UIEventWaiter(IEventSource eventSource) {
            this._debug_identity = Log.CreateUniqueObjectId(baseName: nameof(UIEventWaiter));
            this._blockingEvent = new ManualResetEvent(initialState: false);
            this._eventSource = eventSource;
            this._factory = UIObject.Factory;
        }

        protected override WaitHandle WaitHandle {
            get { return this._blockingEvent; }
        }

        public virtual UIObject Source {
            get { return this.Arguments != null && (UIObject) null != this.Arguments.Sender ? this._factory.Create(element: this.Arguments.Sender) : null; }
        }

        public WaiterEventArgs Arguments { get; set; }

        public IFactory<UIObject> Factory {
            get { return this._factory; }
            set {
                Validate.ArgumentNotNull(parameter: value, parameterName: nameof(value));
                this._factory = value;
            }
        }

        public override void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(obj: this);
        }

        public void SinkEvent(WaiterEventArgs eventArgs) {
            Log.Out(msg: "{0} checking event {1} from {2}", (object) this._debug_identity, eventArgs.EventArgs != null ? (object) eventArgs.EventArgs.ToString() : (object) "null", (object) eventArgs.Sender);
            lock (this._lockObject) {
                if (this._disposed || !Matches(eventArgs: eventArgs))
                    return;
                var flag = true;
                Log.Out(msg: "{0} matched", (object) this._debug_identity);
                if (this._filterDelegate != null)
                    foreach (Predicate<WaiterEventArgs> invocation in this._filterDelegate.GetInvocationList()) {
                        flag = invocation(obj: eventArgs);
                        if (!flag) {
                            Log.Out(msg: "{0} filtered out", (object) this._debug_identity);
                            break;
                        }
                    }

                if (!flag)
                    return;
                this.Arguments = eventArgs;
                Log.Out(msg: "{0} notifying", (object) this._debug_identity);
                try {
                    OnNotify(e: eventArgs);
                } finally {
                    this._blockingEvent.Set();
                }
            }
        }

        ~UIEventWaiter() {
            Dispose(disposing: false);
        }

        protected virtual void Start() {
            if (this._eventSource.IsStarted)
                return;
            Log.Out(msg: "{0} starting listening for {1}", (object) this._debug_identity, (object) this._eventSource);
            this._eventSource.Start(sink: this);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposing)
                return;
            lock (this._lockObject) {
                if (this._disposed)
                    return;
                if (disposing) {
                    this._eventSource.Dispose();
                    this._blockingEvent.Dispose();
                }

                this._blockingEvent = null;
                this._eventSource = null;
                this._disposed = true;
            }
        }

        public override bool TryWait(TimeSpan timeout) {
            Log.Out(msg: "{0} TryWait", (object) this._debug_identity);
            var flag = true;
            if (!this._eventSource.IsStarted)
                throw new WaiterException(message: StringResource.Get(id: "MustResetWaiter"));
            if (!this._blockingEvent.WaitOne(timeout: timeout))
                flag = false;
            this._eventSource.Stop();
            return flag;
        }

        public override void Reset() {
            Log.Out(msg: "{0} Reset", (object) this._debug_identity);
            this._eventSource.Stop();
            this._blockingEvent.Reset();
            this._eventSource.Start(sink: this);
        }

        protected virtual bool Matches(WaiterEventArgs eventArgs) {
            return true;
        }

        public void AddFilter(Predicate<WaiterEventArgs> callback) {
            this._filterDelegate += callback;
        }

        public void RemoveFilter(Predicate<WaiterEventArgs> callback) {
            this._filterDelegate -= callback;
        }

        protected virtual void OnNotify(WaiterEventArgs e) {
            if (Notify == null)
                return;
            Notify(sender: this, e: e);
        }

        public event EventHandler<WaiterEventArgs> Notify;
    }
}