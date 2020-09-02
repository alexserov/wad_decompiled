// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.FilterConverter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections
{
  internal class FilterConverter : IFilter<AutomationElement>
  {
    private IFilter<UIObject> _uiObjectFilter;

    public FilterConverter(IFilter<UIObject> uiObjectFilter)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) uiObjectFilter, nameof (uiObjectFilter));
      this._uiObjectFilter = uiObjectFilter;
    }

    public bool Matches(AutomationElement item) => this._uiObjectFilter.Matches(new UIObject(item));
  }
}
