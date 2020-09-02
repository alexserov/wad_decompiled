// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.GlobalizableCondition
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    internal abstract class GlobalizableCondition {
        public abstract Condition Condition { get; }

        public static GlobalizableCondition Create(Condition condition) {
            GlobalizableCondition globalizableCondition;
            switch (condition) {
                case PropertyCondition condition1:
                    globalizableCondition = new GlobalizablePropertyCondition(condition: condition1);
                    break;
                case AndCondition condition1:
                    globalizableCondition = new GlobalizableAndCondition(condition: condition1);
                    break;
                case OrCondition condition1:
                    globalizableCondition = new GlobalizableOrCondition(condition: condition1);
                    break;
                case NotCondition notCondition:
                    globalizableCondition = new GlobalizableNotCondition(notCondition: notCondition);
                    break;
                default:
                    globalizableCondition = new GlobalizableOtherConditions(condition: condition);
                    break;
            }

            return globalizableCondition;
        }

        public static List<GlobalizableCondition> Create(Condition[] conditions) {
            var globalizableConditionList = new List<GlobalizableCondition>(capacity: conditions.Length);
            for (var index = 0; index < conditions.Length; ++index)
                globalizableConditionList.Add(item: Create(condition: conditions[index]));
            return globalizableConditionList;
        }

        public abstract Condition GlobalizeCondition(
            AutomationElement element,
            ref Dictionary<AutomationProperty, TranslatedStrings> propertyValueTranslations);
    }
}