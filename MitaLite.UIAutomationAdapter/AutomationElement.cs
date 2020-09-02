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

namespace System.Windows.Automation
{
  public sealed class AutomationElement
  {
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
    public static readonly AutomationProperty IsLegacyPatternAvailableProperty = new AutomationProperty(30090, "AutomationElementIdentifiers.IsLegacyIAccessiblePatternAvailableProperty");
    public static readonly AutomationProperty IsVirtualizedItemPatternAvailableProperty = new AutomationProperty(30109, "AutomationElementIdentifiers.IsVirtualizedItemPatternAvailableProperty");
    public static readonly AutomationProperty IsSynchronizedInputPatternAvailableProperty = new AutomationProperty(30110, "AutomationElementIdentifiers.IsSynchronizedInputPatternAvailableProperty");
    public static readonly AutomationProperty IsItemContainerPatternAvailableProperty = new AutomationProperty(30108, "AutomationElementIdentifiers.IsItemContainerPatternAvailableProperty");
    public static readonly AutomationProperty AriaRoleProperty = new AutomationProperty(30101, "AutomationElementIdentifiers.AriaRoleProperty");
    public static readonly AutomationProperty AriaPropertiesProperty = new AutomationProperty(30102, "AutomationElementIdentifiers.AriaPropertiesProperty");
    public static readonly AutomationProperty IsDataValidForFormProperty = new AutomationProperty(30103, "AutomationElementIdentifiers.IsDataValidForFormProperty");
    public static readonly AutomationProperty ControllerForProperty = new AutomationProperty(30104, "AutomationElementIdentifiers.ControllerForProperty");
    public static readonly AutomationProperty DescribedByProperty = new AutomationProperty(30105, "AutomationElementIdentifiers.DescribedByProperty");
    public static readonly AutomationProperty FlowsFromProperty = new AutomationProperty(30148, "AutomationElementIdentifiers.FlowsFromProperty");
    public static readonly AutomationProperty FlowsToProperty = new AutomationProperty(30106, "AutomationElementIdentifiers.FlowsToProperty");
    public static readonly AutomationProperty ProviderDescriptionProperty = new AutomationProperty(30107, "AutomationElementIdentifiers.ProviderDescriptionProperty");
    public static readonly AutomationProperty SearchVirtualItemsProperty = new AutomationProperty(0, "AutomationElementIdentifiers.SearchVirtualItemsProperty");
    public static readonly AutomationProperty OptimizeForVisualContentProperty = new AutomationProperty(30111, "AutomationElementIdentifiers.OptimizeForVisualContentProperty");
    public static readonly AutomationProperty IsObjectModelPatternAvailableProperty = new AutomationProperty(30112, "AutomationElementIdentifiers.IsObjectModelPatternAvailableProperty");
    public static readonly AutomationProperty IsAnnotationPatternAvailableProperty = new AutomationProperty(30118, "AutomationElementIdentifiers.IsAnnotationPatternAvailableProperty");
    public static readonly AutomationProperty IsTextPattern2AvailableProperty = new AutomationProperty(30119, "AutomationElementIdentifiers.IsTextPattern2AvailableProperty");
    public static readonly AutomationProperty IsStylesPatternAvailableProperty = new AutomationProperty(30127, "AutomationElementIdentifiers.IsStylesPatternAvailableProperty");
    public static readonly AutomationProperty IsSpreadsheetPatternAvailableProperty = new AutomationProperty(30128, "AutomationElementIdentifiers.IsSpreadsheetPatternAvailableProperty");
    public static readonly AutomationProperty IsSpreadsheetItemPatternAvailableProperty = new AutomationProperty(30132, "AutomationElementIdentifiers.IsSpreadsheetItemPatternAvailableProperty");
    public static readonly AutomationProperty IsTransformPattern2AvailableProperty = new AutomationProperty(30134, "AutomationElementIdentifiers.IsTransformPattern2AvailableProperty");
    public static readonly AutomationProperty LiveSettingProperty = new AutomationProperty(30135, "AutomationElementIdentifiers.LiveSettingProperty");
    public static readonly AutomationProperty IsTextChildPatternAvailableProperty = new AutomationProperty(30136, "AutomationElementIdentifiers.IsTextChildPatternAvailableProperty");
    public static readonly AutomationProperty IsDragPatternAvailableProperty = new AutomationProperty(30137, "AutomationElementIdentifiers.IsDragPatternAvailableProperty");
    public static readonly AutomationProperty IsDropTargetPatternAvailableProperty = new AutomationProperty(30141, "AutomationElementIdentifiers.IsDropTargetPatternAvailableProperty");
    public static readonly AutomationProperty IsTextEditPatternAvailableProperty = new AutomationProperty(30149, "AutomationElementIdentifiers.IsTextEditPatternAvailableProperty");
    public static readonly AutomationProperty IsPeripheralProperty = new AutomationProperty(30150, "AutomationElementIdentifiers.IsPeripheralProperty");
    public static readonly AutomationProperty IsCustomNavigationPatternAvailableProperty = new AutomationProperty(30151, "AutomationElementIdentifiers.IsCustomNavigationPatternAvailableProperty");
    public static readonly AutomationProperty PositionInSetProperty = new AutomationProperty(30152, "AutomationElementIdentifiers.PositionInSetProperty");
    public static readonly AutomationProperty SizeOfSetProperty = new AutomationProperty(30153, "AutomationElementIdentifiers.SizeOfSetProperty");
    public static readonly AutomationProperty LevelProperty = new AutomationProperty(30154, "AutomationElementIdentifiers.LevelProperty");
    public static readonly AutomationProperty AnnotationTypesProperty = new AutomationProperty(30155, "AutomationElementIdentifiers.AnnotationTypesProperty");
    public static readonly AutomationProperty AnnotationObjectsProperty = new AutomationProperty(30156, "AutomationElementIdentifiers.AnnotationObjectsProperty");
    public static readonly AutomationProperty LandmarkTypeProperty = new AutomationProperty(30157, "AutomationElementIdentifiers.LandmarkTypeProperty");
    public static readonly AutomationProperty LocalizedLandmarkTypeProperty = new AutomationProperty(30158, "AutomationElementIdentifiers.LocalizedLandmarkTypeProperty");
    public static readonly object NotSupported;
    private IUIAutomationElement _autoElement;
    private static readonly CacheRequest _defaultCacheRequest = new CacheRequest();

