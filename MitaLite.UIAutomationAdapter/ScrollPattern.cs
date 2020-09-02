// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ScrollPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class ScrollPattern : BasePattern {
        public const double NoScroll = -1.0;
        public static readonly AutomationPattern Pattern = ScrollPatternIdentifiers.Pattern;
        public static readonly AutomationProperty HorizontallyScrollableProperty = ScrollPatternIdentifiers.HorizontallyScrollableProperty;
        public static readonly AutomationProperty HorizontalScrollPercentProperty = ScrollPatternIdentifiers.HorizontalScrollPercentProperty;
        public static readonly AutomationProperty HorizontalViewSizeProperty = ScrollPatternIdentifiers.HorizontalViewSizeProperty;
        public static readonly AutomationProperty VerticallyScrollableProperty = ScrollPatternIdentifiers.VerticallyScrollableProperty;
        public static readonly AutomationProperty VerticalScrollPercentProperty = ScrollPatternIdentifiers.VerticalScrollPercentProperty;
        public static readonly AutomationProperty VerticalViewSizeProperty = ScrollPatternIdentifiers.VerticalViewSizeProperty;
        readonly IUIAutomationScrollPattern _scrollPattern;

        ScrollPattern(AutomationElement element, IUIAutomationScrollPattern scrollPattern)
            : base(el: element) {
            this._scrollPattern = scrollPattern;
        }

        public ScrollPatternInformation Cached {
            get { return new ScrollPatternInformation(el: this._el, useCache: true); }
        }

        public ScrollPatternInformation Current {
            get { return new ScrollPatternInformation(el: this._el, useCache: false); }
        }

        internal static ScrollPattern Wrap(
            AutomationElement element,
            IUIAutomationScrollPattern scrollPattern) {
            return new ScrollPattern(element: element, scrollPattern: scrollPattern);
        }

        public void SetScrollPercent(double horizontalPercent, double verticalPercent) {
            this._scrollPattern.SetScrollPercent(horizontalPercent: horizontalPercent, verticalPercent: verticalPercent);
        }

        public void Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount) {
            this._scrollPattern.Scroll(horizontalAmount: UiaConvert.Convert(scrollAmount: horizontalAmount), verticalAmount: UiaConvert.Convert(scrollAmount: verticalAmount));
        }

        public void ScrollHorizontal(ScrollAmount amount) {
            this._scrollPattern.Scroll(horizontalAmount: UiaConvert.Convert(scrollAmount: amount), verticalAmount: UiaConvert.Convert(scrollAmount: ScrollAmount.NoAmount));
        }

        public void ScrollVertical(ScrollAmount amount) {
            this._scrollPattern.Scroll(horizontalAmount: UiaConvert.Convert(scrollAmount: ScrollAmount.NoAmount), verticalAmount: UiaConvert.Convert(scrollAmount: amount));
        }

        public struct ScrollPatternInformation {
            readonly AutomationElement _el;
            readonly bool _useCache;

            internal ScrollPatternInformation(AutomationElement el, bool useCache) {
                this._el = el;
                this._useCache = useCache;
            }

            public double HorizontalScrollPercent {
                get { return (double) this._el.GetPatternPropertyValue(property: HorizontalScrollPercentProperty, useCache: this._useCache); }
            }

            public double VerticalScrollPercent {
                get { return (double) this._el.GetPatternPropertyValue(property: VerticalScrollPercentProperty, useCache: this._useCache); }
            }

            public double HorizontalViewSize {
                get { return (double) this._el.GetPatternPropertyValue(property: HorizontalViewSizeProperty, useCache: this._useCache); }
            }

            public double VerticalViewSize {
                get { return (double) this._el.GetPatternPropertyValue(property: VerticalViewSizeProperty, useCache: this._useCache); }
            }

            public bool HorizontallyScrollable {
                get { return (bool) this._el.GetPatternPropertyValue(property: HorizontallyScrollableProperty, useCache: this._useCache); }
            }

            public bool VerticallyScrollable {
                get { return (bool) this._el.GetPatternPropertyValue(property: VerticallyScrollableProperty, useCache: this._useCache); }
            }
        }
    }
}