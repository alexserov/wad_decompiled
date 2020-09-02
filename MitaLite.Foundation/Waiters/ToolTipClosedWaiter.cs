// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.ToolTipClosedWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Waiters {
    public class ToolTipClosedWaiter : UIEventWaiter {
        readonly string _uiObjectDescription;

        public ToolTipClosedWaiter()
            : this(root: UIObject.Root, scope: Scope.Descendants) {
        }

        public ToolTipClosedWaiter(UIObject root)
            : this(root: root, scope: Scope.Element) {
        }

        public ToolTipClosedWaiter(UIObject root, Scope scope)
            : base(eventSource: new AutomationEventSource(eventId: AutomationElement.ToolTipClosedEvent, root: root, scope: scope)) {
            Validate.ArgumentNotNull(parameter: root, parameterName: nameof(root));
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
            return "ToolTipClosedWaiter for element " + this._uiObjectDescription;
        }
    }
}