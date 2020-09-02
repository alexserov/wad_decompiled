// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.CompositeAnyWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Text;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public class CompositeAnyWaiter : Waiter
  {
    private CompositableWaiter[] _waiters;
    private CompositableWaiter _source;

    public CompositeAnyWaiter(params CompositableWaiter[] waiters)
    {
      Validate.ArgumentNotNull((object) waiters, nameof (waiters));
      this._waiters = waiters;
      this._source = (CompositableWaiter) null;
    }

    public override bool TryWait(TimeSpan timeout)
    {
      this._source = CompositableWaiter.TryWaitAny(timeout, this._waiters);
      return this._source != null;
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder("CompositeAllWaiter with sub-waiters:  ");
      for (int index = 0; index < this._waiters.Length; ++index)
      {
        stringBuilder.Append(this._waiters[index].ToString());
        if (index != this._waiters.Length - 1)
          stringBuilder.Append(", ");
      }
      return stringBuilder.ToString();
    }

    public override void Reset()
    {
      foreach (Waiter waiter in this._waiters)
        waiter.Reset();
    }

    public CompositableWaiter Source => this._source;

    public override void Dispose()
    {
      foreach (Waiter waiter in this._waiters)
        waiter.Dispose();
      if (this._source != null)
        this._source.Dispose();
      GC.SuppressFinalize((object) this);
    }
  }
}
