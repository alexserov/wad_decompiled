// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationElement
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Types;
using UIAutomationAdapter.Utilities;
using UIAutomationClient;

namespace System.Windows.Automation {
    public sealed class AutomationElement {
        public static readonly AutomationProperty AcceleratorKeyProperty = AutomationElementIdentifiers.AcceleratorKeyProperty;
        public static readonly AutomationProperty AccessKeyProperty = AutomationElementIdentifiers.AccessKeyProperty;
        public static readonly AutomationProperty AutomationIdProperty = AutomationElementIdentifiers.AutomationIdProperty;
        public static readonly AutomationProperty BoundingRectangleProperty = AutomationElementIdentifiers.BoundingRectangleProperty;
        public static readonly AutomationProperty ClassNameProperty = AutomationElementIdentifiers.ClassNameProperty;
        public static readonly AutomationProperty ClickablePointProperty = AutomationElementIdentifiers.ClickablePointProperty;
        public static readonly AutomationProperty ControlTypeProperty = AutomationElementIdentifiers.ControlTypeProperty;
        public static readonly AutomationProperty CultureProperty = AutomationElementIdentifiers.CultureProperty;
        public static readonly AutomationProperty FrameworkIdProperty = AutomationElementIdentifiers.FrameworkIdProperty;
        public static readonly AutomationProperty HasKeyboardFocusProperty = AutomationElementIdentifiers.HasKeyboardFocusProperty;
        public static readonly AutomationProperty HelpTextProperty = AutomationElementIdentifiers.HelpTextProperty;
        public static readonly AutomationProperty IsContentElementProperty = AutomationElementIdentifiers.IsContentElementProperty;
        public static readonly AutomationProperty IsControlElementProperty = AutomationElementIdentifiers.IsControlElementProperty;
        public static readonly AutomationProperty IsDockPatternAvailableProperty = AutomationElementIdentifiers.IsDockPatternAvailableProperty;
        public static readonly AutomationProperty IsEnabledProperty = AutomationElementIdentifiers.IsEnabledProperty;
        public static readonly AutomationProperty IsExpandCollapsePatternAvailableProperty = AutomationElementIdentifiers.IsExpandCollapsePatternAvailableProperty;
        public static readonly AutomationProperty IsGridItemPatternAvailableProperty = AutomationElementIdentifiers.IsGridItemPatternAvailableProperty;
        public static readonly AutomationProperty IsGridPatternAvailableProperty = AutomationElementIdentifiers.IsGridPatternAvailableProperty;
        public static readonly AutomationProperty IsInvokePatternAvailableProperty = AutomationElementIdentifiers.IsInvokePatternAvailableProperty;
        public static readonly AutomationProperty IsKeyboardFocusableProperty = AutomationElementIdentifiers.IsKeyboardFocusableProperty;
        public static readonly AutomationProperty IsMultipleViewPatternAvailableProperty = AutomationElementIdentifiers.IsMultipleViewPatternAvailableProperty;
        public static readonly AutomationProperty IsOffscreenProperty = AutomationElementIdentifiers.IsOffscreenProperty;
        public static readonly AutomationProperty IsPasswordProperty = AutomationElementIdentifiers.IsPasswordProperty;
        public static readonly AutomationProperty IsRangeValuePatternAvailableProperty = AutomationElementIdentifiers.IsRangeValuePatternAvailableProperty;
        public static readonly AutomationProperty IsRequiredForFormProperty = AutomationElementIdentifiers.IsRequiredForFormProperty;
        public static readonly AutomationProperty IsScrollItemPatternAvailableProperty = AutomationElementIdentifiers.IsScrollItemPatternAvailableProperty;
        public static readonly AutomationProperty IsScrollPatternAvailableProperty = AutomationElementIdentifiers.IsScrollPatternAvailableProperty;
        public static readonly AutomationProperty IsSelectionItemPatternAvailableProperty = AutomationElementIdentifiers.IsSelectionItemPatternAvailableProperty;
        public static readonly AutomationProperty IsSelectionPatternAvailableProperty = AutomationElementIdentifiers.IsSelectionPatternAvailableProperty;
        public static readonly AutomationProperty IsTableItemPatternAvailableProperty = AutomationElementIdentifiers.IsTableItemPatternAvailableProperty;
        public static readonly AutomationProperty IsTablePatternAvailableProperty = AutomationElementIdentifiers.IsTablePatternAvailableProperty;
        public static readonly AutomationProperty IsTextPatternAvailableProperty = AutomationElementIdentifiers.IsTextPatternAvailableProperty;
        public static readonly AutomationProperty IsTogglePatternAvailableProperty = AutomationElementIdentifiers.IsTogglePatternAvailableProperty;
        public static readonly AutomationProperty IsTransformPatternAvailableProperty = AutomationElementIdentifiers.IsTransformPatternAvailableProperty;
        public static readonly AutomationProperty IsValuePatternAvailableProperty = AutomationElementIdentifiers.IsValuePatternAvailableProperty;
        public static readonly AutomationProperty IsWindowPatternAvailableProperty = AutomationElementIdentifiers.IsWindowPatternAvailableProperty;
        public static readonly AutomationProperty ItemStatusProperty = AutomationElementIdentifiers.ItemStatusProperty;
        public static readonly AutomationProperty ItemTypeProperty = AutomationElementIdentifiers.ItemTypeProperty;
        public static readonly AutomationProperty LabeledByProperty = AutomationElementIdentifiers.LabeledByProperty;
        public static readonly AutomationProperty LocalizedControlTypeProperty = AutomationElementIdentifiers.LocalizedControlTypeProperty;
        public static readonly AutomationProperty NameProperty = AutomationElementIdentifiers.NameProperty;
        public static readonly AutomationProperty NativeWindowHandleProperty = AutomationElementIdentifiers.NativeWindowHandleProperty;
        public static readonly AutomationProperty OrientationProperty = AutomationElementIdentifiers.OrientationProperty;
        public static readonly AutomationProperty ProcessIdProperty = AutomationElementIdentifiers.ProcessIdProperty;
        public static readonly AutomationProperty RuntimeIdProperty = AutomationElementIdentifiers.RuntimeIdProperty;
        public static readonly AutomationEvent AsyncContentLoadedEvent = AutomationElementIdentifiers.AsyncContentLoadedEvent;
        public static readonly AutomationEvent AutomationFocusChangedEvent = AutomationElementIdentifiers.AutomationFocusChangedEvent;
        public static readonly AutomationEvent AutomationPropertyChangedEvent = AutomationElementIdentifiers.AutomationPropertyChangedEvent;
        public static readonly AutomationEvent LayoutInvalidatedEvent = AutomationElementIdentifiers.LayoutInvalidatedEvent;
        public static readonly AutomationEvent MenuOpenedEvent = AutomationElementIdentifiers.MenuOpenedEvent;
        public static readonly AutomationEvent MenuClosedEvent = AutomationElementIdentifiers.MenuClosedEvent;
        public static readonly AutomationEvent StructureChangedEvent = AutomationElementIdentifiers.StructureChangedEvent;
        public static readonly AutomationEvent ToolTipClosedEvent = AutomationElementIdentifiers.ToolTipClosedEvent;
        public static readonly AutomationEvent ToolTipOpenedEvent = AutomationElementIdentifiers.ToolTipOpenedEvent;
        public static readonly AutomationProperty IsLegacyPatternAvailableProperty = new AutomationProperty(id: 30090, programmaticName: "AutomationElementIdentifiers.IsLegacyIAccessiblePatternAvailableProperty");
        public static readonly AutomationProperty IsVirtualizedItemPatternAvailableProperty = new AutomationProperty(id: 30109, programmaticName: "AutomationElementIdentifiers.IsVirtualizedItemPatternAvailableProperty");
        public static readonly AutomationProperty IsSynchronizedInputPatternAvailableProperty = new AutomationProperty(id: 30110, programmaticName: "AutomationElementIdentifiers.IsSynchronizedInputPatternAvailableProperty");
        public static readonly AutomationProperty IsItemContainerPatternAvailableProperty = new AutomationProperty(id: 30108, programmaticName: "AutomationElementIdentifiers.IsItemContainerPatternAvailableProperty");
        public static readonly AutomationProperty AriaRoleProperty = new AutomationProperty(id: 30101, programmaticName: "AutomationElementIdentifiers.AriaRoleProperty");
        public static readonly AutomationProperty AriaPropertiesProperty = new AutomationProperty(id: 30102, programmaticName: "AutomationElementIdentifiers.AriaPropertiesProperty");
        public static readonly AutomationProperty IsDataValidForFormProperty = new AutomationProperty(id: 30103, programmaticName: "AutomationElementIdentifiers.IsDataValidForFormProperty");
        public static readonly AutomationProperty ControllerForProperty = new AutomationProperty(id: 30104, programmaticName: "AutomationElementIdentifiers.ControllerForProperty");
        public static readonly AutomationProperty DescribedByProperty = new AutomationProperty(id: 30105, programmaticName: "AutomationElementIdentifiers.DescribedByProperty");
        public static readonly AutomationProperty FlowsFromProperty = new AutomationProperty(id: 30148, programmaticName: "AutomationElementIdentifiers.FlowsFromProperty");
        public static readonly AutomationProperty FlowsToProperty = new AutomationProperty(id: 30106, programmaticName: "AutomationElementIdentifiers.FlowsToProperty");
        public static readonly AutomationProperty ProviderDescriptionProperty = new AutomationProperty(id: 30107, programmaticName: "AutomationElementIdentifiers.ProviderDescriptionProperty");
        public static readonly AutomationProperty SearchVirtualItemsProperty = new AutomationProperty(id: 0, programmaticName: "AutomationElementIdentifiers.SearchVirtualItemsProperty");
        public static readonly AutomationProperty OptimizeForVisualContentProperty = new AutomationProperty(id: 30111, programmaticName: "AutomationElementIdentifiers.OptimizeForVisualContentProperty");
        public static readonly AutomationProperty IsObjectModelPatternAvailableProperty = new AutomationProperty(id: 30112, programmaticName: "AutomationElementIdentifiers.IsObjectModelPatternAvailableProperty");
        public static readonly AutomationProperty IsAnnotationPatternAvailableProperty = new AutomationProperty(id: 30118, programmaticName: "AutomationElementIdentifiers.IsAnnotationPatternAvailableProperty");
        public static readonly AutomationProperty IsTextPattern2AvailableProperty = new AutomationProperty(id: 30119, programmaticName: "AutomationElementIdentifiers.IsTextPattern2AvailableProperty");
        public static readonly AutomationProperty IsStylesPatternAvailableProperty = new AutomationProperty(id: 30127, programmaticName: "AutomationElementIdentifiers.IsStylesPatternAvailableProperty");
        public static readonly AutomationProperty IsSpreadsheetPatternAvailableProperty = new AutomationProperty(id: 30128, programmaticName: "AutomationElementIdentifiers.IsSpreadsheetPatternAvailableProperty");
        public static readonly AutomationProperty IsSpreadsheetItemPatternAvailableProperty = new AutomationProperty(id: 30132, programmaticName: "AutomationElementIdentifiers.IsSpreadsheetItemPatternAvailableProperty");
        public static readonly AutomationProperty IsTransformPattern2AvailableProperty = new AutomationProperty(id: 30134, programmaticName: "AutomationElementIdentifiers.IsTransformPattern2AvailableProperty");
        public static readonly AutomationProperty LiveSettingProperty = new AutomationProperty(id: 30135, programmaticName: "AutomationElementIdentifiers.LiveSettingProperty");
        public static readonly AutomationProperty IsTextChildPatternAvailableProperty = new AutomationProperty(id: 30136, programmaticName: "AutomationElementIdentifiers.IsTextChildPatternAvailableProperty");
        public static readonly AutomationProperty IsDragPatternAvailableProperty = new AutomationProperty(id: 30137, programmaticName: "AutomationElementIdentifiers.IsDragPatternAvailableProperty");
        public static readonly AutomationProperty IsDropTargetPatternAvailableProperty = new AutomationProperty(id: 30141, programmaticName: "AutomationElementIdentifiers.IsDropTargetPatternAvailableProperty");
        public static readonly AutomationProperty IsTextEditPatternAvailableProperty = new AutomationProperty(id: 30149, programmaticName: "AutomationElementIdentifiers.IsTextEditPatternAvailableProperty");
        public static readonly AutomationProperty IsPeripheralProperty = new AutomationProperty(id: 30150, programmaticName: "AutomationElementIdentifiers.IsPeripheralProperty");
        public static readonly AutomationProperty IsCustomNavigationPatternAvailableProperty = new AutomationProperty(id: 30151, programmaticName: "AutomationElementIdentifiers.IsCustomNavigationPatternAvailableProperty");
        public static readonly AutomationProperty PositionInSetProperty = new AutomationProperty(id: 30152, programmaticName: "AutomationElementIdentifiers.PositionInSetProperty");
        public static readonly AutomationProperty SizeOfSetProperty = new AutomationProperty(id: 30153, programmaticName: "AutomationElementIdentifiers.SizeOfSetProperty");
        public static readonly AutomationProperty LevelProperty = new AutomationProperty(id: 30154, programmaticName: "AutomationElementIdentifiers.LevelProperty");
        public static readonly AutomationProperty AnnotationTypesProperty = new AutomationProperty(id: 30155, programmaticName: "AutomationElementIdentifiers.AnnotationTypesProperty");
        public static readonly AutomationProperty AnnotationObjectsProperty = new AutomationProperty(id: 30156, programmaticName: "AutomationElementIdentifiers.AnnotationObjectsProperty");
        public static readonly AutomationProperty LandmarkTypeProperty = new AutomationProperty(id: 30157, programmaticName: "AutomationElementIdentifiers.LandmarkTypeProperty");
        public static readonly AutomationProperty LocalizedLandmarkTypeProperty = new AutomationProperty(id: 30158, programmaticName: "AutomationElementIdentifiers.LocalizedLandmarkTypeProperty");
        public static readonly object NotSupported;

