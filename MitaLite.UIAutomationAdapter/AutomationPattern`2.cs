// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationPattern`2
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation
{
  internal class AutomationPattern<TManagedPattern, TComPattern> : AutomationPattern
    where TManagedPattern : BasePattern
  {
    private readonly Func<AutomationElement, TComPattern, TManagedPattern> wrap;

    internal AutomationPattern(
      int id,
      string programmaticName,
      Func<AutomationElement, TComPattern, TManagedPattern> wrap)
      : base(id, programmaticName)
      => this.wrap = wrap;

    internal override object Wrap(AutomationElement element, object pattern) => (object) this.wrap(element, (TComPattern) pattern);
  }
}
