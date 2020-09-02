// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.Condition
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class Condition
  {
    public static readonly Condition TrueCondition = (Condition) new Condition.BoolCondition(true);
    public static readonly Condition FalseCondition = (Condition) new Condition.BoolCondition(false);
    protected IUIAutomationCondition _condition;

    internal Condition(IUIAutomationCondition condition)
    {
      Validate.ArgumentNotNull((object) condition, nameof (condition));
      this._condition = condition;
    }

    internal Condition()
    {
    }

    internal IUIAutomationCondition IUIAutomationCondition
    {
      get => this._condition;
      set => this._condition = value;
    }

    internal static IUIAutomationCondition[] GetIUIAutomationConditions(
      Condition[] conditions)
    {
      IUIAutomationCondition[] automationConditionArray = new IUIAutomationCondition[conditions.Length];
      for (int index = 0; index < conditions.Length; ++index)
        automationConditionArray[index] = conditions[index].IUIAutomationCondition;
      return automationConditionArray;
    }

    private class BoolCondition : Condition
    {
      internal BoolCondition(bool trueCondition)
      {
        if (trueCondition)
          this._condition = System.Windows.Automation.Automation.AutomationClass.CreateTrueCondition();
        else
          this._condition = System.Windows.Automation.Automation.AutomationClass.CreateFalseCondition();
      }
    }
  }
}
