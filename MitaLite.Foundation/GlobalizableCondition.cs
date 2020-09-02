// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.GlobalizableCondition
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System.Collections.Generic;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation
{
  internal abstract class GlobalizableCondition
  {
    public static GlobalizableCondition Create(Condition condition)
    {
      GlobalizableCondition globalizableCondition;
      switch (condition)
      {
        case PropertyCondition condition1:
          globalizableCondition = (GlobalizableCondition) new GlobalizablePropertyCondition(condition1);
          break;
        case AndCondition condition1:
          globalizableCondition = (GlobalizableCondition) new GlobalizableAndCondition(condition1);
          break;
        case OrCondition condition1:
          globalizableCondition = (GlobalizableCondition) new GlobalizableOrCondition(condition1);
          break;
        case NotCondition notCondition:
          globalizableCondition = (GlobalizableCondition) new GlobalizableNotCondition(notCondition);
          break;
        default:
          globalizableCondition = (GlobalizableCondition) new GlobalizableOtherConditions(condition);
          break;
      }
      return globalizableCondition;
    }

    public static List<GlobalizableCondition> Create(Condition[] conditions)
    {
      List<GlobalizableCondition> globalizableConditionList = new List<GlobalizableCondition>(conditions.Length);
      for (int index = 0; index < conditions.Length; ++index)
        globalizableConditionList.Add(GlobalizableCondition.Create(conditions[index]));
      return globalizableConditionList;
    }

    public abstract Condition GlobalizeCondition(
      AutomationElement element,
      ref Dictionary<AutomationProperty, TranslatedStrings> propertyValueTranslations);

    public abstract Condition Condition { get; }
  }
}
