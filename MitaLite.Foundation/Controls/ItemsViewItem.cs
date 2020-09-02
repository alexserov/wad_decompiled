// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ItemsViewItem
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls {
    public class ItemsViewItem : ItemsViewItem<ItemsView> {
        static IFactory<ItemsViewItem> _factory;

        public ItemsViewItem(UIObject uiObject)
            : base(uiObject: uiObject, containerFactory: ItemsView.Factory) {
        }

        internal ItemsViewItem(AutomationElement element)
            : base(element: element, containerFactory: ItemsView.Factory) {
        }

        public static IFactory<ItemsViewItem> Factory {
            get {
                if (_factory == null)
                    _factory = new ItemsViewItemFactory();
                return _factory;
            }
        }

        class ItemsViewItemFactory : IFactory<ItemsViewItem> {
            public ItemsViewItem Create(UIObject element) => new ItemsViewItem(uiObject: element);
        }
    }
}