// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.CompositeAnyWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Text;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters {
    public class CompositeAnyWaiter : Waiter {
        readonly CompositableWaiter[] _waiters;

        public CompositeAnyWaiter(params CompositableWaiter[] waiters) {
            Validate.ArgumentNotNull(parameter: waiters, parameterName: nameof(waiters));
            this._waiters = waiters;
            this.Source = null;
        }

        public CompositableWaiter Source { get; set; }

        public override bool TryWait(TimeSpan timeout) {
            this.Source = CompositableWaiter.TryWaitAny(timeout: timeout, waiters: this._waiters);
            return this.Source != null;
        }

        public override string ToString() {
            var stringBuilder = new StringBuilder(value: "CompositeAllWaiter with sub-waiters:  ");
            for (var index = 0; index < this._waiters.Length; ++index) {
                stringBuilder.Append(value: this._waiters[index]);
                if (index != this._waiters.Length - 1)
                    stringBuilder.Append(value: ", ");
            }

            return stringBuilder.ToString();
        }

        public override void Reset() {
            foreach (Waiter waiter in this._waiters)
                waiter.Reset();
        }

        public override void Dispose() {
            foreach (Waiter waiter in this._waiters)
                waiter.Dispose();
            if (this.Source != null)
                this.Source.Dispose();
            GC.SuppressFinalize(obj: this);
        }
    }
}