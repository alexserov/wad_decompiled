// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.VirtualizedItemPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class VirtualizedItemPattern : BasePattern {
        public static readonly AutomationPattern Pattern = new AutomationPattern<VirtualizedItemPattern, IUIAutomationVirtualizedItemPattern>(id: 10020, programmaticName: "VirtualizedItemPatternIdentifiers.Pattern", wrap: Wrap);
        readonly IUIAutomationVirtualizedItemPattern _virtualizedItemPattern;

        VirtualizedItemPattern(
            AutomationElement element,
            IUIAutomationVirtualizedItemPattern virtualizedItemPattern)
            : base(el: element) {
            this._virtualizedItemPattern = virtualizedItemPattern;
        }

        internal static VirtualizedItemPattern Wrap(
            AutomationElement element,
            IUIAutomationVirtualizedItemPattern virtualizedItemPattern) {
            return new VirtualizedItemPattern(element: element, virtualizedItemPattern: virtualizedItemPattern);
        }

        public void Realize() {
            this._virtualizedItemPattern.Realize();
        }
    }
}