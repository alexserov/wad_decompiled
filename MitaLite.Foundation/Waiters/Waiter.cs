// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.Waiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public abstract class Waiter : IDisposable
  {
    internal string _debug_identity = Log.CreateUniqueObjectId(nameof (Waiter));
    private static TimeSpan _defaultTimeout = TimeSpan.FromSeconds(10.0);
    private Exception _innerException;

    public virtual void Wait() => this.Wait(Waiter.DefaultTimeout);

    public void Wait(int timeout) => this.Wait(TimeSpan.FromMilliseconds((double) timeout));

    public void Wait(TimeSpan timeout)
    {
      this.InnerException = (Exception) null;
      if (!this.TryWait(timeout))
        throw new WaiterTimedOutException(StringResource.Get("WaiterTimedOut_1", (object) this.ToString()), this.InnerException);
    }

    public virtual bool TryWait() => this.TryWait(Waiter.DefaultTimeout);

    public bool TryWait(int timeout) => this.TryWait(TimeSpan.FromMilliseconds((double) timeout));

    public abstract bool TryWait(TimeSpan timeout);

    public virtual void Reset()
    {
    }

    public static TimeSpan DefaultTimeout
    {
      get => Waiter._defaultTimeout;
      set => Waiter._defaultTimeout = value;
    }

    public virtual Exception InnerException
    {
      get => this._innerException;
      set => this._innerException = value;
    }

    public abstract void Dispose();
  }
}
