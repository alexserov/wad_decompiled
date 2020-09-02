// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.TimeWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Globalization;
using System.Threading;

namespace MS.Internal.Mita.Foundation.Waiters {
    public class TimeWaiter : Waiter {
        readonly TimeSpan _timeout;

        public TimeWaiter()
            : this(timeout: DefaultTimeout) {
        }

        public TimeWaiter(int timeout)
            : this(timeout: TimeSpan.FromMilliseconds(value: timeout)) {
        }

        public TimeWaiter(TimeSpan timeout) {
            this._timeout = timeout;
        }

        public override void Wait() {
            Wait(timeout: this._timeout);
        }

        public override bool TryWait() {
            return TryWait(timeout: this._timeout);
        }

        public override bool TryWait(TimeSpan timeout) {
            Thread.Sleep(timeout: timeout);
            return true;
        }

        public override string ToString() {
            return "TimeWaiter with timeout:  " + Convert.ToString(value: this._timeout, provider: CultureInfo.InvariantCulture);
        }

        public override void Dispose() {
        }
    }
}