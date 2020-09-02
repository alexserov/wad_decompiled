// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.UIAncestors`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections {
    public class UIAncestors<I> : UICollection<I> where I : UIObject {
        public UIAncestors(UIObject root, IFactory<I> factory)
            : base(navigator: new AncestorsNavigator(root: root, treeCondition: Context.Current.TreeCondition), factory: factory) {
        }

        internal UIAncestors(AutomationElement root, IFactory<I> factory)
            : base(navigator: new AncestorsNavigator(root: root, treeCondition: Context.Current.TreeCondition), factory: factory) {
        }

        public UIAncestors(UIObject root, UICondition treeCondition, IFactory<I> factory)
            : base(navigator: new AncestorsNavigator(root: root, treeCondition: treeCondition), factory: factory) {
        }

        internal UIAncestors(AutomationElement root, UICondition treeCondition, IFactory<I> factory)
            : base(navigator: new AncestorsNavigator(root: root, treeCondition: treeCondition), factory: factory) {
        }
    }
}