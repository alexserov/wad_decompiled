// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.AutomationEventWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters {
    public class AutomationEventWaiter : UIEventWaiter {
        readonly UICondition _condition;

        public AutomationEventWaiter(AutomationEvent eventId, UIObject uiObject, Scope scope)
            : this(eventId: eventId, uiObject: uiObject, scope: scope, condition: UICondition.True) {
        }

        public AutomationEventWaiter(
            AutomationEvent eventId,
            UIObject uiObject,
            Scope scope,
            UICondition condition)
            : base(eventSource: new AutomationEventSource(eventId: eventId, root: uiObject, scope: scope)) {
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
            var flag = true;
            if (null != eventArgs.Sender)
                flag = UIObject.Matches(uiObject: eventArgs.Sender, condition: this._condition);
            return flag;
        }
    }
}