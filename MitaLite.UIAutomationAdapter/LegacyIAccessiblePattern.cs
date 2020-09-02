// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.LegacyIAccessiblePattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class LegacyIAccessiblePattern : BasePattern {
        public static readonly AutomationPattern Pattern = new AutomationPattern<LegacyIAccessiblePattern, IUIAutomationLegacyIAccessiblePattern>(id: 10018, programmaticName: "LegacyIAccessiblePatternIdentifiers.Pattern", wrap: Wrap);
        public static readonly AutomationProperty ChildIdProperty = new AutomationProperty(id: 30091, programmaticName: "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleChildIdPropertyId");
        public static readonly AutomationProperty DefaultActionProperty = new AutomationProperty(id: 30100, programmaticName: "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleDefaultActionPropertyId");
        public static readonly AutomationProperty DescriptionProperty = new AutomationProperty(id: 30094, programmaticName: "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleDescriptionPropertyId");
        public static readonly AutomationProperty HelpProperty = new AutomationProperty(id: 30097, programmaticName: "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleHelpPropertyId");
        public static readonly AutomationProperty KeyboardShortcutProperty = new AutomationProperty(id: 30098, programmaticName: "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleKeyboardShortcutPropertyId");
        public static readonly AutomationProperty NameProperty = new AutomationProperty(id: 30092, programmaticName: "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleNamePropertyId");
        public static readonly AutomationProperty RoleProperty = new AutomationProperty(id: 30095, programmaticName: "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleRolePropertyId");
        public static readonly AutomationProperty SelectionProperty = new AutomationProperty(id: 30099, programmaticName: "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleSelectionPropertyId");
        public static readonly AutomationProperty StateProperty = new AutomationProperty(id: 30096, programmaticName: "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleStatePropertyId");
        public static readonly AutomationProperty ValueProperty = new AutomationProperty(id: 30093, programmaticName: "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleValuePropertyId");
        readonly IUIAutomationLegacyIAccessiblePattern _legacyIAccessiblePattern;

        LegacyIAccessiblePattern(
            AutomationElement element,
            IUIAutomationLegacyIAccessiblePattern legacyIAccessiblePattern)
            : base(el: element) {
            this._legacyIAccessiblePattern = legacyIAccessiblePattern;
        }

        internal static LegacyIAccessiblePattern Wrap(
            AutomationElement element,
            IUIAutomationLegacyIAccessiblePattern legacyIAccessiblePattern) {
            return new LegacyIAccessiblePattern(element: element, legacyIAccessiblePattern: legacyIAccessiblePattern);
        }
    }
}