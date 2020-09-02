// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.FocusAcquiredWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public class FocusAcquiredWaiter : UIEventWaiter
  {
    private UICondition _condition;

    public FocusAcquiredWaiter()
      : this(UICondition.True)
    {
    }

    public FocusAcquiredWaiter(string automationId)
      : this(UICondition.CreateFromId(automationId))
    {
    }

    public FocusAcquiredWaiter(UIProperty uiProperty, string value)
      : this(UICondition.Create(uiProperty, (object) value))
    {
    }

    public FocusAcquiredWaiter(UICondition condition)
      : base((IEventSource) new FocusChangedEventSource())
    {
      Validate.ArgumentNotNull((object) condition, nameof (condition));
      this._condition = condition;
      this.Start();
    }

    protected override void Start() => base.Start();

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      this._condition = (UICondition) null;
    }

    protected override bool Matches(WaiterEventArgs eventArgs) => UIObject.Matches(eventArgs.Sender, this._condition);

    public override string ToString() => "FocusAcquiredWaiter with Condition:  " + this._condition.ToString();
  }
}
