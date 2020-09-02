// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.GlobalizableOrCondition
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System.Collections.Generic;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation
{
  internal class GlobalizableOrCondition : GlobalizableCondition
  {
    private List<GlobalizableCondition> _conditions;
    private OrCondition _condition;

    public GlobalizableOrCondition(OrCondition condition)
    {
      this._condition = condition;
      this._conditions = GlobalizableCondition.Create(condition.GetConditions());
    }

    public GlobalizableOrCondition(params GlobalizableCondition[] conditions)
    {
      this._conditions = new List<GlobalizableCondition>((IEnumerable<GlobalizableCondition>) conditions);
      Condition[] conditionArray = new Condition[this._conditions.Count];
      for (int index = 0; index < this._conditions.Count; ++index)
        conditionArray[index] = this._conditions[index].Condition;
      this._condition = new OrCondition(conditionArray);
    }

    public override Condition GlobalizeCondition(
      AutomationElement element,
      ref Dictionary<AutomationProperty, TranslatedStrings> propertyValueTranslations)
    {
      Condition[] conditionArray = new Condition[this._conditions.Count];
      for (int index = 0; index < this._conditions.Count; ++index)
        conditionArray[index] = this._conditions[index].GlobalizeCondition(element, ref propertyValueTranslations);
      return (Condition) new OrCondition(conditionArray);
    }

    public override Condition Condition => (Condition) this._condition;
  }
}
