// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.RangeValuePattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class RangeValuePattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = RangeValuePatternIdentifiers.Pattern;
    public static readonly AutomationProperty IsReadOnlyProperty = RangeValuePatternIdentifiers.IsReadOnlyProperty;
    public static readonly AutomationProperty LargeChangeProperty = RangeValuePatternIdentifiers.LargeChangeProperty;
    public static readonly AutomationProperty MaximumProperty = RangeValuePatternIdentifiers.MaximumProperty;
    public static readonly AutomationProperty MinimumProperty = RangeValuePatternIdentifiers.MinimumProperty;
    public static readonly AutomationProperty SmallChangeProperty = RangeValuePatternIdentifiers.SmallChangeProperty;
    public static readonly AutomationProperty ValueProperty = RangeValuePatternIdentifiers.ValueProperty;
    private readonly IUIAutomationRangeValuePattern _rangeValuePattern;

    private RangeValuePattern(
      AutomationElement element,
      IUIAutomationRangeValuePattern rangeValuePattern)
      : base(element)
      => this._rangeValuePattern = rangeValuePattern;

    internal static RangeValuePattern Wrap(
      AutomationElement element,
      IUIAutomationRangeValuePattern rangeValuePattern) => new RangeValuePattern(element, rangeValuePattern);

    public void SetValue(double value) => this._rangeValuePattern.SetValue(value);

    public RangeValuePattern.RangeValuePatternInformation Cached => new RangeValuePattern.RangeValuePatternInformation(this._el, true);

    public RangeValuePattern.RangeValuePatternInformation Current => new RangeValuePattern.RangeValuePatternInformation(this._el, false);

    public struct RangeValuePatternInformation
    {
      private AutomationElement _el;
      private bool _useCache;

      internal RangeValuePatternInformation(AutomationElement el, bool useCache)
      {
        this._el = el;
        this._useCache = useCache;
      }

      public double Value
      {
        get
        {
          object patternPropertyValue = this._el.GetPatternPropertyValue(RangeValuePattern.ValueProperty, this._useCache);
          switch (patternPropertyValue)
          {
            case int num:
              return (double) num;
            case byte num:
              return (double) num;
            case DateTime dateTime:
              return (double) dateTime.Year;
            default:
              return (double) patternPropertyValue;
          }
        }
      }

      public bool IsReadOnly => (bool) this._el.GetPatternPropertyValue(RangeValuePattern.IsReadOnlyProperty, this._useCache);

      public double Maximum
      {
        get
        {
          object patternPropertyValue = this._el.GetPatternPropertyValue(RangeValuePattern.MaximumProperty, this._useCache);
          switch (patternPropertyValue)
          {
            case int num:
              return (double) num;
            case byte num:
              return (double) num;
            case DateTime dateTime:
              return (double) dateTime.Year;
            default:
              return (double) patternPropertyValue;
          }
        }
      }

      public double Minimum
      {
        get
        {
          object patternPropertyValue = this._el.GetPatternPropertyValue(RangeValuePattern.MinimumProperty, this._useCache);
          switch (patternPropertyValue)
          {
            case int num:
              return (double) num;
            case byte num:
              return (double) num;
            case DateTime dateTime:
              return (double) dateTime.Year;
            default:
              return (double) patternPropertyValue;
          }
        }
      }

      public double LargeChange
      {
        get
        {
          object patternPropertyValue = this._el.GetPatternPropertyValue(RangeValuePattern.LargeChangeProperty, this._useCache);
          switch (patternPropertyValue)
          {
            case int num:
              return (double) num;
            case byte num:
              return (double) num;
            case DateTime dateTime:
              return (double) dateTime.Year;
            default:
              return (double) patternPropertyValue;
          }
        }
      }

      public double SmallChange
      {
        get
        {
          object patternPropertyValue = this._el.GetPatternPropertyValue(RangeValuePattern.SmallChangeProperty, this._useCache);
          switch (patternPropertyValue)
          {
            case int num:
              return (double) num;
            case byte num:
              return (double) num;
            case DateTime dateTime:
              return (double) dateTime.Year;
            default:
              return (double) patternPropertyValue;
          }
        }
      }
    }
  }
}
