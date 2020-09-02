// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.InvokeImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class InvokeImplementation : PatternImplementation<InvokePattern>, IInvoke {
        public InvokeImplementation(UIObject uiObject)
            : base(uiObject: uiObject, patternIdentifier: InvokePattern.Pattern) {
        }

        public void Invoke() {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(Invoke), args: Array.Empty<object>())) != ActionResult.Unhandled)
                return;
            Pattern.Invoke();
        }

        public UIEventWaiter GetInvokedWaiter() {
            return new AutomationEventWaiter(eventId: InvokePattern.InvokedEvent, uiObject: UIObject, scope: Scope.Element);
        }
    }
}