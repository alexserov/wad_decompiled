// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.GridImplementation`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class GridImplementation<I> : PatternImplementation<GridPattern>, IGrid<I>
        where I : UIObject {
        readonly IFactory<I> _itemFactory;

        public GridImplementation(UIObject uiObject, IFactory<I> itemFactory)
            : base(uiObject: uiObject, patternIdentifier: GridPattern.Pattern) {
            Validate.ArgumentNotNull(parameter: itemFactory, parameterName: nameof(itemFactory));
            this._itemFactory = itemFactory;
        }

        public I GetCell(int row, int column) {
            var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            object overridden;
            return ActionHandler.Invoke(sender: UIObject, actionInfo: new ActionEventArgs(action: nameof(GetCell), row, (object) column), overridden: out overridden) == ActionResult.Handled ? this._itemFactory.Create(element: (UIObject) overridden) : this._itemFactory.Create(element: new UIObject(element: Pattern.GetItem(row: row, column: column)));
        }

        public int RowCount {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(RowCount)), overridden: out overridden) == ActionResult.Handled ? (int) overridden : Pattern.Current.RowCount;
            }
        }

        public int ColumnCount {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(ColumnCount)), overridden: out overridden) == ActionResult.Handled ? (int) overridden : Pattern.Current.ColumnCount;
            }
        }
    }
}