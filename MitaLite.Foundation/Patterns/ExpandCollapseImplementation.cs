// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.ExpandCollapseImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class ExpandCollapseImplementation : PatternImplementation<ExpandCollapsePattern>, IExpandCollapse {
        public ExpandCollapseImplementation(UIObject uiObject)
            : base(uiObject: uiObject, patternIdentifier: ExpandCollapsePattern.Pattern) {
        }

        public void Collapse() {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(Collapse))) != ActionResult.Unhandled)
                return;
            Pattern.Collapse();
        }

        public void Expand() {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(Expand))) != ActionResult.Unhandled)
                return;
            Pattern.Expand();
        }

        public ExpandCollapseState ExpandCollapseState {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(ExpandCollapseState)), overridden: out overridden) == ActionResult.Handled ? (ExpandCollapseState) overridden : Pattern.Current.ExpandCollapseState;
            }
        }

        public UIEventWaiter GetCollapsedWaiter() {
            var changedEventWaiter = new PropertyChangedEventWaiter(root: UIObject, scope: Scope.Subtree, UIProperty.Get(name: "ExpandCollapse.ExpandCollapseState"));
            changedEventWaiter.AddFilter(callback: CollapseFilter);
            return changedEventWaiter;
        }

        public UIEventWaiter GetExpandedWaiter() {
            var changedEventWaiter = new PropertyChangedEventWaiter(root: UIObject, scope: Scope.Subtree, UIProperty.Get(name: "ExpandCollapse.ExpandCollapseState"));
            changedEventWaiter.AddFilter(callback: ExpandFilter);
            return changedEventWaiter;
        }

        static bool CollapseFilter(WaiterEventArgs args) {
            return ExpandCollapseStateFilter(args: (AutomationPropertyChangedEventArgs) args.EventArgs, state: ExpandCollapseState.Collapsed);
        }

        static bool ExpandFilter(WaiterEventArgs args) {
            return ExpandCollapseStateFilter(args: (AutomationPropertyChangedEventArgs) args.EventArgs, state: ExpandCollapseState.Expanded);
        }

        static bool ExpandCollapseStateFilter(
            AutomationPropertyChangedEventArgs args,
            ExpandCollapseState state) {
            var flag = false;
            if (ExpandCollapsePattern.ExpandCollapseStateProperty == args.Property && state == (ExpandCollapseState) args.NewValue)
                flag = true;
            return flag;
        }
    }
}