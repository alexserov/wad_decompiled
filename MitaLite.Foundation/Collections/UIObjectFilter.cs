// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.UIObjectFilter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections
{
  public class UIObjectFilter
  {
    private UICondition _condition;
    private List<IFilter<AutomationElement>> _filters;

    public UIObjectFilter()
    {
      this._filters = new List<IFilter<AutomationElement>>();
      this._condition = UICondition.True;
    }

    public UIObjectFilter(UIObjectFilter previous)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) previous, nameof (previous));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) previous.UICondition, "previous.UICondition");
      this._filters = new List<IFilter<AutomationElement>>((IEnumerable<IFilter<AutomationElement>>) previous._filters);
      this._condition = previous.UICondition;
    }

    internal bool Matches(AutomationElement element) => !(element == (AutomationElement) null) && UIObject.Matches(element, this._condition) && this.MatchesFilter(element);

    internal bool MatchesFilter(AutomationElement element)
    {
      foreach (IFilter<AutomationElement> filter in this._filters)
      {
        if (!filter.Matches(element))
          return false;
      }
      return true;
    }

    public void Add(UICondition condition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) condition, nameof (condition));
      this._condition = this._condition.AndWith(condition);
    }

    public void Add(UIProperty property, Regex regularExpression) => this.Add((IFilter<AutomationElement>) new RegexFilter(property, regularExpression));

    public void Add(Predicate<UIObject> filter) => this.Add((IFilter<AutomationElement>) new DelegateFilter(filter));

    internal void Add(IFilter<AutomationElement> filter) => this._filters.Insert(this._filters.Count, filter);

    public override string ToString() => this._filters.Count == 0 ? string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Condition: '{0}'", (object) this._condition.ToString()) : string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Condition: '{0}', and one or more IFilter instances.", (object) this._condition.ToString());

    public UICondition UICondition => this._condition;
  }
}
