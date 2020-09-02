// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.TimeManager
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Runtime.InteropServices;

namespace MS.Internal.Mita.Foundation
{
  internal class TimeManager : ITimeManager, IDisposable
  {
    private const uint NOERROR = 0;
    private uint timePeriod = uint.MaxValue;
    private bool disposedValue;

    public TimeManager()
    {
      this.timePeriod = this.GetMinimumTimerResolution();
      this.BeginPeriod(this.timePeriod);
    }

    private uint GetMinimumTimerResolution()
    {
      TimeManager.TimeCaps timeCaps = new TimeManager.TimeCaps();
      uint devCaps = TimeManager.InternalNativeMethods.timeGetDevCaps(ref timeCaps, (uint) Marshal.SizeOf<TimeManager.TimeCaps>(timeCaps));
      if (devCaps != 0U)
        throw new Exception(string.Format("P/Invoke of timeGetDevCaps returned error {0}", (object) devCaps));
      return timeCaps.periodMin;
    }

    private void BeginPeriod(uint uPeriod)
    {
      Log.Out("BeginPeriod with timer resolution {0}", (object) uPeriod);
      uint num = TimeManager.InternalNativeMethods.timeBeginPeriod(uPeriod);
      if (num != 0U)
        throw new Exception(string.Format("P/Invoke of timeBeginPeriod returned error {0}", (object) num));
    }

    private void EndPeriod(uint uPeriod)
    {
      Log.Out("EndPeriod with timer resolution {0}", (object) uPeriod);
      uint num = TimeManager.InternalNativeMethods.timeEndPeriod(uPeriod);
      if (num != 0U)
        throw new Exception(string.Format("P/Invoke of timeEndPeriod returned error {0}", (object) num));
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposedValue)
        return;
      this.EndPeriod(this.timePeriod);
      this.timePeriod = uint.MaxValue;
      this.disposedValue = true;
    }

    ~TimeManager() => this.Dispose(false);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private struct TimeCaps
    {
      public uint periodMin;
      public uint periodMax;
    }

    private static class InternalNativeMethods
    {
      private const string WIN_MM_TIME = "api-ms-win-mm-time-l1-1-0.dll";

      [DllImport("api-ms-win-mm-time-l1-1-0.dll")]
      public static extern uint timeGetDevCaps([In, Out] ref TimeManager.TimeCaps timeCaps, [In] uint sizeTimeCaps);

      [DllImport("api-ms-win-mm-time-l1-1-0.dll")]
      public static extern uint timeBeginPeriod([In] uint uPeriod);

      [DllImport("api-ms-win-mm-time-l1-1-0.dll")]
      public static extern uint timeEndPeriod([In] uint uPeriod);
    }
  }
}
