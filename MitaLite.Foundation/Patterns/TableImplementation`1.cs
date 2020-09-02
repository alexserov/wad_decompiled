// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.TableImplementation`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class TableImplementation<I> : PatternImplementation<TablePattern>, ITable<I>, IGrid<I>
        where I : UIObject {
        readonly GridImplementation<I> _grid;
        readonly IFactory<I> _itemFactory;

        public TableImplementation(UIObject uiObject, IFactory<I> itemFactory)
            : base(uiObject: uiObject, patternIdentifier: TablePattern.Pattern) {
            Validate.ArgumentNotNull(parameter: itemFactory, parameterName: nameof(itemFactory));
            this._itemFactory = itemFactory;
            this._grid = new GridImplementation<I>(uiObject: uiObject, itemFactory: itemFactory);
        }

        public UICollection<UIObject> RowHeaders {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(RowHeaders)), overridden: out overridden) == ActionResult.Handled ? (UICollection<UIObject>) overridden : new UICollection<UIObject>(navigator: new EnumerableNavigator(enumerable: Pattern.Current.GetRowHeaders()), factory: UIObject.Factory);
            }
        }

        public UICollection<UIObject> ColumnHeaders {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(ColumnHeaders)), overridden: out overridden) == ActionResult.Handled ? (UICollection<UIObject>) overridden : new UICollection<UIObject>(navigator: new EnumerableNavigator(enumerable: Pattern.Current.GetColumnHeaders()), factory: UIObject.Factory);
            }
        }

        public RowOrColumnMajor RowOrColumnMajor {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(RowOrColumnMajor)), overridden: out overridden) == ActionResult.Handled ? (RowOrColumnMajor) overridden : Pattern.Current.RowOrColumnMajor;
            }
        }

        public I GetCell(int row, int column) {
            var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            object overridden;
            return ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(GetCell), row, (object) column), overridden: out overridden) == ActionResult.Handled ? this._itemFactory.Create(element: (UIObject) overridden) : this._itemFactory.Create(element: new UIObject(element: Pattern.GetItem(row: row, column: column)));
        }

        public int RowCount {
            get { return this._grid.RowCount; }
        }

        public int ColumnCount {
            get { return this._grid.ColumnCount; }
        }
    }
}