﻿// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.WindowOpenedWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public class WindowOpenedWaiter : UIEventWaiter
  {
    private UICondition _condition;

    public WindowOpenedWaiter()
      : this(UICondition.True)
    {
    }

    public WindowOpenedWaiter(string automationId)
      : this(UICondition.CreateFromId(automationId))
    {
    }

    public WindowOpenedWaiter(UIProperty uiProperty, string value)
      : this(UICondition.Create(uiProperty, (object) value))
    {
    }

    public WindowOpenedWaiter(UICondition condition)
      : this(UIObject.Root, Scope.Descendants, condition)
    {
    }

    public WindowOpenedWaiter(UIObject rootElement, Scope scope, UICondition condition)
      : base((IEventSource) new AutomationEventSource(WindowPattern.WindowOpenedEvent, rootElement, scope))
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) condition, nameof (condition));
      this._condition = condition;
      this.Start();
    }

    protected override void Start() => base.Start();

    protected override void Dispose(bool disposing) => base.Dispose(disposing);

    protected override bool Matches(WaiterEventArgs eventArgs) => (UIObject) null != eventArgs.Sender && UIObject.Matches(eventArgs.Sender, this._condition);

    public override string ToString() => "WindowOpenedWaiter with Condition:  " + this._condition.ToString();
  }
}
