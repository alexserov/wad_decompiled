// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.Tree
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls {
    public class Tree : Tree<Tree, TreeItem> {
        static IFactory<Tree> _factory;

        public Tree(UIObject uiObject)
            : base(uiObject: uiObject, itemFactory: TreeItem.Factory) {
        }

        internal Tree(AutomationElement element)
            : base(element: element, itemFactory: TreeItem.Factory) {
        }

        public static IFactory<Tree> Factory {
            get {
                if (_factory == null)
                    _factory = new TreeFactory();
                return _factory;
            }
        }

        class TreeFactory : IFactory<Tree> {
            public Tree Create(UIObject element) {
                return new Tree(uiObject: element);
            }
        }
    }
}