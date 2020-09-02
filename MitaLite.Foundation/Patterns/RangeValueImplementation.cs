// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.RangeValueImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class RangeValueImplementation : PatternImplementation<RangeValuePattern>, IRangeValue {
        public RangeValueImplementation(UIObject uiObject)
            : base(uiObject: uiObject, patternIdentifier: RangeValuePattern.Pattern) {
        }

        public double Minimum {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(Minimum)), overridden: out overridden) == ActionResult.Handled ? (double) overridden : Pattern.Current.Minimum;
            }
        }

        public double Maximum {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(Maximum)), overridden: out overridden) == ActionResult.Handled ? (double) overridden : Pattern.Current.Maximum;
            }
        }

        public double LargeChange {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(LargeChange)), overridden: out overridden) == ActionResult.Handled ? (double) overridden : Pattern.Current.LargeChange;
            }
        }

        public double SmallChange {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(SmallChange)), overridden: out overridden) == ActionResult.Handled ? (double) overridden : Pattern.Current.SmallChange;
            }
        }

        public void SetValue(double value) {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: "RangeSetValue", value)) == ActionResult.Unhandled)
                Pattern.SetValue(value: value);
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "UIScrollComplete"));
        }

        public double Value {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "RangeValue"), overridden: out overridden) == ActionResult.Handled ? (double) overridden : Pattern.Current.Value;
            }
        }

        public bool IsReadOnly {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "RangeIsReadOnly"), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : Pattern.Current.IsReadOnly;
            }
        }
    }
}