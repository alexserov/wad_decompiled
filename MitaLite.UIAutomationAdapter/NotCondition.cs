// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.NotCondition
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation
{
  public class NotCondition : Condition
  {
    public NotCondition(Condition condition)
      : base(System.Windows.Automation.Automation.AutomationClass.CreateNotCondition(condition.IUIAutomationCondition))
    {
    }

    public Condition Condition => new Condition(this.IUIAutomationCondition);
  }
}
