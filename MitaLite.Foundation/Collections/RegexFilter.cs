// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.RegexFilter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Text.RegularExpressions;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections
{
  internal class RegexFilter : IFilter<AutomationElement>
  {
    private UIProperty _property;
    private Regex _regularExpression;

    public RegexFilter(UIProperty property, string regularExpression)
      : this(property, new Regex(regularExpression, RegexOptions.Compiled))
    {
    }

    public RegexFilter(UIProperty property, Regex regularExpression)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) property, nameof (property));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) regularExpression, nameof (regularExpression));
      this._property = property;
      this._regularExpression = regularExpression;
    }

    public bool Matches(AutomationElement item)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) item, nameof (item));
      object currentPropertyValue = item.GetCurrentPropertyValue(this._property.Property);
      return currentPropertyValue != null && this._regularExpression.IsMatch(currentPropertyValue.ToString());
    }

    public override string ToString() => StringResource.Get("RegexFilter_ToString_2", (object) this._property.ToString(), (object) this._regularExpression.ToString());
  }
}