        public AutomationElement(IUIAutomationElement autoElement) {
            Validate.ArgumentNotNull(parameter: autoElement, parameterName: nameof(autoElement));
            IUIAutomationElement = autoElement;
        }

        public static AutomationElement RootElement {
            get { return new AutomationElement(autoElement: Automation.AutomationClass.GetRootElementBuildCache(cacheRequest: DefaultCacheRequest.IUIAutomationCacheRequest)); }
        }

        public static AutomationElement FocusedElement {
            get { return new AutomationElement(autoElement: Automation.AutomationClass.GetFocusedElementBuildCache(cacheRequest: DefaultCacheRequest.IUIAutomationCacheRequest)); }
        }

        public AutomationElementInformation Cached {
            get { return new AutomationElementInformation(el: this, useCache: true); }
        }

        public AutomationElementInformation Current {
            get { return new AutomationElementInformation(el: this, useCache: false); }
        }

        internal IUIAutomationElement IUIAutomationElement { get; }

        internal static CacheRequest DefaultCacheRequest { get; } = new CacheRequest();

        public override bool Equals(object obj) {
            var el2 = obj as AutomationElement;
            return obj != null && !(el2 == null) && Automation.Compare(el1: this, el2: el2);
        }

        public override int GetHashCode() {
            var runtimeId = GetRuntimeId();
            var num = 0;
            if (runtimeId == null)
                throw new InvalidOperationException();
            for (var index = 0; index < runtimeId.Length; ++index)
                num = (num * 2) ^ runtimeId[index];
            return num;
        }

