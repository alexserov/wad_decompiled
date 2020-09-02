// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.SelectionImplementation`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Collections;
using System.Collections;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class SelectionImplementation<I> : PatternImplementation<SelectionPattern>, ISelection<I>
    where I : UIObject
  {
    private IFactory<I> _itemFactory;

    public SelectionImplementation(UIObject uiObject, IFactory<I> itemFactory)
      : base(uiObject, SelectionPattern.Pattern)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) itemFactory, nameof (itemFactory));
      this.ItemFactory = itemFactory;
    }

    public UICollection<I> Selection
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (Selection)), out overridden) == ActionResult.Handled ? (UICollection<I>) overridden : new UICollection<I>((UINavigator) new EnumerableNavigator((IEnumerable) this.Pattern.Current.GetSelection()), this.ItemFactory);
      }
    }

    public bool CanSelectMultiple
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (CanSelectMultiple)), out overridden) == ActionResult.Handled ? (bool) overridden : this.Pattern.Current.CanSelectMultiple;
      }
    }

    public bool IsSelectionRequired
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (IsSelectionRequired)), out overridden) == ActionResult.Handled ? (bool) overridden : this.Pattern.Current.IsSelectionRequired;
      }
    }

    protected IFactory<I> ItemFactory
    {
      get => this._itemFactory;
      set => this._itemFactory = value;
    }
  }
}
