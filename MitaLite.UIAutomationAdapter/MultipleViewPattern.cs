// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.MultipleViewPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class MultipleViewPattern : BasePattern {
        public static readonly AutomationPattern Pattern = MultipleViewPatternIdentifiers.Pattern;
        public static readonly AutomationProperty CurrentViewProperty = MultipleViewPatternIdentifiers.CurrentViewProperty;
        public static readonly AutomationProperty SupportedViewsProperty = MultipleViewPatternIdentifiers.SupportedViewsProperty;
        readonly IUIAutomationMultipleViewPattern _multipleViewPattern;

        MultipleViewPattern(
            AutomationElement element,
            IUIAutomationMultipleViewPattern multipleViewPattern)
            : base(el: element) {
            this._multipleViewPattern = multipleViewPattern;
        }

        public MultipleViewPatternInformation Cached {
            get { return new MultipleViewPatternInformation(el: this._el, useCache: true); }
        }

        public MultipleViewPatternInformation Current {
            get { return new MultipleViewPatternInformation(el: this._el, useCache: false); }
        }

        internal static MultipleViewPattern Wrap(
            AutomationElement element,
            IUIAutomationMultipleViewPattern multipleViewPattern) {
            return new MultipleViewPattern(element: element, multipleViewPattern: multipleViewPattern);
        }

        public string GetViewName(int viewId) {
            return this._multipleViewPattern.GetViewName(view: viewId);
        }

        public void SetCurrentView(int viewId) {
            this._multipleViewPattern.SetCurrentView(view: viewId);
        }

        public struct MultipleViewPatternInformation {
            readonly AutomationElement _el;
            readonly bool _useCache;

            internal MultipleViewPatternInformation(AutomationElement el, bool useCache) {
                this._el = el;
                this._useCache = useCache;
            }

            public int CurrentView {
                get { return (int) this._el.GetPatternPropertyValue(property: CurrentViewProperty, useCache: this._useCache); }
            }

            public int[] GetSupportedViews() {
                return (int[]) this._el.GetPatternPropertyValue(property: SupportedViewsProperty, useCache: this._useCache);
            }
        }
    }
}