        public static bool operator ==(AutomationElement left, AutomationElement right) {
            if ((object) left == null)
                return (object) right == null;
            return (object) right == null ? (object) left == null : left.Equals(obj: right);
        }

        public static bool operator !=(AutomationElement left, AutomationElement right) {
            return !(left == right);
        }

        public int[] GetRuntimeId() {
            return IUIAutomationElement.GetRuntimeId().ToTypedArray<int>();
        }

        public static AutomationElement FromPoint(Point pt) {
            return new AutomationElement(autoElement: Automation.AutomationClass.ElementFromPointBuildCache(pt: new tagPOINT {
                x = (int) pt.X,
                y = (int) pt.Y
            }, cacheRequest: DefaultCacheRequest.IUIAutomationCacheRequest));
        }

        public static AutomationElement FromHandle(IntPtr hwnd) {
            return new AutomationElement(autoElement: Automation.AutomationClass.ElementFromHandleBuildCache(hwnd: hwnd, cacheRequest: DefaultCacheRequest.IUIAutomationCacheRequest));
        }

        public object GetCurrentPropertyValue(AutomationProperty property) {
            return GetCurrentPropertyValue(property: property, ignoreDefaultValue: false);
        }

        public object GetCurrentPropertyValue(AutomationProperty property, bool ignoreDefaultValue) {
            Validate.ArgumentNotNull(parameter: property, parameterName: nameof(property));
            var propertyValueVariant = new Variant();
            try {
                propertyValueVariant = IUIAutomationElement.GetCurrentPropertyValueEx(propertyId: property.Id, ignoreDefaultValue: Convert.ToInt32(value: ignoreDefaultValue));
            } catch (COMException ex) {
            }

