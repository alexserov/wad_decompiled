// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationFocusChangedEventHandlerImpl
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    internal class AutomationFocusChangedEventHandlerImpl : UIAutomationEventHandler<AutomationFocusChangedEventHandlerImpl>, IUIAutomationFocusChangedEventHandler {
        readonly AutomationFocusChangedEventHandler _handlingDelegate;

        AutomationFocusChangedEventHandlerImpl(
            AutomationFocusChangedEventHandler handlingDelegate) {
            this._handlingDelegate = handlingDelegate;
        }

        void IUIAutomationFocusChangedEventHandler.HandleFocusChangedEvent(
            IUIAutomationElement sender) {
            AutomationElement automationElement = null;
            if (sender != null)
                automationElement = new AutomationElement(autoElement: sender);
            this._handlingDelegate(sender: automationElement, e: null);
        }

        protected override void Remove() {
            Boundary.NoExceptions(a: () => Automation.AutomationClass.RemoveFocusChangedEventHandler(handler: this));
        }

        internal static void Add(
            AutomationFocusChangedEventHandler handlingDelegate) {
            var e = new AutomationFocusChangedEventHandlerImpl(handlingDelegate: handlingDelegate);
            var cacheRequest = AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest;
            Boundary.UIAutomation(a: () => Automation.AutomationClass.AddFocusChangedEventHandler(cacheRequest: cacheRequest, handler: e));
            Add(instance: e);
        }

        internal static void Remove(
            AutomationFocusChangedEventHandler handlingDelegate) {
            Remove(predicate: item => item._handlingDelegate == handlingDelegate);
        }
    }
}