// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.SpreadsheetPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class SpreadsheetPattern : BasePattern {
        public static readonly AutomationPattern Pattern = new AutomationPattern<SpreadsheetPattern, IUIAutomationSpreadsheetPattern>(id: 10026, programmaticName: "SpreadsheetPatternIdentifiers.Pattern", wrap: Wrap);
        readonly IUIAutomationSpreadsheetPattern _spreadsheetPattern;

        SpreadsheetPattern(
            AutomationElement element,
            IUIAutomationSpreadsheetPattern spreadsheetPattern)
            : base(el: element) {
            this._spreadsheetPattern = spreadsheetPattern;
        }

        internal static SpreadsheetPattern Wrap(
            AutomationElement element,
            IUIAutomationSpreadsheetPattern spreadsheetPattern) {
            return new SpreadsheetPattern(element: element, spreadsheetPattern: spreadsheetPattern);
        }
    }
}