// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.BreadthFirstDescendantsNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Collections {
    internal class BreadthFirstDescendantsNavigator : UINavigator, IDisposable {
        BreadthFirstTreeEnumerator<AutomationElement> _treeEnumerator;

        public BreadthFirstDescendantsNavigator(UIObject root, UICondition treeCondition) {
            Validate.ArgumentNotNull(parameter: root, parameterName: nameof(root));
            Initialize(root: root.AutomationElement, treeCondition: treeCondition);
        }

        public BreadthFirstDescendantsNavigator(AutomationElement root, UICondition treeCondition) {
            Initialize(root: root, treeCondition: treeCondition);
        }

        public BreadthFirstDescendantsNavigator(BreadthFirstDescendantsNavigator previous)
            : base(previous: previous) {
            Validate.ArgumentNotNull(parameter: previous, parameterName: nameof(previous));
            this._treeEnumerator = new BreadthFirstTreeEnumerator<AutomationElement>(previous: previous._treeEnumerator);
        }

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(obj: this);
        }

        void Initialize(AutomationElement root, UICondition treeCondition) {
            this._treeEnumerator = new BreadthFirstTreeEnumerator<AutomationElement>(root: root, navigator: new AutomationElementTreeNavigator(treeWalker: new TreeWalker(condition: treeCondition.Condition)), ignoreRoot: true);
        }

        ~BreadthFirstDescendantsNavigator() {
            Dispose(disposing: false);
        }

        public override UINavigator Duplicate() {
            return new BreadthFirstDescendantsNavigator(previous: this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing && this._treeEnumerator != null)
                this._treeEnumerator.Dispose();
            this._treeEnumerator = null;
        }

        public override IEnumerator<AutomationElement> GetEnumerator() {
            var descendantsNavigator = this;
            var filter = descendantsNavigator.Filter;
            descendantsNavigator._treeEnumerator.Reset();
            while (descendantsNavigator._treeEnumerator.MoveNext())
                if (filter.Matches(element: descendantsNavigator._treeEnumerator.Current))
                    yield return descendantsNavigator._treeEnumerator.Current;
        }

        public override string ToString() {
            return StringResource.Get(id: "BreadthFirstDescendantsNavigator_ToString_2", (object) new UIObject(element: this._treeEnumerator.Root).ToString(), (object) Filter.ToString());
        }
    }
}