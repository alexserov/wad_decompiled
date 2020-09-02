// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.SelectionPatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public static class SelectionPatternIdentifiers
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<SelectionPattern, IUIAutomationSelectionPattern>(10001, "SelectionPatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationSelectionPattern, SelectionPattern>(SelectionPattern.Wrap));
    public static readonly AutomationEvent InvalidatedEvent = new AutomationEvent(20013, "SelectionPatternIdentifiers.InvalidatedEvent");
    public static readonly AutomationProperty CanSelectMultipleProperty = new AutomationProperty(30060, "SelectionPatternIdentifiers.CanSelectMultipleProperty");
    public static readonly AutomationProperty IsSelectionRequiredProperty = new AutomationProperty(30061, "SelectionPatternIdentifiers.IsSelectionRequiredProperty");
    public static readonly AutomationProperty SelectionProperty = new AutomationProperty(30059, "SelectionPatternIdentifiers.SelectionProperty");
  }
}
