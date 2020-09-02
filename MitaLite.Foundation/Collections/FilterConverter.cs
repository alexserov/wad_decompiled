// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.FilterConverter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Collections {
    internal class FilterConverter : IFilter<AutomationElement> {
        readonly IFilter<UIObject> _uiObjectFilter;

        public FilterConverter(IFilter<UIObject> uiObjectFilter) {
            Validate.ArgumentNotNull(parameter: uiObjectFilter, parameterName: nameof(uiObjectFilter));
            this._uiObjectFilter = uiObjectFilter;
        }

        public bool Matches(AutomationElement item) {
            return this._uiObjectFilter.Matches(item: new UIObject(element: item));
        }
    }
}