﻿// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.SiblingsNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Collections {
    internal class SiblingsNavigator : UINavigator {
        AutomationElement _root;
        TreeWalker _treeWalker;

        public SiblingsNavigator(UIObject root, UICondition treeCondition) {
            Validate.ArgumentNotNull(parameter: root, parameterName: nameof(root));
            Initialize(root: root.AutomationElement, treeCondition: treeCondition);
        }

        public SiblingsNavigator(AutomationElement root, UICondition treeCondition) {
            Initialize(root: root, treeCondition: treeCondition);
        }

        public SiblingsNavigator(SiblingsNavigator previous)
            : base(previous: previous) {
            Validate.ArgumentNotNull(parameter: previous, parameterName: nameof(previous));
            this._root = previous._root;
            this._treeWalker = previous._treeWalker;
        }

        void Initialize(AutomationElement root, UICondition treeCondition) {
            Validate.ArgumentNotNull(parameter: root, parameterName: nameof(root));
            Validate.ArgumentNotNull(parameter: treeCondition, parameterName: nameof(treeCondition));
            this._root = root;
            this._treeWalker = new TreeWalker(condition: treeCondition.Condition);
        }

        public override UINavigator Duplicate() {
            return new SiblingsNavigator(previous: this);
        }

        public override IEnumerator<AutomationElement> GetEnumerator() {
            var siblingsNavigator = this;
            var filter = siblingsNavigator.Filter;
            AutomationElement current;
            for (current = siblingsNavigator._treeWalker.GetFirstChild(element: siblingsNavigator._treeWalker.GetParent(element: siblingsNavigator._root)); current != (AutomationElement) null; current = siblingsNavigator._treeWalker.GetNextSibling(element: current))
                if (!Automation.Compare(el1: current, el2: siblingsNavigator._root) && filter.Matches(element: current))
                    yield return current;
            current = null;
        }

        public override string ToString() {
            return StringResource.Get(id: "SiblingsNavigator_ToString_3", (object) new UIObject(element: this._root).ToString(), (object) UICondition.ToString(condition: this._treeWalker.Condition), (object) Filter.ToString());
        }
    }
}