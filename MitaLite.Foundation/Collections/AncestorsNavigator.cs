// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.AncestorsNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Collections {
    internal class AncestorsNavigator : UINavigator {
        AutomationElement _root;
        TreeWalker _treeWalker;

        public AncestorsNavigator(UIObject root, UICondition treeCondition) {
            Validate.ArgumentNotNull(parameter: root, parameterName: nameof(root));
            Validate.ArgumentNotNull(parameter: treeCondition, parameterName: nameof(treeCondition));
            Initialize(root: root.AutomationElement, treeWalker: new TreeWalker(condition: treeCondition.Condition));
        }

        public AncestorsNavigator(AutomationElement root, UICondition treeCondition) {
            Validate.ArgumentNotNull(parameter: treeCondition, parameterName: nameof(treeCondition));
            Initialize(root: root, treeWalker: new TreeWalker(condition: treeCondition.Condition));
        }

        public AncestorsNavigator(AncestorsNavigator previous)
            : base(previous: previous) {
            Validate.ArgumentNotNull(parameter: previous, parameterName: nameof(previous));
            Initialize(root: previous._root, treeWalker: previous._treeWalker);
        }

        void Initialize(AutomationElement root, TreeWalker treeWalker) {
            Validate.ArgumentNotNull(parameter: root, parameterName: nameof(root));
            Validate.ArgumentNotNull(parameter: treeWalker, parameterName: nameof(treeWalker));
            this._root = root;
            this._treeWalker = treeWalker;
        }

        public override UINavigator Duplicate() {
            return new AncestorsNavigator(previous: this);
        }

        public override IEnumerator<AutomationElement> GetEnumerator() {
            var ancestorsNavigator = this;
            var filter = ancestorsNavigator.Filter;
            AutomationElement current;
            for (current = ancestorsNavigator._treeWalker.GetParent(element: ancestorsNavigator._root); current != (AutomationElement) null; current = ancestorsNavigator._treeWalker.GetParent(element: current))
                if (filter.Matches(element: current))
                    yield return current;
            current = null;
        }

        public override string ToString() {
            return StringResource.Get(id: "AncestorsNavigator_ToString_3", (object) new UIObject(element: this._root).ToString(), (object) UICondition.ToString(condition: this._treeWalker.Condition), (object) Filter.ToString());
        }
    }
}