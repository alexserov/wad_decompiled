// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.TreeItem
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls {
    public class TreeItem : TreeItem<Tree, TreeItem> {
        static IFactory<TreeItem> _factory;

        public TreeItem(UIObject uiObject)
            : base(uiObject: uiObject, containerFactory: Tree.Factory, itemFactory: Factory) {
        }

        internal TreeItem(AutomationElement element)
            : base(element: element, containerFactory: Tree.Factory, itemFactory: Factory) {
        }

        public static IFactory<TreeItem> Factory {
            get {
                if (_factory == null)
                    _factory = new TreeItemFactory();
                return _factory;
            }
        }

        class TreeItemFactory : IFactory<TreeItem> {
            public TreeItem Create(UIObject element) => new TreeItem(uiObject: element);
        }
    }
}