// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.MenuOpenedWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters {
    public class MenuOpenedWaiter : UIEventWaiter {
        readonly UICondition _condition;

        public MenuOpenedWaiter()
            : this(condition: UICondition.True) {
        }

        public MenuOpenedWaiter(string automationId)
            : this(condition: UICondition.CreateFromId(automationId: automationId)) {
        }

        public MenuOpenedWaiter(UIProperty uiProperty, object value)
            : this(condition: UICondition.Create(property: uiProperty, value: value)) {
        }

        public MenuOpenedWaiter(UICondition condition)
            : this(rootElement: UIObject.Root, scope: Scope.Descendants, condition: condition) {
        }

        public MenuOpenedWaiter(UIObject rootElement, Scope scope, UICondition condition)
            : base(eventSource: new AutomationEventSource(eventId: AutomationElement.MenuOpenedEvent, root: rootElement, scope: scope)) {
            Validate.ArgumentNotNull(parameter: condition, parameterName: nameof(condition));
            this._condition = condition;
            Start();
        }

        protected override void Start() {
            base.Start();
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing: disposing);
        }

        protected override bool Matches(WaiterEventArgs eventArgs) {
            return null != eventArgs.Sender && UIObject.Matches(uiObject: eventArgs.Sender, condition: this._condition);
        }

        public override string ToString() {
            return "MenuOpenedWaiter with Condition:  " + this._condition;
        }
    }
}