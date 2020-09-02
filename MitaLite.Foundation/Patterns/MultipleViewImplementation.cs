// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.MultipleViewImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class MultipleViewImplementation : PatternImplementation<MultipleViewPattern>, IMultipleView {
        public MultipleViewImplementation(UIObject uiObject)
            : base(uiObject: uiObject, patternIdentifier: MultipleViewPattern.Pattern) {
        }

        public string GetViewName(int viewId) {
            var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            object overridden;
            return ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(GetViewName), viewId), overridden: out overridden) == ActionResult.Handled ? (string) overridden : Pattern.GetViewName(viewId: viewId);
        }

        public void SetCurrentView(int viewId) {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(SetCurrentView), viewId)) == ActionResult.Unhandled)
                Pattern.SetCurrentView(viewId: viewId);
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "UIScrollComplete"));
        }

        public int[] GetSupportedViews() {
            var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            object overridden;
            return ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(GetSupportedViews), args: Array.Empty<object>()), overridden: out overridden) == ActionResult.Handled ? (int[]) overridden : Pattern.Current.GetSupportedViews();
        }

        public int CurrentView {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(CurrentView), args: Array.Empty<object>()), overridden: out overridden) == ActionResult.Handled ? (int) overridden : Pattern.Current.CurrentView;
            }
        }
    }
}