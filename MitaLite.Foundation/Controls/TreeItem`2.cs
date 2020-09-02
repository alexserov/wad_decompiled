// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.TreeItem`2
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public abstract class TreeItem<C, I> : UIObject, IContainer<I>, IExpandCollapse, ISelectionItem<C>
    where C : UIObject
    where I : UIObject
  {
    private IExpandCollapse _expandCollapsePattern;
    private ISelectionItem<C> _selectionItemPattern;
    private IFactory<I> _itemFactory;
    private static readonly UICondition _treeCondition = UICondition.Create("@ControlType=TreeItem");

    protected TreeItem(UIObject uiObject, IFactory<C> containerFactory, IFactory<I> itemFactory)
      : base(uiObject)
      => this.Initialize(containerFactory, itemFactory);

    internal TreeItem(
      AutomationElement element,
      IFactory<C> containerFactory,
      IFactory<I> itemFactory)
      : base(element)
      => this.Initialize(containerFactory, itemFactory);

    private void Initialize(IFactory<C> containerFactory, IFactory<I> itemFactory)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) itemFactory, nameof (itemFactory));
      this.ItemFactory = itemFactory;
      this.ExpandCollapseProvider = (IExpandCollapse) new ExpandCollapseImplementation((UIObject) this);
      this.SelectionItemProvider = (ISelectionItem<C>) new SelectionItemImplementation<C>((UIObject) this, containerFactory);
    }

    public virtual UICollection<I> Items => (UICollection<I>) new UIChildren<I>((UIObject) this, TreeItem<C, I>.TreeCondition, this._itemFactory);

    public virtual void Collapse() => this.ExpandCollapseProvider.Collapse();

    public virtual void Expand() => this.ExpandCollapseProvider.Expand();

    public virtual ExpandCollapseState ExpandCollapseState => this.ExpandCollapseProvider.ExpandCollapseState;

    public UIEventWaiter GetCollapsedWaiter() => this.ExpandCollapseProvider.GetCollapsedWaiter();

    public UIEventWaiter GetExpandedWaiter() => this.ExpandCollapseProvider.GetExpandedWaiter();

    public virtual void Select() => this.SelectionItemProvider.Select();

    public virtual void AddToSelection() => this.SelectionItemProvider.AddToSelection();

    public UIEventWaiter GetAddedToSelectionWaiter() => this.SelectionItemProvider.GetAddedToSelectionWaiter();

    public UIEventWaiter GetRemovedFromSelectionWaiter() => this.SelectionItemProvider.GetRemovedFromSelectionWaiter();

    public UIEventWaiter GetSelectedWaiter() => this.SelectionItemProvider.GetSelectedWaiter();

    public virtual void RemoveFromSelection() => this.SelectionItemProvider.RemoveFromSelection();

    public virtual bool IsSelected => this.SelectionItemProvider.IsSelected;

    public virtual C SelectionContainer => this.SelectionItemProvider.SelectionContainer;

    protected IFactory<I> ItemFactory
    {
      get => this._itemFactory;
      set => this._itemFactory = value;
    }

    protected IExpandCollapse ExpandCollapseProvider
    {
      get => this._expandCollapsePattern;
      set => this._expandCollapsePattern = value;
    }

    protected ISelectionItem<C> SelectionItemProvider
    {
      get => this._selectionItemPattern;
      set => this._selectionItemPattern = value;
    }

    protected static UICondition TreeCondition => TreeItem<C, I>._treeCondition;
  }
}
