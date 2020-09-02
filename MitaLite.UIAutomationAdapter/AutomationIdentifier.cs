// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationIdentifier
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Collections.Generic;

namespace System.Windows.Automation
{
  public class AutomationIdentifier : IComparable
  {
    private static bool initialized;
    private static readonly Dictionary<int, AutomationIdentifier> identifiers = new Dictionary<int, AutomationIdentifier>();
    private int _id;
    private string _programmaticName;
    private AutomationIdType _type;

    internal AutomationIdentifier(AutomationIdType type, int id, string programmaticName)
    {
      this._id = id;
      this._type = type;
      this._programmaticName = programmaticName;
    }

    public int CompareTo(object obj)
    {
      if (obj == null)
        throw new ArgumentException("null object passed to ArgumentIdentifier.CompareTo");
      return this.GetHashCode() - obj.GetHashCode();
    }

    public override bool Equals(object obj) => obj.GetHashCode() == this.GetHashCode();

    public override int GetHashCode() => this._id;

    public int Id => this._id;

    public string ProgrammaticName => this._programmaticName;

    public override string ToString() => this.ProgrammaticName;

    internal static TIdentifier LookupById<TIdentifier>(int id) where TIdentifier : AutomationIdentifier
    {
      AutomationIdentifier.Initialize();
      return (TIdentifier) AutomationIdentifier.identifiers[id];
    }