            return UiaConvert.ConvertPropertyValue(property: property, propertyValueVariant: propertyValueVariant);
        }

        public object GetCurrentPattern(AutomationPattern pattern) {
            Validate.ArgumentNotNull(parameter: pattern, parameterName: nameof(pattern));
            return pattern.Wrap(element: this, pattern: IUIAutomationElement.GetCurrentPattern(patternId: pattern.Id));
        }

        public bool TryGetCurrentPattern(AutomationPattern pattern, out object patternObject) {
            Validate.ArgumentNotNull(parameter: pattern, parameterName: nameof(pattern));
            var currentPattern = IUIAutomationElement.GetCurrentPattern(patternId: pattern.Id);
            if (currentPattern == null) {
                patternObject = null;
                return false;
            }

            patternObject = pattern.Wrap(element: this, pattern: currentPattern);
            return true;
        }

        public object GetCachedPropertyValue(AutomationProperty property) {
            return GetCachedPropertyValue(property: property, ignoreDefaultValue: false);
        }

        public object GetCachedPropertyValue(AutomationProperty property, bool ignoreDefaultValue) {
            Validate.ArgumentNotNull(parameter: property, parameterName: nameof(property));
            var cachedPropertyValueEx = IUIAutomationElement.GetCachedPropertyValueEx(propertyId: property.Id, ignoreDefaultValue: Convert.ToInt32(value: ignoreDefaultValue));
            return UiaConvert.ConvertPropertyValue(property: property, propertyValueVariant: cachedPropertyValueEx);
        }

