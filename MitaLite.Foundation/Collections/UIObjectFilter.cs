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
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Collections {
    public class UIObjectFilter {
        readonly List<IFilter<AutomationElement>> _filters;

        public UIObjectFilter() {
            this._filters = new List<IFilter<AutomationElement>>();
            UICondition = UICondition.True;
        }

        public UIObjectFilter(UIObjectFilter previous) {
            Validate.ArgumentNotNull(parameter: previous, parameterName: nameof(previous));
            Validate.ArgumentNotNull(parameter: previous.UICondition, parameterName: "previous.UICondition");
            this._filters = new List<IFilter<AutomationElement>>(collection: previous._filters);
            UICondition = previous.UICondition;
        }

        public UICondition UICondition { get; set; }

        internal bool Matches(AutomationElement element) {
            return !(element == null) && UIObject.Matches(element: element, condition: UICondition) && MatchesFilter(element: element);
        }

        internal bool MatchesFilter(AutomationElement element) {
            foreach (var filter in this._filters)
                if (!filter.Matches(item: element))
                    return false;
            return true;
        }

        public void Add(UICondition condition) {
            Validate.ArgumentNotNull(parameter: condition, parameterName: nameof(condition));
            UICondition = UICondition.AndWith(newCondition: condition);
        }

        public void Add(UIProperty property, Regex regularExpression) {
            Add(filter: new RegexFilter(property: property, regularExpression: regularExpression));
        }

        public void Add(Predicate<UIObject> filter) {
            Add(filter: new DelegateFilter(callback: filter));
        }

        internal void Add(IFilter<AutomationElement> filter) {
            this._filters.Insert(index: this._filters.Count, item: filter);
        }

        public override string ToString() {
            return this._filters.Count == 0 ? string.Format(provider: CultureInfo.InvariantCulture, format: "Condition: '{0}'", arg0: UICondition.ToString()) : string.Format(provider: CultureInfo.InvariantCulture, format: "Condition: '{0}', and one or more IFilter instances.", arg0: UICondition.ToString());
        }
    }
}