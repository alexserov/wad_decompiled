// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.UIObject
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Types;
using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    public class UIObject {
        static IFactory<UIObject> _factory;

        static UIObject() {
            Provider.LoadProviders();
        }

        public UIObject(UIObject uiObject) {
            Validate.ArgumentNotNull(parameter: uiObject, parameterName: nameof(uiObject));
            Validate.ArgumentNotNull(parameter: uiObject.AutomationElement, parameterName: "uiObject.AutomationElement");
            AutomationElement = uiObject.AutomationElement;
        }

        internal UIObject(AutomationElement element) {
            Validate.ArgumentNotNull(parameter: element, parameterName: nameof(element));
            AutomationElement = element;
        }

        public static UIObject Root {
            get {
                object overridden;
                return ActionHandler.Invoke(sender: null, actionInfo: ActionEventArgs.GetDefault(action: nameof(Root)), overridden: out overridden) == ActionResult.Handled ? (UIObject) overridden : new UIObject(element: AutomationElement.RootElement);
            }
        }

        public static UIObject Focused {
            get {
                object overridden;
                return ActionHandler.Invoke(sender: null, actionInfo: ActionEventArgs.GetDefault(action: nameof(Focused)), overridden: out overridden) == ActionResult.Handled ? (UIObject) overridden : new UIObject(element: AutomationElement.FocusedElement);
            }
        }

        public static IFactory<UIObject> Factory {
            get {
                if (_factory == null)
                    _factory = new UIObjectFactory();
                return _factory;
            }
        }

        public virtual string AcceleratorKey {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(AcceleratorKey)), overridden: out overridden) == ActionResult.Handled ? (string) overridden : AutomationElement.GetCurrentPropertyValue(property: AutomationElement.AcceleratorKeyProperty, ignoreDefaultValue: false).ToString();
            }
        }

        public virtual string AccessKey {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(AccessKey)), overridden: out overridden) == ActionResult.Handled ? (string) overridden : AutomationElement.GetCurrentPropertyValue(property: AutomationElement.AccessKeyProperty, ignoreDefaultValue: false).ToString();
            }
        }

        internal AutomationElement AutomationElement { get; }

        public string AutomationId {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(AutomationId)), overridden: out overridden) == ActionResult.Handled ? (string) overridden : AutomationElement.GetCurrentPropertyValue(property: AutomationElement.AutomationIdProperty, ignoreDefaultValue: false).ToString();
            }
        }

        public RectangleI BoundingRectangle {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                if (ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(BoundingRectangle)), overridden: out overridden) == ActionResult.Handled)
                    return (RectangleI) overridden;
                var boundingRectangle = AutomationElement.Current.BoundingRectangle;
                return new RectangleI(x: (int) boundingRectangle.X, y: (int) boundingRectangle.Y, width: (int) boundingRectangle.Width, height: (int) boundingRectangle.Height);
            }
        }

        public string ClassName {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                if (ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(ClassName)), overridden: out overridden) == ActionResult.Handled)
                    return (string) overridden;
                return AutomationElement.GetCurrentPropertyValue(property: AutomationElement.ClassNameProperty, ignoreDefaultValue: false)?.ToString();
            }
        }

        public ControlType ControlType {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(ControlType)), overridden: out overridden) == ActionResult.Handled ? (ControlType) overridden : (ControlType) AutomationElement.GetCurrentPropertyValue(property: AutomationElement.ControlTypeProperty);
            }
        }

        public CultureInfo Culture {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(Culture)), overridden: out overridden) == ActionResult.Handled ? (CultureInfo) overridden : (CultureInfo) AutomationElement.GetCurrentPropertyValue(property: AutomationElement.CultureProperty);
            }
        }

        public string FrameworkId {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(FrameworkId)), overridden: out overridden) == ActionResult.Handled ? (string) overridden : AutomationElement.GetCurrentPropertyValue(property: AutomationElement.FrameworkIdProperty, ignoreDefaultValue: false).ToString();
            }
        }

        public bool HasKeyboardFocus {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(HasKeyboardFocus)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : (bool) AutomationElement.GetCurrentPropertyValue(property: AutomationElement.HasKeyboardFocusProperty, ignoreDefaultValue: false);
            }
        }

        public string HelpText {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(HelpText)), overridden: out overridden) == ActionResult.Handled ? (string) overridden : AutomationElement.GetCurrentPropertyValue(property: AutomationElement.HelpTextProperty, ignoreDefaultValue: false).ToString();
            }
        }

        public bool IsContentElement {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(IsContentElement)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : (bool) AutomationElement.GetCurrentPropertyValue(property: AutomationElement.IsContentElementProperty, ignoreDefaultValue: false);
            }
        }

        public bool IsItemContainerPatternAvailable {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(IsItemContainerPatternAvailable)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : (bool) AutomationElement.GetCurrentPropertyValue(property: AutomationElement.IsItemContainerPatternAvailableProperty, ignoreDefaultValue: false);
            }
        }

        public bool IsVirtualizedItemPatternAvailable {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(IsVirtualizedItemPatternAvailable)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : (bool) AutomationElement.GetCurrentPropertyValue(property: AutomationElement.IsVirtualizedItemPatternAvailableProperty, ignoreDefaultValue: false);
            }
        }

        public bool IsControlElement {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(IsControlElement)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : (bool) AutomationElement.GetCurrentPropertyValue(property: AutomationElement.IsControlElementProperty, ignoreDefaultValue: false);
            }
        }

        public bool IsEnabled {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(IsEnabled)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : (bool) AutomationElement.GetCurrentPropertyValue(property: AutomationElement.IsEnabledProperty, ignoreDefaultValue: false);
            }
        }

        public bool IsKeyboardFocusable {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(IsKeyboardFocusable)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : (bool) AutomationElement.GetCurrentPropertyValue(property: AutomationElement.IsKeyboardFocusableProperty, ignoreDefaultValue: false);
            }
        }

        public bool IsOffscreen {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(IsOffscreen)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : (bool) AutomationElement.GetCurrentPropertyValue(property: AutomationElement.IsOffscreenProperty, ignoreDefaultValue: false);
            }
        }

        public bool IsPassword {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(IsPassword)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : (bool) AutomationElement.GetCurrentPropertyValue(property: AutomationElement.IsPasswordProperty, ignoreDefaultValue: false);
            }
        }

        public bool IsRequiredForForm {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(IsRequiredForForm)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : (bool) AutomationElement.GetCurrentPropertyValue(property: AutomationElement.IsRequiredForFormProperty, ignoreDefaultValue: false);
            }
        }

        public string ItemStatus {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(ItemStatus)), overridden: out overridden) == ActionResult.Handled ? (string) overridden : AutomationElement.GetCurrentPropertyValue(property: AutomationElement.ItemStatusProperty, ignoreDefaultValue: false).ToString();
            }
        }

        public string ItemType {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(ItemType)), overridden: out overridden) == ActionResult.Handled ? (string) overridden : AutomationElement.GetCurrentPropertyValue(property: AutomationElement.ItemTypeProperty, ignoreDefaultValue: false).ToString();
            }
        }

        public UIObject LabeledBy {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                if (ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(LabeledBy)), overridden: out overridden) == ActionResult.Handled)
                    return (UIObject) overridden;
                var currentPropertyValue = (AutomationElement) AutomationElement.GetCurrentPropertyValue(property: AutomationElement.LabeledByProperty, ignoreDefaultValue: false);
                return currentPropertyValue != (AutomationElement) null ? new UIObject(element: currentPropertyValue) : null;
            }
        }

        public string LocalizedControlType {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(LocalizedControlType)), overridden: out overridden) == ActionResult.Handled ? (string) overridden : AutomationElement.GetCurrentPropertyValue(property: AutomationElement.LocalizedControlTypeProperty, ignoreDefaultValue: false).ToString();
            }
        }

        public virtual string Name {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                if (ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(Name)), overridden: out overridden) == ActionResult.Handled)
                    return (string) overridden;
                return AutomationElement.GetCurrentPropertyValue(property: AutomationElement.NameProperty, ignoreDefaultValue: false)?.ToString();
            }
        }

        public IntPtr NativeWindowHandle {
            get {
                var num1 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                if (ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(NativeWindowHandle)), overridden: out overridden) == ActionResult.Handled)
                    return (IntPtr) overridden;
                var currentPropertyValue = AutomationElement.GetCurrentPropertyValue(property: AutomationElement.NativeWindowHandleProperty, ignoreDefaultValue: false);
                if (currentPropertyValue == null)
                    return IntPtr.Zero;
                return currentPropertyValue is int num2 ? new IntPtr(value: num2) : (IntPtr) currentPropertyValue;
            }
        }

        public OrientationType Orientation {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(Orientation)), overridden: out overridden) == ActionResult.Handled ? (OrientationType) overridden : (OrientationType) AutomationElement.GetCurrentPropertyValue(property: AutomationElement.OrientationProperty, ignoreDefaultValue: false);
            }
        }

        public int ProcessId {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(ProcessId)), overridden: out overridden) == ActionResult.Handled ? (int) overridden : (int) AutomationElement.GetCurrentPropertyValue(property: AutomationElement.ProcessIdProperty, ignoreDefaultValue: false);
            }
        }

        public string RuntimeId {
            get {
                var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(RuntimeId)), overridden: out overridden) == ActionResult.Handled ? (string) overridden : Utilities.RuntimeId.StringFromParts(runtimeIdParts: AutomationElement.GetRuntimeId());
            }
        }

        public UICollection<UIObject> Children {
            get { return new UIChildren<UIObject>(root: this, factory: Factory); }
        }

        public UICollection<UIObject> Siblings {
            get { return new UISiblings<UIObject>(root: this, factory: Factory); }
        }

        public UICollection<UIObject> Ancestors {
            get { return new UIAncestors<UIObject>(root: this, factory: Factory); }
        }

        public UICollection<UIObject> Descendants {
            get { return new UIDepthFirstDescendants<UIObject>(root: this, factory: Factory); }
        }

        public UIObject FirstChild {
            get {
                var firstChild = new TreeWalker(condition: Context.Current.TreeCondition.Condition).GetFirstChild(element: AutomationElement);
                return firstChild == (AutomationElement) null ? null : new UIObject(element: firstChild);
            }
        }

        public UIObject LastChild {
            get {
                var lastChild = new TreeWalker(condition: Context.Current.TreeCondition.Condition).GetLastChild(element: AutomationElement);
                return lastChild == (AutomationElement) null ? null : new UIObject(element: lastChild);
            }
        }

        public UIObject NextSibling {
            get {
                var nextSibling = new TreeWalker(condition: Context.Current.TreeCondition.Condition).GetNextSibling(element: AutomationElement);
                return nextSibling == (AutomationElement) null ? null : new UIObject(element: nextSibling);
            }
        }

        public UIObject PreviousSibling {
            get {
                var previousSibling = new TreeWalker(condition: Context.Current.TreeCondition.Condition).GetPreviousSibling(element: AutomationElement);
                return previousSibling == (AutomationElement) null ? null : new UIObject(element: previousSibling);
            }
        }

        public UIObject Parent {
            get {
                var parent = new TreeWalker(condition: Context.Current.TreeCondition.Condition).GetParent(element: AutomationElement);
                return parent == (AutomationElement) null ? null : new UIObject(element: parent);
            }
        }

        public IEnumerable<UIProperty> SupportedProperties {
            get { return new SupportedProperties(uiObject: this); }
        }

        public static UIObject FromHandle(IntPtr hwnd) {
            object overridden;
            return ActionHandler.Invoke(sender: null, actionInfo: new ActionEventArgs(action: nameof(FromHandle), (object) hwnd), overridden: out overridden) == ActionResult.Handled ? (UIObject) overridden : new UIObject(element: AutomationElement.FromHandle(hwnd: hwnd));
        }

        public static UIObject FromPoint(PointI pixelLocation) {
            object overridden;
            return ActionHandler.Invoke(sender: null, actionInfo: new ActionEventArgs(action: nameof(FromPoint), (object) pixelLocation), overridden: out overridden) == ActionResult.Handled ? (UIObject) overridden : new UIObject(element: AutomationElement.FromPoint(pt: new Point(x: pixelLocation.X, y: pixelLocation.Y)));
        }

        public static bool TrySetConnectionTimeout(TimeSpan timeout) {
            return Automation.TrySetConnectionTimeout(timeout: timeout);
        }

        public static bool TryGetConnectionTimeout(out TimeSpan timeout) {
            return Automation.TryGetConnectionTimeout(timeout: out timeout);
        }

        public static bool TrySetTransactionTimeout(TimeSpan timeout) {
            return Automation.TrySetTransactionTimeout(timeout: timeout);
        }

        public static bool TryGetTransactionTimeout(out TimeSpan timeout) {
            return Automation.TryGetTransactionTimeout(timeout: out timeout);
        }

        public void Click() {
            var num1 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            var fromUpperLeftCorner = ComputeOffsetFromUpperLeftCorner();
            ClickImplementation(button: PointerButtons.Primary, offsetX: fromUpperLeftCorner.X, offsetY: fromUpperLeftCorner.Y);
        }

        public void Click(PointerButtons button) {
            var num1 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            var fromUpperLeftCorner = ComputeOffsetFromUpperLeftCorner();
            ClickImplementation(button: button, offsetX: fromUpperLeftCorner.X, offsetY: fromUpperLeftCorner.Y);
        }

        public void Click(PointerButtons button, double offsetX, double offsetY) {
            var num1 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            ClickImplementation(button: button, offsetX: offsetX, offsetY: offsetY);
        }

        protected void ClickImplementation(PointerButtons button, double offsetX, double offsetY) {
            if (ActionHandler.Invoke(sender: this, actionInfo: new ActionEventArgs(action: "Click", button, (object) offsetX, (object) offsetY)) != ActionResult.Unhandled)
                return;
            MovePointer(offsetX: offsetX, offsetY: offsetY);
            PointerInput.Click(button: button, count: 1);
        }

        public void DoubleClick() {
            var num1 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            var fromUpperLeftCorner = ComputeOffsetFromUpperLeftCorner();
            DoubleClickImplementation(button: PointerButtons.Primary, offsetX: fromUpperLeftCorner.X, offsetY: fromUpperLeftCorner.Y);
        }

        public void DoubleClick(PointerButtons button) {
            var num1 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            var fromUpperLeftCorner = ComputeOffsetFromUpperLeftCorner();
            DoubleClickImplementation(button: button, offsetX: fromUpperLeftCorner.X, offsetY: fromUpperLeftCorner.Y);
        }

        public void DoubleClick(PointerButtons button, double offsetX, double offsetY) {
            var num1 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            DoubleClickImplementation(button: button, offsetX: offsetX, offsetY: offsetY);
        }

        protected void DoubleClickImplementation(PointerButtons button, double offsetX, double offsetY) {
            if (ActionHandler.Invoke(sender: this, actionInfo: new ActionEventArgs(action: "DoubleClick", button, (object) offsetX, (object) offsetY)) != ActionResult.Unhandled)
                return;
            MovePointer(offsetX: offsetX, offsetY: offsetY);
            PointerInput.Click(button: button, count: 2);
        }

        public void ClickDrag(UIObject uiObject) {
            ClickDrag(button: PointerButtons.Primary, uiObject: uiObject);
        }

        public void ClickDrag(PointerButtons button, UIObject uiObject) {
            Validate.ArgumentNotNull(parameter: uiObject, parameterName: nameof(uiObject));
            var num1 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: this, actionInfo: new ActionEventArgs(action: nameof(ClickDrag), button, (object) uiObject)) != ActionResult.Unhandled)
                return;
            PointerInput.ClickDrag(targetObject: this, destinationObject: uiObject, button: button);
        }

        public void MovePointer() {
            var fromUpperLeftCorner = ComputeOffsetFromUpperLeftCorner();
            MovePointer(offsetX: fromUpperLeftCorner.X, offsetY: fromUpperLeftCorner.Y);
        }

        public void MovePointer(double offsetX, double offsetY) {
            var location = BoundingRectangle.Location;
            PointerInput.Move(point: new PointI(x: (int) (location.X + offsetX), y: (int) (location.Y + offsetY)));
        }

        public void Tap() {
            var fromUpperLeftCorner = ComputeOffsetFromUpperLeftCorner();
            Tap(offsetX: fromUpperLeftCorner.X, offsetY: fromUpperLeftCorner.Y);
        }

        public virtual void Tap(double offsetX, double offsetY) {
            var num1 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: this, actionInfo: new ActionEventArgs(action: nameof(Tap), offsetX, (object) offsetY)) != ActionResult.Unhandled)
                return;
            using (InputController.Activate(inputType: PointerInputType.MultiTouch)) {
                PointerInput.Click(uiObject: this, button: PointerButtons.Primary, offsetX: offsetX, offsetY: offsetY);
            }
        }

        public void DoubleTap() {
            var fromUpperLeftCorner = ComputeOffsetFromUpperLeftCorner();
            DoubleTap(offsetX: fromUpperLeftCorner.X, offsetY: fromUpperLeftCorner.Y);
        }

        public virtual void DoubleTap(double offsetX, double offsetY) {
            var num1 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: this, actionInfo: new ActionEventArgs(action: nameof(DoubleTap), offsetX, (object) offsetY)) != ActionResult.Unhandled)
                return;
            using (InputController.Activate(inputType: PointerInputType.MultiTouch)) {
                PointerInput.DoubleClick(uiObject: this, button: PointerButtons.Primary, offsetX: offsetX, offsetY: offsetY);
            }
        }

        public void TapAndHold() {
            TapAndHold(duration: SinglePointGesture.DefaultHoldDuration);
        }

        public void TapAndHold(uint duration) {
            var fromUpperLeftCorner = ComputeOffsetFromUpperLeftCorner();
            TapAndHold(offsetX: fromUpperLeftCorner.X, offsetY: fromUpperLeftCorner.Y, duration: duration);
        }

        public void TapAndHold(double offsetX, double offsetY) {
            TapAndHold(offsetX: offsetX, offsetY: offsetY, duration: SinglePointGesture.DefaultHoldDuration);
        }

        public virtual void TapAndHold(double offsetX, double offsetY, uint duration) {
            var num1 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: this, actionInfo: new ActionEventArgs(action: nameof(TapAndHold), offsetX, (object) offsetY, (object) duration)) != ActionResult.Unhandled)
                return;
            SinglePointGesture.Current.Move(point: ObjectPointToScreenPoint(objectPoint: new PointI(x: (int) Math.Round(a: offsetX), y: (int) Math.Round(a: offsetY))));
            SinglePointGesture.PressAndHold(holdDuration: duration);
        }

        public void Pan(float direction) {
            Pan(distance: SinglePointGesture.MinimumPanFlickDistance, direction: direction);
        }

        public void Pan(int distance, float direction) {
            Pan(startPoint: ComputeOffsetFromUpperLeftCorner(), distance: distance, direction: direction);
        }

        public void Pan(PointI startPoint, int distance, float direction) {
            var num = direction * Math.PI / 180.0;
            var x = startPoint.X + (int) Math.Round(a: distance * Math.Cos(d: num));
            var y = startPoint.Y - (int) Math.Round(a: distance * Math.Sin(a: num));
            Pan(startPoint: startPoint, endPoint: new PointI(x: x, y: y));
        }

        public virtual void Pan(PointI startPoint, PointI endPoint) {
            var num1 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: this, actionInfo: new ActionEventArgs(action: nameof(Pan), startPoint, (object) endPoint)) != ActionResult.Unhandled)
                return;
            var screenPoint1 = ObjectPointToScreenPoint(objectPoint: startPoint);
            var screenPoint2 = ObjectPointToScreenPoint(objectPoint: endPoint);
            SinglePointGesture.Current.Move(point: screenPoint1);
            var defaultPressDuration = (int) InputManager.DefaultPressDuration;
            double defaultPanAcceleration = SinglePointGesture.DefaultPanAcceleration;
            SinglePointGesture.Pan(endPoint: screenPoint2, holdDuration: (uint) defaultPressDuration, acceleration: (float) defaultPanAcceleration);
        }

        public void Flick(float direction) {
            Flick(distance: SinglePointGesture.MinimumPanFlickDistance, direction: direction);
        }

        public void Flick(PointI startPoint, float direction) {
            Flick(startPoint: startPoint, distance: SinglePointGesture.MinimumPanFlickDistance, direction: direction);
        }

        public void Flick(int distance, float direction) {
            Flick(startPoint: ComputeOffsetFromUpperLeftCorner(), distance: distance, direction: direction);
        }

        public void Flick(PointI startPoint, int distance, float direction) {
            var num = direction * Math.PI / 180.0;
            var x = startPoint.X + (int) Math.Round(a: distance * Math.Cos(d: num));
            var y = startPoint.Y - (int) Math.Round(a: distance * Math.Sin(a: num));
            Flick(startPoint: startPoint, endPoint: new PointI(x: x, y: y));
        }

        public virtual void Flick(PointI startPoint, PointI endPoint) {
            var num1 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: this, actionInfo: new ActionEventArgs(action: nameof(Flick), startPoint, (object) endPoint)) == ActionResult.Unhandled) {
                var screenPoint1 = ObjectPointToScreenPoint(objectPoint: startPoint);
                var screenPoint2 = ObjectPointToScreenPoint(objectPoint: endPoint);
                SinglePointGesture.Current.Move(point: screenPoint1);
                var defaultPressDuration = (int) InputManager.DefaultPressDuration;
                double flickAcceleration = SinglePointGesture.DefaultFlickAcceleration;
                SinglePointGesture.Flick(endPoint: screenPoint2, holdDuration: (uint) defaultPressDuration, acceleration: (float) flickAcceleration);
            }

            var num3 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "UIScrollComplete"));
        }

        public void PinchStretch(uint initialDistance, uint finalDistance) {
            PinchStretch(startPoint: ComputeOffsetFromUpperLeftCorner(), initialDistance: initialDistance, finalDistance: finalDistance, duration: MultiPointGesture.DefaultPinchStretchDuration);
        }

        public void PinchStretch(PointI startPoint, uint initialDistance, uint finalDistance) {
            PinchStretch(startPoint: startPoint, initialDistance: initialDistance, finalDistance: finalDistance, duration: MultiPointGesture.DefaultPinchStretchDuration);
        }

        public virtual void PinchStretch(
            PointI startPoint,
            uint initialDistance,
            uint finalDistance,
            uint duration) {
            var num1 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var num2 = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "MakeVisible"));
            if (ActionHandler.Invoke(sender: this, actionInfo: new ActionEventArgs(action: nameof(PinchStretch), startPoint, (object) initialDistance, (object) finalDistance, (object) duration)) != ActionResult.Unhandled)
                return;
            var screenPoint = ObjectPointToScreenPoint(objectPoint: startPoint);
            if (initialDistance > finalDistance) {
                MultiPointGesture.Current.Move(point: MultiPointGesture.OffsetPinchPoints(targetPoint: screenPoint, boundingRectangle: BoundingRectangle, distance: initialDistance, direction: MultiPointGesture.DefaultPinchStretchDirection));
                MultiPointGesture.Pinch(direction: MultiPointGesture.DefaultPinchStretchDirection, duration: duration, startDistance: initialDistance, endDistance: finalDistance);
            } else {
                MultiPointGesture.Current.Move(point: screenPoint);
                MultiPointGesture.Stretch(direction: MultiPointGesture.DefaultPinchStretchDirection, duration: duration, startDistance: initialDistance, endDistance: finalDistance);
            }
        }

        PointI ObjectPointToScreenPoint(PointI objectPoint) {
            var boundingRectangle = BoundingRectangle;
            boundingRectangle.Offset(offsetX: objectPoint.X, offsetY: objectPoint.Y);
            return boundingRectangle.Location;
        }

        public override bool Equals(object obj) {
            var uiObject = obj as UIObject;
            if (!(uiObject != null))
                return base.Equals(obj: obj);
            try {
                return Automation.Compare(el1: AutomationElement, el2: uiObject.AutomationElement);
            } catch (COMException ex) {
                return false;
            }
        }

        public PointI GetClickablePoint() {
            var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            object overridden;
            if (ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(GetClickablePoint)), overridden: out overridden) == ActionResult.Handled)
                return (PointI) overridden;
            Point pt;
            if (AutomationElement.TryGetClickablePoint(pt: out pt))
                return new PointI(x: (int) pt.X, y: (int) pt.Y);
            var boundingRectangle = BoundingRectangle;
            return new PointI(x: boundingRectangle.X + boundingRectangle.Width / 2, y: boundingRectangle.Y + boundingRectangle.Height / 2);
        }

        public override int GetHashCode() {
            var runtimeId = AutomationElement.GetRuntimeId();
            var num1 = 0;
            foreach (var num2 in runtimeId)
                num1 = (num1 + num2) * 101;
            return num1;
        }

        public AutomationPattern[] GetSupportedPatterns() {
            return AutomationElement.GetSupportedPatterns();
        }

        public object GetProperty(UIProperty property) {
            Validate.ArgumentNotNull(parameter: property, parameterName: nameof(property));
            return WrapValue(value: AutomationElement.GetCurrentPropertyValue(property: property.Property, ignoreDefaultValue: false));
        }

        public object GetCachedProperty(UIProperty property) {
            Validate.ArgumentNotNull(parameter: property, parameterName: nameof(property));
            return WrapValue(value: AutomationElement.GetCachedPropertyValue(property: property.Property, ignoreDefaultValue: false));
        }

        public bool Matches(UICondition condition) {
            Validate.ArgumentNotNull(parameter: condition, parameterName: nameof(condition));
            return condition.Matches(element: AutomationElement);
        }

        public static bool Matches(UIObject uiObject, UICondition condition) {
            Validate.ArgumentNotNull(parameter: uiObject, parameterName: nameof(uiObject));
            Validate.ArgumentNotNull(parameter: condition, parameterName: nameof(condition));
            return condition.Matches(element: uiObject.AutomationElement);
        }

        internal static bool Matches(AutomationElement element, UICondition condition) {
            Validate.ArgumentNotNull(parameter: condition, parameterName: nameof(condition));
            return condition.Matches(element: element);
        }

        public void SaveImage(string fileName) {
            Validate.StringNeitherNullNorEmpty(parameter: fileName, parameterName: nameof(fileName));
            var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            if (ActionHandler.Invoke(sender: this, actionInfo: new ActionEventArgs(action: nameof(SaveImage), fileName)) != ActionResult.Unhandled)
                return;
            ScreenCapture.SaveImage(rectangle: BoundingRectangle, fileName: fileName);
        }

        public void SendKeys(string text) {
            Validate.StringNeitherNullNorEmpty(parameter: text, parameterName: nameof(text));
            SetFocus();
            if (ActionHandler.Invoke(sender: this, actionInfo: new ActionEventArgs(action: nameof(SendKeys), text)) != ActionResult.Unhandled)
                return;
            TextInput.SendText(text: text);
        }

        public void SetFocus() {
            var num = (int) ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            if (ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(SetFocus))) != ActionResult.Unhandled || (bool) AutomationElement.GetCurrentPropertyValue(property: AutomationElement.HasKeyboardFocusProperty))
                return;
            AutomationElement.SetFocus();
        }

        public override string ToString() {
            object overridden;
            if (ActionHandler.Invoke(sender: this, actionInfo: ActionEventArgs.GetDefault(action: nameof(ToString)), overridden: out overridden) == ActionResult.Handled)
                return (string) overridden;
            return "{" + Name + ", " + ClassName + ", " + RuntimeId + "}";
        }

        public static bool operator ==(UIObject firstElement, UIObject secondElement) {
            if (firstElement == (object) secondElement)
                return true;
            return (object) firstElement != null && firstElement.Equals(obj: secondElement);
        }

        public static bool operator !=(UIObject firstElement, UIObject secondElement) {
            if (firstElement == (object) secondElement)
                return false;
            return (object) firstElement == null || !firstElement.Equals(obj: secondElement);
        }

        internal static string SafeGetName(UIObject uiObject) {
            if (uiObject == null)
                throw new ArgumentNullException(paramName: nameof(uiObject));
            var name = StringResource.Get(id: "UIObject_SafeGetNameDefault");
            var thread = new Thread(start: o => {
                try {
                    name = uiObject.Name;
                } catch {
                }
            });
            thread.Start();
            thread.Join(millisecondsTimeout: 500);
            return name;
        }

        PointI ComputeOffsetFromUpperLeftCorner() {
            var clickablePoint = GetClickablePoint();
            return new PointI(x: clickablePoint.X - BoundingRectangle.X, y: clickablePoint.Y - BoundingRectangle.Y);
        }

        static object WrapValue(object value) {
            AutomationElement element;
            if ((element = value as AutomationElement) != null)
                return new UIObject(element: element);
            switch (value) {
                case AutomationElement[] automationElementArray:
                    return new UICollection<UIObject>(navigator: new EnumerableNavigator(enumerable: automationElementArray), factory: Factory);
                case AutomationElementCollection elementCollection:
                    return new UICollection<UIObject>(navigator: new EnumerableNavigator(enumerable: elementCollection), factory: Factory);
                case Rect rect:
                    return new RectangleI(x: (int) rect.X, y: (int) rect.Y, width: (int) rect.Width, height: (int) rect.Height);
                default:
                    return value;
            }
        }

        class UIObjectFactory : IFactory<UIObject> {
            public UIObject Create(UIObject element) {
                return new UIObject(uiObject: element);
            }
        }
    }
}