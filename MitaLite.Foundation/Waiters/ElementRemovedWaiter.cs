// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.ElementRemovedWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public class ElementRemovedWaiter : UIEventWaiter
  {
    private int[] _runtimeId;

    public ElementRemovedWaiter(UIObject root, Scope scope)
      : base((IEventSource) new StructureChangedEventSource(root, scope))
      => this.Start();

    public ElementRemovedWaiter(UIObject root, Scope scope, UIObject uiObject)
      : base((IEventSource) new StructureChangedEventSource(root, scope))
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) uiObject, nameof (uiObject));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) uiObject.AutomationElement, "uiObject.AutomationElement");
      this.Initialize(uiObject.AutomationElement.GetRuntimeId());
      this.Start();
    }

    public ElementRemovedWaiter(UIObject root, Scope scope, string runtimeId)
      : this(root, scope, RuntimeId.PartsFromString(runtimeId))
    {
    }

    public ElementRemovedWaiter(UIObject root, Scope scope, int[] runtimeId)
      : base((IEventSource) new StructureChangedEventSource(root, scope))
    {
      this.Initialize(runtimeId);
      this.Start();
    }

    private void Initialize(int[] runtimeId)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) runtimeId, nameof (runtimeId));
      this._runtimeId = (int[]) runtimeId.Clone();
    }

    protected override void Start() => base.Start();

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      this._runtimeId = (int[]) null;
    }

    protected override bool Matches(WaiterEventArgs eventArgs)
    {
      StructureChangedEventArgs eventArgs1 = (StructureChangedEventArgs) eventArgs.EventArgs;
      bool flag = false;
      if (eventArgs1.StructureChangeType == StructureChangeType.ChildRemoved)
      {
        flag = true;
        if (this._runtimeId != null)
          flag = System.Windows.Automation.Automation.Compare(eventArgs1.GetRuntimeId(), this._runtimeId);
      }
      return flag;
    }

    public override string ToString() => nameof (ElementRemovedWaiter) + (this._runtimeId != null ? " with RuntimeId: " + RuntimeId.StringFromParts(this._runtimeId) : "");
  }
}
