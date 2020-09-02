// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ListView`2
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Controls {
    public abstract class ListView<C, I> : UIObject, IContainer<I>, IMultipleView, ISelection<I>, IVirtualizedContainer<I>, ITable<I>, IGrid<I>, IItemContainer, IScroll
        where C : UIObject
        where I : ListItem<C> {
        IItemContainer _itemContainPattern;
        IFactory<I> _itemFactory;
        IMultipleView _multipleViewPattern;
        IScroll _scrollPattern;
        ISelection<I> _selectionPattern;
        ITable<I> _tablePattern;

        protected ListView(UIObject uiObject, IFactory<I> itemFactory)
            : base(uiObject: uiObject) {
            Initialize(itemFactory: itemFactory);
        }

        internal ListView(AutomationElement element, IFactory<I> itemFactory)
            : base(element: element) {
            Initialize(itemFactory: itemFactory);
        }

        public bool IsSelectionPatternAvailable {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(IsSelectionPatternAvailable)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : (bool) AutomationElement.GetCurrentPropertyValue(property: AutomationElement.IsSelectionPatternAvailableProperty, ignoreDefaultValue: false);
            }
        }

        protected static UICondition TreeCondition { get; } = UICondition.Create(query: "@ControlType=List or @ControlType=DataGrid Or @ControlType=ListItem Or @ControlType=DataItem");

        public virtual UICollection<I> Items {
            get { return new UIChildren<I>(root: this, treeCondition: TreeCondition, factory: this._itemFactory); }
        }

        public UIObject FindItemByProperty(
            UIObject uiObject,
            UIProperty uiProperty,
            object value) {
            return this._itemContainPattern.FindItemByProperty(uiObject: uiObject, uiProperty: uiProperty, value: value);
        }

        public virtual string GetViewName(int viewId) {
            return this._multipleViewPattern.GetViewName(viewId: viewId);
        }

        public virtual void SetCurrentView(int viewId) {
            this._multipleViewPattern.SetCurrentView(viewId: viewId);
        }

        public virtual int[] GetSupportedViews() {
            return this._multipleViewPattern.GetSupportedViews();
        }

        public virtual int CurrentView {
            get { return this._multipleViewPattern.CurrentView; }
        }

        public virtual void Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount) {
            this._scrollPattern.Scroll(horizontalAmount: horizontalAmount, verticalAmount: verticalAmount);
        }

        public virtual void ScrollHorizontal(ScrollAmount amount) {
            this._scrollPattern.ScrollHorizontal(amount: amount);
        }

        public virtual void ScrollVertical(ScrollAmount amount) {
            this._scrollPattern.ScrollVertical(amount: amount);
        }

        public virtual void SetScrollPercent(double horizontalPercent, double verticalPercent) {
            this._scrollPattern.SetScrollPercent(horizontalPercent: horizontalPercent, verticalPercent: verticalPercent);
        }

        public virtual bool HorizontallyScrollable {
            get { return this._scrollPattern.HorizontallyScrollable; }
        }

        public virtual bool VerticallyScrollable {
            get { return this._scrollPattern.VerticallyScrollable; }
        }

        public virtual double HorizontalScrollPercent {
            get { return this._scrollPattern.HorizontalScrollPercent; }
        }

        public virtual double VerticalScrollPercent {
            get { return this._scrollPattern.VerticalScrollPercent; }
        }

        public virtual double HorizontalViewSize {
            get { return this._scrollPattern.HorizontalViewSize; }
        }

        public virtual double VerticalViewSize {
            get { return this._scrollPattern.VerticalViewSize; }
        }

        public virtual UICollection<I> Selection {
            get { return this._selectionPattern.Selection; }
        }

        public virtual bool CanSelectMultiple {
            get { return this._selectionPattern.CanSelectMultiple; }
        }

        public virtual bool IsSelectionRequired {
            get { return this._selectionPattern.IsSelectionRequired; }
        }

        public virtual I GetCell(int row, int column) {
            return this._tablePattern.GetCell(row: row, column: column);
        }

        public virtual int RowCount {
            get { return this._tablePattern.RowCount; }
        }

        public virtual int ColumnCount {
            get { return this._tablePattern.ColumnCount; }
        }

        public virtual UICollection<UIObject> RowHeaders {
            get { return this._tablePattern.RowHeaders; }
        }

        public virtual UICollection<UIObject> ColumnHeaders {
            get { return this._tablePattern.ColumnHeaders; }
        }

        public virtual RowOrColumnMajor RowOrColumnMajor {
            get { return this._tablePattern.RowOrColumnMajor; }
        }

        public virtual UICollection<I> AllItems {
            get { return new UIItemContainerChildren<I>(root: this, treeCondition: TreeCondition, factory: this._itemFactory); }
        }

        void Initialize(IFactory<I> itemFactory) {
            Validate.ArgumentNotNull(parameter: itemFactory, parameterName: nameof(itemFactory));
            this._itemFactory = itemFactory;
            this._multipleViewPattern = new MultipleViewImplementation(uiObject: this);
            this._selectionPattern = new SelectionImplementation<I>(uiObject: this, itemFactory: itemFactory);
            this._tablePattern = new TableImplementation<I>(uiObject: this, itemFactory: itemFactory);
            this._itemContainPattern = new ItemContainerImplementation(uiObject: this);
            this._scrollPattern = new ScrollImplementation(uiObject: this);
        }
    }
}