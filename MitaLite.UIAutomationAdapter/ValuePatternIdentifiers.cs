// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ValuePatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public static class ValuePatternIdentifiers
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<ValuePattern, IUIAutomationValuePattern>(10002, "ValuePatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationValuePattern, ValuePattern>(ValuePattern.Wrap));
    public static readonly AutomationProperty IsReadOnlyProperty = new AutomationProperty(30046, "ValuePatternIdentifiers.IsReadOnlyProperty");
    public static readonly AutomationProperty ValueProperty = new AutomationProperty(30045, "ValuePatternIdentifiers.ValueProperty");
  }
}
