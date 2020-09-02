// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.UINavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Collections;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation
{
  internal abstract class UINavigator : IEnumerable<AutomationElement>, IEnumerable
  {
    private UIObjectFilter _filter;

    protected UINavigator() => this._filter = new UIObjectFilter();

    protected UINavigator(UINavigator previous) => this._filter = new UIObjectFilter(previous.Filter);

    public abstract UINavigator Duplicate();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public abstract IEnumerator<AutomationElement> GetEnumerator();

    public virtual AutomationElement this[int index]
    {
      get
      {
        IEnumerator<AutomationElement> enumerator = this.GetEnumerator();
        for (int index1 = 0; index1 <= index; ++index1)
        {
          if (!enumerator.MoveNext())
            return (AutomationElement) null;
        }
        return enumerator.Current;
      }
    }

    public virtual UINavigator ToStaticNavigator() => (UINavigator) new StaticListNavigator((IEnumerable<AutomationElement>) this);

    public void AddFilter(UICondition condition) => this.Filter.Add(condition);

    public void AddFilter(IFilter<UIObject> filter) => this.AddFilter((IFilter<AutomationElement>) new FilterConverter(filter));

    public void AddFilter(IFilter<AutomationElement> filter) => this.Filter.Add(filter);

    public virtual UIObjectFilter Filter => this._filter;
  }
}
