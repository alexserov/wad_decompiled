// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.ToolTipClosedWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public class ToolTipClosedWaiter : UIEventWaiter
  {
    private string _uiObjectDescription;

    public ToolTipClosedWaiter()
      : this(UIObject.Root, Scope.Descendants)
    {
    }

    public ToolTipClosedWaiter(UIObject root)
      : this(root, Scope.Element)
    {
    }

    public ToolTipClosedWaiter(UIObject root, Scope scope)
      : base((IEventSource) new AutomationEventSource(AutomationElement.ToolTipClosedEvent, root, scope))
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) root, nameof (root));
      this._uiObjectDescription = root.ToString();
      this.Start();
    }

    protected override void Start() => base.Start();

    protected override void Dispose(bool disposing) => base.Dispose(disposing);

    public override string ToString() => "ToolTipClosedWaiter for element " + this._uiObjectDescription;
  }
}
