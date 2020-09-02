// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationPropertyChangedEventHandlerImpl
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Linq;
using UIAutomationClient;

namespace System.Windows.Automation {
    internal class AutomationPropertyChangedEventHandlerImpl : UIAutomationEventHandler<AutomationPropertyChangedEventHandlerImpl>, IUIAutomationPropertyChangedEventHandler {
        readonly AutomationPropertyChangedEventHandler _handlingDelegate;
        readonly IUIAutomationElement _uiAutomationElement;

        AutomationPropertyChangedEventHandlerImpl(
            IUIAutomationElement uiAutomationElement,
            AutomationPropertyChangedEventHandler handlingDelegate) {
            this._uiAutomationElement = uiAutomationElement;
            this._handlingDelegate = handlingDelegate;
        }

        void IUIAutomationPropertyChangedEventHandler.HandlePropertyChangedEvent(
            IUIAutomationElement sender,
            int propertyId,
            Variant newValue) {
            AutomationElement automationElement = null;
            if (sender != null)
                automationElement = new AutomationElement(autoElement: sender);
            this._handlingDelegate(sender: automationElement, e: new AutomationPropertyChangedEventArgs(property: AutomationProperty.LookupById(id: propertyId), oldValue: null, newValue: newValue));
        }

        protected override void Remove() {
            Boundary.NoExceptions(a: () => Automation.AutomationClass.RemovePropertyChangedEventHandler(element: this._uiAutomationElement, handler: this));
        }

        internal static void Add(
            AutomationElement element,
            TreeScope scope,
            AutomationPropertyChangedEventHandler handlingDelegate,
            AutomationProperty[] properties) {
            var e = new AutomationPropertyChangedEventHandlerImpl(uiAutomationElement: element.IUIAutomationElement, handlingDelegate: handlingDelegate);
            var cacheRequest = AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest;
            var propertiesArray = properties.Select(selector: p => p.Id).ToArray();
            Boundary.UIAutomation(a: () => Automation.AutomationClass.AddPropertyChangedEventHandler(element: e._uiAutomationElement, scope: UiaConvert.Convert(treeScope: scope), cacheRequest: cacheRequest, handler: e, propertyArray: propertiesArray));
            Add(instance: e);
        }

        internal static void Remove(
            AutomationElement element,
            AutomationPropertyChangedEventHandler handlingDelegate) {
            Remove(predicate: item => 1 == Automation.AutomationClass.CompareElements(el1: item._uiAutomationElement, el2: element.IUIAutomationElement) && item._handlingDelegate == handlingDelegate);
        }
    }
}