// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.TableItemImplementation`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class TableItemImplementation<C> : PatternImplementation<TableItemPattern>, ITableItem<C>, IGridItem<C>
        where C : UIObject {
        IFactory<C> _containerFactory;
        readonly GridItemImplementation<C> _gridItem;

        public TableItemImplementation(UIObject uiObject, IFactory<C> containerFactory)
            : base(uiObject: uiObject, patternIdentifier: TableItemPattern.Pattern) {
            Validate.ArgumentNotNull(parameter: containerFactory, parameterName: nameof(containerFactory));
            this._containerFactory = containerFactory;
            this._gridItem = new GridItemImplementation<C>(uiObject: uiObject, containerFactory: containerFactory);
        }

        public UICollection<UIObject> RowHeaderItems {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(RowHeaderItems)), overridden: out overridden) == ActionResult.Handled ? (UICollection<UIObject>) overridden : new UICollection<UIObject>(navigator: new EnumerableNavigator(enumerable: Pattern.Current.GetRowHeaderItems()), factory: UIObject.Factory);
            }
        }

        public UICollection<UIObject> ColumnHeaderItems {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(ColumnHeaderItems)), overridden: out overridden) == ActionResult.Handled ? (UICollection<UIObject>) overridden : new UICollection<UIObject>(navigator: new EnumerableNavigator(enumerable: Pattern.Current.GetColumnHeaderItems()), factory: UIObject.Factory);
            }
        }

        public C ContainingGrid {
            get { return this._gridItem.ContainingGrid; }
        }

        public int Row {
            get { return this._gridItem.Row; }
        }

        public int Column {
            get { return this._gridItem.Column; }
        }

        public int RowSpan {
            get { return this._gridItem.RowSpan; }
        }

        public int ColumnSpan {
            get { return this._gridItem.ColumnSpan; }
        }
    }
}