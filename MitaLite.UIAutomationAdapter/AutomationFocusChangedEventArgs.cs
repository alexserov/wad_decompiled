// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationFocusChangedEventArgs
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation
{
  public class AutomationFocusChangedEventArgs : AutomationEventArgs
  {
    private int _childId;
    private int _objectId;

    public AutomationFocusChangedEventArgs(int idObject, int idChild)
      : base(AutomationElement.AutomationFocusChangedEvent)
    {
      Validate.ArgumentNotNull((object) idObject, nameof (idObject));
      Validate.ArgumentNotNull((object) idChild, nameof (idChild));
      this._objectId = idObject;
      this._childId = idChild;
    }

    public int ObjectId => this._objectId;

    public int ChildId => this._childId;
  }
}
