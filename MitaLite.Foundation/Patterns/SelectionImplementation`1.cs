// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.SelectionImplementation`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class SelectionImplementation<I> : PatternImplementation<SelectionPattern>, ISelection<I>
        where I : UIObject {
        public SelectionImplementation(UIObject uiObject, IFactory<I> itemFactory)
            : base(uiObject: uiObject, patternIdentifier: SelectionPattern.Pattern) {
            Validate.ArgumentNotNull(parameter: itemFactory, parameterName: nameof(itemFactory));
            ItemFactory = itemFactory;
        }

        protected IFactory<I> ItemFactory { get; set; }

        public UICollection<I> Selection {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(Selection)), overridden: out overridden) == ActionResult.Handled ? (UICollection<I>) overridden : new UICollection<I>(navigator: new EnumerableNavigator(enumerable: Pattern.Current.GetSelection()), factory: ItemFactory);
            }
        }

        public bool CanSelectMultiple {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(CanSelectMultiple)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : Pattern.Current.CanSelectMultiple;
            }
        }

        public bool IsSelectionRequired {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(IsSelectionRequired)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : Pattern.Current.IsSelectionRequired;
            }
        }
    }
}