// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ExpandCollapsePatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public static class ExpandCollapsePatternIdentifiers
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<ExpandCollapsePattern, IUIAutomationExpandCollapsePattern>(10005, "ExpandCollapsePatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationExpandCollapsePattern, ExpandCollapsePattern>(ExpandCollapsePattern.Wrap));
    public static readonly AutomationProperty ExpandCollapseStateProperty = new AutomationProperty(30070, "ExpandCollapsePatternIdentifiers.ExpandCollapseStateProperty");
  }
}
