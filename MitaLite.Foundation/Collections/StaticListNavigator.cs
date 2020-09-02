// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.StaticListNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections
{
  internal class StaticListNavigator : UINavigator
  {
    private List<AutomationElement> _elementList;

    public StaticListNavigator(IEnumerable<AutomationElement> enumerable) => this._elementList = new List<AutomationElement>(enumerable);

    public StaticListNavigator(AutomationElement[] elementArray)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) elementArray, nameof (elementArray));
      this._elementList = new List<AutomationElement>(elementArray.Length);
      foreach (AutomationElement element in elementArray)
        this._elementList.Insert(this._elementList.Count, element);
    }

    public StaticListNavigator(StaticListNavigator previous)
      : base((UINavigator) previous)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) previous, nameof (previous));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) previous._elementList, "previous.elementList");
      this._elementList = previous._elementList;
    }

    public override UINavigator Duplicate() => (UINavigator) new StaticListNavigator(this);

    public override IEnumerator<AutomationElement> GetEnumerator()
    {
      StaticListNavigator staticListNavigator = this;
      UIObjectFilter filter = staticListNavigator.Filter;
      foreach (AutomationElement element in staticListNavigator._elementList)
      {
        if (filter.Matches(element))
          yield return element;
      }
    }

    public override string ToString() => StringResource.Get("StaticListNavigator_ToString_1", (object) this.Filter.ToString());
  }
}
