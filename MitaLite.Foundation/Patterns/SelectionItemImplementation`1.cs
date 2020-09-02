// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.SelectionItemImplementation`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;
using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class SelectionItemImplementation<C> : PatternImplementation<SelectionItemPattern>, ISelectionItem<C>
        where C : UIObject {
        public SelectionItemImplementation(UIObject uiObject, IFactory<C> containerFactory)
            : base(uiObject: uiObject, patternIdentifier: SelectionItemPattern.Pattern) {
            Validate.ArgumentNotNull(parameter: containerFactory, parameterName: nameof(containerFactory));
            ContainerFactory = containerFactory;
        }

        protected IFactory<C> ContainerFactory { get; set; }

        public void Select() {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(Select))) != ActionResult.Unhandled)
                return;
            Pattern.Select();
        }

        public UIEventWaiter GetSelectedWaiter() {
            return new AutomationEventWaiter(eventId: SelectionItemPattern.ElementSelectedEvent, uiObject: UIObject, scope: Scope.Element);
        }

        public void AddToSelection() {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(AddToSelection))) != ActionResult.Unhandled)
                return;
            Pattern.AddToSelection();
        }

        public UIEventWaiter GetAddedToSelectionWaiter() {
            return new AutomationEventWaiter(eventId: SelectionItemPattern.ElementAddedToSelectionEvent, uiObject: UIObject, scope: Scope.Element);
        }

        public void RemoveFromSelection() {
            var num1 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(RemoveFromSelection))) != ActionResult.Unhandled)
                return;
            Pattern.RemoveFromSelection();
        }

        public UIEventWaiter GetRemovedFromSelectionWaiter() {
            return new AutomationEventWaiter(eventId: SelectionItemPattern.ElementRemovedFromSelectionEvent, uiObject: UIObject, scope: Scope.Element);
        }

        public bool IsSelected {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(IsSelected)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : Pattern.Current.IsSelected;
            }
        }

        public C SelectionContainer {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(SelectionContainer)), overridden: out overridden) == ActionResult.Handled ? ContainerFactory.Create(element: (UIObject) overridden) : ContainerFactory.Create(element: new UIObject(element: Pattern.Current.SelectionContainer));
            }
        }
    }
}