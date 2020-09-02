// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.EventSource
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters {
    public abstract class EventSource : IEventSource, IDisposable {
        readonly string _debug_identity = Log.CreateUniqueObjectId(baseName: nameof(EventSource));

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(obj: this);
        }

        public abstract void Start(IEventSink sink);

        public abstract void Stop();

        public abstract bool IsStarted { get; }

        ~EventSource() {
            Dispose(disposing: false);
        }

        public override string ToString() {
            return this._debug_identity;
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposing)
                return;
            Stop();
        }
    }
}