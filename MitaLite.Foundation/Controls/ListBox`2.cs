// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ListBox`2
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Controls {
    public abstract class ListBox<C, I> : UIObject, IContainer<I>, ISelection<I>, IVirtualizedContainer<I>, IScroll
        where C : UIObject
        where I : ListItem<C> {
        IFactory<I> _itemFactory;
        IScroll _scrollPattern;
        ISelection<I> _selectionPattern;

        protected ListBox(UIObject uiObject, IFactory<I> itemFactory)
            : base(uiObject: uiObject) {
            Initialize(itemFactory: itemFactory);
        }

        internal ListBox(AutomationElement element, IFactory<I> itemFactory)
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

        public virtual UICollection<I> AllItems {
            get { return new UIItemContainerChildren<I>(root: this, treeCondition: TreeCondition, factory: this._itemFactory); }
        }

        void Initialize(IFactory<I> itemFactory) {
            Validate.ArgumentNotNull(parameter: itemFactory, parameterName: nameof(itemFactory));
            this._itemFactory = itemFactory;
            this._selectionPattern = new SelectionImplementation<I>(uiObject: this, itemFactory: itemFactory);
            this._scrollPattern = new ScrollImplementation(uiObject: this);
        }
    }
}