// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.GlobalizableOrCondition
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    internal class GlobalizableOrCondition : GlobalizableCondition {
        readonly OrCondition _condition;
        readonly List<GlobalizableCondition> _conditions;

        public GlobalizableOrCondition(OrCondition condition) {
            this._condition = condition;
            this._conditions = Create(conditions: condition.GetConditions());
        }

        public GlobalizableOrCondition(params GlobalizableCondition[] conditions) {
            this._conditions = new List<GlobalizableCondition>(collection: conditions);
            var conditionArray = new Condition[this._conditions.Count];
            for (var index = 0; index < this._conditions.Count; ++index)
                conditionArray[index] = this._conditions[index: index].Condition;
            this._condition = new OrCondition(conditions: conditionArray);
        }

        public override Condition Condition {
            get { return this._condition; }
        }

        public override Condition GlobalizeCondition(
            AutomationElement element,
            ref Dictionary<AutomationProperty, TranslatedStrings> propertyValueTranslations) {
            var conditionArray = new Condition[this._conditions.Count];
            for (var index = 0; index < this._conditions.Count; ++index)
                conditionArray[index] = this._conditions[index: index].GlobalizeCondition(element: element, propertyValueTranslations: ref propertyValueTranslations);
            return new OrCondition(conditions: conditionArray);
        }
    }
}