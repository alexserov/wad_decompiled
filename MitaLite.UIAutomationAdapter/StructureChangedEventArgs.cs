// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.StructureChangedEventArgs
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation {
    public sealed class StructureChangedEventArgs : AutomationEventArgs {
        readonly int[] _runtimeId;

        public StructureChangedEventArgs(StructureChangeType structureChangeType, int[] runtimeId)
            : base(eventId: AutomationElement.StructureChangedEvent) {
            Validate.ArgumentNotNull(parameter: runtimeId, parameterName: nameof(runtimeId));
            StructureChangeType = structureChangeType;
            this._runtimeId = (int[]) runtimeId.Clone();
        }

        public StructureChangeType StructureChangeType { get; }

        public int[] GetRuntimeId() {
            return (int[]) this._runtimeId.Clone();
        }
    }
}