// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.WaiterEventArgs
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public class WaiterEventArgs : EventArgs
  {
    private UIObject _sender;
    private EventArgs _eventArgs;

    internal WaiterEventArgs(object sender, EventArgs eventArgs)
    {
      this._eventArgs = eventArgs;
      AutomationElement element = sender as AutomationElement;
      if (!(element != (AutomationElement) null))
        return;
      this._sender = new UIObject(element);
    }

    public UIObject Sender => this._sender;

    public EventArgs EventArgs => this._eventArgs;
  }
}
