// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.TableImplementation`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Collections;
using System.Collections;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class TableImplementation<I> : PatternImplementation<TablePattern>, ITable<I>, IGrid<I>
    where I : UIObject
  {
    private IFactory<I> _itemFactory;
    private GridImplementation<I> _grid;

    public TableImplementation(UIObject uiObject, IFactory<I> itemFactory)
      : base(uiObject, TablePattern.Pattern)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) itemFactory, nameof (itemFactory));
      this._itemFactory = itemFactory;
      this._grid = new GridImplementation<I>(uiObject, itemFactory);
    }

    public UICollection<UIObject> RowHeaders
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (RowHeaders)), out overridden) == ActionResult.Handled ? (UICollection<UIObject>) overridden : new UICollection<UIObject>((UINavigator) new EnumerableNavigator((IEnumerable) this.Pattern.Current.GetRowHeaders()), UIObject.Factory);
      }
    }

    public UICollection<UIObject> ColumnHeaders
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (ColumnHeaders)), out overridden) == ActionResult.Handled ? (UICollection<UIObject>) overridden : new UICollection<UIObject>((UINavigator) new EnumerableNavigator((IEnumerable) this.Pattern.Current.GetColumnHeaders()), UIObject.Factory);
      }
    }

    public RowOrColumnMajor RowOrColumnMajor
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (RowOrColumnMajor)), out overridden) == ActionResult.Handled ? (RowOrColumnMajor) overridden : this.Pattern.Current.RowOrColumnMajor;
      }
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

    public int RowCount => this._grid.RowCount;

    public int ColumnCount => this._grid.ColumnCount;
  }
}
