// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.InvokePattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class InvokePattern : BasePattern {
        public static readonly AutomationPattern Pattern = InvokePatternIdentifiers.Pattern;
        public static readonly AutomationEvent InvokedEvent = InvokePatternIdentifiers.InvokedEvent;
        readonly IUIAutomationInvokePattern _invokePattern;

        InvokePattern(AutomationElement element, IUIAutomationInvokePattern invokePattern)
            : base(el: element) {
            this._invokePattern = invokePattern;
        }

        internal static InvokePattern Wrap(
            AutomationElement element,
            IUIAutomationInvokePattern invokePattern) {
            return new InvokePattern(element: element, invokePattern: invokePattern);
        }

        public void Invoke() {
            this._invokePattern.Invoke();
        }
    }
}