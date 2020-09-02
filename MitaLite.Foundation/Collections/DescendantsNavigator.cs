// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.DescendantsNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Collections {
    internal class DescendantsNavigator : UINavigator {
        AutomationElement _root;
        UICondition _treeCondition;

        public DescendantsNavigator(UIObject root, UICondition treeCondition) {
            Validate.ArgumentNotNull(parameter: root, parameterName: nameof(root));
            Initialize(root: root.AutomationElement, treeCondition: treeCondition);
        }

        public DescendantsNavigator(AutomationElement root, UICondition treeCondition) {
            Initialize(root: root, treeCondition: treeCondition);
        }

        public DescendantsNavigator(DescendantsNavigator previous)
            : base(previous: previous) {
            Validate.ArgumentNotNull(parameter: previous, parameterName: nameof(previous));
            Initialize(root: previous._root, treeCondition: previous._treeCondition);
        }

        void Initialize(AutomationElement root, UICondition treeCondition) {
            Validate.ArgumentNotNull(parameter: root, parameterName: nameof(root));
            Validate.ArgumentNotNull(parameter: treeCondition, parameterName: nameof(treeCondition));
            this._root = root;
            this._treeCondition = treeCondition;
        }

        public override UINavigator Duplicate() {
            return new DescendantsNavigator(previous: this);
        }

        public override IEnumerator<AutomationElement> GetEnumerator() {
            var descendantsNavigator = this;
            var filter = descendantsNavigator.Filter;
            foreach (AutomationElement element in filter.UICondition.FindAll(root: descendantsNavigator._root, scope: Scope.Descendants, cr: new CacheRequest {
                TreeFilter = descendantsNavigator._treeCondition.Condition
            }))
                if (filter.MatchesFilter(element: element))
                    yield return element;
        }

        public override string ToString() {
            return StringResource.Get(id: "DescendantsNavigator_ToString_3", (object) new UIObject(element: this._root).ToString(), (object) this._treeCondition.ToString(), (object) Filter.ToString());
        }
    }
}