        public object GetCachedPattern(AutomationPattern pattern) {
            object patternObject;
            if (!TryGetCachedPattern(pattern: pattern, patternObject: out patternObject))
                throw new InvalidOperationException(message: "UnsupportedPattern");
            return patternObject;
        }

        public bool TryGetCachedPattern(AutomationPattern pattern, out object patternObject) {
            patternObject = null;
            throw new NotImplementedException();
        }

        public AutomationElement GetUpdatedCache(CacheRequest request) {
            try {
                return new AutomationElement(autoElement: IUIAutomationElement.BuildUpdatedCache(cacheRequest: request.IUIAutomationCacheRequest));
            } catch (COMException ex) {
                Exception exception = null;
                if (UiaConvert.ConvertException(e: ex, newException: out exception))
                    throw exception;
                throw;
            }
        }

        public AutomationElement FindFirst(TreeScope scope, Condition condition) {
            Validate.ArgumentNotNull(parameter: condition, parameterName: nameof(condition));
            var firstBuildCache = IUIAutomationElement.FindFirstBuildCache(scope: UiaConvert.Convert(treeScope: scope), condition: condition.IUIAutomationCondition, cacheRequest: DefaultCacheRequest.IUIAutomationCacheRequest);
            return firstBuildCache != null ? new AutomationElement(autoElement: firstBuildCache) : null;
        }

