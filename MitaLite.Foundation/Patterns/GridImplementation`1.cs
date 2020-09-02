// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.GridImplementation`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class GridImplementation<I> : PatternImplementation<GridPattern>, IGrid<I>
    where I : UIObject
  {
    private IFactory<I> _itemFactory;

    public GridImplementation(UIObject uiObject, IFactory<I> itemFactory)
      : base(uiObject, GridPattern.Pattern)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) itemFactory, nameof (itemFactory));
      this._itemFactory = itemFactory;
    }

    public I GetCell(int row, int column)
    {
      int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      object overridden;
      return ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (GetCell), new object[2]
      {
        (object) row,
        (object) column
      }), out overridden) == ActionResult.Handled ? this._itemFactory.Create((UIObject) overridden) : this._itemFactory.Create(new UIObject(this.Pattern.GetItem(row, column)));
    }

    public int RowCount
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (RowCount)), out overridden) == ActionResult.Handled ? (int) overridden : this.Pattern.Current.RowCount;
      }
    }

    public int ColumnCount
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (ColumnCount)), out overridden) == ActionResult.Handled ? (int) overridden : this.Pattern.Current.ColumnCount;
      }
    }
  }
}
