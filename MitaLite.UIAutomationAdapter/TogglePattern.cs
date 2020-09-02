// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TogglePattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class TogglePattern : BasePattern {
        public static readonly AutomationPattern Pattern = TogglePatternIdentifiers.Pattern;
        public static readonly AutomationProperty ToggleStateProperty = TogglePatternIdentifiers.ToggleStateProperty;
        readonly IUIAutomationTogglePattern _togglePattern;

        TogglePattern(AutomationElement element, IUIAutomationTogglePattern togglePattern)
            : base(el: element) {
            this._togglePattern = togglePattern;
        }

        public TogglePatternInformation Cached {
            get { return new TogglePatternInformation(el: this._el, useCache: true); }
        }

        public TogglePatternInformation Current {
            get { return new TogglePatternInformation(el: this._el, useCache: false); }
        }

        internal static TogglePattern Wrap(
            AutomationElement element,
            IUIAutomationTogglePattern pattern) {
            return new TogglePattern(element: element, togglePattern: pattern);
        }

        public void Toggle() {
            this._togglePattern.Toggle();
        }

        public struct TogglePatternInformation {
            readonly AutomationElement _el;
            readonly bool _useCache;

            internal TogglePatternInformation(AutomationElement el, bool useCache) {
                this._el = el;
                this._useCache = useCache;
            }

            public ToggleState ToggleState {
                get { return (ToggleState) this._el.GetPatternPropertyValue(property: ToggleStateProperty, useCache: this._useCache); }
            }
        }
    }
}