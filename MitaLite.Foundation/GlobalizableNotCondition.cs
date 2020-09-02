// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.GlobalizableNotCondition
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System.Collections.Generic;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation
{
  internal class GlobalizableNotCondition : GlobalizableCondition
  {
    private NotCondition _notCondition;
    private GlobalizableCondition _condition;

    public GlobalizableNotCondition(NotCondition notCondition)
    {
      this._notCondition = notCondition;
      this._condition = GlobalizableCondition.Create(this._notCondition.Condition);
    }

    public GlobalizableNotCondition(GlobalizableCondition condition)
    {
      this._condition = condition;
      this._notCondition = new NotCondition(condition.Condition);
    }

    public override Condition GlobalizeCondition(
      AutomationElement element,
      ref Dictionary<AutomationProperty, TranslatedStrings> propertyValueTranslations) => (Condition) new NotCondition(this._condition.GlobalizeCondition(element, ref propertyValueTranslations));

    public override Condition Condition => (Condition) this._notCondition;
  }
}
