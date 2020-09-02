// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.ElementRemovedWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters {
    public class ElementRemovedWaiter : UIEventWaiter {
        int[] _runtimeId;

        public ElementRemovedWaiter(UIObject root, Scope scope)
            : base(eventSource: new StructureChangedEventSource(root: root, scope: scope)) {
            Start();
        }

        public ElementRemovedWaiter(UIObject root, Scope scope, UIObject uiObject)
            : base(eventSource: new StructureChangedEventSource(root: root, scope: scope)) {
            Validate.ArgumentNotNull(parameter: uiObject, parameterName: nameof(uiObject));
            Validate.ArgumentNotNull(parameter: uiObject.AutomationElement, parameterName: "uiObject.AutomationElement");
            Initialize(runtimeId: uiObject.AutomationElement.GetRuntimeId());
            Start();
        }

        public ElementRemovedWaiter(UIObject root, Scope scope, string runtimeId)
            : this(root: root, scope: scope, runtimeId: RuntimeId.PartsFromString(runtimeIdString: runtimeId)) {
        }

        public ElementRemovedWaiter(UIObject root, Scope scope, int[] runtimeId)
            : base(eventSource: new StructureChangedEventSource(root: root, scope: scope)) {
            Initialize(runtimeId: runtimeId);
            Start();
        }

        void Initialize(int[] runtimeId) {
            Validate.ArgumentNotNull(parameter: runtimeId, parameterName: nameof(runtimeId));
            this._runtimeId = (int[]) runtimeId.Clone();
        }

        protected override void Start() {
            base.Start();
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing: disposing);
            this._runtimeId = null;
        }

        protected override bool Matches(WaiterEventArgs eventArgs) {
            var eventArgs1 = (StructureChangedEventArgs) eventArgs.EventArgs;
            var flag = false;
            if (eventArgs1.StructureChangeType == StructureChangeType.ChildRemoved) {
                flag = true;
                if (this._runtimeId != null)
                    flag = Automation.Compare(runtimeId1: eventArgs1.GetRuntimeId(), runtimeId2: this._runtimeId);
            }

            return flag;
        }

        public override string ToString() {
            return nameof(ElementRemovedWaiter) + (this._runtimeId != null ? " with RuntimeId: " + RuntimeId.StringFromParts(runtimeIdParts: this._runtimeId) : "");
        }
    }
}