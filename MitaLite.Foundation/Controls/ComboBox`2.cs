// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ComboBox`2
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public abstract class ComboBox<C, I> : UIObject, IExpandCollapse, IValue, ISelection<I>, IContainer<I>, IVirtualizedContainer<I>
    where C : UIObject
    where I : ListItem<C>
  {
    private IExpandCollapse _expandCollapsePattern;
    private ISelection<I> _selectionPattern;
    private IValue _valuePattern;
    private IFactory<I> _itemFactory;
    private static readonly UICondition _treeCondition = UICondition.Create("@ControlType=ComboBox or @ControlType=ListItem");

    protected ComboBox(UIObject uiObject, IFactory<I> itemFactory)
      : base(uiObject)
      => this.Initialize(itemFactory);

    internal ComboBox(AutomationElement element, IFactory<I> itemFactory)
      : base(element)
      => this.Initialize(itemFactory);

    private void Initialize(IFactory<I> itemFactory)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) itemFactory, nameof (itemFactory));
      this.ItemFactory = itemFactory;
      this.ExpandCollapseProvider = (IExpandCollapse) new ExpandCollapseImplementation((UIObject) this);
      this.SelectionProvider = (ISelection<I>) new SelectionImplementation<I>((UIObject) this, itemFactory);
      this.ValueProvider = (IValue) null;
    }

    public virtual UICollection<I> Items => (UICollection<I>) new UIBreadthFirstDescendants<I>((UIObject) this, ComboBox<C, I>.TreeCondition, this._itemFactory);

    public virtual void Collapse() => this.ExpandCollapseProvider.Collapse();

    public virtual void Expand() => this.ExpandCollapseProvider.Expand();

    public virtual ExpandCollapseState ExpandCollapseState => this.ExpandCollapseProvider.ExpandCollapseState;

    public UIEventWaiter GetCollapsedWaiter() => this.ExpandCollapseProvider.GetCollapsedWaiter();

    public UIEventWaiter GetExpandedWaiter() => this.ExpandCollapseProvider.GetExpandedWaiter();

    public virtual UICollection<I> Selection => this.SelectionProvider.Selection;

    public virtual bool CanSelectMultiple => this.SelectionProvider.CanSelectMultiple;

    public virtual bool IsSelectionRequired => this.SelectionProvider.IsSelectionRequired;

    public UICollection<I> AllItems => (UICollection<I>) new UIItemContainerChildren<I>((UIObject) this, ComboBox<C, I>.TreeCondition, this._itemFactory);

    public virtual void SetValue(string value) => this.ValueProvider.SetValue(value);

    public virtual string Value => this.ValueProvider.Value;

    public virtual bool IsReadOnly => this.ValueProvider.IsReadOnly;

    protected static UICondition TreeCondition => ComboBox<C, I>._treeCondition;

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

    protected ISelection<I> SelectionProvider
    {
      get => this._selectionPattern;
      set => this._selectionPattern = value;
    }

    private IValue ValueProvider
    {
      get
      {
        if (this._valuePattern == null)
        {
          if (!(bool) this.AutomationElement.GetCurrentPropertyValue(AutomationElement.IsValuePatternAvailableProperty))
            throw new PatternNotFoundException("Value Pattern Not found on Combobox");
          this._valuePattern = (IValue) new ValueImplementation((UIObject) this);
        }
        return this._valuePattern;
      }
      set => this._valuePattern = value;
    }
  }
}
