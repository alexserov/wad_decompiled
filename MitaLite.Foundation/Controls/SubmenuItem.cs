// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.SubmenuItem
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation.Controls {
    public class SubmenuItem : UIObject, IExpandCollapse {
        static IFactory<SubmenuItem> _factory;
        IExpandCollapse _expandCollapsePattern;

        public SubmenuItem(UIObject uiObject)
            : base(uiObject: uiObject) {
            Initialize();
        }

        internal SubmenuItem(AutomationElement element)
            : base(element: element) {
            Initialize();
        }

        public static IFactory<SubmenuItem> Factory {
            get {
                if (_factory == null)
                    _factory = new SubmenuItemFactory();
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

        void Initialize() {
            this._expandCollapsePattern = new ExpandCollapseImplementation(uiObject: this);
        }

        class SubmenuItemFactory : IFactory<SubmenuItem> {
            public SubmenuItem Create(UIObject element) {
                return new SubmenuItem(uiObject: element);
            }
        }
    }
}