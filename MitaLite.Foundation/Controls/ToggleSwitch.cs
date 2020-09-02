// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ToggleSwitch
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Utilities;
using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation.Controls {
    public class ToggleSwitch : UIObject, IToggle {
        static IFactory<ToggleSwitch> _factory;
        IToggle _togglePattern;

        public ToggleSwitch(UIObject uiObject)
            : base(uiObject: uiObject) {
            Initialize();
        }

        public ToggleSwitch(AutomationElement element)
            : base(element: element) {
            Initialize();
        }

        public static IFactory<ToggleSwitch> Factory {
            get {
                if (_factory == null)
                    _factory = new ToggleSwitchFactory();
                return _factory;
            }
        }

        public ToggleState ToggleState {
            get { return this._togglePattern.ToggleState; }
        }

        public virtual void Toggle() {
            this._togglePattern.Toggle();
        }

        public UIEventWaiter GetToggledWaiter() {
            return this._togglePattern.GetToggledWaiter();
        }

        void Initialize() {
            this._togglePattern = new ToggleImplementation(uiObject: this);
        }

        public void TurnOn() {
            var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            if (ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "Toggle")) != ActionResult.Handled && !SetToggleState(toggleState: ToggleState.On))
                throw new ActionException(message: StringResource.Get(id: "ToggleSwitch_TurnOnFailed", (object) SafeGetName(uiObject: this)));
        }

        public void TurnOff() {
            var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            if (ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "Toggle")) != ActionResult.Handled && !SetToggleState(toggleState: ToggleState.Off))
                throw new ActionException(message: StringResource.Get(id: "ToggleSwitch_TurnOffFailed", (object) SafeGetName(uiObject: this)));
        }

        bool SetToggleState(ToggleState toggleState) {
            for (var index = 0; index < 3; ++index) {
                if (this._togglePattern.ToggleState == toggleState)
                    return true;
                if (index < 2)
                    using (var changedEventWaiter = new PropertyChangedEventWaiter(root: this, scope: Scope.Element, UIProperty.Get(name: "Toggle.ToggleState"))) {
                        Toggle();
                        if (!changedEventWaiter.TryWait())
                            Log.Out(msg: "ToggleState did not change before timeout.");
                    }
            }

            return false;
        }

        class ToggleSwitchFactory : IFactory<ToggleSwitch> {
            public ToggleSwitch Create(UIObject element) {
                return new ToggleSwitch(uiObject: element);
            }
        }
    }
}