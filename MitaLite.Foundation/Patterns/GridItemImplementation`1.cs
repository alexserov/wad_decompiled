// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.GridItemImplementation`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class GridItemImplementation<C> : PatternImplementation<GridItemPattern>, IGridItem<C>
    where C : UIObject
  {
    private IFactory<C> _containerFactory;

    public GridItemImplementation(UIObject uiObject, IFactory<C> containerFactory)
      : base(uiObject, GridItemPattern.Pattern)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) containerFactory, nameof (containerFactory));
      this._containerFactory = containerFactory;
    }

    public C ContainingGrid
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (ContainingGrid)), out overridden) == ActionResult.Handled ? this._containerFactory.Create((UIObject) overridden) : this._containerFactory.Create(new UIObject(this.Pattern.Current.ContainingGrid));
      }
    }

    public int Row
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (Row)), out overridden) == ActionResult.Handled ? (int) overridden : this.Pattern.Current.Row;
      }
    }

    public int Column
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (Column)), out overridden) == ActionResult.Handled ? (int) overridden : this.Pattern.Current.Column;
      }
    }

    public int RowSpan
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (RowSpan)), out overridden) == ActionResult.Handled ? (int) overridden : this.Pattern.Current.RowSpan;
      }
    }

    public int ColumnSpan
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (ColumnSpan)), out overridden) == ActionResult.Handled ? (int) overridden : this.Pattern.Current.ColumnSpan;
      }
    }
  }
}
