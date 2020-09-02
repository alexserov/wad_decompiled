// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.StructureChangedEventArgs
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation
{
  public sealed class StructureChangedEventArgs : AutomationEventArgs
  {
    private int[] _runtimeId;
    private StructureChangeType _structureChangeType;

    public StructureChangedEventArgs(StructureChangeType structureChangeType, int[] runtimeId)
      : base(AutomationElement.StructureChangedEvent)
    {
      Validate.ArgumentNotNull((object) runtimeId, nameof (runtimeId));
      this._structureChangeType = structureChangeType;
      this._runtimeId = (int[]) runtimeId.Clone();
    }

    public int[] GetRuntimeId() => (int[]) this._runtimeId.Clone();

    public StructureChangeType StructureChangeType => this._structureChangeType;
  }
}
