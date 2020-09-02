// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.GlobalizablePropertyCondition
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using MS.Internal.Mita.Localization;
using System.Collections.Generic;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation
{
  internal class GlobalizablePropertyCondition : GlobalizableCondition
  {
    private object _value;
    private AutomationProperty _property;
    private PropertyCondition _condition;

    public GlobalizablePropertyCondition(PropertyCondition condition)
    {
      this._condition = condition;
      this._property = condition.Property;
      this._value = condition.Value;
    }

    public GlobalizablePropertyCondition(AutomationProperty property, object value)
    {
      this._property = property;
      this._value = value;
      if (this._value is ILocalizationProvider2)
        this._condition = new PropertyCondition(this._property, (object) this._value.ToString());
      else
        this._condition = new PropertyCondition(this._property, this._value);
    }

    public override Condition GlobalizeCondition(
      AutomationElement element,
      ref Dictionary<AutomationProperty, TranslatedStrings> propertyValueTranslations)
    {
      Condition condition = (Condition) this._condition;
      if (UICondition.IsGlobalizableProperty(this._property))
      {
        TranslatedStrings translatedStrings;
        if (!propertyValueTranslations.TryGetValue(this._property, out translatedStrings))
        {
          translatedStrings = new TranslatedStrings();
          propertyValueTranslations[this._property] = translatedStrings;
        }
        ILocalizedStrings localizedStrings = !(this._value is ILocalizationProvider2) ? (ILocalizedStrings) new LocalizedStringsAdapter(this._value.ToString()) : (ILocalizedStrings) new LocalizedStrings2(element, (ILocalizationProvider2) this._value);
        IStringResourceData[] translations = localizedStrings.GetTranslations();
        if (1 == translations.Length)
        {
          translatedStrings.Add(translations[0].Raw(), localizedStrings, 0L);
          condition = (Condition) new PropertyCondition(this._property, (object) translations[0].DefaultString());
        }
        else
        {
          Condition[] conditionArray = new Condition[translations.Length];
          for (int index = 0; index < translations.Length; ++index)
          {
            translatedStrings.Add(translations[index].Raw(), localizedStrings, (long) index);
            conditionArray[index] = (Condition) new PropertyCondition(this._property, (object) translations[index].DefaultString());
          }
          condition = (Condition) new OrCondition(conditionArray);
        }
      }
      return condition;
    }

    public override Condition Condition => (Condition) this._condition;
  }
}
