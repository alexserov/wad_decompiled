// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.CompositableWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Text;
using System.Threading;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters {
    public abstract class CompositableWaiter : Waiter {
        protected abstract WaitHandle WaitHandle { get; }

        public static CompositableWaiter WaitAny(params CompositableWaiter[] waiters) {
            return WaitAny(timeout: DefaultTimeout, waiters: waiters);
        }

        public static CompositableWaiter WaitAny(
            int timeout,
            params CompositableWaiter[] waiters) {
            return WaitAny(timeout: TimeSpan.FromMilliseconds(value: timeout), waiters: waiters);
        }

        public static CompositableWaiter WaitAny(
            TimeSpan timeout,
            params CompositableWaiter[] waiters) {
            return TryWaitAny(timeout: timeout, waiters: waiters) ?? throw new WaiterTimedOutException(message: StringResource.Get(id: "CompositedWaiterTimedOut_1", (object) WaitersToString(waiters: waiters)));
        }

        public static CompositableWaiter TryWaitAny(
            params CompositableWaiter[] waiters) {
            return TryWaitAny(timeout: DefaultTimeout, waiters: waiters);
        }

        public static CompositableWaiter TryWaitAny(
            int timeout,
            params CompositableWaiter[] waiters) {
            return TryWaitAny(timeout: TimeSpan.FromMilliseconds(value: timeout), waiters: waiters);
        }

        public static CompositableWaiter TryWaitAny(
            TimeSpan timeout,
            params CompositableWaiter[] waiters) {
            Validate.ArgumentNotNull(parameter: waiters, parameterName: nameof(waiters));
            var waitHandles = new WaitHandle[waiters.Length];
            for (var index = 0; index < waiters.Length; ++index) {
                Validate.ArgumentNotNull(parameter: waiters[index], parameterName: "waiters[count]");
                if (waiters[index].WaitHandle == null)
                    throw new WaiterException(message: StringResource.Get(id: "UnableToWaitAnyOnGivenWaiter"));
                waitHandles[index] = waiters[index].WaitHandle;
            }

            var index1 = WaitHandle.WaitAny(waitHandles: waitHandles, timeout: timeout);
            return index1 == 258 ? null : waiters[index1];
        }

        public static void WaitAll(params CompositableWaiter[] waiters) {
            WaitAll(timeout: DefaultTimeout, waiters: waiters);
        }

        public static void WaitAll(int timeout, params CompositableWaiter[] waiters) {
            WaitAll(timeout: TimeSpan.FromMilliseconds(value: timeout), waiters: waiters);
        }

        public static void WaitAll(TimeSpan timeout, params CompositableWaiter[] waiters) {
            if (!TryWaitAll(timeout: timeout, waiters: waiters))
                throw new WaiterTimedOutException(message: StringResource.Get(id: "CompositeAllWaiterTimedOut_1", (object) WaitersToString(waiters: waiters)));
        }

        public static bool TryWaitAll(params CompositableWaiter[] waiters) {
            return TryWaitAll(timeout: DefaultTimeout, waiters: waiters);
        }

        public static bool TryWaitAll(int timeout, params CompositableWaiter[] waiters) {
            return TryWaitAll(timeout: TimeSpan.FromMilliseconds(value: timeout), waiters: waiters);
        }

        public static bool TryWaitAll(TimeSpan timeout, params CompositableWaiter[] waiters) {
            Validate.ArgumentNotNull(parameter: waiters, parameterName: nameof(waiters));
            var waitHandles = new WaitHandle[waiters.Length];
            for (var index = 0; index < waiters.Length; ++index) {
                Validate.ArgumentNotNull(parameter: waiters[index], parameterName: "waiters[count]");
                if (waiters[index].WaitHandle == null)
                    throw new WaiterException(message: StringResource.Get(id: "UnableToWaitAnyOnGivenWaiter"));
                waitHandles[index] = waiters[index].WaitHandle;
            }

            return WaitHandle.WaitAll(waitHandles: waitHandles, timeout: timeout);
        }

        static string WaitersToString(Waiter[] waiters) {
            var stringBuilder = new StringBuilder();
            if (waiters.Length != 0) {
                for (var index = 0; index < waiters.Length - 1; ++index) {
                    stringBuilder.Append(value: waiters[index]);
                    stringBuilder.Append(value: ", ");
                }

                stringBuilder.Append(value: waiters[waiters.Length - 1]);
            }

            return stringBuilder.ToString();
        }
    }
}