// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.ElementAddedWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters {
    public class ElementAddedWaiter : UIEventWaiter {
        UICondition _condition;

        public ElementAddedWaiter(UIObject root, Scope scope)
            : this(root: root, scope: scope, condition: UICondition.True) {
        }

        public ElementAddedWaiter(UIObject root, Scope scope, string automationId)
            : this(root: root, scope: scope, condition: UICondition.CreateFromId(automationId: automationId)) {
        }

        public ElementAddedWaiter(UIObject root, Scope scope, UIProperty uiProperty, object value)
            : this(root: root, scope: scope, condition: UICondition.Create(property: uiProperty, value: value)) {
        }

        public ElementAddedWaiter(UIObject root, Scope scope, UICondition condition)
            : base(eventSource: new StructureChangedEventSource(root: root, scope: scope)) {
            Validate.ArgumentNotNull(parameter: condition, parameterName: nameof(condition));
            this._condition = condition;
            Start();
        }

        protected override void Start() {
            base.Start();
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing: disposing);
            this._condition = null;
        }

        protected override bool Matches(WaiterEventArgs eventArgs) {
            var eventArgs1 = (StructureChangedEventArgs) eventArgs.EventArgs;
            var flag = false;
            if (eventArgs1.StructureChangeType == StructureChangeType.ChildAdded)
                flag = UIObject.Matches(uiObject: eventArgs.Sender, condition: this._condition);
            return flag;
        }

        public override string ToString() {
            return "ElementAddedWaiter with Condition:  " + this._condition;
        }
    }
}