// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.GlobalizablePropertyCondition
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;
using MS.Internal.Mita.Localization;

namespace MS.Internal.Mita.Foundation {
    internal class GlobalizablePropertyCondition : GlobalizableCondition {
        readonly PropertyCondition _condition;
        readonly AutomationProperty _property;
        readonly object _value;

        public GlobalizablePropertyCondition(PropertyCondition condition) {
            this._condition = condition;
            this._property = condition.Property;
            this._value = condition.Value;
        }

        public GlobalizablePropertyCondition(AutomationProperty property, object value) {
            this._property = property;
            this._value = value;
            if (this._value is ILocalizationProvider2)
                this._condition = new PropertyCondition(property: this._property, value: this._value.ToString());
            else
                this._condition = new PropertyCondition(property: this._property, value: this._value);
        }

        public override Condition Condition {
            get { return this._condition; }
        }

        public override Condition GlobalizeCondition(
            AutomationElement element,
            ref Dictionary<AutomationProperty, TranslatedStrings> propertyValueTranslations) {
            Condition condition = this._condition;
            if (UICondition.IsGlobalizableProperty(property: this._property)) {
                TranslatedStrings translatedStrings;
                if (!propertyValueTranslations.TryGetValue(key: this._property, value: out translatedStrings)) {
                    translatedStrings = new TranslatedStrings();
                    propertyValueTranslations[key: this._property] = translatedStrings;
                }

                var localizedStrings = !(this._value is ILocalizationProvider2) ? new LocalizedStringsAdapter(taggedText: this._value.ToString()) : (ILocalizedStrings) new LocalizedStrings2(contextElement: element, provider: (ILocalizationProvider2) this._value);
                var translations = localizedStrings.GetTranslations();
                if (1 == translations.Length) {
                    translatedStrings.Add(translatedString: translations[0].Raw(), localizedStrings: localizedStrings, index: 0L);
                    condition = new PropertyCondition(property: this._property, value: translations[0].DefaultString());
                } else {
                    var conditionArray = new Condition[translations.Length];
                    for (var index = 0; index < translations.Length; ++index) {
                        translatedStrings.Add(translatedString: translations[index].Raw(), localizedStrings: localizedStrings, index: index);
                        conditionArray[index] = new PropertyCondition(property: this._property, value: translations[index].DefaultString());
                    }

                    condition = new OrCondition(conditions: conditionArray);
                }
            }

            return condition;
        }
    }
}