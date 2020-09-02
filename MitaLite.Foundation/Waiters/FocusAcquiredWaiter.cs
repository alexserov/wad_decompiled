// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.FocusAcquiredWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters {
    public class FocusAcquiredWaiter : UIEventWaiter {
        UICondition _condition;

        public FocusAcquiredWaiter()
            : this(condition: UICondition.True) {
        }

        public FocusAcquiredWaiter(string automationId)
            : this(condition: UICondition.CreateFromId(automationId: automationId)) {
        }

        public FocusAcquiredWaiter(UIProperty uiProperty, string value)
            : this(condition: UICondition.Create(property: uiProperty, value: value)) {
        }

        public FocusAcquiredWaiter(UICondition condition)
            : base(eventSource: new FocusChangedEventSource()) {
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
            return UIObject.Matches(uiObject: eventArgs.Sender, condition: this._condition);
        }

        public override string ToString() {
            return "FocusAcquiredWaiter with Condition:  " + this._condition;
        }
    }
}