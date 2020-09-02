// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.ProcessClosedWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public class ProcessClosedWaiter : CompositableWaiter, IDisposable
  {
    private readonly object _lockObject = new object();
    private bool _isUserProcess;
    private Process _process;
    private ManualResetEvent _blockingProcessEvent;
    private bool _disposed;

    public ProcessClosedWaiter(string applicationName) => this.Initialize(((IEnumerable<Process>) Process.GetProcessesByName(applicationName)).First<Process>());

    public ProcessClosedWaiter(UIObject applicationUIObject)
    {
      Validate.ArgumentNotNull((object) applicationUIObject, nameof (applicationUIObject));
      this.Initialize(Process.GetProcessById(applicationUIObject.ProcessId));
    }

    public ProcessClosedWaiter(int processId) => this.Initialize(Process.GetProcessById(processId));

    public ProcessClosedWaiter(Process process)
    {
      this._isUserProcess = true;
      this.Initialize(process);
    }

    private void Initialize(Process process)
    {
      Validate.ArgumentNotNull((object) process, nameof (process));
      this._blockingProcessEvent = new ManualResetEvent(false);
      this._process = process;
      this._process.EnableRaisingEvents = true;
      this._process.Exited += new EventHandler(this.Handler);
      if (!this._process.HasExited)
        return;
      Log.Out("The process has already exited.");
      this.Handler((object) null, (EventArgs) null);
    }

    public override void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      lock (this._lockObject)
      {
        if (this._disposed)
          return;
        this._blockingProcessEvent.Dispose();
        if (!this._isUserProcess)
          this._process.Dispose();
        this._process = (Process) null;
        this._blockingProcessEvent = (ManualResetEvent) null;
        this._disposed = true;
      }
    }

    public override bool TryWait(TimeSpan timeout)
    {
      bool flag = true;
      if (!this._blockingProcessEvent.WaitOne(timeout))
        flag = false;
      return flag;
    }

    private void Handler(object sender, EventArgs e)
    {
      Log.Out("{0} saw event {1}", (object) this.ToString(), e != null ? (object) e.ToString() : (object) "null");
      lock (this._lockObject)
      {
        if (this._disposed)
          return;
        Log.Out("{0} notifying", (object) this.ToString());
        this._blockingProcessEvent.Set();
      }
    }

    public override string ToString()
    {
      string str = string.Format("ProcessClosedWaiter ({0}) with processID: ", (object) this._debug_identity);
      return this._process != null ? str + this._process.Id.ToString((IFormatProvider) CultureInfo.InvariantCulture) : str + "NULL";
    }

    protected override WaitHandle WaitHandle => (WaitHandle) this._blockingProcessEvent;
  }
}