        public AutomationElement FindFirst(
            TreeScope scope,
            Condition condition,
            CacheRequest cr) {
            Validate.ArgumentNotNull(parameter: condition, parameterName: nameof(condition));
            Validate.ArgumentNotNull(parameter: cr, parameterName: nameof(cr));
            var firstBuildCache = IUIAutomationElement.FindFirstBuildCache(scope: UiaConvert.Convert(treeScope: scope), condition: condition.IUIAutomationCondition, cacheRequest: cr.IUIAutomationCacheRequest);
            return firstBuildCache != null ? new AutomationElement(autoElement: firstBuildCache) : null;
        }

        public AutomationElementCollection FindAll(
            TreeScope scope,
            Condition condition) {
            Validate.ArgumentNotNull(parameter: condition, parameterName: nameof(condition));
            var allBuildCache = IUIAutomationElement.FindAllBuildCache(scope: UiaConvert.Convert(treeScope: scope), condition: condition.IUIAutomationCondition, cacheRequest: DefaultCacheRequest.IUIAutomationCacheRequest);
            return allBuildCache != null ? new AutomationElementCollection(elementArray: allBuildCache) : null;
        }

        public AutomationElementCollection FindAll(
            TreeScope scope,
            Condition condition,
            CacheRequest cr) {
            Validate.ArgumentNotNull(parameter: condition, parameterName: nameof(condition));
            Validate.ArgumentNotNull(parameter: cr, parameterName: nameof(cr));
            var allBuildCache = IUIAutomationElement.FindAllBuildCache(scope: UiaConvert.Convert(treeScope: scope), condition: condition.IUIAutomationCondition, cacheRequest: cr.IUIAutomationCacheRequest);
            return allBuildCache != null ? new AutomationElementCollection(elementArray: allBuildCache) : null;
        }

        public AutomationProperty[] GetSupportedProperties() {
            int[] propertyIds;
            Automation.AutomationClass.PollForPotentialSupportedProperties(pElement: IUIAutomationElement, propertyIds: out propertyIds, propertyNames: out var _);
            var automationPropertyList = new List<AutomationProperty>();
            for (var index = 0; index < propertyIds.Length; ++index)
                if (propertyIds[index] != 0)
                    try {
                        var automationProperty = AutomationProperty.LookupById(id: propertyIds[index]);
                        automationPropertyList.Add(item: automationProperty);
                    } catch (KeyNotFoundException ex) {
                    }

            return automationPropertyList.ToArray();
        }

        public AutomationPattern[] GetSupportedPatterns() {
            int[] patternIds;
            Automation.AutomationClass.PollForPotentialSupportedPatterns(pElement: IUIAutomationElement, patternIds: out patternIds, patternNames: out var _);
            var automationPatternArray = new AutomationPattern[patternIds.Length];
            for (var index = 0; index < patternIds.Length; ++index)
                if (patternIds[index] != 0)
                    try {
                        var automationPattern = AutomationPattern.LookupById(id: patternIds[index]);
                        automationPatternArray[index] = automationPattern;
                    } catch (KeyNotFoundException ex) {
                    }

            return automationPatternArray;
        }

        public void SetFocus() {
            IUIAutomationElement.SetFocus();
        }

        public bool TryGetClickablePoint(out Point pt) {
            pt = new Point();
            tagPOINT clickable;
            if (IUIAutomationElement.GetClickablePoint(clickable: out clickable) == 0)
                return false;
            pt.X = clickable.x;
            pt.Y = clickable.y;
            return true;
        }

        public Point GetClickablePoint() {
            Point pt;
            if (!TryGetClickablePoint(pt: out pt))
                throw new NoClickablePointException(message: "Clickable point not found for the UI element");
            return pt;
        }

        internal object GetPatternPropertyValue(AutomationProperty property, bool useCache) {
            return useCache ? GetCachedPropertyValue(property: property) : GetCurrentPropertyValue(property: property);
        }

