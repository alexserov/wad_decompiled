// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.PropertyChangedEventWaiter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.Waiters {
    public class PropertyChangedEventWaiter : UIEventWaiter {
        public PropertyChangedEventWaiter(UIObject root, params UIProperty[] uiProperties)
            : this(root: root, scope: Scope.Element, uiProperties: uiProperties) {
        }

        public PropertyChangedEventWaiter(UIObject root, Scope scope, params UIProperty[] uiProperties)
            : base(eventSource: new PropertyChangedEventSource(root: root, scope: scope, uiProperties: uiProperties)) {
            Start();
        }

        public PropertyChangedEventWaiter(UIObject root, Scope scope)
            : base(eventSource: new PropertyChangedEventSource(root: root, scope: scope)) {
            Start();
        }

        protected override void Start() {
            base.Start();
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing: disposing);
        }
    }
}