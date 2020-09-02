// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.MenuClosedWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters {
    public class MenuClosedWaiter : UIEventWaiter {
        readonly string _uiObjectDescription;

        public MenuClosedWaiter()
            : base(eventSource: new AutomationEventSource(eventId: AutomationElement.MenuClosedEvent, root: UIObject.Root, scope: Scope.Subtree)) {
            this._uiObjectDescription = UIObject.Root.ToString();
            Start();
        }

        public MenuClosedWaiter(UIObject root)
            : base(eventSource: new AutomationEventSource(eventId: AutomationElement.MenuClosedEvent, root: root, scope: Scope.Element)) {
            Validate.ArgumentNotNull(parameter: root, parameterName: "uiObject");
            this._uiObjectDescription = root.ToString();
            Start();
        }

        protected override void Start() {
            base.Start();
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing: disposing);
        }

        public override string ToString() {
            return "MenuClosedWaiter for element " + this._uiObjectDescription;
        }
    }
}