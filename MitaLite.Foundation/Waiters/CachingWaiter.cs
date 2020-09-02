// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.CachingWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using System.Threading;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters {
    public class CachingWaiter : Waiter, IDisposable {
        bool _disposed;
        Queue<WaiterEventArgs> _eventQueue;
        Predicate<WaiterEventArgs> _filterDelegate;
        ManualResetEvent _resetEvent;
        bool _shutdown;
        UIObject _source;
        readonly object _syncRoot;
        Timer _timer;
        UIEventWaiter _waiter;

        public CachingWaiter(UIEventWaiter waiter) {
            Validate.ArgumentNotNull(parameter: waiter, parameterName: nameof(waiter));
            this._eventQueue = new Queue<WaiterEventArgs>();
            this._resetEvent = new ManualResetEvent(initialState: false);
            this._shutdown = false;
            this._disposed = false;
            this._waiter = waiter;
            this._syncRoot = new object();
            this._waiter.Notify += NotifyHandler;
        }

        public virtual UIObject Source {
            get { return (UIObject) null != this._source ? this._waiter.Factory.Create(element: this._source) : null; }
        }

        public override void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(obj: this);
        }

        ~CachingWaiter() {
            Dispose(disposing: false);
        }

        protected virtual void Dispose(bool disposing) {
            if (!this._disposed) {
                if (disposing) {
                    this._waiter.Notify -= NotifyHandler;
                    this._eventQueue.Clear();
                    if (this._timer != null)
                        this._timer.Dispose();
                    this._resetEvent.Dispose();
                }

                this._resetEvent = null;
                this._eventQueue = null;
                this._waiter = null;
                this._source = null;
                this._timer = null;
            }

            this._disposed = true;
        }

        public override bool TryWait(TimeSpan timeout) {
            var flag = false;
            this._shutdown = false;
            this._source = null;
            this._timer = new Timer(callback: TimerHandler, state: null, dueTime: timeout, period: TimeSpan.FromMilliseconds(value: -1.0));
            while (!this._shutdown) {
                this._resetEvent.WaitOne(timeout: timeout);
                this._resetEvent.Reset();
                while (this._eventQueue.Count != 0) {
                    WaiterEventArgs waiterEventArgs;
                    lock (this._syncRoot) {
                        waiterEventArgs = this._eventQueue.Dequeue();
                    }

                    flag = true;
                    if (this._filterDelegate != null)
                        foreach (Predicate<WaiterEventArgs> invocation in this._filterDelegate.GetInvocationList()) {
                            flag = invocation(obj: waiterEventArgs);
                            if (!flag)
                                break;
                        }

                    if (flag) {
                        this._source = waiterEventArgs.Sender;
                        this._shutdown = true;
                        break;
                    }
                }
            }

            return flag;
        }

        public override void Reset() {
            this._eventQueue.Clear();
        }

        public void AddFilter(Predicate<WaiterEventArgs> callback) {
            this._filterDelegate += callback;
        }

        public void RemoveFilter(Predicate<WaiterEventArgs> callback) {
            this._filterDelegate -= callback;
        }

        public override string ToString() {
            return "CachingWaiter constructed around " + this._waiter;
        }

        void NotifyHandler(object sender, WaiterEventArgs args) {
            lock (this._syncRoot) {
                this._eventQueue.Enqueue(item: args);
            }

            this._resetEvent.Set();
        }

        void TimerHandler(object state) {
            this._shutdown = true;
            this._resetEvent.Set();
        }
    }
}