    public AutomationElement(IUIAutomationElement autoElement)
    {
      Validate.ArgumentNotNull((object) autoElement, nameof (autoElement));
      this._autoElement = autoElement;
    }

    public override bool Equals(object obj)
    {
      AutomationElement el2 = obj as AutomationElement;
      return obj != null && !(el2 == (AutomationElement) null) && System.Windows.Automation.Automation.Compare(this, el2);
    }

    public override int GetHashCode()
    {
      int[] runtimeId = this.GetRuntimeId();
      int num = 0;
      if (runtimeId == null)
        throw new InvalidOperationException();
      for (int index = 0; index < runtimeId.Length; ++index)
        num = num * 2 ^ runtimeId[index];
      return num;
    }

    public static bool operator ==(AutomationElement left, AutomationElement right)
    {
      if ((object) left == null)
        return (object) right == null;
      return (object) right == null ? (object) left == null : left.Equals((object) right);
    }

    public static bool operator !=(AutomationElement left, AutomationElement right) => !(left == right);

    public int[] GetRuntimeId() => this._autoElement.GetRuntimeId().ToTypedArray<int>();

    public static AutomationElement FromPoint(Point pt) => new AutomationElement(System.Windows.Automation.Automation.AutomationClass.ElementFromPointBuildCache(new tagPOINT()
    {
      x = (int) pt.X,
      y = (int) pt.Y
    }, AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest));

    public static AutomationElement FromHandle(IntPtr hwnd) => new AutomationElement(System.Windows.Automation.Automation.AutomationClass.ElementFromHandleBuildCache(hwnd, AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest));

    public object GetCurrentPropertyValue(AutomationProperty property) => this.GetCurrentPropertyValue(property, false);

    public object GetCurrentPropertyValue(AutomationProperty property, bool ignoreDefaultValue)
    {
      Validate.ArgumentNotNull((object) property, nameof (property));
      Variant propertyValueVariant = new Variant();
      try
      {
        propertyValueVariant = this._autoElement.GetCurrentPropertyValueEx(property.Id, Convert.ToInt32(ignoreDefaultValue));
      }
      catch (COMException ex)
      {
      }
      return UiaConvert.ConvertPropertyValue(property, propertyValueVariant);
    }

    public object GetCurrentPattern(AutomationPattern pattern)
    {
      Validate.ArgumentNotNull((object) pattern, nameof (pattern));
      return pattern.Wrap(this, this._autoElement.GetCurrentPattern(pattern.Id));
    }

    public bool TryGetCurrentPattern(AutomationPattern pattern, out object patternObject)
    {
      Validate.ArgumentNotNull((object) pattern, nameof (pattern));
      object currentPattern = this._autoElement.GetCurrentPattern(pattern.Id);
      if (currentPattern == null)
      {
        patternObject = (object) null;
        return false;
      }
      patternObject = pattern.Wrap(this, currentPattern);
      return true;
    }

    public object GetCachedPropertyValue(AutomationProperty property) => this.GetCachedPropertyValue(property, false);

