// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.CompositeAllWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Text;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public class CompositeAllWaiter : Waiter
  {
    private CompositableWaiter[] _waiters;

    public CompositeAllWaiter(params CompositableWaiter[] waiters)
    {
      Validate.ArgumentNotNull((object) waiters, nameof (waiters));
      this._waiters = waiters;
    }

    public override bool TryWait(TimeSpan timeout) => CompositableWaiter.TryWaitAll(timeout, this._waiters);

    public override void Reset()
    {
      foreach (Waiter waiter in this._waiters)
        waiter.Reset();
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

    public override void Dispose()
    {
      foreach (Waiter waiter in this._waiters)
        waiter.Dispose();
      GC.SuppressFinalize((object) this);
    }
  }
}
