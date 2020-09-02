// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.TextImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using System.Windows.Automation.Text;
using System.Windows.Types;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class TextImplementation : PatternImplementation<TextPattern>, IText {
        public TextImplementation(UIObject uiObject)
            : base(uiObject: uiObject, patternIdentifier: TextPattern.Pattern) {
        }

        public bool SupportsTextSelection {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(SupportsTextSelection)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : (uint) Pattern.SupportedTextSelection > 0U;
            }
        }

        public TextPatternRange DocumentRange {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(DocumentRange)), overridden: out overridden) == ActionResult.Handled ? (TextPatternRange) overridden : Pattern.DocumentRange;
            }
        }

        public TextPatternRange GetSelection() {
            var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            object overridden;
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(GetSelection)), overridden: out overridden) == ActionResult.Handled)
                return (TextPatternRange) overridden;
            var selection = Pattern.GetSelection();
            return selection != null && selection.Length != 0 ? selection[0] : null;
        }

        public TextPatternRange RangeFromPoint(PointI screenLocation) {
            var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            object overridden;
            return ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(RangeFromPoint), screenLocation), overridden: out overridden) == ActionResult.Handled ? (TextPatternRange) overridden : Pattern.RangeFromPoint(screenLocation: new Point(x: screenLocation.X, y: screenLocation.Y));
        }

        public TextPatternRange RangeFromChild(UIObject childElement) {
            Validate.ArgumentNotNull(parameter: childElement, parameterName: nameof(childElement));
            var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            object overridden;
            return ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(RangeFromChild), childElement), overridden: out overridden) == ActionResult.Handled ? (TextPatternRange) overridden : Pattern.RangeFromChild(childElement: childElement.AutomationElement);
        }

        public TextPatternRange GetVisibleRange() {
            var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            object overridden;
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(GetVisibleRange)), overridden: out overridden) == ActionResult.Handled)
                return (TextPatternRange) overridden;
            var visibleRanges = Pattern.GetVisibleRanges();
            return visibleRanges != null && visibleRanges.Length != 0 ? visibleRanges[0] : null;
        }
    }
}