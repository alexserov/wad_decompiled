// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.TabItem`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public abstract class TabItem<C> : UIObject, ISelectionItem<C> where C : UIObject
  {
    private ISelectionItem<C> _selectionItemPattern;

    protected TabItem(UIObject uiObject, IFactory<C> containerFactory)
      : base(uiObject)
      => this.Initialize(containerFactory);

    internal TabItem(AutomationElement element, IFactory<C> containerFactory)
      : base(element)
      => this.Initialize(containerFactory);

    private void Initialize(IFactory<C> containerFactory) => this.SelectionItemProvider = (ISelectionItem<C>) new SelectionItemImplementation<C>((UIObject) this, containerFactory);

    public virtual void Select() => this.SelectionItemProvider.Select();

    public virtual void AddToSelection() => this.SelectionItemProvider.AddToSelection();

    public UIEventWaiter GetAddedToSelectionWaiter() => this.SelectionItemProvider.GetAddedToSelectionWaiter();

    public UIEventWaiter GetRemovedFromSelectionWaiter() => this.SelectionItemProvider.GetRemovedFromSelectionWaiter();

    public UIEventWaiter GetSelectedWaiter() => this.SelectionItemProvider.GetSelectedWaiter();

    public virtual void RemoveFromSelection() => this.SelectionItemProvider.RemoveFromSelection();

    public virtual bool IsSelected => this.SelectionItemProvider.IsSelected;

    public virtual C SelectionContainer => this.SelectionItemProvider.SelectionContainer;

    protected ISelectionItem<C> SelectionItemProvider
    {
      get => this._selectionItemPattern;
      set => this._selectionItemPattern = value;
    }
  }
}
