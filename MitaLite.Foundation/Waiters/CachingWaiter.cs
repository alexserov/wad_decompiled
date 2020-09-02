// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.CachingWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public class CachingWaiter : Waiter, IDisposable
  {
    private Predicate<WaiterEventArgs> _filterDelegate;
    private Queue<WaiterEventArgs> _eventQueue;
    private ManualResetEvent _resetEvent;
    private bool _disposed;
    private bool _shutdown;
    private Timer _timer;
    private UIEventWaiter _waiter;
    private object _syncRoot;
    private UIObject _source;

    public CachingWaiter(UIEventWaiter waiter)
    {
      Validate.ArgumentNotNull((object) waiter, nameof (waiter));
      this._eventQueue = new Queue<WaiterEventArgs>();
      this._resetEvent = new ManualResetEvent(false);
      this._shutdown = false;
      this._disposed = false;
      this._waiter = waiter;
      this._syncRoot = new object();
      this._waiter.Notify += new EventHandler<WaiterEventArgs>(this.NotifyHandler);
    }

    ~CachingWaiter() => this.Dispose(false);

    public override void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!this._disposed)
      {
        if (disposing)
        {
          this._waiter.Notify -= new EventHandler<WaiterEventArgs>(this.NotifyHandler);
          this._eventQueue.Clear();
          if (this._timer != null)
            this._timer.Dispose();
          this._resetEvent.Dispose();
        }
        this._resetEvent = (ManualResetEvent) null;
        this._eventQueue = (Queue<WaiterEventArgs>) null;
        this._waiter = (UIEventWaiter) null;
        this._source = (UIObject) null;
        this._timer = (Timer) null;
      }
      this._disposed = true;
    }

    public override bool TryWait(TimeSpan timeout)
    {
      bool flag = false;
      this._shutdown = false;
      this._source = (UIObject) null;
      this._timer = new Timer(new TimerCallback(this.TimerHandler), (object) null, timeout, TimeSpan.FromMilliseconds(-1.0));
      while (!this._shutdown)
      {
        this._resetEvent.WaitOne(timeout);
        this._resetEvent.Reset();
        while (this._eventQueue.Count != 0)
        {
          WaiterEventArgs waiterEventArgs;
          lock (this._syncRoot)
            waiterEventArgs = this._eventQueue.Dequeue();
          flag = true;
          if (this._filterDelegate != null)
          {
            foreach (Predicate<WaiterEventArgs> invocation in this._filterDelegate.GetInvocationList())
            {
              flag = invocation(waiterEventArgs);
              if (!flag)
                break;
            }
          }
          if (flag)
          {
            this._source = waiterEventArgs.Sender;
            this._shutdown = true;
            break;
          }
        }
      }
      return flag;
    }

    public override void Reset() => this._eventQueue.Clear();

    public void AddFilter(Predicate<WaiterEventArgs> callback) => this._filterDelegate += callback;

    public void RemoveFilter(Predicate<WaiterEventArgs> callback) => this._filterDelegate -= callback;

    public virtual UIObject Source => (UIObject) null != this._source ? this._waiter.Factory.Create(this._source) : (UIObject) null;

    public override string ToString() => "CachingWaiter constructed around " + this._waiter.ToString();

    private void NotifyHandler(object sender, WaiterEventArgs args)
    {
      lock (this._syncRoot)
        this._eventQueue.Enqueue(args);
      this._resetEvent.Set();
    }

    private void TimerHandler(object state)
    {
      this._shutdown = true;
      this._resetEvent.Set();
    }
  }
}
