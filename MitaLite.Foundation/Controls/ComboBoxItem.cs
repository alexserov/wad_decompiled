// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ComboBoxItem
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls {
    public class ComboBoxItem : ListItem<ComboBox> {
        static IFactory<ComboBoxItem> _factory;

        public ComboBoxItem(UIObject uiObject)
            : base(uiObject: uiObject, containerFactory: ComboBox.Factory) {
        }

        internal ComboBoxItem(AutomationElement element)
            : base(element: element, containerFactory: ComboBox.Factory) {
        }

        public static IFactory<ComboBoxItem> Factory {
            get {
                if (_factory == null)
                    _factory = new ComboBoxItemFactory();
                return _factory;
            }
        }

        class ComboBoxItemFactory : IFactory<ComboBoxItem> {
            public ComboBoxItem Create(UIObject element) {
                return new ComboBoxItem(uiObject: element);
            }
        }
    }
}