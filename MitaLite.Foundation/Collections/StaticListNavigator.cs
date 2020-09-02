// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.StaticListNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Collections {
    internal class StaticListNavigator : UINavigator {
        readonly List<AutomationElement> _elementList;

        public StaticListNavigator(IEnumerable<AutomationElement> enumerable) {
            this._elementList = new List<AutomationElement>(collection: enumerable);
        }

        public StaticListNavigator(AutomationElement[] elementArray) {
            Validate.ArgumentNotNull(parameter: elementArray, parameterName: nameof(elementArray));
            this._elementList = new List<AutomationElement>(capacity: elementArray.Length);
            foreach (var element in elementArray)
                this._elementList.Insert(index: this._elementList.Count, item: element);
        }

        public StaticListNavigator(StaticListNavigator previous)
            : base(previous: previous) {
            Validate.ArgumentNotNull(parameter: previous, parameterName: nameof(previous));
            Validate.ArgumentNotNull(parameter: previous._elementList, parameterName: "previous.elementList");
            this._elementList = previous._elementList;
        }

        public override UINavigator Duplicate() {
            return new StaticListNavigator(previous: this);
        }

        public override IEnumerator<AutomationElement> GetEnumerator() {
            var staticListNavigator = this;
            var filter = staticListNavigator.Filter;
            foreach (var element in staticListNavigator._elementList)
                if (filter.Matches(element: element))
                    yield return element;
        }

        public override string ToString() {
            return StringResource.Get(id: "StaticListNavigator_ToString_1", (object) Filter.ToString());
        }
    }
}