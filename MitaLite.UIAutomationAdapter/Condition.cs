// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.Condition
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class Condition {
        public static readonly Condition TrueCondition = new BoolCondition(trueCondition: true);
        public static readonly Condition FalseCondition = new BoolCondition(trueCondition: false);
        protected IUIAutomationCondition _condition;

        internal Condition(IUIAutomationCondition condition) {
            Validate.ArgumentNotNull(parameter: condition, parameterName: nameof(condition));
            this._condition = condition;
        }

        internal Condition() {
        }

        internal IUIAutomationCondition IUIAutomationCondition {
            get { return this._condition; }
            set { this._condition = value; }
        }

        internal static IUIAutomationCondition[] GetIUIAutomationConditions(
            Condition[] conditions) {
            var automationConditionArray = new IUIAutomationCondition[conditions.Length];
            for (var index = 0; index < conditions.Length; ++index)
                automationConditionArray[index] = conditions[index].IUIAutomationCondition;
            return automationConditionArray;
        }

        class BoolCondition : Condition {
            internal BoolCondition(bool trueCondition) {
                if (trueCondition)
                    this._condition = Automation.AutomationClass.CreateTrueCondition();
                else
                    this._condition = Automation.AutomationClass.CreateFalseCondition();
            }
        }
    }
}