// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.SpreadsheetItemPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class SpreadsheetItemPattern : BasePattern {
        public static readonly AutomationPattern Pattern = new AutomationPattern<SpreadsheetItemPattern, IUIAutomationSpreadsheetItemPattern>(id: 10027, programmaticName: "SpreadsheetItemPatternIdentifiers.Pattern", wrap: Wrap);
        public static readonly AutomationProperty FormulaProperty = new AutomationProperty(id: 30129, programmaticName: "SpreadsheetItemPatternIdentifiers.FormulaProperty");
        public static readonly AutomationProperty AnnotationObjectsProperty = new AutomationProperty(id: 30130, programmaticName: "SpreadsheetItemPatternIdentifiers.AnnotationObjectsProperty");
        public static readonly AutomationProperty AnnotationTypesProperty = new AutomationProperty(id: 30131, programmaticName: "SpreadsheetItemPatternIdentifiers.AnnotationTypesProperty");
        readonly IUIAutomationSpreadsheetItemPattern _spreadsheetItemPattern;

        SpreadsheetItemPattern(
            AutomationElement element,
            IUIAutomationSpreadsheetItemPattern spreadsheetItemPattern)
            : base(el: element) {
            this._spreadsheetItemPattern = spreadsheetItemPattern;
        }

        internal static SpreadsheetItemPattern Wrap(
            AutomationElement element,
            IUIAutomationSpreadsheetItemPattern spreadsheetItemPattern) {
            return new SpreadsheetItemPattern(element: element, spreadsheetItemPattern: spreadsheetItemPattern);
        }
    }
}