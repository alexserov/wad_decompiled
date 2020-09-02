// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.ScrollImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class ScrollImplementation : PatternImplementation<ScrollPattern>, IScroll {
        public ScrollImplementation(UIObject uiObject)
            : base(uiObject: uiObject, patternIdentifier: ScrollPattern.Pattern) {
        }

        public void Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount) {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(Scroll), horizontalAmount, (object) verticalAmount)) == ActionResult.Unhandled)
                Pattern.Scroll(horizontalAmount: horizontalAmount, verticalAmount: verticalAmount);
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "UIScrollComplete"));
        }

        public void ScrollHorizontal(ScrollAmount amount) {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(ScrollHorizontal), amount)) == ActionResult.Unhandled)
                Pattern.ScrollHorizontal(amount: amount);
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "UIScrollComplete"));
        }

        public void ScrollVertical(ScrollAmount amount) {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(ScrollVertical), amount)) == ActionResult.Unhandled)
                Pattern.ScrollVertical(amount: amount);
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "UIScrollComplete"));
        }

        public void SetScrollPercent(double horizontalPercent, double verticalPercent) {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(SetScrollPercent), horizontalPercent, (object) verticalPercent)) == ActionResult.Unhandled)
                Pattern.SetScrollPercent(horizontalPercent: horizontalPercent, verticalPercent: verticalPercent);
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "UIScrollComplete"));
        }

        public bool HorizontallyScrollable {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(HorizontallyScrollable)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : Pattern.Current.HorizontallyScrollable;
            }
        }

        public bool VerticallyScrollable {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(VerticallyScrollable)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : Pattern.Current.VerticallyScrollable;
            }
        }

        public double HorizontalScrollPercent {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(HorizontalScrollPercent)), overridden: out overridden) == ActionResult.Handled ? (double) overridden : Pattern.Current.HorizontalScrollPercent;
            }
        }

        public double VerticalScrollPercent {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(VerticalScrollPercent)), overridden: out overridden) == ActionResult.Handled ? (double) overridden : Pattern.Current.VerticalScrollPercent;
            }
        }

        public double HorizontalViewSize {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(HorizontalViewSize)), overridden: out overridden) == ActionResult.Handled ? (double) overridden : Pattern.Current.HorizontalViewSize;
            }
        }

        public double VerticalViewSize {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(VerticalViewSize)), overridden: out overridden) == ActionResult.Handled ? (double) overridden : Pattern.Current.VerticalViewSize;
            }
        }
    }
}