// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.EnumerableNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections;
using System.Collections.Generic;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections
{
  internal class EnumerableNavigator : UINavigator
  {
    private IEnumerable _enumerable;

    public EnumerableNavigator(IEnumerable enumerable)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) enumerable, nameof (enumerable));
      this._enumerable = enumerable;
    }

    public EnumerableNavigator(EnumerableNavigator previous)
      : base((UINavigator) previous)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) previous, nameof (previous));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) previous._enumerable, "previous.enumerable");
      this._enumerable = previous._enumerable;
    }

    public override UINavigator Duplicate() => (UINavigator) new EnumerableNavigator(this);

    public override IEnumerator<AutomationElement> GetEnumerator()
    {
      EnumerableNavigator enumerableNavigator = this;
      UIObjectFilter filter = enumerableNavigator.Filter;
      foreach (AutomationElement element in enumerableNavigator._enumerable)
      {
        if (filter.Matches(element))
          yield return element;
      }
    }

    public override string ToString() => StringResource.Get("EnumerableNavigator_ToString_1", (object) this.Filter.ToString());
  }
}
