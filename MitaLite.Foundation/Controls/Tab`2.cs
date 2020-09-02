// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.Tab`2
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Patterns;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public abstract class Tab<C, I> : UIObject, IContainer<I>, ISelection<I>
    where C : UIObject
    where I : TabItem<C>
  {
    private ISelection<I> _selectionPattern;
    private IFactory<I> _itemFactory;
    private static readonly UICondition _treeCondition = UICondition.Create("@ControlType=Tab Or @ControlType=TabItem");

    protected Tab(UIObject uiObject, IFactory<I> itemFactory)
      : base(uiObject)
      => this.Initialize(itemFactory);

    internal Tab(AutomationElement element, IFactory<I> itemFactory)
      : base(element)
      => this.Initialize(itemFactory);

    private void Initialize(IFactory<I> itemFactory)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) itemFactory, nameof (itemFactory));
      this.ItemFactory = itemFactory;
      this.SelectionProvider = (ISelection<I>) new SelectionImplementation<I>((UIObject) this, itemFactory);
    }

    public virtual UICollection<I> Items => (UICollection<I>) new UIChildren<I>((UIObject) this, Tab<C, I>.TreeCondition, this._itemFactory);

    public virtual UICollection<I> Selection => this.SelectionProvider.Selection;

    public virtual bool CanSelectMultiple => this.SelectionProvider.CanSelectMultiple;

    public virtual bool IsSelectionRequired => this.SelectionProvider.IsSelectionRequired;

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

    protected static UICondition TreeCondition => Tab<C, I>._treeCondition;
  }
}
