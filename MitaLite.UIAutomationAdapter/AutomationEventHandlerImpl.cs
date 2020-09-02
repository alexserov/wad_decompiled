// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationEventHandlerImpl
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    internal class AutomationEventHandlerImpl : UIAutomationEventHandler<AutomationEventHandlerImpl>, IUIAutomationEventHandler {
        readonly int _eventId;
        readonly AutomationEventHandler _handlingDelegate;
        readonly IUIAutomationElement _uiAutomationElement;

        AutomationEventHandlerImpl(
            int eventId,
            IUIAutomationElement uiAutomationElement,
            AutomationEventHandler handlingDelegate) {
            this._eventId = eventId;
            this._uiAutomationElement = uiAutomationElement;
            this._handlingDelegate = handlingDelegate;
        }

        void IUIAutomationEventHandler.HandleAutomationEvent(
            IUIAutomationElement sender,
            int eventId) {
            AutomationElement automationElement = null;
            if (sender != null)
                automationElement = new AutomationElement(autoElement: sender);
            if (eventId == 20017)
                this._handlingDelegate(sender: automationElement, e: new WindowClosedEventArgs(runtimeId: automationElement.GetRuntimeId()));
            else
                this._handlingDelegate(sender: automationElement, e: new AutomationEventArgs(eventId: AutomationEvent.LookupById(id: eventId)));
        }

        protected override void Remove() {
            Boundary.NoExceptions(a: () => Automation.AutomationClass.RemoveAutomationEventHandler(eventId: this._eventId, element: this._uiAutomationElement, handler: this));
        }

        internal static void Add(
            AutomationEvent eventId,
            AutomationElement element,
            TreeScope scope,
            AutomationEventHandler handlingDelegate) {
            var e = new AutomationEventHandlerImpl(eventId: eventId.Id, uiAutomationElement: element.IUIAutomationElement, handlingDelegate: handlingDelegate);
            var cacheRequest = AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest;
            Boundary.UIAutomation(a: () => Automation.AutomationClass.AddAutomationEventHandler(eventId: e._eventId, element: e._uiAutomationElement, scope: UiaConvert.Convert(treeScope: scope), cacheRequest: cacheRequest, handler: e));
            Add(instance: e);
        }

        internal static void Remove(
            AutomationEvent eventId,
            AutomationElement element,
            AutomationEventHandler handlingDelegate) {
            Remove(predicate: item => item._eventId == eventId.Id && 1 == Automation.AutomationClass.CompareElements(el1: item._uiAutomationElement, el2: element.IUIAutomationElement) && item._handlingDelegate == handlingDelegate);
        }
    }
}