// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation
{
  public abstract class AutomationPattern : AutomationIdentifier
  {
    internal AutomationPattern(int id, string programmaticName)
      : base(AutomationIdType.Pattern, id, programmaticName)
    {
    }

    internal abstract object Wrap(AutomationElement element, object pattern);

    public static AutomationPattern LookupById(int id) => AutomationIdentifier.LookupById<AutomationPattern>(id);
  }
}
