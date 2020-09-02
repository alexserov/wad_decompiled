// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationTextAttribute
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation
{
  public class AutomationTextAttribute : AutomationIdentifier
  {
    internal AutomationTextAttribute(int id, string programmaticName)
      : base(AutomationIdType.TextAttribute, id, programmaticName)
    {
    }

    public static AutomationTextAttribute LookupById(int id) => AutomationIdentifier.LookupById<AutomationTextAttribute>(id);
  }
}
