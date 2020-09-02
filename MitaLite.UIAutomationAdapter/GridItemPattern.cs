// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.GridItemPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class GridItemPattern : BasePattern {
        public static readonly AutomationPattern Pattern = GridItemPatternIdentifiers.Pattern;
        public static readonly AutomationProperty RowProperty = GridItemPatternIdentifiers.RowProperty;
        public static readonly AutomationProperty RowSpanProperty = GridItemPatternIdentifiers.RowSpanProperty;
        public static readonly AutomationProperty ColumnProperty = GridItemPatternIdentifiers.ColumnProperty;
        public static readonly AutomationProperty ColumnSpanProperty = GridItemPatternIdentifiers.ColumnSpanProperty;
        public static readonly AutomationProperty ContainingGridProperty = GridItemPatternIdentifiers.ContainingGridProperty;
        readonly IUIAutomationGridItemPattern _gridItemPattern;

        internal GridItemPattern(AutomationElement element, object gridItemPattern)
            : base(el: element) {
            this._gridItemPattern = (IUIAutomationGridItemPattern) gridItemPattern;
        }

        public GridItemPatternInformation Cached {
            get { return new GridItemPatternInformation(el: this._el, useCache: true); }
        }

        public GridItemPatternInformation Current {
            get { return new GridItemPatternInformation(el: this._el, useCache: false); }
        }

        internal static GridItemPattern Wrap(
            AutomationElement element,
            IUIAutomationGridItemPattern gridItemPattern) {
            return new GridItemPattern(element: element, gridItemPattern: gridItemPattern);
        }

        public struct GridItemPatternInformation {
            readonly AutomationElement _el;
            readonly bool _useCache;

            internal GridItemPatternInformation(AutomationElement el, bool useCache) {
                this._el = el;
                this._useCache = useCache;
            }

            public int Row {
                get { return (int) this._el.GetPatternPropertyValue(property: RowProperty, useCache: this._useCache); }
            }

            public int Column {
                get { return (int) this._el.GetPatternPropertyValue(property: ColumnProperty, useCache: this._useCache); }
            }

            public int RowSpan {
                get { return (int) this._el.GetPatternPropertyValue(property: RowSpanProperty, useCache: this._useCache); }
            }

            public int ColumnSpan {
                get { return (int) this._el.GetPatternPropertyValue(property: ColumnSpanProperty, useCache: this._useCache); }
            }

            public AutomationElement ContainingGrid {
                get { return (AutomationElement) this._el.GetPatternPropertyValue(property: ContainingGridProperty, useCache: this._useCache); }
            }
        }
    }
}