// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.MenuClosedWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public class MenuClosedWaiter : UIEventWaiter
  {
    private string _uiObjectDescription;

    public MenuClosedWaiter()
      : base((IEventSource) new AutomationEventSource(AutomationElement.MenuClosedEvent, UIObject.Root, Scope.Subtree))
    {
      this._uiObjectDescription = UIObject.Root.ToString();
      this.Start();
    }

    public MenuClosedWaiter(UIObject root)
      : base((IEventSource) new AutomationEventSource(AutomationElement.MenuClosedEvent, root, Scope.Element))
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) root, "uiObject");
      this._uiObjectDescription = root.ToString();
      this.Start();
    }

    protected override void Start() => base.Start();

    protected override void Dispose(bool disposing) => base.Dispose(disposing);

    public override string ToString() => "MenuClosedWaiter for element " + this._uiObjectDescription;
  }
}
