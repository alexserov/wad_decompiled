// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.AppLaunchWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters {
    public class AppLaunchWaiter : Waiter {
        UIObject source;
        readonly UICondition topLevelWindowCondition;
        readonly UIEventWaiter[] waiters;

        public AppLaunchWaiter(UICondition topLevelWindowCondition) {
            this.topLevelWindowCondition = topLevelWindowCondition;
            this.waiters = new UIEventWaiter[2] {
                new WindowOpenedWaiter(condition: topLevelWindowCondition),
                new FocusAcquiredWaiter(condition: topLevelWindowCondition)
            };
        }

        public UIObject Source {
            get { return this.source; }
        }

        public override bool TryWait(TimeSpan timeout) {
            Log.Out(msg: "AppLaunchWaiter.TryWait");
            var flag = false;
            using (var compositeAnyWaiter = new CompositeAnyWaiter(waiters: this.waiters)) {
                flag = compositeAnyWaiter.TryWait(timeout: timeout);
                if (flag)
                    foreach (var waiter in this.waiters)
                        if (waiter == compositeAnyWaiter.Source) {
                            Log.Out(msg: "Found UI elment with UIA events");
                            this.source = waiter.Source;
                            break;
                        }
            }

            if (!flag) {
                Log.Out(msg: "Failed to identify UI element with UIA events, manually searching tree");
                flag = UIObject.Root.Children.TryFind(condition: this.topLevelWindowCondition, element: out this.source);
            }

            return flag;
        }

        public override void Dispose() {
            foreach (Waiter waiter in this.waiters)
                waiter.Dispose();
        }
    }
}