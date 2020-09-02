// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.CompositableWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Text;
using System.Threading;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public abstract class CompositableWaiter : Waiter
  {
    public static CompositableWaiter WaitAny(params CompositableWaiter[] waiters) => CompositableWaiter.WaitAny(Waiter.DefaultTimeout, waiters);

    public static CompositableWaiter WaitAny(
      int timeout,
      params CompositableWaiter[] waiters) => CompositableWaiter.WaitAny(TimeSpan.FromMilliseconds((double) timeout), waiters);

    public static CompositableWaiter WaitAny(
      TimeSpan timeout,
      params CompositableWaiter[] waiters) => CompositableWaiter.TryWaitAny(timeout, waiters) ?? throw new WaiterTimedOutException(StringResource.Get("CompositedWaiterTimedOut_1", (object) CompositableWaiter.WaitersToString((Waiter[]) waiters)));

    public static CompositableWaiter TryWaitAny(
      params CompositableWaiter[] waiters) => CompositableWaiter.TryWaitAny(Waiter.DefaultTimeout, waiters);

    public static CompositableWaiter TryWaitAny(
      int timeout,
      params CompositableWaiter[] waiters) => CompositableWaiter.TryWaitAny(TimeSpan.FromMilliseconds((double) timeout), waiters);

    public static CompositableWaiter TryWaitAny(
      TimeSpan timeout,
      params CompositableWaiter[] waiters)
    {
      Validate.ArgumentNotNull((object) waiters, nameof (waiters));
      WaitHandle[] waitHandles = new WaitHandle[waiters.Length];
      for (int index = 0; index < waiters.Length; ++index)
      {
        Validate.ArgumentNotNull((object) waiters[index], "waiters[count]");
        if (waiters[index].WaitHandle == null)
          throw new WaiterException(StringResource.Get("UnableToWaitAnyOnGivenWaiter"));
        waitHandles[index] = waiters[index].WaitHandle;
      }
      int index1 = WaitHandle.WaitAny(waitHandles, timeout);
      return index1 == 258 ? (CompositableWaiter) null : waiters[index1];
    }

    public static void WaitAll(params CompositableWaiter[] waiters) => CompositableWaiter.WaitAll(Waiter.DefaultTimeout, waiters);

    public static void WaitAll(int timeout, params CompositableWaiter[] waiters) => CompositableWaiter.WaitAll(TimeSpan.FromMilliseconds((double) timeout), waiters);

    public static void WaitAll(TimeSpan timeout, params CompositableWaiter[] waiters)
    {
      if (!CompositableWaiter.TryWaitAll(timeout, waiters))
        throw new WaiterTimedOutException(StringResource.Get("CompositeAllWaiterTimedOut_1", (object) CompositableWaiter.WaitersToString((Waiter[]) waiters)));
    }

    public static bool TryWaitAll(params CompositableWaiter[] waiters) => CompositableWaiter.TryWaitAll(Waiter.DefaultTimeout, waiters);

    public static bool TryWaitAll(int timeout, params CompositableWaiter[] waiters) => CompositableWaiter.TryWaitAll(TimeSpan.FromMilliseconds((double) timeout), waiters);

    public static bool TryWaitAll(TimeSpan timeout, params CompositableWaiter[] waiters)
    {
      Validate.ArgumentNotNull((object) waiters, nameof (waiters));
      WaitHandle[] waitHandles = new WaitHandle[waiters.Length];
      for (int index = 0; index < waiters.Length; ++index)
      {
        Validate.ArgumentNotNull((object) waiters[index], "waiters[count]");
        if (waiters[index].WaitHandle == null)
          throw new WaiterException(StringResource.Get("UnableToWaitAnyOnGivenWaiter"));
        waitHandles[index] = waiters[index].WaitHandle;
      }
      return WaitHandle.WaitAll(waitHandles, timeout);
    }

    private static string WaitersToString(Waiter[] waiters)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (waiters.Length != 0)
      {
        for (int index = 0; index < waiters.Length - 1; ++index)
        {
          stringBuilder.Append(waiters[index].ToString());
          stringBuilder.Append(", ");
        }
        stringBuilder.Append(waiters[waiters.Length - 1].ToString());
      }
      return stringBuilder.ToString();
    }

    protected abstract WaitHandle WaitHandle { get; }
  }
}
