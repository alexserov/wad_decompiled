// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ItemsView
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls {
    public class ItemsView : ItemsView<ItemsView, ItemsViewItem> {
        static IFactory<ItemsView> _factory;

        public ItemsView(UIObject uiObject)
            : base(uiObject: uiObject, itemFactory: ItemsViewItem.Factory) {
        }

        internal ItemsView(AutomationElement element)
            : base(element: element, itemFactory: ItemsViewItem.Factory) {
        }

        public static IFactory<ItemsView> Factory {
            get {
                if (_factory == null)
                    _factory = new ItemsViewFactory();
                return _factory;
            }
        }

        class ItemsViewFactory : IFactory<ItemsView> {
            public ItemsView Create(UIObject element) {
                return new ItemsView(uiObject: element);
            }
        }
    }
}