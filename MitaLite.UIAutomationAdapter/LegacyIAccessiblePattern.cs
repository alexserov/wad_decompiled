// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.LegacyIAccessiblePattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class LegacyIAccessiblePattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<LegacyIAccessiblePattern, IUIAutomationLegacyIAccessiblePattern>(10018, "LegacyIAccessiblePatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationLegacyIAccessiblePattern, LegacyIAccessiblePattern>(LegacyIAccessiblePattern.Wrap));
    public static readonly AutomationProperty ChildIdProperty = new AutomationProperty(30091, "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleChildIdPropertyId");
    public static readonly AutomationProperty DefaultActionProperty = new AutomationProperty(30100, "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleDefaultActionPropertyId");
    public static readonly AutomationProperty DescriptionProperty = new AutomationProperty(30094, "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleDescriptionPropertyId");
    public static readonly AutomationProperty HelpProperty = new AutomationProperty(30097, "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleHelpPropertyId");
    public static readonly AutomationProperty KeyboardShortcutProperty = new AutomationProperty(30098, "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleKeyboardShortcutPropertyId");
    public static readonly AutomationProperty NameProperty = new AutomationProperty(30092, "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleNamePropertyId");
    public static readonly AutomationProperty RoleProperty = new AutomationProperty(30095, "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleRolePropertyId");
    public static readonly AutomationProperty SelectionProperty = new AutomationProperty(30099, "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleSelectionPropertyId");
    public static readonly AutomationProperty StateProperty = new AutomationProperty(30096, "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleStatePropertyId");
    public static readonly AutomationProperty ValueProperty = new AutomationProperty(30093, "LegacyIAccessiblePatternIdentifiers.LegacyIAccessibleValuePropertyId");
    private readonly IUIAutomationLegacyIAccessiblePattern _legacyIAccessiblePattern;

    private LegacyIAccessiblePattern(
      AutomationElement element,
      IUIAutomationLegacyIAccessiblePattern legacyIAccessiblePattern)
      : base(element)
      => this._legacyIAccessiblePattern = legacyIAccessiblePattern;

    internal static LegacyIAccessiblePattern Wrap(
      AutomationElement element,
      IUIAutomationLegacyIAccessiblePattern legacyIAccessiblePattern) => new LegacyIAccessiblePattern(element, legacyIAccessiblePattern);
  }
}
