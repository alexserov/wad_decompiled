// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.Calendar`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Patterns;

namespace MS.Internal.Mita.Foundation.Controls {
    public abstract class Calendar<I> : UIObject, IGrid<I>, ISelection<I>, IValue
        where I : UIObject {
        protected Calendar(UIObject uiObject, IFactory<I> itemFactory)
            : base(uiObject: uiObject) {
            Initialize(itemFactory: itemFactory);
        }

        internal Calendar(AutomationElement element, IFactory<I> itemFactory)
            : base(element: element) {
            Initialize(itemFactory: itemFactory);
        }

        protected IValue ValueProvider { get; set; }

        protected IGrid<I> GridProvider { get; set; }

        protected ISelection<I> SelectionProvider { get; set; }

        public virtual I GetCell(int row, int column) {
            return GridProvider.GetCell(row: row, column: column);
        }

        public virtual int RowCount {
            get { return GridProvider.RowCount; }
        }

        public virtual int ColumnCount {
            get { return GridProvider.ColumnCount; }
        }

        public virtual UICollection<I> Selection {
            get { return SelectionProvider.Selection; }
        }

        public virtual bool CanSelectMultiple {
            get { return SelectionProvider.CanSelectMultiple; }
        }

        public virtual bool IsSelectionRequired {
            get { return SelectionProvider.IsSelectionRequired; }
        }

        public virtual void SetValue(string value) {
            ValueProvider.SetValue(value: value);
        }

        public virtual string Value {
            get { return ValueProvider.Value; }
        }

        public virtual bool IsReadOnly {
            get { return ValueProvider.IsReadOnly; }
        }

        void Initialize(IFactory<I> itemFactory) {
            GridProvider = new GridImplementation<I>(uiObject: this, itemFactory: itemFactory);
            SelectionProvider = new SelectionImplementation<I>(uiObject: this, itemFactory: itemFactory);
            ValueProvider = new ValueImplementation(uiObject: this);
        }
    }
}