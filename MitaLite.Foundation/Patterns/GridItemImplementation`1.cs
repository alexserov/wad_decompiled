// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.GridItemImplementation`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class GridItemImplementation<C> : PatternImplementation<GridItemPattern>, IGridItem<C>
        where C : UIObject {
        readonly IFactory<C> _containerFactory;

        public GridItemImplementation(UIObject uiObject, IFactory<C> containerFactory)
            : base(uiObject: uiObject, patternIdentifier: GridItemPattern.Pattern) {
            Validate.ArgumentNotNull(parameter: containerFactory, parameterName: nameof(containerFactory));
            this._containerFactory = containerFactory;
        }

        public C ContainingGrid {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(ContainingGrid)), overridden: out overridden) == ActionResult.Handled ? this._containerFactory.Create(element: (UIObject) overridden) : this._containerFactory.Create(element: new UIObject(element: Pattern.Current.ContainingGrid));
            }
        }

        public int Row {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(Row)), overridden: out overridden) == ActionResult.Handled ? (int) overridden : Pattern.Current.Row;
            }
        }

        public int Column {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(Column)), overridden: out overridden) == ActionResult.Handled ? (int) overridden : Pattern.Current.Column;
            }
        }

        public int RowSpan {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(RowSpan)), overridden: out overridden) == ActionResult.Handled ? (int) overridden : Pattern.Current.RowSpan;
            }
        }

        public int ColumnSpan {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(ColumnSpan)), overridden: out overridden) == ActionResult.Handled ? (int) overridden : Pattern.Current.ColumnSpan;
            }
        }
    }
}