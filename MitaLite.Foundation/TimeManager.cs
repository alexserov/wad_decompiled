// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.TimeManager
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Runtime.InteropServices;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    internal class TimeManager : ITimeManager, IDisposable {
        const uint NOERROR = 0;
        bool disposedValue;
        uint timePeriod = uint.MaxValue;

        public TimeManager() {
            this.timePeriod = GetMinimumTimerResolution();
            BeginPeriod(uPeriod: this.timePeriod);
        }

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(obj: this);
        }

        uint GetMinimumTimerResolution() {
            var timeCaps = new TimeCaps();
            var devCaps = InternalNativeMethods.timeGetDevCaps(timeCaps: ref timeCaps, sizeTimeCaps: (uint) Marshal.SizeOf(structure: timeCaps));
            if (devCaps != 0U)
                throw new Exception(message: string.Format(format: "P/Invoke of timeGetDevCaps returned error {0}", arg0: devCaps));
            return timeCaps.periodMin;
        }

        void BeginPeriod(uint uPeriod) {
            Log.Out(msg: "BeginPeriod with timer resolution {0}", (object) uPeriod);
            var num = InternalNativeMethods.timeBeginPeriod(uPeriod: uPeriod);
            if (num != 0U)
                throw new Exception(message: string.Format(format: "P/Invoke of timeBeginPeriod returned error {0}", arg0: num));
        }

        void EndPeriod(uint uPeriod) {
            Log.Out(msg: "EndPeriod with timer resolution {0}", (object) uPeriod);
            var num = InternalNativeMethods.timeEndPeriod(uPeriod: uPeriod);
            if (num != 0U)
                throw new Exception(message: string.Format(format: "P/Invoke of timeEndPeriod returned error {0}", arg0: num));
        }

        protected virtual void Dispose(bool disposing) {
            if (this.disposedValue)
                return;
            EndPeriod(uPeriod: this.timePeriod);
            this.timePeriod = uint.MaxValue;
            this.disposedValue = true;
        }

        ~TimeManager() {
            Dispose(disposing: false);
        }

        struct TimeCaps {
            public uint periodMin;
            public uint periodMax;
        }

        static class InternalNativeMethods {
            const string WIN_MM_TIME = "api-ms-win-mm-time-l1-1-0.dll";

            [DllImport(dllName: "api-ms-win-mm-time-l1-1-0.dll")]
            public static extern uint timeGetDevCaps([In, Out] ref TimeCaps timeCaps, [In] uint sizeTimeCaps);

            [DllImport(dllName: "api-ms-win-mm-time-l1-1-0.dll")]
            public static extern uint timeBeginPeriod([In] uint uPeriod);

            [DllImport(dllName: "api-ms-win-mm-time-l1-1-0.dll")]
            public static extern uint timeEndPeriod([In] uint uPeriod);
        }
    }
}