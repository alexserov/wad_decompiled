// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.EventSource
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public abstract class EventSource : IEventSource, IDisposable
  {
    private string _debug_identity = Log.CreateUniqueObjectId(nameof (EventSource));

    ~EventSource() => this.Dispose(false);

    public override string ToString() => this._debug_identity;

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      this.Stop();
    }

    public abstract void Start(IEventSink sink);

    public abstract void Stop();

    public abstract bool IsStarted { get; }
  }
}
