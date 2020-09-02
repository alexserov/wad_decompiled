// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ExpandCollapsePattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class ExpandCollapsePattern : BasePattern {
        public static readonly AutomationPattern Pattern = ExpandCollapsePatternIdentifiers.Pattern;
        public static readonly AutomationProperty ExpandCollapseStateProperty = ExpandCollapsePatternIdentifiers.ExpandCollapseStateProperty;
        readonly IUIAutomationExpandCollapsePattern _expandCollapsePattern;

        ExpandCollapsePattern(
            AutomationElement element,
            IUIAutomationExpandCollapsePattern expandCollapsePattern)
            : base(el: element) {
            this._expandCollapsePattern = expandCollapsePattern;
        }

        public ExpandCollapsePatternInformation Cached {
            get { return new ExpandCollapsePatternInformation(el: this._el, useCache: true); }
        }

        public ExpandCollapsePatternInformation Current {
            get { return new ExpandCollapsePatternInformation(el: this._el, useCache: false); }
        }

        internal static ExpandCollapsePattern Wrap(
            AutomationElement element,
            IUIAutomationExpandCollapsePattern expandCollapsePattern) {
            return new ExpandCollapsePattern(element: element, expandCollapsePattern: expandCollapsePattern);
        }

        public void Expand() {
            this._expandCollapsePattern.Expand();
        }

        public void Collapse() {
            this._expandCollapsePattern.Collapse();
        }

        public struct ExpandCollapsePatternInformation {
            readonly AutomationElement _el;
            readonly bool _useCache;

            internal ExpandCollapsePatternInformation(AutomationElement el, bool useCache) {
                this._el = el;
                this._useCache = useCache;
            }

            public ExpandCollapseState ExpandCollapseState {
                get { return (ExpandCollapseState) this._el.GetPatternPropertyValue(property: ExpandCollapseStateProperty, useCache: this._useCache); }
            }
        }
    }
}