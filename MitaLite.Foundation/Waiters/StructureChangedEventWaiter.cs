// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.StructureChangedEventWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.Waiters {
    public class StructureChangedEventWaiter : UIEventWaiter {
        public StructureChangedEventWaiter(UIObject root)
            : this(root: root, scope: Scope.Descendants) {
        }

        public StructureChangedEventWaiter(UIObject root, Scope scope)
            : base(eventSource: new StructureChangedEventSource(root: root, scope: scope)) {
            Start();
        }

        protected override void Start() {
            base.Start();
        }
    }
}