// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.UIProperty
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    public class UIProperty {
        static Dictionary<string, UIProperty> _propertyTable;
        static Dictionary<AutomationProperty, UIProperty> _automationPropertyTable;
        static readonly object lockObject = new object();

        static UIProperty() {
            Initialization();
        }

        UIProperty(string name, AutomationProperty property, Type allowedType) {
            Name = name;
            Property = property;
            Type = allowedType;
        }

        public static IEnumerable<UIProperty> AllProperties {
            get { return _propertyTable.Values; }
        }

        public string Name { get; }

        internal AutomationProperty Property { get; }

        internal Type Type { get; }

        static UIProperty Create(string name, AutomationProperty property) {
            return Create(name: name, property: property, allowedType: typeof(object));
        }

        static UIProperty Create(
            string name,
            AutomationProperty property,
            Type allowedType) {
            UIProperty uiProperty;
            if (!_propertyTable.TryGetValue(key: name, value: out uiProperty)) {
                uiProperty = new UIProperty(name: name, property: property, allowedType: allowedType);
                _propertyTable.Add(key: name, value: uiProperty);
            }

            return uiProperty;
        }

        public static UIProperty Get(string name) {
            Validate.StringNeitherNullNorEmpty(parameter: name, parameterName: nameof(name));
            try {
                return _propertyTable[key: name];
            } catch (KeyNotFoundException ex) {
                throw new UIQueryException(message: StringResource.Get(id: "PropertyNotFound_1", (object) name));
            }
        }

        public static bool Exists(string name) {
            Validate.StringNeitherNullNorEmpty(parameter: name, parameterName: nameof(name));
            return _propertyTable.ContainsKey(key: name);
        }

        internal static UIProperty Get(AutomationProperty property) {
            lock (lockObject) {
                if (_automationPropertyTable == null) {
                    _automationPropertyTable = new Dictionary<AutomationProperty, UIProperty>(capacity: _propertyTable.Count);
                    foreach (var uiProperty in _propertyTable.Values)
                        _automationPropertyTable.Add(key: uiProperty.Property, value: uiProperty);
                }
            }

            try {
                return _automationPropertyTable[key: property];
            } catch (KeyNotFoundException ex) {
                throw new UIQueryException(message: StringResource.Get(id: "PropertyNotFound_1", (object) property.ToString()));
            }
        }

        public static void Initialization() {
            lock (lockObject) {
                if (_propertyTable != null)
                    return;
                _propertyTable = new Dictionary<string, UIProperty>();
                Create(name: "AcceleratorKey", property: AutomationElement.AcceleratorKeyProperty);
                Create(name: "AccessKey", property: AutomationElement.AccessKeyProperty);
                Create(name: "AutomationId", property: AutomationElement.AutomationIdProperty, allowedType: typeof(string));
                Create(name: "BoundingRectangle", property: AutomationElement.BoundingRectangleProperty, allowedType: typeof(RectangleI));
                Create(name: "ClassName", property: AutomationElement.ClassNameProperty, allowedType: typeof(string));
                Create(name: "ClickablePoint", property: AutomationElement.ClickablePointProperty);
                Create(name: "ControlType", property: AutomationElement.ControlTypeProperty, allowedType: typeof(ControlType));
                Create(name: "Culture", property: AutomationElement.CultureProperty);
                Create(name: "FrameworkId", property: AutomationElement.FrameworkIdProperty, allowedType: typeof(string));
                Create(name: "HasKeyboardFocus", property: AutomationElement.HasKeyboardFocusProperty, allowedType: typeof(bool));
                Create(name: "HelpText", property: AutomationElement.HelpTextProperty);
                Create(name: "IsContentElement", property: AutomationElement.IsContentElementProperty, allowedType: typeof(bool));
                Create(name: "IsControlElement", property: AutomationElement.IsControlElementProperty, allowedType: typeof(bool));
                Create(name: "IsCustomNavigationPatternAvailable", property: AutomationElement.IsCustomNavigationPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsDockPatternAvailable", property: AutomationElement.IsDockPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsEnabled", property: AutomationElement.IsEnabledProperty, allowedType: typeof(bool));
                Create(name: "IsExpandCollapsePatternAvailable", property: AutomationElement.IsExpandCollapsePatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsGridItemPatternAvailable", property: AutomationElement.IsGridItemPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsGridPatternAvailable", property: AutomationElement.IsGridPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsInvokePatternAvailable", property: AutomationElement.IsInvokePatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsKeyboardFocusable", property: AutomationElement.IsKeyboardFocusableProperty, allowedType: typeof(bool));
                Create(name: "IsMultipleViewPatternAvailable", property: AutomationElement.IsMultipleViewPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsOffscreen", property: AutomationElement.IsOffscreenProperty, allowedType: typeof(bool));
                Create(name: "IsPassword", property: AutomationElement.IsPasswordProperty, allowedType: typeof(bool));
                Create(name: "IsRangeValuePatternAvailable", property: AutomationElement.IsRangeValuePatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsRequiredForForm", property: AutomationElement.IsRequiredForFormProperty, allowedType: typeof(bool));
                Create(name: "IsScrollItemPatternAvailable", property: AutomationElement.IsScrollItemPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsScrollPatternAvailable", property: AutomationElement.IsScrollPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsSelectionItemPatternAvailable", property: AutomationElement.IsSelectionItemPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsSelectionPatternAvailable", property: AutomationElement.IsSelectionPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsTableItemPatternAvailable", property: AutomationElement.IsTableItemPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsTablePatternAvailable", property: AutomationElement.IsTablePatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsTextPatternAvailable", property: AutomationElement.IsTextPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsTogglePatternAvailable", property: AutomationElement.IsTogglePatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsTransformPatternAvailable", property: AutomationElement.IsTransformPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsValuePatternAvailable", property: AutomationElement.IsValuePatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsWindowPatternAvailable", property: AutomationElement.IsWindowPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "ItemType", property: AutomationElement.ItemTypeProperty, allowedType: typeof(string));
                Create(name: "ItemStatus", property: AutomationElement.ItemStatusProperty, allowedType: typeof(string));
                Create(name: "LabeledBy", property: AutomationElement.LabeledByProperty);
                Create(name: "LocalizedControlType", property: AutomationElement.LocalizedControlTypeProperty);
                Create(name: "Name", property: AutomationElement.NameProperty);
                Create(name: "NativeWindowHandle", property: AutomationElement.NativeWindowHandleProperty);
                Create(name: "Orientation", property: AutomationElement.OrientationProperty, allowedType: typeof(OrientationType));
                Create(name: "ProcessId", property: AutomationElement.ProcessIdProperty, allowedType: typeof(int));
                Create(name: "RuntimeId", property: AutomationElement.RuntimeIdProperty);
                Create(name: "Value.Value", property: ValuePattern.ValueProperty, allowedType: typeof(string));
                Create(name: "Value.IsReadOnly", property: ValuePattern.IsReadOnlyProperty, allowedType: typeof(bool));
                Create(name: "RangeValue.IsReadOnly", property: RangeValuePattern.IsReadOnlyProperty, allowedType: typeof(bool));
                Create(name: "RangeValue.LargeChange", property: RangeValuePattern.LargeChangeProperty, allowedType: typeof(double));
                Create(name: "RangeValue.Maximum", property: RangeValuePattern.MaximumProperty, allowedType: typeof(double));
                Create(name: "RangeValue.Minimum", property: RangeValuePattern.MinimumProperty, allowedType: typeof(double));
                Create(name: "RangeValue.SmallChange", property: RangeValuePattern.SmallChangeProperty, allowedType: typeof(double));
                Create(name: "RangeValue.Value", property: RangeValuePattern.ValueProperty, allowedType: typeof(double));
                Create(name: "Scroll.HorizontallyScrollable", property: ScrollPattern.HorizontallyScrollableProperty, allowedType: typeof(bool));
                Create(name: "Scroll.HorizontalScrollPercent", property: ScrollPattern.HorizontalScrollPercentProperty, allowedType: typeof(double));
                Create(name: "Scroll.HorizontalViewSize", property: ScrollPattern.HorizontalViewSizeProperty, allowedType: typeof(double));
                Create(name: "Scroll.VerticallyScrollable", property: ScrollPattern.VerticallyScrollableProperty, allowedType: typeof(bool));
                Create(name: "Scroll.VerticalScrollPercent", property: ScrollPattern.VerticalScrollPercentProperty, allowedType: typeof(double));
                Create(name: "Scroll.VerticalViewSize", property: ScrollPattern.VerticalViewSizeProperty, allowedType: typeof(double));
                Create(name: "Selection.CanSelectMultiple", property: SelectionPattern.CanSelectMultipleProperty, allowedType: typeof(bool));
                Create(name: "Selection.IsSelectionRequired", property: SelectionPattern.IsSelectionRequiredProperty, allowedType: typeof(bool));
                Create(name: "Selection.Selection", property: SelectionPattern.SelectionProperty);
                Create(name: "Grid.ColumnCount", property: GridPattern.ColumnCountProperty, allowedType: typeof(int));
                Create(name: "Grid.RowCount", property: GridPattern.RowCountProperty, allowedType: typeof(int));
                Create(name: "GridItem.Column", property: GridItemPattern.ColumnProperty);
                Create(name: "GridItem.ColumnSpan", property: GridItemPattern.ColumnSpanProperty);
                Create(name: "GridItem.ContainingGrid", property: GridItemPattern.ContainingGridProperty);
                Create(name: "GridItem.Row", property: GridItemPattern.RowProperty);
                Create(name: "GridItem.RowSpan", property: GridItemPattern.RowSpanProperty);
                Create(name: "Dock.DockPosition", property: DockPattern.DockPositionProperty);
                Create(name: "ExpandCollapse.ExpandCollapseState", property: ExpandCollapsePattern.ExpandCollapseStateProperty);
                Create(name: "MultipleView.CurrentView", property: MultipleViewPattern.CurrentViewProperty, allowedType: typeof(int));
                Create(name: "MultipleView.SupportedViews", property: MultipleViewPattern.SupportedViewsProperty);
                Create(name: "Window.CanMaximize", property: WindowPattern.CanMaximizeProperty, allowedType: typeof(bool));
                Create(name: "Window.CanMinimize", property: WindowPattern.CanMinimizeProperty, allowedType: typeof(bool));
                Create(name: "Window.WindowInteractionState", property: WindowPattern.WindowInteractionStateProperty, allowedType: typeof(WindowInteractionState));
                Create(name: "Window.IsModal", property: WindowPattern.IsModalProperty, allowedType: typeof(bool));
                Create(name: "Window.IsTopmost", property: WindowPattern.IsTopmostProperty, allowedType: typeof(bool));
                Create(name: "Window.WindowVisualState", property: WindowPattern.WindowVisualStateProperty, allowedType: typeof(WindowVisualState));
                Create(name: "SelectionItem.IsSelected", property: SelectionItemPattern.IsSelectedProperty, allowedType: typeof(bool));
                Create(name: "SelectionItem.SelectionContainer", property: SelectionItemPattern.SelectionContainerProperty);
                Create(name: "Table.RowHeaders", property: TablePattern.RowHeadersProperty);
                Create(name: "Table.ColumnHeaders", property: TablePattern.ColumnHeadersProperty);
                Create(name: "Table.RowOrColumnMajor", property: TablePattern.RowOrColumnMajorProperty, allowedType: typeof(RowOrColumnMajor));
                Create(name: "TableItem.RowHeaderItems", property: TableItemPattern.RowHeaderItemsProperty);
                Create(name: "TableItem.ColumnHeaderItems", property: TableItemPattern.ColumnHeaderItemsProperty);
                Create(name: "Toggle.ToggleState", property: TogglePattern.ToggleStateProperty, allowedType: typeof(ToggleState));
                Create(name: "Transform.CanMove", property: TransformPattern.CanMoveProperty, allowedType: typeof(bool));
                Create(name: "Transform.CanResize", property: TransformPattern.CanResizeProperty, allowedType: typeof(bool));
                Create(name: "Transform.CanRotate", property: TransformPattern.CanRotateProperty, allowedType: typeof(bool));
                Create(name: "IsLegacyPatternAvailable", property: AutomationElement.IsLegacyPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsPeripheral", property: AutomationElement.IsPeripheralProperty, allowedType: typeof(bool));
                Create(name: "IsTextEditPatternAvailable", property: AutomationElement.IsTextEditPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsVirtualizedItemPatternAvailable", property: AutomationElement.IsVirtualizedItemPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsSynchronizedInputPatternAvailable", property: AutomationElement.IsSynchronizedInputPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsItemContainerPatternAvailable", property: AutomationElement.IsItemContainerPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "AnnotationObjects", property: AutomationElement.AnnotationObjectsProperty);
                Create(name: "AnnotationTypes", property: AutomationElement.AnnotationTypesProperty);
                Create(name: "AriaRole", property: AutomationElement.AriaRoleProperty);
                Create(name: "AriaProperties", property: AutomationElement.AriaPropertiesProperty);
                Create(name: "IsDataValidForForm", property: AutomationElement.IsDataValidForFormProperty);
                Create(name: "ControllerFor", property: AutomationElement.ControllerForProperty);
                Create(name: "DescribedBy", property: AutomationElement.DescribedByProperty);
                Create(name: "FlowsFrom", property: AutomationElement.FlowsFromProperty);
                Create(name: "FlowsTo", property: AutomationElement.FlowsToProperty);
                Create(name: "Level", property: AutomationElement.LevelProperty, allowedType: typeof(int));
                Create(name: "PositionInSet", property: AutomationElement.PositionInSetProperty, allowedType: typeof(int));
                Create(name: "ProviderDescription", property: AutomationElement.ProviderDescriptionProperty);
                Create(name: "SizeOfSet", property: AutomationElement.SizeOfSetProperty, allowedType: typeof(int));
                Create(name: "LandmarkType", property: AutomationElement.LandmarkTypeProperty, allowedType: typeof(LandmarkType));
                Create(name: "LocalizedLandmarkType", property: AutomationElement.LocalizedLandmarkTypeProperty, allowedType: typeof(string));
                Create(name: "LegacyChildId", property: LegacyIAccessiblePattern.ChildIdProperty);
                Create(name: "LegacyDefaultAction", property: LegacyIAccessiblePattern.DefaultActionProperty);
                Create(name: "LegacyDescription", property: LegacyIAccessiblePattern.DescriptionProperty);
                Create(name: "LegacyHelp", property: LegacyIAccessiblePattern.HelpProperty);
                Create(name: "LegacyKeyboardShortcut", property: LegacyIAccessiblePattern.KeyboardShortcutProperty);
                Create(name: "LegacyName", property: LegacyIAccessiblePattern.NameProperty);
                Create(name: "LegacyRole", property: LegacyIAccessiblePattern.RoleProperty);
                Create(name: "LegacySelection", property: LegacyIAccessiblePattern.SelectionProperty);
                Create(name: "LegacyState", property: LegacyIAccessiblePattern.StateProperty);
                Create(name: "LegacyValue", property: LegacyIAccessiblePattern.ValueProperty);
                Create(name: "SearchVirtualItems", property: AutomationElement.SearchVirtualItemsProperty, allowedType: typeof(int));
                Create(name: "OptimizeForVisualContent", property: AutomationElement.OptimizeForVisualContentProperty, allowedType: typeof(bool));
                Create(name: "IsObjectModelPatternAvailable", property: AutomationElement.IsObjectModelPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsAnnotationPatternAvailable", property: AutomationElement.IsAnnotationPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsTextPattern2Available", property: AutomationElement.IsTextPattern2AvailableProperty, allowedType: typeof(bool));
                Create(name: "IsStylesPatternAvailable", property: AutomationElement.IsStylesPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsSpreadsheetPatternAvailable", property: AutomationElement.IsSpreadsheetPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsSpreadsheetItemPatternAvailable", property: AutomationElement.IsSpreadsheetItemPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsTransformPattern2Available", property: AutomationElement.IsTransformPattern2AvailableProperty, allowedType: typeof(bool));
                Create(name: "LiveSetting", property: AutomationElement.LiveSettingProperty);
                Create(name: "IsTextChildPatternAvailable", property: AutomationElement.IsTextChildPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsDragPatternAvailable", property: AutomationElement.IsDragPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "IsDropTargetPatternAvailable", property: AutomationElement.IsDropTargetPatternAvailableProperty, allowedType: typeof(bool));
                Create(name: "Annotation.AnnotationTypeId", property: AnnotationPattern.AnnotationTypeIdProperty);
                Create(name: "Annotation.AnnotationTypeName", property: AnnotationPattern.AnnotationTypeNameProperty);
                Create(name: "Annotation.Author", property: AnnotationPattern.AuthorProperty);
                Create(name: "Annotation.DateTime", property: AnnotationPattern.DateTimeProperty);
                Create(name: "Annotation.Target", property: AnnotationPattern.TargetProperty);
                Create(name: "Drag.IsGrabbed", property: DragPattern.IsGrabbedProperty, allowedType: typeof(bool));
                Create(name: "Drag.DropEffect", property: DragPattern.DropEffectProperty, allowedType: typeof(string));
                Create(name: "Drag.DropEffects", property: DragPattern.DropEffectsProperty);
                Create(name: "Drag.GrabbedItems", property: DragPattern.GrabbedItemsProperty);
                Create(name: "DropTarget.DropTargetEffect", property: DropTargetPattern.DropTargetEffectProperty, allowedType: typeof(string));
                Create(name: "DropTarget.DropTargetEffects", property: DropTargetPattern.DropTargetEffectsProperty);
                Create(name: "SpreadsheetItem.Formula", property: SpreadsheetItemPattern.FormulaProperty, allowedType: typeof(string));
                Create(name: "SpreadsheetItem.AnnotationObjects", property: SpreadsheetItemPattern.AnnotationObjectsProperty);
                Create(name: "SpreadsheetItem.AnnotationTypes", property: SpreadsheetItemPattern.AnnotationTypesProperty);
                Create(name: "Styles.StyleId", property: StylesPattern.StyleIdProperty);
                Create(name: "Styles.StyleName", property: StylesPattern.StyleNameProperty);
                Create(name: "Styles.FillColor", property: StylesPattern.FillColorProperty);
                Create(name: "Styles.FillPatternStyle", property: StylesPattern.FillPatternStyleProperty);
                Create(name: "Styles.Shape", property: StylesPattern.ShapeProperty);
                Create(name: "Styles.FillPatternColor", property: StylesPattern.FillPatternColorProperty);
                Create(name: "Styles.ExtendedProperties", property: StylesPattern.ExtendedPropertiesProperty);
                Create(name: "Transform2.CanZoom", property: TransformPattern2.CanZoomProperty, allowedType: typeof(bool));
                Create(name: "Transform2.ZoomLevel", property: TransformPattern2.ZoomLevelProperty);
                Create(name: "Transform2.ZoomMinimum", property: TransformPattern2.ZoomMinimumProperty);
                Create(name: "Transform2.ZoomMaximum", property: TransformPattern2.ZoomMaximumProperty);
            }
        }

        public override string ToString() {
            return Name;
        }
    }
}