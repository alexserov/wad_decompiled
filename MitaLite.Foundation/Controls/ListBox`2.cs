// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ListBox`2
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Patterns;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public abstract class ListBox<C, I> : UIObject, IContainer<I>, ISelection<I>, IVirtualizedContainer<I>, IScroll
    where C : UIObject
    where I : ListItem<C>
  {
    private ISelection<I> _selectionPattern;
    private IFactory<I> _itemFactory;
    private IScroll _scrollPattern;
    private static readonly UICondition _treeCondition = UICondition.Create("@ControlType=List or @ControlType=DataGrid Or @ControlType=ListItem Or @ControlType=DataItem");

    protected ListBox(UIObject uiObject, IFactory<I> itemFactory)
      : base(uiObject)
      => this.Initialize(itemFactory);

    internal ListBox(AutomationElement element, IFactory<I> itemFactory)
      : base(element)
      => this.Initialize(itemFactory);

    private void Initialize(IFactory<I> itemFactory)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) itemFactory, nameof (itemFactory));
      this._itemFactory = itemFactory;
      this._selectionPattern = (ISelection<I>) new SelectionImplementation<I>((UIObject) this, itemFactory);
      this._scrollPattern = (IScroll) new ScrollImplementation((UIObject) this);
    }

    public virtual UICollection<I> Items => (UICollection<I>) new UIChildren<I>((UIObject) this, ListBox<C, I>.TreeCondition, this._itemFactory);

    public virtual UICollection<I> AllItems => (UICollection<I>) new UIItemContainerChildren<I>((UIObject) this, ListBox<C, I>.TreeCondition, this._itemFactory);

    public virtual UICollection<I> Selection => this._selectionPattern.Selection;

    public virtual bool CanSelectMultiple => this._selectionPattern.CanSelectMultiple;

    public virtual bool IsSelectionRequired => this._selectionPattern.IsSelectionRequired;

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

    public bool IsSelectionPatternAvailable
    {
      get
      {
        int num = (int) ActionHandler.Invoke((UIObject) this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke((UIObject) this, ActionEventArgs.GetDefault(nameof (IsSelectionPatternAvailable)), out overridden) == ActionResult.Handled ? (bool) overridden : (bool) this.AutomationElement.GetCurrentPropertyValue(AutomationElement.IsSelectionPatternAvailableProperty, false);
      }
    }

    protected static UICondition TreeCondition => ListBox<C, I>._treeCondition;
  }
}
