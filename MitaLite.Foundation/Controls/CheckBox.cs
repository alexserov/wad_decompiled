// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.CheckBox
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation.Controls {
    public class CheckBox : UIObject, IToggle {
        static IFactory<CheckBox> _factory;
        IToggle _togglePattern;

        public CheckBox(UIObject uiObject)
            : base(uiObject: uiObject) {
            Initialize();
        }

        internal CheckBox(AutomationElement element)
            : base(element: element) {
            Initialize();
        }

        public static IFactory<CheckBox> Factory {
            get {
                if (_factory == null)
                    _factory = new CheckBoxFactory();
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

        public void Check() {
            var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            if (ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(Check))) != ActionResult.Handled && !SetToggleState(toggleState: ToggleState.On))
                throw new ActionException(message: StringResource.Get(id: "CheckBox_CheckFailed", (object) SafeGetName(uiObject: this)));
        }

        public void Uncheck() {
            var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            if (ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(Uncheck))) != ActionResult.Handled && !SetToggleState(toggleState: ToggleState.Off))
                throw new ActionException(message: StringResource.Get(id: "CheckBox_UncheckFailed", (object) SafeGetName(uiObject: this)));
        }

        bool SetToggleState(ToggleState toggleState) {
            for (var index = 0; index < 3; ++index) {
                if (this._togglePattern.ToggleState == toggleState)
                    return true;
                if (index < 2)
                    using (var changedEventWaiter = new PropertyChangedEventWaiter(root: this, scope: Scope.Element, UIProperty.Get(name: "Toggle.ToggleState"))) {
                        Toggle();
                        changedEventWaiter.TryWait(timeout: 500);
                    }
            }

            return false;
        }

        class CheckBoxFactory : IFactory<CheckBox> {
            public CheckBox Create(UIObject element) {
                return new CheckBox(uiObject: element);
            }
        }
    }
}