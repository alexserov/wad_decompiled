// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.GlobalizableOtherConditions
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System.Collections.Generic;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation
{
  internal class GlobalizableOtherConditions : GlobalizableCondition
  {
    private Condition _condition;

    public GlobalizableOtherConditions(Condition condition) => this._condition = condition;

    public override Condition GlobalizeCondition(
      AutomationElement element,
      ref Dictionary<AutomationProperty, TranslatedStrings> propertyValueTranslations) => this._condition;

    public override Condition Condition => this._condition;
  }
}
