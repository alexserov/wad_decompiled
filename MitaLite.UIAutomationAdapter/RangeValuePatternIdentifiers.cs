// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.RangeValuePatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public static class RangeValuePatternIdentifiers
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<RangeValuePattern, IUIAutomationRangeValuePattern>(10003, "RangeValuePatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationRangeValuePattern, RangeValuePattern>(RangeValuePattern.Wrap));
    public static readonly AutomationProperty IsReadOnlyProperty = new AutomationProperty(30048, "RangeValuePatternIdentifiers.IsReadOnlyProperty");
    public static readonly AutomationProperty LargeChangeProperty = new AutomationProperty(30051, "RangeValuePatternIdentifiers.LargeChangeProperty");
    public static readonly AutomationProperty MaximumProperty = new AutomationProperty(30050, "RangeValuePatternIdentifiers.MaximumProperty");
    public static readonly AutomationProperty MinimumProperty = new AutomationProperty(30049, "RangeValuePatternIdentifiers.MinimumProperty");
    public static readonly AutomationProperty SmallChangeProperty = new AutomationProperty(30052, "RangeValuePatternIdentifiers.SmallChangeProperty");
    public static readonly AutomationProperty ValueProperty = new AutomationProperty(30047, "RangeValuePatternIdentifiers.ValueProperty");
  }
}
