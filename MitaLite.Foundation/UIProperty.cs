// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.UIProperty
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation
{
  public class UIProperty
  {
    private string _name;
    private AutomationProperty _property;
    private Type _allowedType;
    private static Dictionary<string, UIProperty> _propertyTable;
    private static Dictionary<AutomationProperty, UIProperty> _automationPropertyTable;
    private static object lockObject = new object();

    static UIProperty() => UIProperty.Initialization();

    private UIProperty(string name, AutomationProperty property, Type allowedType)
    {
      this._name = name;
      this._property = property;
      this._allowedType = allowedType;
    }

    private static UIProperty Create(string name, AutomationProperty property) => UIProperty.Create(name, property, typeof (object));

    private static UIProperty Create(
      string name,
      AutomationProperty property,
      Type allowedType)
    {
      UIProperty uiProperty;
      if (!UIProperty._propertyTable.TryGetValue(name, out uiProperty))
      {
        uiProperty = new UIProperty(name, property, allowedType);
        UIProperty._propertyTable.Add(name, uiProperty);
      }
      return uiProperty;
    }

    public static UIProperty Get(string name)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.StringNeitherNullNorEmpty(name, nameof (name));
      try
      {
        return UIProperty._propertyTable[name];
      }
      catch (KeyNotFoundException ex)
      {
        throw new UIQueryException(StringResource.Get("PropertyNotFound_1", (object) name));
      }
    }

    public static bool Exists(string name)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.StringNeitherNullNorEmpty(name, nameof (name));
      return UIProperty._propertyTable.ContainsKey(name);
    }

    internal static UIProperty Get(AutomationProperty property)
    {
      lock (UIProperty.lockObject)
      {
        if (UIProperty._automationPropertyTable == null)
        {
          UIProperty._automationPropertyTable = new Dictionary<AutomationProperty, UIProperty>(UIProperty._propertyTable.Count);
          foreach (UIProperty uiProperty in UIProperty._propertyTable.Values)
            UIProperty._automationPropertyTable.Add(uiProperty._property, uiProperty);
        }
      }
      try
      {
        return UIProperty._automationPropertyTable[property];
      }
      catch (KeyNotFoundException ex)
      {
        throw new UIQueryException(StringResource.Get("PropertyNotFound_1", (object) property.ToString()));
      }
    }

    public static void Initialization()
    {
      lock (UIProperty.lockObject)
      {
        if (UIProperty._propertyTable != null)
          return;
        UIProperty._propertyTable = new Dictionary<string, UIProperty>();
        UIProperty.Create("AcceleratorKey", AutomationElement.AcceleratorKeyProperty);
        UIProperty.Create("AccessKey", AutomationElement.AccessKeyProperty);
        UIProperty.Create("AutomationId", AutomationElement.AutomationIdProperty, typeof (string));
        UIProperty.Create("BoundingRectangle", AutomationElement.BoundingRectangleProperty, typeof (RectangleI));
        UIProperty.Create("ClassName", AutomationElement.ClassNameProperty, typeof (string));
        UIProperty.Create("ClickablePoint", AutomationElement.ClickablePointProperty);
        UIProperty.Create("ControlType", AutomationElement.ControlTypeProperty, typeof (ControlType));
        UIProperty.Create("Culture", AutomationElement.CultureProperty);
        UIProperty.Create("FrameworkId", AutomationElement.FrameworkIdProperty, typeof (string));
        UIProperty.Create("HasKeyboardFocus", AutomationElement.HasKeyboardFocusProperty, typeof (bool));
        UIProperty.Create("HelpText", AutomationElement.HelpTextProperty);
        UIProperty.Create("IsContentElement", AutomationElement.IsContentElementProperty, typeof (bool));
        UIProperty.Create("IsControlElement", AutomationElement.IsControlElementProperty, typeof (bool));
        UIProperty.Create("IsCustomNavigationPatternAvailable", AutomationElement.IsCustomNavigationPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsDockPatternAvailable", AutomationElement.IsDockPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsEnabled", AutomationElement.IsEnabledProperty, typeof (bool));
        UIProperty.Create("IsExpandCollapsePatternAvailable", AutomationElement.IsExpandCollapsePatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsGridItemPatternAvailable", AutomationElement.IsGridItemPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsGridPatternAvailable", AutomationElement.IsGridPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsInvokePatternAvailable", AutomationElement.IsInvokePatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsKeyboardFocusable", AutomationElement.IsKeyboardFocusableProperty, typeof (bool));
        UIProperty.Create("IsMultipleViewPatternAvailable", AutomationElement.IsMultipleViewPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsOffscreen", AutomationElement.IsOffscreenProperty, typeof (bool));
        UIProperty.Create("IsPassword", AutomationElement.IsPasswordProperty, typeof (bool));
        UIProperty.Create("IsRangeValuePatternAvailable", AutomationElement.IsRangeValuePatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsRequiredForForm", AutomationElement.IsRequiredForFormProperty, typeof (bool));
        UIProperty.Create("IsScrollItemPatternAvailable", AutomationElement.IsScrollItemPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsScrollPatternAvailable", AutomationElement.IsScrollPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsSelectionItemPatternAvailable", AutomationElement.IsSelectionItemPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsSelectionPatternAvailable", AutomationElement.IsSelectionPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsTableItemPatternAvailable", AutomationElement.IsTableItemPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsTablePatternAvailable", AutomationElement.IsTablePatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsTextPatternAvailable", AutomationElement.IsTextPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsTogglePatternAvailable", AutomationElement.IsTogglePatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsTransformPatternAvailable", AutomationElement.IsTransformPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsValuePatternAvailable", AutomationElement.IsValuePatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsWindowPatternAvailable", AutomationElement.IsWindowPatternAvailableProperty, typeof (bool));
        UIProperty.Create("ItemType", AutomationElement.ItemTypeProperty, typeof (string));
        UIProperty.Create("ItemStatus", AutomationElement.ItemStatusProperty, typeof (string));
        UIProperty.Create("LabeledBy", AutomationElement.LabeledByProperty);
        UIProperty.Create("LocalizedControlType", AutomationElement.LocalizedControlTypeProperty);
        UIProperty.Create("Name", AutomationElement.NameProperty);
        UIProperty.Create("NativeWindowHandle", AutomationElement.NativeWindowHandleProperty);
        UIProperty.Create("Orientation", AutomationElement.OrientationProperty, typeof (OrientationType));
        UIProperty.Create("ProcessId", AutomationElement.ProcessIdProperty, typeof (int));
        UIProperty.Create("RuntimeId", AutomationElement.RuntimeIdProperty);
        UIProperty.Create("Value.Value", ValuePattern.ValueProperty, typeof (string));
        UIProperty.Create("Value.IsReadOnly", ValuePattern.IsReadOnlyProperty, typeof (bool));
        UIProperty.Create("RangeValue.IsReadOnly", RangeValuePattern.IsReadOnlyProperty, typeof (bool));
        UIProperty.Create("RangeValue.LargeChange", RangeValuePattern.LargeChangeProperty, typeof (double));
        UIProperty.Create("RangeValue.Maximum", RangeValuePattern.MaximumProperty, typeof (double));
        UIProperty.Create("RangeValue.Minimum", RangeValuePattern.MinimumProperty, typeof (double));
        UIProperty.Create("RangeValue.SmallChange", RangeValuePattern.SmallChangeProperty, typeof (double));
        UIProperty.Create("RangeValue.Value", RangeValuePattern.ValueProperty, typeof (double));
        UIProperty.Create("Scroll.HorizontallyScrollable", ScrollPattern.HorizontallyScrollableProperty, typeof (bool));
        UIProperty.Create("Scroll.HorizontalScrollPercent", ScrollPattern.HorizontalScrollPercentProperty, typeof (double));
        UIProperty.Create("Scroll.HorizontalViewSize", ScrollPattern.HorizontalViewSizeProperty, typeof (double));
        UIProperty.Create("Scroll.VerticallyScrollable", ScrollPattern.VerticallyScrollableProperty, typeof (bool));
        UIProperty.Create("Scroll.VerticalScrollPercent", ScrollPattern.VerticalScrollPercentProperty, typeof (double));
        UIProperty.Create("Scroll.VerticalViewSize", ScrollPattern.VerticalViewSizeProperty, typeof (double));
        UIProperty.Create("Selection.CanSelectMultiple", SelectionPattern.CanSelectMultipleProperty, typeof (bool));
        UIProperty.Create("Selection.IsSelectionRequired", SelectionPattern.IsSelectionRequiredProperty, typeof (bool));
        UIProperty.Create("Selection.Selection", SelectionPattern.SelectionProperty);
        UIProperty.Create("Grid.ColumnCount", GridPattern.ColumnCountProperty, typeof (int));
        UIProperty.Create("Grid.RowCount", GridPattern.RowCountProperty, typeof (int));
        UIProperty.Create("GridItem.Column", GridItemPattern.ColumnProperty);
        UIProperty.Create("GridItem.ColumnSpan", GridItemPattern.ColumnSpanProperty);
        UIProperty.Create("GridItem.ContainingGrid", GridItemPattern.ContainingGridProperty);
        UIProperty.Create("GridItem.Row", GridItemPattern.RowProperty);
        UIProperty.Create("GridItem.RowSpan", GridItemPattern.RowSpanProperty);
        UIProperty.Create("Dock.DockPosition", DockPattern.DockPositionProperty);
        UIProperty.Create("ExpandCollapse.ExpandCollapseState", ExpandCollapsePattern.ExpandCollapseStateProperty);
        UIProperty.Create("MultipleView.CurrentView", MultipleViewPattern.CurrentViewProperty, typeof (int));
        UIProperty.Create("MultipleView.SupportedViews", MultipleViewPattern.SupportedViewsProperty);
        UIProperty.Create("Window.CanMaximize", WindowPattern.CanMaximizeProperty, typeof (bool));
        UIProperty.Create("Window.CanMinimize", WindowPattern.CanMinimizeProperty, typeof (bool));
        UIProperty.Create("Window.WindowInteractionState", WindowPattern.WindowInteractionStateProperty, typeof (WindowInteractionState));
        UIProperty.Create("Window.IsModal", WindowPattern.IsModalProperty, typeof (bool));
        UIProperty.Create("Window.IsTopmost", WindowPattern.IsTopmostProperty, typeof (bool));
        UIProperty.Create("Window.WindowVisualState", WindowPattern.WindowVisualStateProperty, typeof (WindowVisualState));
        UIProperty.Create("SelectionItem.IsSelected", SelectionItemPattern.IsSelectedProperty, typeof (bool));
        UIProperty.Create("SelectionItem.SelectionContainer", SelectionItemPattern.SelectionContainerProperty);
        UIProperty.Create("Table.RowHeaders", TablePattern.RowHeadersProperty);
        UIProperty.Create("Table.ColumnHeaders", TablePattern.ColumnHeadersProperty);
        UIProperty.Create("Table.RowOrColumnMajor", TablePattern.RowOrColumnMajorProperty, typeof (RowOrColumnMajor));
        UIProperty.Create("TableItem.RowHeaderItems", TableItemPattern.RowHeaderItemsProperty);
        UIProperty.Create("TableItem.ColumnHeaderItems", TableItemPattern.ColumnHeaderItemsProperty);
        UIProperty.Create("Toggle.ToggleState", TogglePattern.ToggleStateProperty, typeof (ToggleState));
        UIProperty.Create("Transform.CanMove", TransformPattern.CanMoveProperty, typeof (bool));
        UIProperty.Create("Transform.CanResize", TransformPattern.CanResizeProperty, typeof (bool));
        UIProperty.Create("Transform.CanRotate", TransformPattern.CanRotateProperty, typeof (bool));
        UIProperty.Create("IsLegacyPatternAvailable", AutomationElement.IsLegacyPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsPeripheral", AutomationElement.IsPeripheralProperty, typeof (bool));
        UIProperty.Create("IsTextEditPatternAvailable", AutomationElement.IsTextEditPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsVirtualizedItemPatternAvailable", AutomationElement.IsVirtualizedItemPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsSynchronizedInputPatternAvailable", AutomationElement.IsSynchronizedInputPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsItemContainerPatternAvailable", AutomationElement.IsItemContainerPatternAvailableProperty, typeof (bool));
        UIProperty.Create("AnnotationObjects", AutomationElement.AnnotationObjectsProperty);
        UIProperty.Create("AnnotationTypes", AutomationElement.AnnotationTypesProperty);
        UIProperty.Create("AriaRole", AutomationElement.AriaRoleProperty);
        UIProperty.Create("AriaProperties", AutomationElement.AriaPropertiesProperty);
        UIProperty.Create("IsDataValidForForm", AutomationElement.IsDataValidForFormProperty);
        UIProperty.Create("ControllerFor", AutomationElement.ControllerForProperty);
        UIProperty.Create("DescribedBy", AutomationElement.DescribedByProperty);
        UIProperty.Create("FlowsFrom", AutomationElement.FlowsFromProperty);
        UIProperty.Create("FlowsTo", AutomationElement.FlowsToProperty);
        UIProperty.Create("Level", AutomationElement.LevelProperty, typeof (int));
        UIProperty.Create("PositionInSet", AutomationElement.PositionInSetProperty, typeof (int));
        UIProperty.Create("ProviderDescription", AutomationElement.ProviderDescriptionProperty);
        UIProperty.Create("SizeOfSet", AutomationElement.SizeOfSetProperty, typeof (int));
        UIProperty.Create("LandmarkType", AutomationElement.LandmarkTypeProperty, typeof (LandmarkType));
        UIProperty.Create("LocalizedLandmarkType", AutomationElement.LocalizedLandmarkTypeProperty, typeof (string));
        UIProperty.Create("LegacyChildId", LegacyIAccessiblePattern.ChildIdProperty);
        UIProperty.Create("LegacyDefaultAction", LegacyIAccessiblePattern.DefaultActionProperty);
        UIProperty.Create("LegacyDescription", LegacyIAccessiblePattern.DescriptionProperty);
        UIProperty.Create("LegacyHelp", LegacyIAccessiblePattern.HelpProperty);
        UIProperty.Create("LegacyKeyboardShortcut", LegacyIAccessiblePattern.KeyboardShortcutProperty);
        UIProperty.Create("LegacyName", LegacyIAccessiblePattern.NameProperty);
        UIProperty.Create("LegacyRole", LegacyIAccessiblePattern.RoleProperty);
        UIProperty.Create("LegacySelection", LegacyIAccessiblePattern.SelectionProperty);
        UIProperty.Create("LegacyState", LegacyIAccessiblePattern.StateProperty);
        UIProperty.Create("LegacyValue", LegacyIAccessiblePattern.ValueProperty);
        UIProperty.Create("SearchVirtualItems", AutomationElement.SearchVirtualItemsProperty, typeof (int));
        UIProperty.Create("OptimizeForVisualContent", AutomationElement.OptimizeForVisualContentProperty, typeof (bool));
        UIProperty.Create("IsObjectModelPatternAvailable", AutomationElement.IsObjectModelPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsAnnotationPatternAvailable", AutomationElement.IsAnnotationPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsTextPattern2Available", AutomationElement.IsTextPattern2AvailableProperty, typeof (bool));
        UIProperty.Create("IsStylesPatternAvailable", AutomationElement.IsStylesPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsSpreadsheetPatternAvailable", AutomationElement.IsSpreadsheetPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsSpreadsheetItemPatternAvailable", AutomationElement.IsSpreadsheetItemPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsTransformPattern2Available", AutomationElement.IsTransformPattern2AvailableProperty, typeof (bool));
        UIProperty.Create("LiveSetting", AutomationElement.LiveSettingProperty);
        UIProperty.Create("IsTextChildPatternAvailable", AutomationElement.IsTextChildPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsDragPatternAvailable", AutomationElement.IsDragPatternAvailableProperty, typeof (bool));
        UIProperty.Create("IsDropTargetPatternAvailable", AutomationElement.IsDropTargetPatternAvailableProperty, typeof (bool));
        UIProperty.Create("Annotation.AnnotationTypeId", AnnotationPattern.AnnotationTypeIdProperty);
        UIProperty.Create("Annotation.AnnotationTypeName", AnnotationPattern.AnnotationTypeNameProperty);
        UIProperty.Create("Annotation.Author", AnnotationPattern.AuthorProperty);
        UIProperty.Create("Annotation.DateTime", AnnotationPattern.DateTimeProperty);
        UIProperty.Create("Annotation.Target", AnnotationPattern.TargetProperty);
        UIProperty.Create("Drag.IsGrabbed", DragPattern.IsGrabbedProperty, typeof (bool));
        UIProperty.Create("Drag.DropEffect", DragPattern.DropEffectProperty, typeof (string));
        UIProperty.Create("Drag.DropEffects", DragPattern.DropEffectsProperty);
        UIProperty.Create("Drag.GrabbedItems", DragPattern.GrabbedItemsProperty);
        UIProperty.Create("DropTarget.DropTargetEffect", DropTargetPattern.DropTargetEffectProperty, typeof (string));
        UIProperty.Create("DropTarget.DropTargetEffects", DropTargetPattern.DropTargetEffectsProperty);
        UIProperty.Create("SpreadsheetItem.Formula", SpreadsheetItemPattern.FormulaProperty, typeof (string));
        UIProperty.Create("SpreadsheetItem.AnnotationObjects", SpreadsheetItemPattern.AnnotationObjectsProperty);
        UIProperty.Create("SpreadsheetItem.AnnotationTypes", SpreadsheetItemPattern.AnnotationTypesProperty);
        UIProperty.Create("Styles.StyleId", StylesPattern.StyleIdProperty);
        UIProperty.Create("Styles.StyleName", StylesPattern.StyleNameProperty);
        UIProperty.Create("Styles.FillColor", StylesPattern.FillColorProperty);
        UIProperty.Create("Styles.FillPatternStyle", StylesPattern.FillPatternStyleProperty);
        UIProperty.Create("Styles.Shape", StylesPattern.ShapeProperty);
        UIProperty.Create("Styles.FillPatternColor", StylesPattern.FillPatternColorProperty);
        UIProperty.Create("Styles.ExtendedProperties", StylesPattern.ExtendedPropertiesProperty);
        UIProperty.Create("Transform2.CanZoom", TransformPattern2.CanZoomProperty, typeof (bool));
        UIProperty.Create("Transform2.ZoomLevel", TransformPattern2.ZoomLevelProperty);
        UIProperty.Create("Transform2.ZoomMinimum", TransformPattern2.ZoomMinimumProperty);
        UIProperty.Create("Transform2.ZoomMaximum", TransformPattern2.ZoomMaximumProperty);
      }
    }

    public override string ToString() => this._name;

    public static IEnumerable<UIProperty> AllProperties => (IEnumerable<UIProperty>) UIProperty._propertyTable.Values;

    public string Name => this._name;

    internal AutomationProperty Property => this._property;

    internal Type Type => this._allowedType;
  }
}
