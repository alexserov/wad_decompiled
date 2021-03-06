﻿// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ListBox
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls {
    public class ListBox : ListBox<ListBox, ListBoxItem> {
        static IFactory<ListBox> _factory;

        public ListBox(UIObject uiObject)
            : base(uiObject: uiObject, itemFactory: ListBoxItem.Factory) {
        }

        internal ListBox(AutomationElement element)
            : base(element: element, itemFactory: ListBoxItem.Factory) {
        }

        public static IFactory<ListBox> Factory {
            get {
                if (_factory == null)
                    _factory = new ListBoxFactory();
                return _factory;
            }
        }

        class ListBoxFactory : IFactory<ListBox> {
            public ListBox Create(UIObject element) {
                return new ListBox(uiObject: element);
            }
        }
    }
}