// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.DockImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class DockImplementation : PatternImplementation<DockPattern>, IDock {
        public DockImplementation(UIObject uiObject)
            : base(uiObject: uiObject, patternIdentifier: DockPattern.Pattern) {
        }

        public void SetDockPosition(DockPosition position) {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(SetDockPosition), position)) == ActionResult.Unhandled)
                Pattern.SetDockPosition(dockPosition: position);
            var num3 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "UIScrollComplete"));
        }

        public DockPosition DockPosition {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(DockPosition)), overridden: out overridden) == ActionResult.Handled ? (DockPosition) overridden : DockPosition.None;
            }
        }
    }
}