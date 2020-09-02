// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.OrCondition
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationAdapter.Utilities;
using UIAutomationClient;

namespace System.Windows.Automation
{
  public class OrCondition : Condition
  {
    public OrCondition(params Condition[] conditions)
      : base(System.Windows.Automation.Automation.AutomationClass.CreateOrConditionFromArray(Condition.GetIUIAutomationConditions(conditions)))
    {
    }

    internal OrCondition(params IUIAutomationCondition[] iUIAConditions)
      : base(System.Windows.Automation.Automation.AutomationClass.CreateOrConditionFromArray(iUIAConditions))
    {
    }

    public Condition[] GetConditions()
    {
      if (!(this.IUIAutomationCondition is IUIAutomationOrCondition automationCondition))
        return (Condition[]) null;
      IUIAutomationCondition[] typedArray = automationCondition.GetChildren().ToTypedArray<IUIAutomationCondition>();
      Condition[] conditionArray = new Condition[typedArray.Length];
      for (int index = 0; index < conditionArray.Length; ++index)
      {
        if (typedArray[index] is IUIAutomationPropertyCondition)
        {
          IUIAutomationPropertyCondition propertyCondition = typedArray[index] as IUIAutomationPropertyCondition;
          conditionArray[index] = (Condition) new PropertyCondition(AutomationProperty.LookupById(propertyCondition.propertyId), (object) propertyCondition.PropertyValue);
        }
        else if (typedArray[index] is IUIAutomationAndCondition)
        {
          IUIAutomationAndCondition automationAndCondition = typedArray[index] as IUIAutomationAndCondition;
          conditionArray[index] = (Condition) new AndCondition(automationAndCondition.GetChildren().ToTypedArray<IUIAutomationCondition>());
        }
        else if (typedArray[index] is IUIAutomationOrCondition)
        {
          IUIAutomationOrCondition automationOrCondition = typedArray[index] as IUIAutomationOrCondition;
          conditionArray[index] = (Condition) new OrCondition(automationOrCondition.GetChildren().ToTypedArray<IUIAutomationCondition>());
        }
        else if (typedArray[index] is IUIAutomationNotCondition)
          conditionArray[index] = (Condition) new NotCondition(new Condition(typedArray[index]));
        else if (typedArray[index] != null)
          conditionArray[index] = new Condition(typedArray[index]);
      }
      return conditionArray;
    }
  }
}
