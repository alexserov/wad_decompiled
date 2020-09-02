// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.CheckMenuItem
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation.Controls {
    public class CheckMenuItem : UIObject, IToggle {
        static IFactory<CheckMenuItem> _factory;
        IToggle _togglePattern;

        public CheckMenuItem(UIObject uiObject)
            : base(uiObject: uiObject) {
            Initialize();
        }

        internal CheckMenuItem(AutomationElement element)
            : base(element: element) {
            Initialize();
        }

        public static IFactory<CheckMenuItem> Factory {
            get {
                if (_factory == null)
                    _factory = new CheckMenuItemFactory();
                return _factory;
            }
        }

        public UIEventWaiter GetToggledWaiter() {
            return this._togglePattern.GetToggledWaiter();
        }

        public virtual void Toggle() {
            this._togglePattern.Toggle();
        }

        public virtual ToggleState ToggleState {
            get { return this._togglePattern.ToggleState; }
        }

        void Initialize() {
            this._togglePattern = new ToggleImplementation(uiObject: this);
        }

        class CheckMenuItemFactory : IFactory<CheckMenuItem> {
            public CheckMenuItem Create(UIObject element) {
                return new CheckMenuItem(uiObject: element);
            }
        }
    }
}