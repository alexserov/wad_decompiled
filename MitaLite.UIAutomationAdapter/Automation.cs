// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.Automation
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public static class Automation {
        public static readonly Condition RawViewCondition = new Condition(condition: AutomationClass.RawViewCondition);
        public static readonly Condition ControlViewCondition = new Condition(condition: AutomationClass.ControlViewCondition);
        public static readonly Condition ContentViewCondition = new Condition(condition: AutomationClass.ContentViewCondition);
        internal static IUIAutomation _automation;

        internal static IUIAutomation AutomationClass {
            get {
                if (_automation == null)
                    _automation = new CUIAutomation8Class();
                return _automation;
            }
        }

        public static bool Compare(AutomationElement el1, AutomationElement el2) {
            return Convert.ToBoolean(value: Boundary.UIAutomation(f: () => AutomationClass.CompareElements(el1: el1.IUIAutomationElement, el2: el2.IUIAutomationElement)));
        }

        public static bool Compare(int[] runtimeId1, int[] runtimeId2) {
            return Convert.ToBoolean(value: Boundary.UIAutomation(f: () => AutomationClass.CompareRuntimeIds(runtimeId1: runtimeId1, runtimeId2: runtimeId2)));
        }

        public static string PropertyName(AutomationProperty property) {
            return Boundary.UIAutomation(f: () => AutomationClass.GetPropertyProgrammaticName(property: property.Id));
        }

        public static string PatternName(AutomationPattern pattern) {
            return Boundary.UIAutomation(f: () => AutomationClass.GetPatternProgrammaticName(pattern: pattern.Id));
        }

        public static void AddAutomationEventHandler(
            AutomationEvent eventId,
            AutomationElement element,
            TreeScope scope,
            AutomationEventHandler eventHandler) {
            AutomationEventHandlerImpl.Add(eventId: eventId, element: element, scope: scope, handlingDelegate: eventHandler);
        }

        public static void RemoveAutomationEventHandler(
            AutomationEvent eventId,
            AutomationElement element,
            AutomationEventHandler eventHandler) {
            AutomationEventHandlerImpl.Remove(eventId: eventId, element: element, handlingDelegate: eventHandler);
        }

        public static void AddAutomationPropertyChangedEventHandler(
            AutomationElement element,
            TreeScope scope,
            AutomationPropertyChangedEventHandler eventHandler,
            params AutomationProperty[] properties) {
            AutomationPropertyChangedEventHandlerImpl.Add(element: element, scope: scope, handlingDelegate: eventHandler, properties: properties);
        }

        public static void RemoveAutomationPropertyChangedEventHandler(
            AutomationElement element,
            AutomationPropertyChangedEventHandler eventHandler) {
            AutomationPropertyChangedEventHandlerImpl.Remove(element: element, handlingDelegate: eventHandler);
        }

        public static void AddStructureChangedEventHandler(
            AutomationElement element,
            TreeScope scope,
            StructureChangedEventHandler eventHandler) {
            StructureChangedEventHandlerImpl.Add(element: element, scope: scope, handlingDelegate: eventHandler);
        }

        public static void RemoveStructureChangedEventHandler(
            AutomationElement element,
            StructureChangedEventHandler eventHandler) {
            StructureChangedEventHandlerImpl.Remove(element: element, handlingDelegate: eventHandler);
        }

        public static void AddAutomationFocusChangedEventHandler(
            AutomationFocusChangedEventHandler eventHandler) {
            AutomationFocusChangedEventHandlerImpl.Add(handlingDelegate: eventHandler);
        }

        public static void RemoveAutomationFocusChangedEventHandler(
            AutomationFocusChangedEventHandler eventHandler) {
            AutomationFocusChangedEventHandlerImpl.Remove(handlingDelegate: eventHandler);
        }

        public static void RemoveAllEventHandlers() {
            UIAutomationEventHandler<AutomationEventHandlerImpl>.RemoveAll();
            UIAutomationEventHandler<StructureChangedEventHandlerImpl>.RemoveAll();
            UIAutomationEventHandler<AutomationPropertyChangedEventHandlerImpl>.RemoveAll();
            UIAutomationEventHandler<AutomationFocusChangedEventHandlerImpl>.RemoveAll();
        }

        public static bool TrySetConnectionTimeout(TimeSpan timeout) {
            if (!(AutomationClass is IUIAutomation2 automationClass))
                return false;
            try {
                automationClass.ConnectionTimeout = Convert.ToUInt32(value: timeout.TotalMilliseconds);
                return true;
            } catch (OverflowException ex) {
                return false;
            }
        }

        public static bool TryGetConnectionTimeout(out TimeSpan timeout) {
            if (!(AutomationClass is IUIAutomation2 automationClass)) {
                timeout = TimeSpan.Zero;
                return false;
            }

            timeout = TimeSpan.FromMilliseconds(value: automationClass.ConnectionTimeout);
            return true;
        }

        public static bool TrySetTransactionTimeout(TimeSpan timeout) {
            if (!(AutomationClass is IUIAutomation2 automationClass))
                return false;
            try {
                automationClass.TransactionTimeout = Convert.ToUInt32(value: timeout.TotalMilliseconds);
                return true;
            } catch (OverflowException ex) {
                return false;
            }
        }

        public static bool TryGetTransactionTimeout(out TimeSpan timeout) {
            if (!(AutomationClass is IUIAutomation2 automationClass)) {
                timeout = TimeSpan.Zero;
                return false;
            }

            timeout = TimeSpan.FromMilliseconds(value: automationClass.TransactionTimeout);
            return true;
        }
    }
}