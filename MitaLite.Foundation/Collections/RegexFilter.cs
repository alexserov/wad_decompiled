// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.RegexFilter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Text.RegularExpressions;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Collections {
    internal class RegexFilter : IFilter<AutomationElement> {
        readonly UIProperty _property;
        readonly Regex _regularExpression;

        public RegexFilter(UIProperty property, string regularExpression)
            : this(property: property, regularExpression: new Regex(pattern: regularExpression, options: RegexOptions.Compiled)) {
        }

        public RegexFilter(UIProperty property, Regex regularExpression) {
            Validate.ArgumentNotNull(parameter: property, parameterName: nameof(property));
            Validate.ArgumentNotNull(parameter: regularExpression, parameterName: nameof(regularExpression));
            this._property = property;
            this._regularExpression = regularExpression;
        }

        public bool Matches(AutomationElement item) {
            Validate.ArgumentNotNull(parameter: item, parameterName: nameof(item));
            var currentPropertyValue = item.GetCurrentPropertyValue(property: this._property.Property);
            return currentPropertyValue != null && this._regularExpression.IsMatch(input: currentPropertyValue.ToString());
        }

        public override string ToString() {
            return StringResource.Get(id: "RegexFilter_ToString_2", (object) this._property.ToString(), (object) this._regularExpression.ToString());
        }
    }
}