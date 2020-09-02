// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.SplitButton
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation.Controls {
    public class SplitButton : UIObject, IExpandCollapse, IInvoke {
        static IFactory<SplitButton> _factory;
        IExpandCollapse _expandCollapsePattern;
        IInvoke _invokePattern;

        public SplitButton(UIObject uiObject)
            : base(uiObject: uiObject) {
            Initialize();
        }

        internal SplitButton(AutomationElement element)
            : base(element: element) {
            Initialize();
        }

        public static IFactory<SplitButton> Factory {
            get {
                if (_factory == null)
                    _factory = new SplitButtonFactory();
                return _factory;
            }
        }

        public virtual void Collapse() {
            this._expandCollapsePattern.Collapse();
        }

        public virtual void Expand() {
            this._expandCollapsePattern.Expand();
        }

        public virtual ExpandCollapseState ExpandCollapseState {
            get { return this._expandCollapsePattern.ExpandCollapseState; }
        }

        public UIEventWaiter GetCollapsedWaiter() {
            return this._expandCollapsePattern.GetCollapsedWaiter();
        }

        public UIEventWaiter GetExpandedWaiter() {
            return this._expandCollapsePattern.GetExpandedWaiter();
        }

        public virtual void Invoke() {
            this._invokePattern.Invoke();
        }

        public UIEventWaiter GetInvokedWaiter() {
            return this._invokePattern.GetInvokedWaiter();
        }

        void Initialize() {
            this._expandCollapsePattern = new ExpandCollapseImplementation(uiObject: this);
            this._invokePattern = new InvokeImplementation(uiObject: this);
        }

        class SplitButtonFactory : IFactory<SplitButton> {
            public SplitButton Create(UIObject element) {
                return new SplitButton(uiObject: element);
            }
        }
    }
}