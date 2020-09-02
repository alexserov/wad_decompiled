// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.SelectionSpinner
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Patterns;

namespace MS.Internal.Mita.Foundation.Controls {
    public class SelectionSpinner : UIObject, IContainer<SelectionSpinnerItem>, ISelection<SelectionSpinnerItem> {
        static IFactory<SelectionSpinner> _factory;
        ISelection<SelectionSpinnerItem> _selectionPattern;

        public SelectionSpinner(UIObject uiObject)
            : base(uiObject: uiObject) {
            Initialize();
        }

        internal SelectionSpinner(AutomationElement element)
            : base(element: element) {
            Initialize();
        }

        public static IFactory<SelectionSpinner> Factory {
            get {
                if (_factory == null)
                    _factory = new SelectionSpinnerFactory();
                return _factory;
            }
        }

        public UICollection<SelectionSpinnerItem> Items {
            get { return new UIChildren<SelectionSpinnerItem>(root: this, treeCondition: UICondition.ControlTree, factory: SelectionSpinnerItem.Factory); }
        }

        public UICollection<SelectionSpinnerItem> Selection {
            get { return this._selectionPattern.Selection; }
        }

        public virtual bool CanSelectMultiple {
            get { return this._selectionPattern.CanSelectMultiple; }
        }

        public virtual bool IsSelectionRequired {
            get { return this._selectionPattern.IsSelectionRequired; }
        }

        void Initialize() {
            this._selectionPattern = new SelectionImplementation<SelectionSpinnerItem>(uiObject: this, itemFactory: SelectionSpinnerItem.Factory);
        }

        class SelectionSpinnerFactory : IFactory<SelectionSpinner> {
            public SelectionSpinner Create(UIObject element) {
                return new SelectionSpinner(uiObject: element);
            }
        }
    }
}