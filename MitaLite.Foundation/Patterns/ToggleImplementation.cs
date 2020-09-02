// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.ToggleImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class ToggleImplementation : PatternImplementation<TogglePattern>, IToggle {
        public ToggleImplementation(UIObject uiObject)
            : base(uiObject: uiObject, patternIdentifier: TogglePattern.Pattern) {
        }

        public UIEventWaiter GetToggledWaiter() {
            return new PropertyChangedEventWaiter(root: UIObject, scope: Scope.Element, UIProperty.Get(name: "Toggle.ToggleState"));
        }

        public void Toggle() {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(Toggle))) != ActionResult.Unhandled)
                return;
            Pattern.Toggle();
        }

        public ToggleState ToggleState {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(ToggleState)), overridden: out overridden) == ActionResult.Handled ? (ToggleState) overridden : Pattern.Current.ToggleState;
            }
        }
    }
}