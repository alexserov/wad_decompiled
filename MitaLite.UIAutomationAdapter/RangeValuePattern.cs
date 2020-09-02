// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.RangeValuePattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class RangeValuePattern : BasePattern {
        public static readonly AutomationPattern Pattern = RangeValuePatternIdentifiers.Pattern;
        public static readonly AutomationProperty IsReadOnlyProperty = RangeValuePatternIdentifiers.IsReadOnlyProperty;
        public static readonly AutomationProperty LargeChangeProperty = RangeValuePatternIdentifiers.LargeChangeProperty;
        public static readonly AutomationProperty MaximumProperty = RangeValuePatternIdentifiers.MaximumProperty;
        public static readonly AutomationProperty MinimumProperty = RangeValuePatternIdentifiers.MinimumProperty;
        public static readonly AutomationProperty SmallChangeProperty = RangeValuePatternIdentifiers.SmallChangeProperty;
        public static readonly AutomationProperty ValueProperty = RangeValuePatternIdentifiers.ValueProperty;
        readonly IUIAutomationRangeValuePattern _rangeValuePattern;

        RangeValuePattern(
            AutomationElement element,
            IUIAutomationRangeValuePattern rangeValuePattern)
            : base(el: element) {
            this._rangeValuePattern = rangeValuePattern;
        }

        public RangeValuePatternInformation Cached {
            get { return new RangeValuePatternInformation(el: this._el, useCache: true); }
        }

        public RangeValuePatternInformation Current {
            get { return new RangeValuePatternInformation(el: this._el, useCache: false); }
        }

        internal static RangeValuePattern Wrap(
            AutomationElement element,
            IUIAutomationRangeValuePattern rangeValuePattern) {
            return new RangeValuePattern(element: element, rangeValuePattern: rangeValuePattern);
        }

        public void SetValue(double value) {
            this._rangeValuePattern.SetValue(val: value);
        }

        public struct RangeValuePatternInformation {
            readonly AutomationElement _el;
            readonly bool _useCache;

            internal RangeValuePatternInformation(AutomationElement el, bool useCache) {
                this._el = el;
                this._useCache = useCache;
            }

            public double Value {
                get {
                    var patternPropertyValue = this._el.GetPatternPropertyValue(property: ValueProperty, useCache: this._useCache);
                    switch (patternPropertyValue) {
                        case int num:
                            return num;
                        case byte num:
                            return num;
                        case DateTime dateTime:
                            return dateTime.Year;
                        default:
                            return (double) patternPropertyValue;
                    }
                }
            }

            public bool IsReadOnly {
                get { return (bool) this._el.GetPatternPropertyValue(property: IsReadOnlyProperty, useCache: this._useCache); }
            }

            public double Maximum {
                get {
                    var patternPropertyValue = this._el.GetPatternPropertyValue(property: MaximumProperty, useCache: this._useCache);
                    switch (patternPropertyValue) {
                        case int num:
                            return num;
                        case byte num:
                            return num;
                        case DateTime dateTime:
                            return dateTime.Year;
                        default:
                            return (double) patternPropertyValue;
                    }
                }
            }

            public double Minimum {
                get {
                    var patternPropertyValue = this._el.GetPatternPropertyValue(property: MinimumProperty, useCache: this._useCache);
                    switch (patternPropertyValue) {
                        case int num:
                            return num;
                        case byte num:
                            return num;
                        case DateTime dateTime:
                            return dateTime.Year;
                        default:
                            return (double) patternPropertyValue;
                    }
                }
            }

            public double LargeChange {
                get {
                    var patternPropertyValue = this._el.GetPatternPropertyValue(property: LargeChangeProperty, useCache: this._useCache);
                    switch (patternPropertyValue) {
                        case int num:
                            return num;
                        case byte num:
                            return num;
                        case DateTime dateTime:
                            return dateTime.Year;
                        default:
                            return (double) patternPropertyValue;
                    }
                }
            }

            public double SmallChange {
                get {
                    var patternPropertyValue = this._el.GetPatternPropertyValue(property: SmallChangeProperty, useCache: this._useCache);
                    switch (patternPropertyValue) {
                        case int num:
                            return num;
                        case byte num:
                            return num;
                        case DateTime dateTime:
                            return dateTime.Year;
                        default:
                            return (double) patternPropertyValue;
                    }
                }
            }
        }
    }
}