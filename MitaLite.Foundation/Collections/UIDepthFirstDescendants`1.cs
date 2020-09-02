// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.UIDepthFirstDescendants`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections {
    public class UIDepthFirstDescendants<T> : UICollection<T> where T : UIObject {
        public UIDepthFirstDescendants(UIObject root, IFactory<T> factory)
            : base(navigator: new DepthFirstDescendantsNavigator(root: root, treeCondition: Context.Current.TreeCondition), factory: factory) {
        }

        internal UIDepthFirstDescendants(AutomationElement root, IFactory<T> factory)
            : base(navigator: new DepthFirstDescendantsNavigator(root: root, treeCondition: Context.Current.TreeCondition), factory: factory) {
        }

        public UIDepthFirstDescendants(UIObject root, UICondition treeCondition, IFactory<T> factory)
            : base(navigator: new DepthFirstDescendantsNavigator(root: root, treeCondition: treeCondition), factory: factory) {
        }

        internal UIDepthFirstDescendants(
            AutomationElement root,
            UICondition treeCondition,
            IFactory<T> factory)
            : base(navigator: new DepthFirstDescendantsNavigator(root: root, treeCondition: treeCondition), factory: factory) {
        }
    }
}