// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.UINavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections;
using System.Collections.Generic;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Collections;

namespace MS.Internal.Mita.Foundation {
    internal abstract class UINavigator : IEnumerable<AutomationElement>, IEnumerable {
        protected UINavigator() {
            Filter = new UIObjectFilter();
        }

        protected UINavigator(UINavigator previous) {
            Filter = new UIObjectFilter(previous: previous.Filter);
        }

        public virtual AutomationElement this[int index] {
            get {
                var enumerator = GetEnumerator();
                for (var index1 = 0; index1 <= index; ++index1)
                    if (!enumerator.MoveNext())
                        return null;
                return enumerator.Current;
            }
        }

        public virtual UIObjectFilter Filter { get; }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public abstract IEnumerator<AutomationElement> GetEnumerator();

        public abstract UINavigator Duplicate();

        public virtual UINavigator ToStaticNavigator() {
            return new StaticListNavigator(enumerable: this);
        }

        public void AddFilter(UICondition condition) {
            Filter.Add(condition: condition);
        }

        public void AddFilter(IFilter<UIObject> filter) {
            AddFilter(filter: new FilterConverter(uiObjectFilter: filter));
        }

        public void AddFilter(IFilter<AutomationElement> filter) {
            Filter.Add(filter: filter);
        }
    }
}