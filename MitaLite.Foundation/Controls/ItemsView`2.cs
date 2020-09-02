// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ItemsView`2
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Controls {
    public abstract class ItemsView<C, I> : UIObject, IContainer<I>, IMultipleView, IItemContainer, ISelection<I>, ITable<I>, IGrid<I>, IVirtualizedContainer<I>
        where C : UIObject
        where I : ItemsViewItem<C> {
        protected ItemsView(UIObject uiObject, IFactory<I> itemFactory)
            : base(uiObject: uiObject) {
            Initialize(itemFactory: itemFactory);
        }

        internal ItemsView(AutomationElement element, IFactory<I> itemFactory)
            : base(element: element) {
            Initialize(itemFactory: itemFactory);
        }

        protected IMultipleView MultipleViewProvider { get; set; }

        protected IItemContainer ItemContainerProvider { get; set; }

        protected ITable<I> TableProvider { get; set; }

        protected IFactory<I> ItemFactory { get; set; }

        protected ISelection<I> SelectionProvider { get; set; }

        protected static UICondition TreeCondition { get; } = UICondition.Create(query: "@ControlType=List or @ControlType=DataGrid Or @ControlType=ListItem Or @ControlType=DataItem");

        public virtual UICollection<I> Items {
            get { return new UIChildren<I>(root: this, treeCondition: TreeCondition, factory: ItemFactory); }
        }

        public UIObject FindItemByProperty(
            UIObject uiObject,
            UIProperty property,
            object value) {
            return ItemContainerProvider.FindItemByProperty(uiObject: uiObject, uiProperty: property, value: value);
        }

        public virtual string GetViewName(int viewId) {
            return MultipleViewProvider.GetViewName(viewId: viewId);
        }

        public virtual void SetCurrentView(int viewId) {
            MultipleViewProvider.SetCurrentView(viewId: viewId);
        }

        public virtual int[] GetSupportedViews() {
            return MultipleViewProvider.GetSupportedViews();
        }

        public virtual int CurrentView {
            get { return MultipleViewProvider.CurrentView; }
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

        public virtual I GetCell(int row, int column) {
            return TableProvider.GetCell(row: row, column: column);
        }

        public virtual int RowCount {
            get { return TableProvider.RowCount; }
        }

        public virtual int ColumnCount {
            get { return TableProvider.ColumnCount; }
        }

        public virtual UICollection<UIObject> RowHeaders {
            get { return TableProvider.RowHeaders; }
        }

        public virtual UICollection<UIObject> ColumnHeaders {
            get { return TableProvider.ColumnHeaders; }
        }

        public virtual RowOrColumnMajor RowOrColumnMajor {
            get { return TableProvider.RowOrColumnMajor; }
        }

        public virtual UICollection<I> AllItems {
            get { return new UIItemContainerChildren<I>(root: this, treeCondition: TreeCondition, factory: ItemFactory); }
        }

        void Initialize(IFactory<I> itemFactory) {
            Validate.ArgumentNotNull(parameter: itemFactory, parameterName: nameof(itemFactory));
            ItemFactory = itemFactory;
            MultipleViewProvider = new MultipleViewImplementation(uiObject: this);
            ItemContainerProvider = new ItemContainerImplementation(uiObject: this);
            SelectionProvider = new SelectionImplementation<I>(uiObject: this, itemFactory: itemFactory);
            TableProvider = new TableImplementation<I>(uiObject: this, itemFactory: itemFactory);
        }
    }
}