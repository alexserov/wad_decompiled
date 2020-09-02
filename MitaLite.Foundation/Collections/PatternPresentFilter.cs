// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.PatternPresentFilter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Collections {
    internal class PatternPresentFilter : IFilter<AutomationElement> {
        readonly AutomationPattern _pattern;

        public PatternPresentFilter(AutomationPattern pattern) {
            Validate.ArgumentNotNull(parameter: pattern, parameterName: nameof(pattern));
            this._pattern = pattern;
        }

        public bool Matches(AutomationElement item) {
            Validate.ArgumentNotNull(parameter: item, parameterName: nameof(item));
            object patternObject = null;
            return item.TryGetCurrentPattern(pattern: this._pattern, patternObject: out patternObject);
        }

        public override string ToString() {
            return StringResource.Get(id: "PatternPresentFilter_ToString_1", (object) this._pattern.ToString());
        }
    }
}