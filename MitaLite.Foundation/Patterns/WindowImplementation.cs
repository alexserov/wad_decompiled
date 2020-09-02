// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.WindowImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class WindowImplementation : PatternImplementation<WindowPattern>, IWindow {
        public WindowImplementation(UIObject uiObject)
            : base(uiObject: uiObject, patternIdentifier: WindowPattern.Pattern) {
        }

        public void SetWindowVisualState(WindowVisualState state) {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(SetWindowVisualState), state)) != ActionResult.Unhandled)
                return;
            Pattern.SetWindowVisualState(state: state);
        }

        public void Close() {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(Close))) != ActionResult.Unhandled)
                return;
            Pattern.Close();
        }

        public void WaitForInputIdle(int milliseconds) {
            var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(WaitForInputIdle), milliseconds)) != ActionResult.Unhandled)
                return;
            Pattern.WaitForInputIdle(milliseconds: milliseconds);
        }

        public UIEventWaiter GetWindowClosedWaiter() {
            return new WindowClosedWaiter(root: UIObject);
        }

        public bool CanMaximize {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(CanMaximize)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : Pattern.Current.CanMaximize;
            }
        }

        public bool CanMinimize {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(CanMinimize)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : Pattern.Current.CanMinimize;
            }
        }

        public bool IsModal {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(IsModal)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : Pattern.Current.IsModal;
            }
        }

        public WindowVisualState WindowVisualState {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(WindowVisualState)), overridden: out overridden) == ActionResult.Handled ? (WindowVisualState) overridden : Pattern.Current.WindowVisualState;
            }
        }

        public WindowInteractionState WindowInteractionState {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(WindowInteractionState)), overridden: out overridden) == ActionResult.Handled ? (WindowInteractionState) overridden : Pattern.Current.WindowInteractionState;
            }
        }

        public bool IsTopmost {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(IsTopmost)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : Pattern.Current.IsTopmost;
            }
        }
    }
}