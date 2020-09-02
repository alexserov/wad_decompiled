// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.SelectionItemPatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public static class SelectionItemPatternIdentifiers
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<SelectionItemPattern, IUIAutomationSelectionItemPattern>(10010, "SelectionItemPatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationSelectionItemPattern, SelectionItemPattern>(SelectionItemPattern.Wrap));
    public static readonly AutomationEvent ElementAddedToSelectionEvent = new AutomationEvent(20010, "SelectionItemPatternIdentifiers.ElementAddedToSelectionEvent");
    public static readonly AutomationEvent ElementRemovedFromSelectionEvent = new AutomationEvent(20011, "SelectionItemPatternIdentifiers.ElementRemovedFromSelectionEvent");
    public static readonly AutomationEvent ElementSelectedEvent = new AutomationEvent(20012, "SelectionItemPatternIdentifiers.ElementSelectedEvent");
    public static readonly AutomationProperty IsSelectedProperty = new AutomationProperty(30079, "SelectionItemPatternIdentifiers.IsSelectedProperty");
    public static readonly AutomationProperty SelectionContainerProperty = new AutomationProperty(30080, "SelectionItemPatternIdentifiers.SelectionContainerProperty");
  }
}
