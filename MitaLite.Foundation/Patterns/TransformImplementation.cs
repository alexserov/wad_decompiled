// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.TransformImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class TransformImplementation : PatternImplementation<TransformPattern>, ITransform {
        public TransformImplementation(UIObject uiObject)
            : base(uiObject: uiObject, patternIdentifier: TransformPattern.Pattern) {
        }

        public void Rotate(double degrees) {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(Rotate), degrees)) != ActionResult.Unhandled)
                return;
            Pattern.Rotate(degrees: degrees);
        }

        public void Resize(double width, double height) {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(Resize), width, (object) height)) != ActionResult.Unhandled)
                return;
            Pattern.Resize(width: width, height: height);
        }

        public void Move(double x, double y) {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(Move), x, (object) y)) != ActionResult.Unhandled)
                return;
            Pattern.Move(x: x, y: y);
        }

        public bool CanRotate {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(CanRotate)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : Pattern.Current.CanRotate;
            }
        }

        public bool CanResize {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(CanResize)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : Pattern.Current.CanResize;
            }
        }

        public bool CanMove {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(CanMove)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : Pattern.Current.CanMove;
            }
        }
    }
}