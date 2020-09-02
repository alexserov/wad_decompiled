// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.ValueImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class ValueImplementation : PatternImplementation<ValuePattern>, IValue {
        public ValueImplementation(UIObject uiObject)
            : base(uiObject: uiObject, patternIdentifier: ValuePattern.Pattern) {
        }

        public void SetValue(string value) {
            Validate.ArgumentNotNull(parameter: value, parameterName: nameof(value));
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(SetValue), value)) == ActionResult.Unhandled)
                Pattern.SetValue(value: value);
            var num3 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "UIScrollComplete"));
        }

        public string Value {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(Value)), overridden: out overridden) == ActionResult.Handled ? (string) overridden : Pattern.Current.Value;
            }
        }

        public bool IsReadOnly {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(IsReadOnly)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : Pattern.Current.IsReadOnly;
            }
        }
    }
}