// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.WindowClosedWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public class WindowClosedWaiter : UIEventWaiter
  {
    private string _uiObjectDescription;

    public WindowClosedWaiter()
      : base((IEventSource) new AutomationEventSource(WindowPattern.WindowClosedEvent, UIObject.Root, Scope.Subtree))
    {
      this._uiObjectDescription = UIObject.Root.ToString();
      this.Start();
    }

    public WindowClosedWaiter(UIObject root)
      : base((IEventSource) new AutomationEventSource(WindowPattern.WindowClosedEvent, root, Scope.Element))
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) root, "uiObject");
      this._uiObjectDescription = root.ToString();
      this.Start();
    }

    protected override void Start() => base.Start();

    protected override void Dispose(bool disposing) => base.Dispose(disposing);

    public override string ToString() => "WindowClosedWaiter for element " + this._uiObjectDescription;
  }
}
