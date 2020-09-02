// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.ProcessClosedWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters {
    public class ProcessClosedWaiter : CompositableWaiter, IDisposable {
        readonly object _lockObject = new object();
        ManualResetEvent _blockingProcessEvent;
        bool _disposed;
        readonly bool _isUserProcess;
        Process _process;

        public ProcessClosedWaiter(string applicationName) {
            Initialize(process: Process.GetProcessesByName(processName: applicationName).First());
        }

        public ProcessClosedWaiter(UIObject applicationUIObject) {
            Validate.ArgumentNotNull(parameter: applicationUIObject, parameterName: nameof(applicationUIObject));
            Initialize(process: Process.GetProcessById(processId: applicationUIObject.ProcessId));
        }

        public ProcessClosedWaiter(int processId) {
            Initialize(process: Process.GetProcessById(processId: processId));
        }

        public ProcessClosedWaiter(Process process) {
            this._isUserProcess = true;
            Initialize(process: process);
        }

        protected override WaitHandle WaitHandle {
            get { return this._blockingProcessEvent; }
        }

        public override void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(obj: this);
        }

        void Initialize(Process process) {
            Validate.ArgumentNotNull(parameter: process, parameterName: nameof(process));
            this._blockingProcessEvent = new ManualResetEvent(initialState: false);
            this._process = process;
            this._process.EnableRaisingEvents = true;
            this._process.Exited += Handler;
            if (!this._process.HasExited)
                return;
            Log.Out(msg: "The process has already exited.");
            Handler(sender: null, e: null);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposing)
                return;
            lock (this._lockObject) {
                if (this._disposed)
                    return;
                this._blockingProcessEvent.Dispose();
                if (!this._isUserProcess)
                    this._process.Dispose();
                this._process = null;
                this._blockingProcessEvent = null;
                this._disposed = true;
            }
        }

        public override bool TryWait(TimeSpan timeout) {
            var flag = true;
            if (!this._blockingProcessEvent.WaitOne(timeout: timeout))
                flag = false;
            return flag;
        }

        void Handler(object sender, EventArgs e) {
            Log.Out(msg: "{0} saw event {1}", (object) ToString(), e != null ? (object) e.ToString() : (object) "null");
            lock (this._lockObject) {
                if (this._disposed)
                    return;
                Log.Out(msg: "{0} notifying", (object) ToString());
                this._blockingProcessEvent.Set();
            }
        }

        public override string ToString() {
            var str = string.Format(format: "ProcessClosedWaiter ({0}) with processID: ", arg0: this._debug_identity);
            return this._process != null ? str + this._process.Id.ToString(provider: CultureInfo.InvariantCulture) : str + "NULL";
        }
    }
}