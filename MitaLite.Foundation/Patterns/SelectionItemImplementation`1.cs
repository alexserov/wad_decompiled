// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.SelectionItemImplementation`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Waiters;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class SelectionItemImplementation<C> : PatternImplementation<SelectionItemPattern>, ISelectionItem<C>
    where C : UIObject
  {
    private IFactory<C> _containerFactory;

    public SelectionItemImplementation(UIObject uiObject, IFactory<C> containerFactory)
      : base(uiObject, SelectionItemPattern.Pattern)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) containerFactory, nameof (containerFactory));
      this.ContainerFactory = containerFactory;
    }

    public void Select()
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (Select))) != ActionResult.Unhandled)
        return;
      this.Pattern.Select();
    }

    public UIEventWaiter GetSelectedWaiter() => (UIEventWaiter) new AutomationEventWaiter(SelectionItemPattern.ElementSelectedEvent, this.UIObject, Scope.Element);

    public void AddToSelection()
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (AddToSelection))) != ActionResult.Unhandled)
        return;
      this.Pattern.AddToSelection();
    }

    public UIEventWaiter GetAddedToSelectionWaiter() => (UIEventWaiter) new AutomationEventWaiter(SelectionItemPattern.ElementAddedToSelectionEvent, this.UIObject, Scope.Element);

    public void RemoveFromSelection()
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (RemoveFromSelection))) != ActionResult.Unhandled)
        return;
      this.Pattern.RemoveFromSelection();
    }

    public UIEventWaiter GetRemovedFromSelectionWaiter() => (UIEventWaiter) new AutomationEventWaiter(SelectionItemPattern.ElementRemovedFromSelectionEvent, this.UIObject, Scope.Element);

    public bool IsSelected
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (IsSelected)), out overridden) == ActionResult.Handled ? (bool) overridden : this.Pattern.Current.IsSelected;
      }
    }

    public C SelectionContainer
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (SelectionContainer)), out overridden) == ActionResult.Handled ? this.ContainerFactory.Create((UIObject) overridden) : this.ContainerFactory.Create(new UIObject(this.Pattern.Current.SelectionContainer));
      }
    }

    protected IFactory<C> ContainerFactory
    {
      get => this._containerFactory;
      set => this._containerFactory = value;
    }
  }
}
