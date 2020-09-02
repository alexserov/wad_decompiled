// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.GlobalizableNotCondition
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    internal class GlobalizableNotCondition : GlobalizableCondition {
        readonly GlobalizableCondition _condition;
        readonly NotCondition _notCondition;

        public GlobalizableNotCondition(NotCondition notCondition) {
            this._notCondition = notCondition;
            this._condition = Create(condition: this._notCondition.Condition);
        }

        public GlobalizableNotCondition(GlobalizableCondition condition) {
            this._condition = condition;
            this._notCondition = new NotCondition(condition: condition.Condition);
        }

        public override Condition Condition {
            get { return this._notCondition; }
        }

        public override Condition GlobalizeCondition(
            AutomationElement element,
            ref Dictionary<AutomationProperty, TranslatedStrings> propertyValueTranslations) {
            return new NotCondition(condition: this._condition.GlobalizeCondition(element: element, propertyValueTranslations: ref propertyValueTranslations));
        }
    }
}