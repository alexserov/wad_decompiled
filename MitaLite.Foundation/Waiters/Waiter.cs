// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.Waiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters {
    public abstract class Waiter : IDisposable {
        static TimeSpan _defaultTimeout = TimeSpan.FromSeconds(value: 10.0);
        internal string _debug_identity = Log.CreateUniqueObjectId(baseName: nameof(Waiter));

        public static TimeSpan DefaultTimeout {
            get { return _defaultTimeout; }
            set { _defaultTimeout = value; }
        }

        public virtual Exception InnerException { get; set; }

        public abstract void Dispose();

        public virtual void Wait() => Wait(timeout: DefaultTimeout);

        public void Wait(int timeout) => Wait(timeout: TimeSpan.FromMilliseconds(value: timeout));

        public void Wait(TimeSpan timeout) {
            InnerException = null;
            if (!TryWait(timeout: timeout))
                throw new WaiterTimedOutException(message: StringResource.Get(id: "WaiterTimedOut_1", (object) ToString()), innerException: InnerException);
        }

        public virtual bool TryWait() => TryWait(timeout: DefaultTimeout);

        public bool TryWait(int timeout) => TryWait(timeout: TimeSpan.FromMilliseconds(value: timeout));

        public abstract bool TryWait(TimeSpan timeout);

        public virtual void Reset() {
        }
    }
}