    public object GetCachedPropertyValue(AutomationProperty property, bool ignoreDefaultValue)
    {
      Validate.ArgumentNotNull((object) property, nameof (property));
      Variant cachedPropertyValueEx = this._autoElement.GetCachedPropertyValueEx(property.Id, Convert.ToInt32(ignoreDefaultValue));
      return UiaConvert.ConvertPropertyValue(property, cachedPropertyValueEx);
    }

    public object GetCachedPattern(AutomationPattern pattern)
    {
      object patternObject;
      if (!this.TryGetCachedPattern(pattern, out patternObject))
        throw new InvalidOperationException("UnsupportedPattern");
      return patternObject;
    }

    public bool TryGetCachedPattern(AutomationPattern pattern, out object patternObject)
    {
      patternObject = (object) null;
      throw new NotImplementedException();
    }

    public AutomationElement GetUpdatedCache(CacheRequest request)
    {
      try
      {
        return new AutomationElement(this._autoElement.BuildUpdatedCache(request.IUIAutomationCacheRequest));
      }
      catch (COMException ex)
      {
        Exception exception = null;
        if (UiaConvert.ConvertException(ex, out exception))
          throw exception;
        throw;
      }
    }

    public AutomationElement FindFirst(TreeScope scope, Condition condition)
    {
      Validate.ArgumentNotNull((object) condition, nameof (condition));
      IUIAutomationElement firstBuildCache = this._autoElement.FindFirstBuildCache(UiaConvert.Convert(scope), condition.IUIAutomationCondition, AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest);
      return firstBuildCache != null ? new AutomationElement(firstBuildCache) : (AutomationElement) null;
    }

    public AutomationElement FindFirst(
      TreeScope scope,
      Condition condition,
      CacheRequest cr)
    {
      Validate.ArgumentNotNull((object) condition, nameof (condition));
      Validate.ArgumentNotNull((object) cr, nameof (cr));
      IUIAutomationElement firstBuildCache = this._autoElement.FindFirstBuildCache(UiaConvert.Convert(scope), condition.IUIAutomationCondition, cr.IUIAutomationCacheRequest);
      return firstBuildCache != null ? new AutomationElement(firstBuildCache) : (AutomationElement) null;
    }

    public AutomationElementCollection FindAll(
      TreeScope scope,
      Condition condition)
    {
      Validate.ArgumentNotNull((object) condition, nameof (condition));
      IUIAutomationElementArray allBuildCache = this._autoElement.FindAllBuildCache(UiaConvert.Convert(scope), condition.IUIAutomationCondition, AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest);
      return allBuildCache != null ? new AutomationElementCollection(allBuildCache) : (AutomationElementCollection) null;
    }

    public AutomationElementCollection FindAll(
      TreeScope scope,
      Condition condition,
      CacheRequest cr)
    {
      Validate.ArgumentNotNull((object) condition, nameof (condition));
      Validate.ArgumentNotNull((object) cr, nameof (cr));
      IUIAutomationElementArray allBuildCache = this._autoElement.FindAllBuildCache(UiaConvert.Convert(scope), condition.IUIAutomationCondition, cr.IUIAutomationCacheRequest);
      return allBuildCache != null ? new AutomationElementCollection(allBuildCache) : (AutomationElementCollection) null;
    }

    public AutomationProperty[] GetSupportedProperties()
    {
      int[] propertyIds;
      System.Windows.Automation.Automation.AutomationClass.PollForPotentialSupportedProperties(this._autoElement, out propertyIds, out string[] _);
      List<AutomationProperty> automationPropertyList = new List<AutomationProperty>();
      for (int index = 0; index < propertyIds.Length; ++index)
      {
        if (propertyIds[index] != 0)
        {
          try
          {
            AutomationProperty automationProperty = AutomationProperty.LookupById(propertyIds[index]);
            automationPropertyList.Add(automationProperty);
          }
          catch (KeyNotFoundException ex)
          {
          }
        }
      }
      return automationPropertyList.ToArray();
    }

    public AutomationPattern[] GetSupportedPatterns()
    {
      int[] patternIds;
      System.Windows.Automation.Automation.AutomationClass.PollForPotentialSupportedPatterns(this._autoElement, out patternIds, out string[] _);
      AutomationPattern[] automationPatternArray = new AutomationPattern[patternIds.Length];
      for (int index = 0; index < patternIds.Length; ++index)
      {
        if (patternIds[index] != 0)
        {
          try
          {
            AutomationPattern automationPattern = AutomationPattern.LookupById(patternIds[index]);
            automationPatternArray[index] = automationPattern;
          }
          catch (KeyNotFoundException ex)
          {
          }
        }
      }
      return automationPatternArray;
    }

    public void SetFocus() => this._autoElement.SetFocus();

    public bool TryGetClickablePoint(out Point pt)
    {
      pt = new Point();
      tagPOINT clickable;
      if (this._autoElement.GetClickablePoint(out clickable) == 0)
        return false;
      pt.X = (double) clickable.x;
      pt.Y = (double) clickable.y;
      return true;
    }

