// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ListViewItem
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls {
    public class ListViewItem : ListItem<ListView> {
        static IFactory<ListViewItem> _factory;

        public ListViewItem(UIObject uiObject)
            : base(uiObject: uiObject, containerFactory: ListView.Factory) {
        }

        internal ListViewItem(AutomationElement element)
            : base(element: element, containerFactory: ListView.Factory) {
        }

        public static IFactory<ListViewItem> Factory {
            get {
                if (_factory == null)
                    _factory = new ListViewItemFactory();
                return _factory;
            }
        }

        class ListViewItemFactory : IFactory<ListViewItem> {
            public ListViewItem Create(UIObject element) {
                return new ListViewItem(uiObject: element);
            }
        }
    }
}