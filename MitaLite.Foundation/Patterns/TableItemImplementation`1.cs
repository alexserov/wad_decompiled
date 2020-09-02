// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.TableItemImplementation`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Collections;
using System.Collections;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class TableItemImplementation<C> : PatternImplementation<TableItemPattern>, ITableItem<C>, IGridItem<C>
    where C : UIObject
  {
    private IFactory<C> _containerFactory;
    private GridItemImplementation<C> _gridItem;

    public TableItemImplementation(UIObject uiObject, IFactory<C> containerFactory)
      : base(uiObject, TableItemPattern.Pattern)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) containerFactory, nameof (containerFactory));
      this._containerFactory = containerFactory;
      this._gridItem = new GridItemImplementation<C>(uiObject, containerFactory);
    }

    public UICollection<UIObject> RowHeaderItems
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (RowHeaderItems)), out overridden) == ActionResult.Handled ? (UICollection<UIObject>) overridden : new UICollection<UIObject>((UINavigator) new EnumerableNavigator((IEnumerable) this.Pattern.Current.GetRowHeaderItems()), UIObject.Factory);
      }
    }

    public UICollection<UIObject> ColumnHeaderItems
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (ColumnHeaderItems)), out overridden) == ActionResult.Handled ? (UICollection<UIObject>) overridden : new UICollection<UIObject>((UINavigator) new EnumerableNavigator((IEnumerable) this.Pattern.Current.GetColumnHeaderItems()), UIObject.Factory);
      }
    }

    public C ContainingGrid => this._gridItem.ContainingGrid;

    public int Row => this._gridItem.Row;

    public int Column => this._gridItem.Column;

    public int RowSpan => this._gridItem.RowSpan;

    public int ColumnSpan => this._gridItem.ColumnSpan;
  }
}
