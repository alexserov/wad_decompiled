// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.SelectionSlider
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Patterns;

namespace MS.Internal.Mita.Foundation.Controls {
    public class SelectionSlider : UIObject, IContainer<SelectionSliderItem>, ISelection<SelectionSliderItem> {
        static IFactory<SelectionSlider> _factory;
        ISelection<SelectionSliderItem> _selectionPattern;

        public SelectionSlider(UIObject uiObject)
            : base(uiObject: uiObject) {
            Initialize();
        }

        internal SelectionSlider(AutomationElement element)
            : base(element: element) {
            Initialize();
        }

        public static IFactory<SelectionSlider> Factory {
            get {
                if (_factory == null)
                    _factory = new SelectionSliderFactory();
                return _factory;
            }
        }

        public UICollection<SelectionSliderItem> Items {
            get { return new UIChildren<SelectionSliderItem>(root: this, treeCondition: UICondition.ControlTree, factory: SelectionSliderItem.Factory); }
        }

        public UICollection<SelectionSliderItem> Selection {
            get { return this._selectionPattern.Selection; }
        }

        public virtual bool CanSelectMultiple {
            get { return this._selectionPattern.CanSelectMultiple; }
        }

        public virtual bool IsSelectionRequired {
            get { return this._selectionPattern.IsSelectionRequired; }
        }

        void Initialize() {
            this._selectionPattern = new SelectionImplementation<SelectionSliderItem>(uiObject: this, itemFactory: SelectionSliderItem.Factory);
        }

        class SelectionSliderFactory : IFactory<SelectionSlider> {
            public SelectionSlider Create(UIObject element) {
                return new SelectionSlider(uiObject: element);
            }
        }
    }
}