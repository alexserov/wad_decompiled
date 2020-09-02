// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ListBoxItem
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls {
    public class ListBoxItem : ListItem<ListBox> {
        static IFactory<ListBoxItem> _factory;

        public ListBoxItem(UIObject uiObject)
            : base(uiObject: uiObject, containerFactory: ListBox.Factory) {
        }

        internal ListBoxItem(AutomationElement element)
            : base(element: element, containerFactory: ListBox.Factory) {
        }

        public static IFactory<ListBoxItem> Factory {
            get {
                if (_factory == null)
                    _factory = new ListBoxItemFactory();
                return _factory;
            }
        }

        class ListBoxItemFactory : IFactory<ListBoxItem> {
            public ListBoxItem Create(UIObject element) {
                return new ListBoxItem(uiObject: element);
            }
        }
    }
}