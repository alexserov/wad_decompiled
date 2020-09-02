// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ItemsView`2
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Patterns;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public abstract class ItemsView<C, I> : UIObject, IContainer<I>, IMultipleView, IItemContainer, ISelection<I>, ITable<I>, IGrid<I>, IVirtualizedContainer<I>
    where C : UIObject
    where I : ItemsViewItem<C>
  {
    private IMultipleView _multipleViewPattern;
    private IItemContainer _itemContainerPattern;
    private ISelection<I> _selectionPattern;
    private ITable<I> _tablePattern;
    private IFactory<I> _itemFactory;
    private static readonly UICondition _treeCondition = UICondition.Create("@ControlType=List or @ControlType=DataGrid Or @ControlType=ListItem Or @ControlType=DataItem");

    protected ItemsView(UIObject uiObject, IFactory<I> itemFactory)
      : base(uiObject)
      => this.Initialize(itemFactory);

    internal ItemsView(AutomationElement element, IFactory<I> itemFactory)
      : base(element)
      => this.Initialize(itemFactory);

    private void Initialize(IFactory<I> itemFactory)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) itemFactory, nameof (itemFactory));
      this.ItemFactory = itemFactory;
      this.MultipleViewProvider = (IMultipleView) new MultipleViewImplementation((UIObject) this);
      this.ItemContainerProvider = (IItemContainer) new ItemContainerImplementation((UIObject) this);
      this.SelectionProvider = (ISelection<I>) new SelectionImplementation<I>((UIObject) this, itemFactory);
      this.TableProvider = (ITable<I>) new TableImplementation<I>((UIObject) this, itemFactory);
    }

    public virtual UICollection<I> Items => (UICollection<I>) new UIChildren<I>((UIObject) this, ItemsView<C, I>.TreeCondition, this._itemFactory);

    public virtual UICollection<I> AllItems => (UICollection<I>) new UIItemContainerChildren<I>((UIObject) this, ItemsView<C, I>.TreeCondition, this._itemFactory);

    public virtual string GetViewName(int viewId) => this.MultipleViewProvider.GetViewName(viewId);

    public virtual void SetCurrentView(int viewId) => this.MultipleViewProvider.SetCurrentView(viewId);

    public virtual int[] GetSupportedViews() => this.MultipleViewProvider.GetSupportedViews();

    public virtual int CurrentView => this.MultipleViewProvider.CurrentView;

    public UIObject FindItemByProperty(
      UIObject uiObject,
      UIProperty property,
      object value) => this.ItemContainerProvider.FindItemByProperty(uiObject, property, value);

    public virtual UICollection<I> Selection => this.SelectionProvider.Selection;

    public virtual bool CanSelectMultiple => this.SelectionProvider.CanSelectMultiple;

    public virtual bool IsSelectionRequired => this.SelectionProvider.IsSelectionRequired;

    public virtual I GetCell(int row, int column) => this.TableProvider.GetCell(row, column);

    public virtual int RowCount => this.TableProvider.RowCount;

    public virtual int ColumnCount => this.TableProvider.ColumnCount;

    public virtual UICollection<UIObject> RowHeaders => this.TableProvider.RowHeaders;

    public virtual UICollection<UIObject> ColumnHeaders => this.TableProvider.ColumnHeaders;

    public virtual RowOrColumnMajor RowOrColumnMajor => this.TableProvider.RowOrColumnMajor;

    protected IMultipleView MultipleViewProvider
    {
      get => this._multipleViewPattern;
      set => this._multipleViewPattern = value;
    }

    protected IItemContainer ItemContainerProvider
    {
      get => this._itemContainerPattern;
      set => this._itemContainerPattern = value;
    }

    protected ITable<I> TableProvider
    {
      get => this._tablePattern;
      set => this._tablePattern = value;
    }

    protected IFactory<I> ItemFactory
    {
      get => this._itemFactory;
      set => this._itemFactory = value;
    }

    protected ISelection<I> SelectionProvider
    {
      get => this._selectionPattern;
      set => this._selectionPattern = value;
    }

    protected static UICondition TreeCondition => ItemsView<C, I>._treeCondition;
  }
}
