// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ValuePattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class ValuePattern : BasePattern {
        public static readonly AutomationPattern Pattern = ValuePatternIdentifiers.Pattern;
        public static readonly AutomationProperty ValueProperty = ValuePatternIdentifiers.ValueProperty;
        public static readonly AutomationProperty IsReadOnlyProperty = ValuePatternIdentifiers.IsReadOnlyProperty;
        readonly IUIAutomationValuePattern _valuePattern;

        ValuePattern(AutomationElement element, IUIAutomationValuePattern valuePattern)
            : base(el: element) {
            this._valuePattern = valuePattern;
        }

        public ValuePatternInformation Cached {
            get { return new ValuePatternInformation(el: this._el, useCache: true); }
        }

        public ValuePatternInformation Current {
            get { return new ValuePatternInformation(el: this._el, useCache: false); }
        }

        internal static ValuePattern Wrap(
            AutomationElement element,
            IUIAutomationValuePattern valuePattern) {
            return new ValuePattern(element: element, valuePattern: valuePattern);
        }

        public void SetValue(string value) {
            this._valuePattern.SetValue(val: value);
        }

        public struct ValuePatternInformation {
            readonly AutomationElement _el;
            readonly bool _useCache;

            internal ValuePatternInformation(AutomationElement el, bool useCache) {
                this._el = el;
                this._useCache = useCache;
            }

            public string Value {
                get { return this._el.GetPatternPropertyValue(property: ValueProperty, useCache: this._useCache).ToString(); }
            }

            public bool IsReadOnly {
                get { return (bool) this._el.GetPatternPropertyValue(property: IsReadOnlyProperty, useCache: this._useCache); }
            }
        }
    }
}