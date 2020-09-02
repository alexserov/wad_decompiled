// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.UIBreadthFirstDescendants`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections {
    public class UIBreadthFirstDescendants<T> : UICollection<T> where T : UIObject {
        public UIBreadthFirstDescendants(UIObject root, IFactory<T> factory)
            : base(navigator: new BreadthFirstDescendantsNavigator(root: root, treeCondition: Context.Current.TreeCondition), factory: factory) {
        }

        internal UIBreadthFirstDescendants(AutomationElement root, IFactory<T> factory)
            : base(navigator: new BreadthFirstDescendantsNavigator(root: root, treeCondition: Context.Current.TreeCondition), factory: factory) {
        }

        public UIBreadthFirstDescendants(UIObject root, UICondition treeCondition, IFactory<T> factory)
            : base(navigator: new BreadthFirstDescendantsNavigator(root: root, treeCondition: treeCondition), factory: factory) {
        }

        internal UIBreadthFirstDescendants(
            AutomationElement root,
            UICondition treeCondition,
            IFactory<T> factory)
            : base(navigator: new BreadthFirstDescendantsNavigator(root: root, treeCondition: treeCondition), factory: factory) {
        }
    }
}