        public struct AutomationElementInformation {
            readonly AutomationElement _el;
            readonly bool _useCache;

            internal AutomationElementInformation(AutomationElement el, bool useCache) {
                this._el = el;
                this._useCache = useCache;
            }

            public ControlType ControlType {
                get { return (ControlType) this._el.GetPatternPropertyValue(property: ControlTypeProperty, useCache: this._useCache); }
            }

            public string LocalizedControlType {
                get { return (string) this._el.GetPatternPropertyValue(property: LocalizedControlTypeProperty, useCache: this._useCache); }
            }

            public string Name {
                get { return (string) this._el.GetPatternPropertyValue(property: NameProperty, useCache: this._useCache); }
            }

            public string AcceleratorKey {
                get { return (string) this._el.GetPatternPropertyValue(property: AcceleratorKeyProperty, useCache: this._useCache); }
            }

            public string AccessKey {
                get { return (string) this._el.GetPatternPropertyValue(property: AccessKeyProperty, useCache: this._useCache); }
            }

            public bool HasKeyboardFocus {
                get { return (bool) this._el.GetPatternPropertyValue(property: HasKeyboardFocusProperty, useCache: this._useCache); }
            }

            public bool IsKeyboardFocusable {
                get { return (bool) this._el.GetPatternPropertyValue(property: IsKeyboardFocusableProperty, useCache: this._useCache); }
            }

            public bool IsEnabled {
                get { return (bool) this._el.GetPatternPropertyValue(property: IsEnabledProperty, useCache: this._useCache); }
            }

            public Rect BoundingRectangle {
                get { return (Rect) this._el.GetPatternPropertyValue(property: BoundingRectangleProperty, useCache: this._useCache); }
            }

            public string HelpText {
                get { return (string) this._el.GetPatternPropertyValue(property: HelpTextProperty, useCache: this._useCache); }
            }

            public bool IsControlElement {
                get { return (bool) this._el.GetPatternPropertyValue(property: IsControlElementProperty, useCache: this._useCache); }
            }

            public bool IsContentElement {
                get { return (bool) this._el.GetPatternPropertyValue(property: IsContentElementProperty, useCache: this._useCache); }
            }

            public AutomationElement LabeledBy {
                get { return (AutomationElement) this._el.GetPatternPropertyValue(property: LabeledByProperty, useCache: this._useCache); }
            }

            public string AutomationId {
                get { return (string) this._el.GetPatternPropertyValue(property: AutomationIdProperty, useCache: this._useCache); }
            }

            public string ItemType {
                get { return (string) this._el.GetPatternPropertyValue(property: ItemTypeProperty, useCache: this._useCache); }
            }

            public bool IsPassword {
                get { return (bool) this._el.GetPatternPropertyValue(property: IsPasswordProperty, useCache: this._useCache); }
            }

            public string ClassName {
                get { return (string) this._el.GetPatternPropertyValue(property: ClassNameProperty, useCache: this._useCache); }
            }

            public int NativeWindowHandle {
                get { return (int) this._el.GetPatternPropertyValue(property: NativeWindowHandleProperty, useCache: this._useCache); }
            }

            public int ProcessId {
                get { return (int) this._el.GetPatternPropertyValue(property: ProcessIdProperty, useCache: this._useCache); }
            }

            public bool IsOffscreen {
                get { return (bool) this._el.GetPatternPropertyValue(property: IsOffscreenProperty, useCache: this._useCache); }
            }

            public OrientationType Orientation {
                get { return (OrientationType) this._el.GetPatternPropertyValue(property: OrientationProperty, useCache: this._useCache); }
            }

            public string FrameworkId {
                get { return (string) this._el.GetPatternPropertyValue(property: FrameworkIdProperty, useCache: this._useCache); }
            }

            public bool IsRequiredForForm {
                get { return (bool) this._el.GetPatternPropertyValue(property: IsRequiredForFormProperty, useCache: this._useCache); }
            }

            public string ItemStatus {
                get { return (string) this._el.GetPatternPropertyValue(property: ItemStatusProperty, useCache: this._useCache); }
            }
        }
    }
}