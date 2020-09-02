// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.WaiterEventArgs
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Waiters {
    public class WaiterEventArgs : EventArgs {
        internal WaiterEventArgs(object sender, EventArgs eventArgs) {
            EventArgs = eventArgs;
            var element = sender as AutomationElement;
            if (!(element != null))
                return;
            Sender = new UIObject(element: element);
        }

        public UIObject Sender { get; }

        public EventArgs EventArgs { get; }
    }
}