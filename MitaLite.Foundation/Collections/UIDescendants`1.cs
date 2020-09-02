// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.UIDescendants`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections {
    public class UIDescendants<I> : UICollection<I> where I : UIObject {
        public UIDescendants(UIObject root, IFactory<I> factory)
            : base(navigator: new DescendantsNavigator(root: root, treeCondition: Context.Current.TreeCondition), factory: factory) {
        }

        internal UIDescendants(AutomationElement root, IFactory<I> factory)
            : base(navigator: new DescendantsNavigator(root: root, treeCondition: Context.Current.TreeCondition), factory: factory) {
        }

        public UIDescendants(UIObject root, UICondition treeCondition, IFactory<I> factory)
            : base(navigator: new DescendantsNavigator(root: root, treeCondition: treeCondition), factory: factory) {
        }

        internal UIDescendants(AutomationElement root, UICondition treeCondition, IFactory<I> factory)
            : base(navigator: new DescendantsNavigator(root: root, treeCondition: treeCondition), factory: factory) {
        }
    }
}