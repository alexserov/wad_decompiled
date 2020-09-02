// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TogglePatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public static class TogglePatternIdentifiers
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<TogglePattern, IUIAutomationTogglePattern>(10015, "TogglePatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationTogglePattern, TogglePattern>(TogglePattern.Wrap));
    public static readonly AutomationProperty ToggleStateProperty = new AutomationProperty(30086, "TogglePatternIdentifiers.ToggleStateProperty");
  }
}
