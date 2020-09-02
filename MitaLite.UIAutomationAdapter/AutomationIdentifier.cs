// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationIdentifier
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Collections.Generic;

namespace System.Windows.Automation {
    public class AutomationIdentifier : IComparable {
        static bool initialized;
        static readonly Dictionary<int, AutomationIdentifier> identifiers = new Dictionary<int, AutomationIdentifier>();
        AutomationIdType _type;

        internal AutomationIdentifier(AutomationIdType type, int id, string programmaticName) {
            Id = id;
            this._type = type;
            ProgrammaticName = programmaticName;
        }

        public int Id { get; }

        public string ProgrammaticName { get; }

        public int CompareTo(object obj) {
            if (obj == null)
                throw new ArgumentException(message: "null object passed to ArgumentIdentifier.CompareTo");
            return GetHashCode() - obj.GetHashCode();
        }

        public override bool Equals(object obj) {
            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode() {
            return Id;
        }

        public override string ToString() {
            return ProgrammaticName;
        }

        internal static TIdentifier LookupById<TIdentifier>(int id) where TIdentifier : AutomationIdentifier {
            Initialize();
            return (TIdentifier) identifiers[key: id];
        }

        static void Initialize() {
            try {
                if (initialized)
                    return;
                Register(identifier: AutomationElement.AsyncContentLoadedEvent);
                Register(identifier: AutomationElement.AutomationFocusChangedEvent);
                Register(identifier: AutomationElement.AutomationPropertyChangedEvent);
                Register(identifier: AutomationElement.LayoutInvalidatedEvent);
                Register(identifier: AutomationElement.MenuClosedEvent);
                Register(identifier: AutomationElement.MenuOpenedEvent);
                Register(identifier: AutomationElement.StructureChangedEvent);
                Register(identifier: AutomationElement.ToolTipClosedEvent);
                Register(identifier: AutomationElement.ToolTipOpenedEvent);
                Register(identifier: AutomationElement.AcceleratorKeyProperty);
                Register(identifier: AutomationElement.AccessKeyProperty);
                Register(identifier: AutomationElement.AnnotationObjectsProperty);
                Register(identifier: AutomationElement.AnnotationTypesProperty);
                Register(identifier: AutomationElement.AriaRoleProperty);
                Register(identifier: AutomationElement.AriaPropertiesProperty);
                Register(identifier: AutomationElement.AutomationIdProperty);
                Register(identifier: AutomationElement.BoundingRectangleProperty);
                Register(identifier: AutomationElement.ClassNameProperty);
                Register(identifier: AutomationElement.ClickablePointProperty);
                Register(identifier: AutomationElement.ControllerForProperty);
                Register(identifier: AutomationElement.ControlTypeProperty);
                Register(identifier: AutomationElement.CultureProperty);
                Register(identifier: AutomationElement.DescribedByProperty);
                Register(identifier: AutomationElement.FlowsFromProperty);
                Register(identifier: AutomationElement.FlowsToProperty);
                Register(identifier: AutomationElement.FrameworkIdProperty);
                Register(identifier: AutomationElement.HasKeyboardFocusProperty);
                Register(identifier: AutomationElement.HelpTextProperty);
                Register(identifier: AutomationElement.IsAnnotationPatternAvailableProperty);
                Register(identifier: AutomationElement.IsContentElementProperty);
                Register(identifier: AutomationElement.IsControlElementProperty);
                Register(identifier: AutomationElement.IsCustomNavigationPatternAvailableProperty);
                Register(identifier: AutomationElement.IsDataValidForFormProperty);
                Register(identifier: AutomationElement.IsDockPatternAvailableProperty);
                Register(identifier: AutomationElement.IsDragPatternAvailableProperty);
                Register(identifier: AutomationElement.IsDropTargetPatternAvailableProperty);
                Register(identifier: AutomationElement.IsEnabledProperty);
                Register(identifier: AutomationElement.IsExpandCollapsePatternAvailableProperty);
                Register(identifier: AutomationElement.IsGridItemPatternAvailableProperty);
                Register(identifier: AutomationElement.IsGridPatternAvailableProperty);
                Register(identifier: AutomationElement.IsInvokePatternAvailableProperty);
                Register(identifier: AutomationElement.IsItemContainerPatternAvailableProperty);
                Register(identifier: AutomationElement.IsKeyboardFocusableProperty);
                Register(identifier: AutomationElement.IsLegacyPatternAvailableProperty);
                Register(identifier: AutomationElement.IsMultipleViewPatternAvailableProperty);
                Register(identifier: AutomationElement.IsObjectModelPatternAvailableProperty);
                Register(identifier: AutomationElement.IsOffscreenProperty);
                Register(identifier: AutomationElement.IsPasswordProperty);
                Register(identifier: AutomationElement.IsPeripheralProperty);
                Register(identifier: AutomationElement.IsRangeValuePatternAvailableProperty);
                Register(identifier: AutomationElement.IsRequiredForFormProperty);
                Register(identifier: AutomationElement.IsScrollItemPatternAvailableProperty);
                Register(identifier: AutomationElement.IsScrollPatternAvailableProperty);
                Register(identifier: AutomationElement.IsSelectionItemPatternAvailableProperty);
                Register(identifier: AutomationElement.IsSelectionPatternAvailableProperty);
                Register(identifier: AutomationElement.IsSpreadsheetPatternAvailableProperty);
                Register(identifier: AutomationElement.IsSpreadsheetItemPatternAvailableProperty);
                Register(identifier: AutomationElement.IsStylesPatternAvailableProperty);
                Register(identifier: AutomationElement.IsSynchronizedInputPatternAvailableProperty);
                Register(identifier: AutomationElement.IsTableItemPatternAvailableProperty);
                Register(identifier: AutomationElement.IsTablePatternAvailableProperty);
                Register(identifier: AutomationElement.IsTextChildPatternAvailableProperty);
                Register(identifier: AutomationElement.IsTextEditPatternAvailableProperty);
                Register(identifier: AutomationElement.IsTextPatternAvailableProperty);
                Register(identifier: AutomationElement.IsTextPattern2AvailableProperty);
                Register(identifier: AutomationElement.IsTogglePatternAvailableProperty);
                Register(identifier: AutomationElement.IsTransformPatternAvailableProperty);
                Register(identifier: AutomationElement.IsTransformPattern2AvailableProperty);
                Register(identifier: AutomationElement.IsValuePatternAvailableProperty);
                Register(identifier: AutomationElement.IsVirtualizedItemPatternAvailableProperty);
                Register(identifier: AutomationElement.IsWindowPatternAvailableProperty);
                Register(identifier: AutomationElement.ItemStatusProperty);
                Register(identifier: AutomationElement.ItemTypeProperty);
                Register(identifier: AutomationElement.LabeledByProperty);
                Register(identifier: AutomationElement.LevelProperty);
                Register(identifier: AutomationElement.LiveSettingProperty);
                Register(identifier: AutomationElement.LocalizedControlTypeProperty);
                Register(identifier: AutomationElement.NameProperty);
                Register(identifier: AutomationElement.NativeWindowHandleProperty);
                Register(identifier: AutomationElement.OptimizeForVisualContentProperty);
                Register(identifier: AutomationElement.OrientationProperty);
                Register(identifier: AutomationElement.PositionInSetProperty);
                Register(identifier: AutomationElement.ProcessIdProperty);
                Register(identifier: AutomationElement.ProviderDescriptionProperty);
                Register(identifier: AutomationElement.RuntimeIdProperty);
                Register(identifier: AutomationElement.SearchVirtualItemsProperty);
                Register(identifier: AutomationElement.SizeOfSetProperty);
                Register(identifier: AutomationElement.LandmarkTypeProperty);
                Register(identifier: AutomationElement.LocalizedLandmarkTypeProperty);
                Register(identifier: AnnotationPattern.Pattern);
                Register(identifier: AnnotationPattern.AnnotationTypeIdProperty);
                Register(identifier: AnnotationPattern.AnnotationTypeNameProperty);
                Register(identifier: AnnotationPattern.AuthorProperty);
                Register(identifier: AnnotationPattern.DateTimeProperty);
                Register(identifier: AnnotationPattern.TargetProperty);
                Register(identifier: CustomNavigationPattern.Pattern);
                Register(identifier: DockPattern.Pattern);
                Register(identifier: DockPattern.DockPositionProperty);
                Register(identifier: DragPattern.Pattern);
                Register(identifier: DragPattern.IsGrabbedProperty);
                Register(identifier: DragPattern.DropEffectProperty);
                Register(identifier: DragPattern.DropEffectsProperty);
                Register(identifier: DragPattern.GrabbedItemsProperty);
                Register(identifier: DropTargetPattern.Pattern);
                Register(identifier: DropTargetPattern.DropTargetEffectProperty);
                Register(identifier: DropTargetPattern.DropTargetEffectsProperty);
                Register(identifier: ExpandCollapsePattern.Pattern);
                Register(identifier: ExpandCollapsePattern.ExpandCollapseStateProperty);
                Register(identifier: GridPattern.Pattern);
                Register(identifier: GridPattern.ColumnCountProperty);
                Register(identifier: GridPattern.RowCountProperty);
                Register(identifier: GridItemPattern.Pattern);
                Register(identifier: GridItemPattern.ColumnProperty);
                Register(identifier: GridItemPattern.ColumnSpanProperty);
                Register(identifier: GridItemPattern.ContainingGridProperty);
                Register(identifier: GridItemPattern.RowProperty);
                Register(identifier: GridItemPattern.RowSpanProperty);
                Register(identifier: InvokePattern.Pattern);
                Register(identifier: InvokePattern.InvokedEvent);
                Register(identifier: ItemContainerPattern.Pattern);
                Register(identifier: LegacyIAccessiblePattern.Pattern);
                Register(identifier: LegacyIAccessiblePattern.ChildIdProperty);
                Register(identifier: LegacyIAccessiblePattern.DefaultActionProperty);
                Register(identifier: LegacyIAccessiblePattern.DescriptionProperty);
                Register(identifier: LegacyIAccessiblePattern.HelpProperty);
                Register(identifier: LegacyIAccessiblePattern.KeyboardShortcutProperty);
                Register(identifier: LegacyIAccessiblePattern.NameProperty);
                Register(identifier: LegacyIAccessiblePattern.RoleProperty);
                Register(identifier: LegacyIAccessiblePattern.SelectionProperty);
                Register(identifier: LegacyIAccessiblePattern.StateProperty);
                Register(identifier: LegacyIAccessiblePattern.ValueProperty);
                Register(identifier: MultipleViewPattern.Pattern);
                Register(identifier: MultipleViewPattern.CurrentViewProperty);
                Register(identifier: MultipleViewPattern.SupportedViewsProperty);
                Register(identifier: ObjectModelPattern.Pattern);
                Register(identifier: RangeValuePattern.Pattern);
                Register(identifier: RangeValuePattern.IsReadOnlyProperty);
                Register(identifier: RangeValuePattern.LargeChangeProperty);
                Register(identifier: RangeValuePattern.MaximumProperty);
                Register(identifier: RangeValuePattern.MinimumProperty);
                Register(identifier: RangeValuePattern.SmallChangeProperty);
                Register(identifier: RangeValuePattern.ValueProperty);
                Register(identifier: ScrollItemPattern.Pattern);
                Register(identifier: ScrollPattern.Pattern);
                Register(identifier: ScrollPattern.HorizontallyScrollableProperty);
                Register(identifier: ScrollPattern.HorizontalScrollPercentProperty);
                Register(identifier: ScrollPattern.HorizontalViewSizeProperty);
                Register(identifier: ScrollPattern.VerticallyScrollableProperty);
                Register(identifier: ScrollPattern.VerticalScrollPercentProperty);
                Register(identifier: ScrollPattern.VerticalViewSizeProperty);
                Register(identifier: SelectionPattern.Pattern);
                Register(identifier: SelectionPattern.InvalidatedEvent);
                Register(identifier: SelectionPattern.CanSelectMultipleProperty);
                Register(identifier: SelectionPattern.IsSelectionRequiredProperty);
                Register(identifier: SelectionPattern.SelectionProperty);
                Register(identifier: SelectionItemPattern.Pattern);
                Register(identifier: SelectionItemPattern.ElementAddedToSelectionEvent);
                Register(identifier: SelectionItemPattern.ElementRemovedFromSelectionEvent);
                Register(identifier: SelectionItemPattern.ElementSelectedEvent);
                Register(identifier: SelectionItemPattern.IsSelectedProperty);
                Register(identifier: SelectionItemPattern.SelectionContainerProperty);
                Register(identifier: SpreadsheetPattern.Pattern);
                Register(identifier: SpreadsheetItemPattern.Pattern);
                Register(identifier: SpreadsheetItemPattern.FormulaProperty);
                Register(identifier: SpreadsheetItemPattern.AnnotationObjectsProperty);
                Register(identifier: SpreadsheetItemPattern.AnnotationTypesProperty);
                Register(identifier: StylesPattern.Pattern);
                Register(identifier: StylesPattern.StyleIdProperty);
                Register(identifier: StylesPattern.StyleNameProperty);
                Register(identifier: StylesPattern.FillColorProperty);
                Register(identifier: StylesPattern.FillPatternStyleProperty);
                Register(identifier: StylesPattern.ShapeProperty);
                Register(identifier: StylesPattern.FillPatternColorProperty);
                Register(identifier: StylesPattern.ExtendedPropertiesProperty);
                Register(identifier: SynchronizedInputPattern.Pattern);
                Register(identifier: TablePattern.Pattern);
                Register(identifier: TablePattern.ColumnHeadersProperty);
                Register(identifier: TablePattern.RowHeadersProperty);
                Register(identifier: TablePattern.RowOrColumnMajorProperty);
                Register(identifier: TableItemPattern.Pattern);
                Register(identifier: TableItemPattern.ColumnHeaderItemsProperty);
                Register(identifier: TableItemPattern.RowHeaderItemsProperty);
                Register(identifier: TextChildPattern.Pattern);
                Register(identifier: TextEditPattern.Pattern);
                Register(identifier: TextPattern.Pattern);
                Register(identifier: TextPattern.TextChangedEvent);
                Register(identifier: TextPattern.TextSelectionChangedEvent);
                Register(identifier: TextPattern.AnimationStyleAttribute);
                Register(identifier: TextPattern.BackgroundColorAttribute);
                Register(identifier: TextPattern.BulletStyleAttribute);
                Register(identifier: TextPattern.CapStyleAttribute);
                Register(identifier: TextPattern.CultureAttribute);
                Register(identifier: TextPattern.FontNameAttribute);
                Register(identifier: TextPattern.FontSizeAttribute);
                Register(identifier: TextPattern.FontWeightAttribute);
                Register(identifier: TextPattern.ForegroundColorAttribute);
                Register(identifier: TextPattern.HorizontalTextAlignmentAttribute);
                Register(identifier: TextPattern.IndentationFirstLineAttribute);
                Register(identifier: TextPattern.IndentationLeadingAttribute);
                Register(identifier: TextPattern.IndentationTrailingAttribute);
                Register(identifier: TextPattern.IsHiddenAttribute);
                Register(identifier: TextPattern.IsItalicAttribute);
                Register(identifier: TextPattern.IsReadOnlyAttribute);
                Register(identifier: TextPattern.IsSubscriptAttribute);
                Register(identifier: TextPattern.IsSuperscriptAttribute);
                Register(identifier: TextPattern.MarginBottomAttribute);
                Register(identifier: TextPattern.MarginLeadingAttribute);
                Register(identifier: TextPattern.MarginTopAttribute);
                Register(identifier: TextPattern.MarginTrailingAttribute);
                Register(identifier: TextPattern.OutlineStylesAttribute);
                Register(identifier: TextPattern.OverlineColorAttribute);
                Register(identifier: TextPattern.OverlineStyleAttribute);
                Register(identifier: TextPattern.StrikethroughColorAttribute);
                Register(identifier: TextPattern.StrikethroughStyleAttribute);
                Register(identifier: TextPattern.TabsAttribute);
                Register(identifier: TextPattern.TextFlowDirectionsAttribute);
                Register(identifier: TextPattern.UnderlineColorAttribute);
                Register(identifier: TextPattern.UnderlineStyleAttribute);
                Register(identifier: TextPattern2.Pattern);
                Register(identifier: TogglePattern.Pattern);
                Register(identifier: TogglePattern.ToggleStateProperty);
                Register(identifier: TransformPattern.Pattern);
                Register(identifier: TransformPattern.CanMoveProperty);
                Register(identifier: TransformPattern.CanResizeProperty);
                Register(identifier: TransformPattern.CanRotateProperty);
                Register(identifier: TransformPattern2.Pattern);
                Register(identifier: TransformPattern2.CanZoomProperty);
                Register(identifier: TransformPattern2.ZoomLevelProperty);
                Register(identifier: TransformPattern2.ZoomMinimumProperty);
                Register(identifier: TransformPattern2.ZoomMaximumProperty);
                Register(identifier: ValuePattern.Pattern);
                Register(identifier: ValuePattern.IsReadOnlyProperty);
                Register(identifier: ValuePattern.ValueProperty);
                Register(identifier: VirtualizedItemPattern.Pattern);
                Register(identifier: WindowPattern.Pattern);
                Register(identifier: WindowPattern.WindowOpenedEvent);
                Register(identifier: WindowPattern.WindowClosedEvent);
                Register(identifier: WindowPattern.CanMaximizeProperty);
                Register(identifier: WindowPattern.CanMinimizeProperty);
                Register(identifier: WindowPattern.IsModalProperty);
                Register(identifier: WindowPattern.IsTopmostProperty);
                Register(identifier: WindowPattern.WindowInteractionStateProperty);
                Register(identifier: WindowPattern.WindowVisualStateProperty);
            } finally {
                initialized = true;
            }
        }

        static void Register(AutomationIdentifier identifier) {
            identifiers.Add(key: identifier.Id, value: identifier);
        }
    }
}