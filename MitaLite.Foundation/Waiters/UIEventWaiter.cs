// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.UIEventWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Threading;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public abstract class UIEventWaiter : CompositableWaiter, IDisposable, IEventSink
  {
    private readonly object _lockObject = new object();
    private ManualResetEvent _blockingEvent;
    private IEventSource _eventSource;
    private Predicate<WaiterEventArgs> _filterDelegate;
    private WaiterEventArgs _eventArgs;
    private IFactory<UIObject> _factory;
    private bool _disposed;

    protected UIEventWaiter(IEventSource eventSource)
    {
      this._debug_identity = Log.CreateUniqueObjectId(nameof (UIEventWaiter));
      this._blockingEvent = new ManualResetEvent(false);
      this._eventSource = eventSource;
      this._factory = UIObject.Factory;
    }

    ~UIEventWaiter() => this.Dispose(false);

    protected virtual void Start()
    {
      if (this._eventSource.IsStarted)
        return;
      Log.Out("{0} starting listening for {1}", (object) this._debug_identity, (object) this._eventSource);
      this._eventSource.Start((IEventSink) this);
    }

    public override void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      lock (this._lockObject)
      {
        if (this._disposed)
          return;
        if (disposing)
        {
          this._eventSource.Dispose();
          this._blockingEvent.Dispose();
        }
        this._blockingEvent = (ManualResetEvent) null;
        this._eventSource = (IEventSource) null;
        this._disposed = true;
      }
    }

    public override bool TryWait(TimeSpan timeout)
    {
      Log.Out("{0} TryWait", (object) this._debug_identity);
      bool flag = true;
      if (!this._eventSource.IsStarted)
        throw new WaiterException(StringResource.Get("MustResetWaiter"));
      if (!this._blockingEvent.WaitOne(timeout))
        flag = false;
      this._eventSource.Stop();
      return flag;
    }

    public override void Reset()
    {
      Log.Out("{0} Reset", (object) this._debug_identity);
      this._eventSource.Stop();
      this._blockingEvent.Reset();
      this._eventSource.Start((IEventSink) this);
    }

    public void SinkEvent(WaiterEventArgs eventArgs)
    {
      Log.Out("{0} checking event {1} from {2}", (object) this._debug_identity, eventArgs.EventArgs != null ? (object) eventArgs.EventArgs.ToString() : (object) "null", (object) eventArgs.Sender);
      lock (this._lockObject)
      {
        if (this._disposed || !this.Matches(eventArgs))
          return;
        bool flag = true;
        Log.Out("{0} matched", (object) this._debug_identity);
        if (this._filterDelegate != null)
        {
          foreach (Predicate<WaiterEventArgs> invocation in this._filterDelegate.GetInvocationList())
          {
            flag = invocation(eventArgs);
            if (!flag)
            {
              Log.Out("{0} filtered out", (object) this._debug_identity);
              break;
            }
          }
        }
        if (!flag)
          return;
        this._eventArgs = eventArgs;
        Log.Out("{0} notifying", (object) this._debug_identity);
        try
        {
          this.OnNotify(eventArgs);
        }
        finally
        {
          this._blockingEvent.Set();
        }
      }
    }

    protected virtual bool Matches(WaiterEventArgs eventArgs) => true;

    public void AddFilter(Predicate<WaiterEventArgs> callback) => this._filterDelegate += callback;

    public void RemoveFilter(Predicate<WaiterEventArgs> callback) => this._filterDelegate -= callback;

    protected virtual void OnNotify(WaiterEventArgs e)
    {
      if (this.Notify == null)
        return;
      this.Notify((object) this, e);
    }

    protected override WaitHandle WaitHandle => (WaitHandle) this._blockingEvent;

    public virtual UIObject Source => this._eventArgs != null && (UIObject) null != this._eventArgs.Sender ? this._factory.Create(this._eventArgs.Sender) : (UIObject) null;

    public WaiterEventArgs Arguments => this._eventArgs;

    public IFactory<UIObject> Factory
    {
      get => this._factory;
      set
      {
        Validate.ArgumentNotNull((object) value, nameof (value));
        this._factory = value;
      }
    }

    public event EventHandler<WaiterEventArgs> Notify;
  }
}
