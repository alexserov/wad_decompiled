// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.AutomationEventWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public class AutomationEventWaiter : UIEventWaiter
  {
    private UICondition _condition;

    public AutomationEventWaiter(AutomationEvent eventId, UIObject uiObject, Scope scope)
      : this(eventId, uiObject, scope, UICondition.True)
    {
    }

    public AutomationEventWaiter(
      AutomationEvent eventId,
      UIObject uiObject,
      Scope scope,
      UICondition condition)
      : base((IEventSource) new AutomationEventSource(eventId, uiObject, scope))
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) condition, nameof (condition));
      this._condition = condition;
      this.Start();
    }

    protected override void Start() => base.Start();

    protected override void Dispose(bool disposing) => base.Dispose(disposing);

    protected override bool Matches(WaiterEventArgs eventArgs)
    {
      bool flag = true;
      if ((UIObject) null != eventArgs.Sender)
        flag = UIObject.Matches(eventArgs.Sender, this._condition);
      return flag;
    }
  }
}
