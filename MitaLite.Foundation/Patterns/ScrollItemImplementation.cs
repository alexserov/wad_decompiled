// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.ScrollItemImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class ScrollItemImplementation : PatternImplementation<ScrollItemPattern>, IScrollItem {
        public ScrollItemImplementation(UIObject uiObject)
            : base(uiObject: uiObject, patternIdentifier: ScrollItemPattern.Pattern) {
        }

        public void ScrollIntoView() {
            var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(ScrollIntoView), args: Array.Empty<object>())) != ActionResult.Unhandled)
                return;
            Pattern.ScrollIntoView();
        }
    }
}