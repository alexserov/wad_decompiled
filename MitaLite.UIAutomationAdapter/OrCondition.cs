// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.OrCondition
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationAdapter.Utilities;
using UIAutomationClient;

namespace System.Windows.Automation {
    public class OrCondition : Condition {
        public OrCondition(params Condition[] conditions)
            : base(condition: Automation.AutomationClass.CreateOrConditionFromArray(conditions: GetIUIAutomationConditions(conditions: conditions))) {
        }

        internal OrCondition(params IUIAutomationCondition[] iUIAConditions)
            : base(condition: Automation.AutomationClass.CreateOrConditionFromArray(conditions: iUIAConditions)) {
        }

        public Condition[] GetConditions() {
            if (!(IUIAutomationCondition is IUIAutomationOrCondition automationCondition))
                return null;
            var typedArray = automationCondition.GetChildren().ToTypedArray<IUIAutomationCondition>();
            var conditionArray = new Condition[typedArray.Length];
            for (var index = 0; index < conditionArray.Length; ++index)
                if (typedArray[index] is IUIAutomationPropertyCondition) {
                    var propertyCondition = typedArray[index] as IUIAutomationPropertyCondition;
                    conditionArray[index] = new PropertyCondition(property: AutomationProperty.LookupById(id: propertyCondition.propertyId), value: propertyCondition.PropertyValue);
                } else if (typedArray[index] is IUIAutomationAndCondition) {
                    var automationAndCondition = typedArray[index] as IUIAutomationAndCondition;
                    conditionArray[index] = new AndCondition(iUIAConditions: automationAndCondition.GetChildren().ToTypedArray<IUIAutomationCondition>());
                } else if (typedArray[index] is IUIAutomationOrCondition) {
                    var automationOrCondition = typedArray[index] as IUIAutomationOrCondition;
                    conditionArray[index] = new OrCondition(iUIAConditions: automationOrCondition.GetChildren().ToTypedArray<IUIAutomationCondition>());
                } else if (typedArray[index] is IUIAutomationNotCondition) {
                    conditionArray[index] = new NotCondition(condition: new Condition(condition: typedArray[index]));
                } else if (typedArray[index] != null) {
                    conditionArray[index] = new Condition(condition: typedArray[index]);
                }

            return conditionArray;
        }
    }
}