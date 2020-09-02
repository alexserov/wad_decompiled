// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.StylesPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class StylesPattern : BasePattern {
        public static readonly AutomationPattern Pattern = new AutomationPattern<StylesPattern, IUIAutomationStylesPattern>(id: 10025, programmaticName: "StylesPatternIdentifiers.Pattern", wrap: Wrap);
        public static readonly AutomationProperty StyleIdProperty = new AutomationProperty(id: 30120, programmaticName: "StylesPatternIdentifiers.StyleIdProperty");
        public static readonly AutomationProperty StyleNameProperty = new AutomationProperty(id: 30121, programmaticName: "StylesPatternIdentifiers.StyleNameProperty");
        public static readonly AutomationProperty FillColorProperty = new AutomationProperty(id: 30122, programmaticName: "StylesPatternIdentifiers.FillColorProperty");
        public static readonly AutomationProperty FillPatternStyleProperty = new AutomationProperty(id: 30123, programmaticName: "StylesPatternIdentifiers.FillPatternStyleProperty");
        public static readonly AutomationProperty ShapeProperty = new AutomationProperty(id: 30124, programmaticName: "StylesPatternIdentifiers.ShapeProperty");
        public static readonly AutomationProperty FillPatternColorProperty = new AutomationProperty(id: 30125, programmaticName: "StylesPatternIdentifiers.FillPatternColorProperty");
        public static readonly AutomationProperty ExtendedPropertiesProperty = new AutomationProperty(id: 30126, programmaticName: "StylesPatternIdentifiers.ExtendedPropertiesProperty");
        readonly IUIAutomationStylesPattern _stylesPattern;

        StylesPattern(AutomationElement element, IUIAutomationStylesPattern stylesPattern)
            : base(el: element) {
            this._stylesPattern = stylesPattern;
        }

        internal static StylesPattern Wrap(
            AutomationElement element,
            IUIAutomationStylesPattern stylesPattern) {
            return new StylesPattern(element: element, stylesPattern: stylesPattern);
        }
    }
}