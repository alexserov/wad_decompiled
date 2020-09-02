// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.StructureChangedEventHandlerImpl
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    internal class StructureChangedEventHandlerImpl : UIAutomationEventHandler<StructureChangedEventHandlerImpl>, IUIAutomationStructureChangedEventHandler {
        readonly StructureChangedEventHandler _handlingDelegate;
        readonly IUIAutomationElement _uiAutomationElement;

        StructureChangedEventHandlerImpl(
            IUIAutomationElement uiAutomationElement,
            StructureChangedEventHandler handlingDelegate) {
            this._uiAutomationElement = uiAutomationElement;
            this._handlingDelegate = handlingDelegate;
        }

        void IUIAutomationStructureChangedEventHandler.HandleStructureChangedEvent(
            IUIAutomationElement sender,
            UIAutomationClient.StructureChangeType changeType,
            int[] runtimeId) {
            AutomationElement automationElement = null;
            if (sender != null)
                automationElement = new AutomationElement(autoElement: sender);
            this._handlingDelegate(sender: automationElement, e: new StructureChangedEventArgs(structureChangeType: UiaConvert.Convert(structureChangeType: changeType), runtimeId: runtimeId));
        }

        protected override void Remove() {
            Boundary.NoExceptions(a: () => Automation.AutomationClass.RemoveStructureChangedEventHandler(element: this._uiAutomationElement, handler: this));
        }

        internal static void Add(
            AutomationElement element,
            TreeScope scope,
            StructureChangedEventHandler handlingDelegate) {
            var e = new StructureChangedEventHandlerImpl(uiAutomationElement: element.IUIAutomationElement, handlingDelegate: handlingDelegate);
            var cacheRequest = AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest;
            Boundary.UIAutomation(a: () => Automation.AutomationClass.AddStructureChangedEventHandler(element: e._uiAutomationElement, scope: UiaConvert.Convert(treeScope: scope), cacheRequest: cacheRequest, handler: e));
            Add(instance: e);
        }

        internal static void Remove(
            AutomationElement element,
            StructureChangedEventHandler handlingDelegate) {
            Remove(predicate: item => item._handlingDelegate == handlingDelegate && 1 == Automation.AutomationClass.CompareElements(el1: item._uiAutomationElement, el2: element.IUIAutomationElement));
        }
    }
}