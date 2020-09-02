// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ListView`2
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Patterns;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public abstract class ListView<C, I> : UIObject, IContainer<I>, IMultipleView, ISelection<I>, IVirtualizedContainer<I>, ITable<I>, IGrid<I>, IItemContainer, IScroll
    where C : UIObject
    where I : ListItem<C>
  {
    private IMultipleView _multipleViewPattern;
    private ISelection<I> _selectionPattern;
    private ITable<I> _tablePattern;
    private IItemContainer _itemContainPattern;
    private IFactory<I> _itemFactory;
    private IScroll _scrollPattern;
    private static readonly UICondition _treeCondition = UICondition.Create("@ControlType=List or @ControlType=DataGrid Or @ControlType=ListItem Or @ControlType=DataItem");

    protected ListView(UIObject uiObject, IFactory<I> itemFactory)
      : base(uiObject)
      => this.Initialize(itemFactory);

    internal ListView(AutomationElement element, IFactory<I> itemFactory)
      : base(element)
      => this.Initialize(itemFactory);

    private void Initialize(IFactory<I> itemFactory)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) itemFactory, nameof (itemFactory));
      this._itemFactory = itemFactory;
      this._multipleViewPattern = (IMultipleView) new MultipleViewImplementation((UIObject) this);
      this._selectionPattern = (ISelection<I>) new SelectionImplementation<I>((UIObject) this, itemFactory);
      this._tablePattern = (ITable<I>) new TableImplementation<I>((UIObject) this, itemFactory);
      this._itemContainPattern = (IItemContainer) new ItemContainerImplementation((UIObject) this);
      this._scrollPattern = (IScroll) new ScrollImplementation((UIObject) this);
    }

    public virtual UICollection<I> Items => (UICollection<I>) new UIChildren<I>((UIObject) this, ListView<C, I>.TreeCondition, this._itemFactory);

    public virtual UICollection<I> AllItems => (UICollection<I>) new UIItemContainerChildren<I>((UIObject) this, ListView<C, I>.TreeCondition, this._itemFactory);

    public UIObject FindItemByProperty(
      UIObject uiObject,
      UIProperty uiProperty,
      object value) => this._itemContainPattern.FindItemByProperty(uiObject, uiProperty, value);

    public virtual string GetViewName(int viewId) => this._multipleViewPattern.GetViewName(viewId);

    public virtual void SetCurrentView(int viewId) => this._multipleViewPattern.SetCurrentView(viewId);

    public virtual int[] GetSupportedViews() => this._multipleViewPattern.GetSupportedViews();

    public virtual int CurrentView => this._multipleViewPattern.CurrentView;

    public bool IsSelectionPatternAvailable
    {
      get
      {
        int num = (int) ActionHandler.Invoke((UIObject) this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke((UIObject) this, ActionEventArgs.GetDefault(nameof (IsSelectionPatternAvailable)), out overridden) == ActionResult.Handled ? (bool) overridden : (bool) this.AutomationElement.GetCurrentPropertyValue(AutomationElement.IsSelectionPatternAvailableProperty, false);
      }
    }

    public virtual UICollection<I> Selection => this._selectionPattern.Selection;

    public virtual bool CanSelectMultiple => this._selectionPattern.CanSelectMultiple;

    public virtual bool IsSelectionRequired => this._selectionPattern.IsSelectionRequired;

    public virtual I GetCell(int row, int column) => this._tablePattern.GetCell(row, column);

    public virtual int RowCount => this._tablePattern.RowCount;

    public virtual int ColumnCount => this._tablePattern.ColumnCount;

    public virtual UICollection<UIObject> RowHeaders => this._tablePattern.RowHeaders;

    public virtual UICollection<UIObject> ColumnHeaders => this._tablePattern.ColumnHeaders;

    public virtual RowOrColumnMajor RowOrColumnMajor => this._tablePattern.RowOrColumnMajor;

    public virtual void Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount) => this._scrollPattern.Scroll(horizontalAmount, verticalAmount);

    public virtual void ScrollHorizontal(ScrollAmount amount) => this._scrollPattern.ScrollHorizontal(amount);

    public virtual void ScrollVertical(ScrollAmount amount) => this._scrollPattern.ScrollVertical(amount);

    public virtual void SetScrollPercent(double horizontalPercent, double verticalPercent) => this._scrollPattern.SetScrollPercent(horizontalPercent, verticalPercent);

    public virtual bool HorizontallyScrollable => this._scrollPattern.HorizontallyScrollable;

    public virtual bool VerticallyScrollable => this._scrollPattern.VerticallyScrollable;

    public virtual double HorizontalScrollPercent => this._scrollPattern.HorizontalScrollPercent;

    public virtual double VerticalScrollPercent => this._scrollPattern.VerticalScrollPercent;

    public virtual double HorizontalViewSize => this._scrollPattern.HorizontalViewSize;

    public virtual double VerticalViewSize => this._scrollPattern.VerticalViewSize;

    protected static UICondition TreeCondition => ListView<C, I>._treeCondition;
  }
}
