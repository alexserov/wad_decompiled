// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.ElementAddedWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public class ElementAddedWaiter : UIEventWaiter
  {
    private UICondition _condition;

    public ElementAddedWaiter(UIObject root, Scope scope)
      : this(root, scope, UICondition.True)
    {
    }

    public ElementAddedWaiter(UIObject root, Scope scope, string automationId)
      : this(root, scope, UICondition.CreateFromId(automationId))
    {
    }

    public ElementAddedWaiter(UIObject root, Scope scope, UIProperty uiProperty, object value)
      : this(root, scope, UICondition.Create(uiProperty, value))
    {
    }

    public ElementAddedWaiter(UIObject root, Scope scope, UICondition condition)
      : base((IEventSource) new StructureChangedEventSource(root, scope))
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) condition, nameof (condition));
      this._condition = condition;
      this.Start();
    }

    protected override void Start() => base.Start();

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      this._condition = (UICondition) null;
    }

    protected override bool Matches(WaiterEventArgs eventArgs)
    {
      StructureChangedEventArgs eventArgs1 = (StructureChangedEventArgs) eventArgs.EventArgs;
      bool flag = false;
      if (eventArgs1.StructureChangeType == StructureChangeType.ChildAdded)
        flag = UIObject.Matches(eventArgs.Sender, this._condition);
      return flag;
    }

    public override string ToString() => "ElementAddedWaiter with Condition:  " + this._condition.ToString();
  }
}
