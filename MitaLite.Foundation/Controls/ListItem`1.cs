// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ListItem`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public abstract class ListItem<C> : UIObject, IScrollItem, IVirtualizedItem, ISelectionItem<C>
    where C : UIObject
  {
    private ISelectionItem<C> _selectionItemPattern;
    private IScrollItem _scrollItemPattern;
    private IVirtualizedItem _virtualizedItemPattern;

    protected ListItem(UIObject uiObject, IFactory<C> containerFactory)
      : base(uiObject)
      => this.Initialize(containerFactory);

    internal ListItem(AutomationElement element, IFactory<C> containerFactory)
      : base(element)
      => this.Initialize(containerFactory);

    private void Initialize(IFactory<C> containerFactory)
    {
      this.SelectionItemProvider = (ISelectionItem<C>) new SelectionItemImplementation<C>((UIObject) this, containerFactory);
      this.ScrollItemProvider = (IScrollItem) new ScrollItemImplementation((UIObject) this);
      this.VirtualizedItemProvider = (IVirtualizedItem) new VirtualizedItemImplementation((UIObject) this);
    }

    public virtual void Select() => this.SelectionItemProvider.Select();

    public virtual void AddToSelection() => this.SelectionItemProvider.AddToSelection();

    public UIEventWaiter GetAddedToSelectionWaiter() => this.SelectionItemProvider.GetAddedToSelectionWaiter();

    public UIEventWaiter GetRemovedFromSelectionWaiter() => this.SelectionItemProvider.GetRemovedFromSelectionWaiter();

    public UIEventWaiter GetSelectedWaiter() => this.SelectionItemProvider.GetSelectedWaiter();

    public virtual void RemoveFromSelection() => this.SelectionItemProvider.RemoveFromSelection();

    public virtual bool IsSelected => this.SelectionItemProvider.IsSelected;

    public virtual C SelectionContainer => this.SelectionItemProvider.SelectionContainer;

    public void ScrollIntoView() => this.ScrollItemProvider.ScrollIntoView();

    public void Realize() => this.VirtualizedItemProvider.Realize();

    public bool IsSelectionItemPatternAvailable
    {
      get
      {
        int num = (int) ActionHandler.Invoke((UIObject) this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke((UIObject) this, ActionEventArgs.GetDefault(nameof (IsSelectionItemPatternAvailable)), out overridden) == ActionResult.Handled ? (bool) overridden : (bool) this.AutomationElement.GetCurrentPropertyValue(AutomationElement.IsSelectionItemPatternAvailableProperty, false);
      }
    }

    protected ISelectionItem<C> SelectionItemProvider
    {
      get => this._selectionItemPattern;
      set => this._selectionItemPattern = value;
    }

    protected IScrollItem ScrollItemProvider
    {
      get => this._scrollItemPattern;
      set => this._scrollItemPattern = value;
    }

    protected IVirtualizedItem VirtualizedItemProvider
    {
      get => this._virtualizedItemPattern;
      set => this._virtualizedItemPattern = value;
    }
  }
}
