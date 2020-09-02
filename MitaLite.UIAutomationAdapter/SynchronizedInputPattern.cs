// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.SynchronizedInputPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class SynchronizedInputPattern : BasePattern {
        public static readonly AutomationPattern Pattern = new AutomationPattern<SynchronizedInputPattern, IUIAutomationSynchronizedInputPattern>(id: 10021, programmaticName: "SynchronizedInputPatternIdentifiers.Pattern", wrap: Wrap);
        readonly IUIAutomationSynchronizedInputPattern _synchronizedInputPattern;

        SynchronizedInputPattern(
            AutomationElement element,
            IUIAutomationSynchronizedInputPattern synchronizedInputPattern)
            : base(el: element) {
            this._synchronizedInputPattern = synchronizedInputPattern;
        }

        internal static SynchronizedInputPattern Wrap(
            AutomationElement element,
            IUIAutomationSynchronizedInputPattern synchronizedInputPattern) {
            return new SynchronizedInputPattern(element: element, synchronizedInputPattern: synchronizedInputPattern);
        }
    }
}