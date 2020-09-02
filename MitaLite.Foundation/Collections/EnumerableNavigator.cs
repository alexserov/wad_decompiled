// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.EnumerableNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections;
using System.Collections.Generic;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Collections {
    internal class EnumerableNavigator : UINavigator {
        readonly IEnumerable _enumerable;

        public EnumerableNavigator(IEnumerable enumerable) {
            Validate.ArgumentNotNull(parameter: enumerable, parameterName: nameof(enumerable));
            this._enumerable = enumerable;
        }

        public EnumerableNavigator(EnumerableNavigator previous)
            : base(previous: previous) {
            Validate.ArgumentNotNull(parameter: previous, parameterName: nameof(previous));
            Validate.ArgumentNotNull(parameter: previous._enumerable, parameterName: "previous.enumerable");
            this._enumerable = previous._enumerable;
        }

        public override UINavigator Duplicate() {
            return new EnumerableNavigator(previous: this);
        }

        public override IEnumerator<AutomationElement> GetEnumerator() {
            var enumerableNavigator = this;
            var filter = enumerableNavigator.Filter;
            foreach (AutomationElement element in enumerableNavigator._enumerable)
                if (filter.Matches(element: element))
                    yield return element;
        }

        public override string ToString() {
            return StringResource.Get(id: "EnumerableNavigator_ToString_1", (object) Filter.ToString());
        }
    }
}