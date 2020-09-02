// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.TimeWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Globalization;
using System.Threading;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public class TimeWaiter : Waiter
  {
    private TimeSpan _timeout;

    public TimeWaiter()
      : this(Waiter.DefaultTimeout)
    {
    }

    public TimeWaiter(int timeout)
      : this(TimeSpan.FromMilliseconds((double) timeout))
    {
    }

    public TimeWaiter(TimeSpan timeout) => this._timeout = timeout;

    public override void Wait() => this.Wait(this._timeout);

    public override bool TryWait() => this.TryWait(this._timeout);

    public override bool TryWait(TimeSpan timeout)
    {
      Thread.Sleep(timeout);
      return true;
    }

    public override string ToString() => "TimeWaiter with timeout:  " + Convert.ToString((object) this._timeout, (IFormatProvider) CultureInfo.InvariantCulture);

    public override void Dispose()
    {
    }
  }
}