    private static void Initialize()
    {
      try
      {
        if (AutomationIdentifier.initialized)
          return;
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.AsyncContentLoadedEvent);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.AutomationFocusChangedEvent);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.AutomationPropertyChangedEvent);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.LayoutInvalidatedEvent);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.MenuClosedEvent);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.MenuOpenedEvent);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.StructureChangedEvent);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.ToolTipClosedEvent);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.ToolTipOpenedEvent);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.AcceleratorKeyProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.AccessKeyProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.AnnotationObjectsProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.AnnotationTypesProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.AriaRoleProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.AriaPropertiesProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.AutomationIdProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.BoundingRectangleProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.ClassNameProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.ClickablePointProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.ControllerForProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.ControlTypeProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.CultureProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.DescribedByProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.FlowsFromProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.FlowsToProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.FrameworkIdProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.HasKeyboardFocusProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.HelpTextProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsAnnotationPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsContentElementProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsControlElementProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsCustomNavigationPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsDataValidForFormProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsDockPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsDragPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsDropTargetPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsEnabledProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsExpandCollapsePatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsGridItemPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsGridPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsInvokePatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsItemContainerPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsKeyboardFocusableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsLegacyPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsMultipleViewPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsObjectModelPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsOffscreenProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsPasswordProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsPeripheralProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsRangeValuePatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsRequiredForFormProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsScrollItemPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsScrollPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsSelectionItemPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsSelectionPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsSpreadsheetPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsSpreadsheetItemPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsStylesPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsSynchronizedInputPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsTableItemPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsTablePatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsTextChildPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsTextEditPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsTextPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsTextPattern2AvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsTogglePatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsTransformPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsTransformPattern2AvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsValuePatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsVirtualizedItemPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.IsWindowPatternAvailableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.ItemStatusProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.ItemTypeProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.LabeledByProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.LevelProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.LiveSettingProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.LocalizedControlTypeProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.NameProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.NativeWindowHandleProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.OptimizeForVisualContentProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.OrientationProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.PositionInSetProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.ProcessIdProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.ProviderDescriptionProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.RuntimeIdProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.SearchVirtualItemsProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.SizeOfSetProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.LandmarkTypeProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AutomationElement.LocalizedLandmarkTypeProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AnnotationPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) AnnotationPattern.AnnotationTypeIdProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AnnotationPattern.AnnotationTypeNameProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AnnotationPattern.AuthorProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AnnotationPattern.DateTimeProperty);
        AutomationIdentifier.Register((AutomationIdentifier) AnnotationPattern.TargetProperty);
        AutomationIdentifier.Register((AutomationIdentifier) CustomNavigationPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) DockPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) DockPattern.DockPositionProperty);
        AutomationIdentifier.Register((AutomationIdentifier) DragPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) DragPattern.IsGrabbedProperty);
        AutomationIdentifier.Register((AutomationIdentifier) DragPattern.DropEffectProperty);
        AutomationIdentifier.Register((AutomationIdentifier) DragPattern.DropEffectsProperty);
        AutomationIdentifier.Register((AutomationIdentifier) DragPattern.GrabbedItemsProperty);
        AutomationIdentifier.Register((AutomationIdentifier) DropTargetPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) DropTargetPattern.DropTargetEffectProperty);
        AutomationIdentifier.Register((AutomationIdentifier) DropTargetPattern.DropTargetEffectsProperty);
        AutomationIdentifier.Register((AutomationIdentifier) ExpandCollapsePattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) ExpandCollapsePattern.ExpandCollapseStateProperty);
        AutomationIdentifier.Register((AutomationIdentifier) GridPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) GridPattern.ColumnCountProperty);
        AutomationIdentifier.Register((AutomationIdentifier) GridPattern.RowCountProperty);
        AutomationIdentifier.Register((AutomationIdentifier) GridItemPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) GridItemPattern.ColumnProperty);
        AutomationIdentifier.Register((AutomationIdentifier) GridItemPattern.ColumnSpanProperty);
        AutomationIdentifier.Register((AutomationIdentifier) GridItemPattern.ContainingGridProperty);
        AutomationIdentifier.Register((AutomationIdentifier) GridItemPattern.RowProperty);
        AutomationIdentifier.Register((AutomationIdentifier) GridItemPattern.RowSpanProperty);
        AutomationIdentifier.Register((AutomationIdentifier) InvokePattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) InvokePattern.InvokedEvent);
        AutomationIdentifier.Register((AutomationIdentifier) ItemContainerPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) LegacyIAccessiblePattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) LegacyIAccessiblePattern.ChildIdProperty);
        AutomationIdentifier.Register((AutomationIdentifier) LegacyIAccessiblePattern.DefaultActionProperty);
        AutomationIdentifier.Register((AutomationIdentifier) LegacyIAccessiblePattern.DescriptionProperty);
        AutomationIdentifier.Register((AutomationIdentifier) LegacyIAccessiblePattern.HelpProperty);
        AutomationIdentifier.Register((AutomationIdentifier) LegacyIAccessiblePattern.KeyboardShortcutProperty);
        AutomationIdentifier.Register((AutomationIdentifier) LegacyIAccessiblePattern.NameProperty);
        AutomationIdentifier.Register((AutomationIdentifier) LegacyIAccessiblePattern.RoleProperty);
        AutomationIdentifier.Register((AutomationIdentifier) LegacyIAccessiblePattern.SelectionProperty);
        AutomationIdentifier.Register((AutomationIdentifier) LegacyIAccessiblePattern.StateProperty);
        AutomationIdentifier.Register((AutomationIdentifier) LegacyIAccessiblePattern.ValueProperty);
        AutomationIdentifier.Register((AutomationIdentifier) MultipleViewPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) MultipleViewPattern.CurrentViewProperty);
        AutomationIdentifier.Register((AutomationIdentifier) MultipleViewPattern.SupportedViewsProperty);
        AutomationIdentifier.Register((AutomationIdentifier) ObjectModelPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) RangeValuePattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) RangeValuePattern.IsReadOnlyProperty);
        AutomationIdentifier.Register((AutomationIdentifier) RangeValuePattern.LargeChangeProperty);
        AutomationIdentifier.Register((AutomationIdentifier) RangeValuePattern.MaximumProperty);
        AutomationIdentifier.Register((AutomationIdentifier) RangeValuePattern.MinimumProperty);
        AutomationIdentifier.Register((AutomationIdentifier) RangeValuePattern.SmallChangeProperty);
        AutomationIdentifier.Register((AutomationIdentifier) RangeValuePattern.ValueProperty);
        AutomationIdentifier.Register((AutomationIdentifier) ScrollItemPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) ScrollPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) ScrollPattern.HorizontallyScrollableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) ScrollPattern.HorizontalScrollPercentProperty);
        AutomationIdentifier.Register((AutomationIdentifier) ScrollPattern.HorizontalViewSizeProperty);
        AutomationIdentifier.Register((AutomationIdentifier) ScrollPattern.VerticallyScrollableProperty);
        AutomationIdentifier.Register((AutomationIdentifier) ScrollPattern.VerticalScrollPercentProperty);
        AutomationIdentifier.Register((AutomationIdentifier) ScrollPattern.VerticalViewSizeProperty);
        AutomationIdentifier.Register((AutomationIdentifier) SelectionPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) SelectionPattern.InvalidatedEvent);
        AutomationIdentifier.Register((AutomationIdentifier) SelectionPattern.CanSelectMultipleProperty);
        AutomationIdentifier.Register((AutomationIdentifier) SelectionPattern.IsSelectionRequiredProperty);
        AutomationIdentifier.Register((AutomationIdentifier) SelectionPattern.SelectionProperty);
        AutomationIdentifier.Register((AutomationIdentifier) SelectionItemPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) SelectionItemPattern.ElementAddedToSelectionEvent);
        AutomationIdentifier.Register((AutomationIdentifier) SelectionItemPattern.ElementRemovedFromSelectionEvent);
        AutomationIdentifier.Register((AutomationIdentifier) SelectionItemPattern.ElementSelectedEvent);
        AutomationIdentifier.Register((AutomationIdentifier) SelectionItemPattern.IsSelectedProperty);
        AutomationIdentifier.Register((AutomationIdentifier) SelectionItemPattern.SelectionContainerProperty);
        AutomationIdentifier.Register((AutomationIdentifier) SpreadsheetPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) SpreadsheetItemPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) SpreadsheetItemPattern.FormulaProperty);
        AutomationIdentifier.Register((AutomationIdentifier) SpreadsheetItemPattern.AnnotationObjectsProperty);
        AutomationIdentifier.Register((AutomationIdentifier) SpreadsheetItemPattern.AnnotationTypesProperty);
        AutomationIdentifier.Register((AutomationIdentifier) StylesPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) StylesPattern.StyleIdProperty);
        AutomationIdentifier.Register((AutomationIdentifier) StylesPattern.StyleNameProperty);
        AutomationIdentifier.Register((AutomationIdentifier) StylesPattern.FillColorProperty);
        AutomationIdentifier.Register((AutomationIdentifier) StylesPattern.FillPatternStyleProperty);
        AutomationIdentifier.Register((AutomationIdentifier) StylesPattern.ShapeProperty);
        AutomationIdentifier.Register((AutomationIdentifier) StylesPattern.FillPatternColorProperty);
        AutomationIdentifier.Register((AutomationIdentifier) StylesPattern.ExtendedPropertiesProperty);
        AutomationIdentifier.Register((AutomationIdentifier) SynchronizedInputPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) TablePattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) TablePattern.ColumnHeadersProperty);
        AutomationIdentifier.Register((AutomationIdentifier) TablePattern.RowHeadersProperty);
        AutomationIdentifier.Register((AutomationIdentifier) TablePattern.RowOrColumnMajorProperty);
        AutomationIdentifier.Register((AutomationIdentifier) TableItemPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) TableItemPattern.ColumnHeaderItemsProperty);
        AutomationIdentifier.Register((AutomationIdentifier) TableItemPattern.RowHeaderItemsProperty);
        AutomationIdentifier.Register((AutomationIdentifier) TextChildPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) TextEditPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.TextChangedEvent);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.TextSelectionChangedEvent);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.AnimationStyleAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.BackgroundColorAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.BulletStyleAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.CapStyleAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.CultureAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.FontNameAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.FontSizeAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.FontWeightAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.ForegroundColorAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.HorizontalTextAlignmentAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.IndentationFirstLineAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.IndentationLeadingAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.IndentationTrailingAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.IsHiddenAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.IsItalicAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.IsReadOnlyAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.IsSubscriptAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.IsSuperscriptAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.MarginBottomAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.MarginLeadingAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.MarginTopAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.MarginTrailingAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.OutlineStylesAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.OverlineColorAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.OverlineStyleAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.StrikethroughColorAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.StrikethroughStyleAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.TabsAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.TextFlowDirectionsAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.UnderlineColorAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern.UnderlineStyleAttribute);
        AutomationIdentifier.Register((AutomationIdentifier) TextPattern2.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) TogglePattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) TogglePattern.ToggleStateProperty);
        AutomationIdentifier.Register((AutomationIdentifier) TransformPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) TransformPattern.CanMoveProperty);
        AutomationIdentifier.Register((AutomationIdentifier) TransformPattern.CanResizeProperty);
        AutomationIdentifier.Register((AutomationIdentifier) TransformPattern.CanRotateProperty);
        AutomationIdentifier.Register((AutomationIdentifier) TransformPattern2.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) TransformPattern2.CanZoomProperty);
        AutomationIdentifier.Register((AutomationIdentifier) TransformPattern2.ZoomLevelProperty);
        AutomationIdentifier.Register((AutomationIdentifier) TransformPattern2.ZoomMinimumProperty);
        AutomationIdentifier.Register((AutomationIdentifier) TransformPattern2.ZoomMaximumProperty);
        AutomationIdentifier.Register((AutomationIdentifier) ValuePattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) ValuePattern.IsReadOnlyProperty);
        AutomationIdentifier.Register((AutomationIdentifier) ValuePattern.ValueProperty);
        AutomationIdentifier.Register((AutomationIdentifier) VirtualizedItemPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) WindowPattern.Pattern);
        AutomationIdentifier.Register((AutomationIdentifier) WindowPattern.WindowOpenedEvent);
        AutomationIdentifier.Register((AutomationIdentifier) WindowPattern.WindowClosedEvent);
        AutomationIdentifier.Register((AutomationIdentifier) WindowPattern.CanMaximizeProperty);
        AutomationIdentifier.Register((AutomationIdentifier) WindowPattern.CanMinimizeProperty);
        AutomationIdentifier.Register((AutomationIdentifier) WindowPattern.IsModalProperty);
        AutomationIdentifier.Register((AutomationIdentifier) WindowPattern.IsTopmostProperty);
        AutomationIdentifier.Register((AutomationIdentifier) WindowPattern.WindowInteractionStateProperty);
        AutomationIdentifier.Register((AutomationIdentifier) WindowPattern.WindowVisualStateProperty);
      }
      finally
      {
        AutomationIdentifier.initialized = true;
      }
    }

    private static void Register(AutomationIdentifier identifier) => AutomationIdentifier.identifiers.Add(identifier._id, identifier);
  }
}
