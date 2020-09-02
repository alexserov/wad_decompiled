// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.AppLaunchWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public class AppLaunchWaiter : Waiter
  {
    private UICondition topLevelWindowCondition;
    private UIEventWaiter[] waiters;
    private UIObject source;

    public AppLaunchWaiter(UICondition topLevelWindowCondition)
    {
      this.topLevelWindowCondition = topLevelWindowCondition;
      this.waiters = new UIEventWaiter[2]
      {
        (UIEventWaiter) new WindowOpenedWaiter(topLevelWindowCondition),
        (UIEventWaiter) new FocusAcquiredWaiter(topLevelWindowCondition)
      };
    }

    public override bool TryWait(TimeSpan timeout)
    {
      Log.Out("AppLaunchWaiter.TryWait");
      bool flag = false;
      using (CompositeAnyWaiter compositeAnyWaiter = new CompositeAnyWaiter((CompositableWaiter[]) this.waiters))
      {
        flag = compositeAnyWaiter.TryWait(timeout);
        if (flag)
        {
          foreach (UIEventWaiter waiter in this.waiters)
          {
            if (waiter == compositeAnyWaiter.Source)
            {
              Log.Out("Found UI elment with UIA events");
              this.source = waiter.Source;
              break;
            }
          }
        }
      }
      if (!flag)
      {
        Log.Out("Failed to identify UI element with UIA events, manually searching tree");
        flag = UIObject.Root.Children.TryFind(this.topLevelWindowCondition, out this.source);
      }
      return flag;
    }

    public UIObject Source => this.source;

    public override void Dispose()
    {
      foreach (Waiter waiter in this.waiters)
        waiter.Dispose();
    }
  }
}
