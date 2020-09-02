// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.SelectionSpinnerItem
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls {
    public class SelectionSpinnerItem : ListItem<SelectionSpinner> {
        static IFactory<SelectionSpinnerItem> _factory;

        public SelectionSpinnerItem(UIObject uiObject)
            : base(uiObject: uiObject, containerFactory: SelectionSpinner.Factory) {
        }

        internal SelectionSpinnerItem(AutomationElement element)
            : base(element: element, containerFactory: SelectionSpinner.Factory) {
        }

        public static IFactory<SelectionSpinnerItem> Factory {
            get {
                if (_factory == null)
                    _factory = new SelectionSpinnerItemFactory();
                return _factory;
            }
        }

        class SelectionSpinnerItemFactory : IFactory<SelectionSpinnerItem> {
            public SelectionSpinnerItem Create(UIObject element) {
                return new SelectionSpinnerItem(uiObject: element);
            }
        }
    }
}