    public Point GetClickablePoint()
    {
      Point pt;
      if (!this.TryGetClickablePoint(out pt))
        throw new NoClickablePointException("Clickable point not found for the UI element");
      return pt;
    }

    public static AutomationElement RootElement => new AutomationElement(System.Windows.Automation.Automation.AutomationClass.GetRootElementBuildCache(AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest));

    public static AutomationElement FocusedElement => new AutomationElement(System.Windows.Automation.Automation.AutomationClass.GetFocusedElementBuildCache(AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest));

    public AutomationElement.AutomationElementInformation Cached => new AutomationElement.AutomationElementInformation(this, true);

    public AutomationElement.AutomationElementInformation Current => new AutomationElement.AutomationElementInformation(this, false);

    internal IUIAutomationElement IUIAutomationElement => this._autoElement;

    internal static CacheRequest DefaultCacheRequest => AutomationElement._defaultCacheRequest;

    internal object GetPatternPropertyValue(AutomationProperty property, bool useCache) => useCache ? this.GetCachedPropertyValue(property) : this.GetCurrentPropertyValue(property);

    public struct AutomationElementInformation
    {
      private AutomationElement _el;
      private bool _useCache;

      internal AutomationElementInformation(AutomationElement el, bool useCache)
      {
        this._el = el;
        this._useCache = useCache;
      }

      public ControlType ControlType => (ControlType) this._el.GetPatternPropertyValue(AutomationElement.ControlTypeProperty, this._useCache);

      public string LocalizedControlType => (string) this._el.GetPatternPropertyValue(AutomationElement.LocalizedControlTypeProperty, this._useCache);

      public string Name => (string) this._el.GetPatternPropertyValue(AutomationElement.NameProperty, this._useCache);

      public string AcceleratorKey => (string) this._el.GetPatternPropertyValue(AutomationElement.AcceleratorKeyProperty, this._useCache);

      public string AccessKey => (string) this._el.GetPatternPropertyValue(AutomationElement.AccessKeyProperty, this._useCache);

      public bool HasKeyboardFocus => (bool) this._el.GetPatternPropertyValue(AutomationElement.HasKeyboardFocusProperty, this._useCache);

      public bool IsKeyboardFocusable => (bool) this._el.GetPatternPropertyValue(AutomationElement.IsKeyboardFocusableProperty, this._useCache);

      public bool IsEnabled => (bool) this._el.GetPatternPropertyValue(AutomationElement.IsEnabledProperty, this._useCache);

      public Rect BoundingRectangle => (Rect) this._el.GetPatternPropertyValue(AutomationElement.BoundingRectangleProperty, this._useCache);

      public string HelpText => (string) this._el.GetPatternPropertyValue(AutomationElement.HelpTextProperty, this._useCache);

      public bool IsControlElement => (bool) this._el.GetPatternPropertyValue(AutomationElement.IsControlElementProperty, this._useCache);

      public bool IsContentElement => (bool) this._el.GetPatternPropertyValue(AutomationElement.IsContentElementProperty, this._useCache);

      public AutomationElement LabeledBy => (AutomationElement) this._el.GetPatternPropertyValue(AutomationElement.LabeledByProperty, this._useCache);

      public string AutomationId => (string) this._el.GetPatternPropertyValue(AutomationElement.AutomationIdProperty, this._useCache);

      public string ItemType => (string) this._el.GetPatternPropertyValue(AutomationElement.ItemTypeProperty, this._useCache);

      public bool IsPassword => (bool) this._el.GetPatternPropertyValue(AutomationElement.IsPasswordProperty, this._useCache);

      public string ClassName => (string) this._el.GetPatternPropertyValue(AutomationElement.ClassNameProperty, this._useCache);

      public int NativeWindowHandle => (int) this._el.GetPatternPropertyValue(AutomationElement.NativeWindowHandleProperty, this._useCache);

      public int ProcessId => (int) this._el.GetPatternPropertyValue(AutomationElement.ProcessIdProperty, this._useCache);

      public bool IsOffscreen => (bool) this._el.GetPatternPropertyValue(AutomationElement.IsOffscreenProperty, this._useCache);

      public OrientationType Orientation => (OrientationType) this._el.GetPatternPropertyValue(AutomationElement.OrientationProperty, this._useCache);

      public string FrameworkId => (string) this._el.GetPatternPropertyValue(AutomationElement.FrameworkIdProperty, this._useCache);

      public bool IsRequiredForForm => (bool) this._el.GetPatternPropertyValue(AutomationElement.IsRequiredForFormProperty, this._useCache);

      public string ItemStatus => (string) this._el.GetPatternPropertyValue(AutomationElement.ItemStatusProperty, this._useCache);
    }
  }
}
