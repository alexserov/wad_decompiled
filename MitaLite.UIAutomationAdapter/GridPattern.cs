// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.GridPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class GridPattern : BasePattern {
        public static readonly AutomationPattern Pattern = GridPatternIdentifiers.Pattern;
        public static readonly AutomationProperty ColumnCountProperty = GridPatternIdentifiers.ColumnCountProperty;
        public static readonly AutomationProperty RowCountProperty = GridPatternIdentifiers.RowCountProperty;
        readonly IUIAutomationGridPattern _gridPattern;

        internal GridPattern(AutomationElement element, IUIAutomationGridPattern gridPattern)
            : base(el: element) {
            this._gridPattern = gridPattern;
        }

        public GridPatternInformation Cached {
            get { return new GridPatternInformation(el: this._el, useCache: true); }
        }

        public GridPatternInformation Current {
            get { return new GridPatternInformation(el: this._el, useCache: false); }
        }

        internal static GridPattern Wrap(
            AutomationElement element,
            IUIAutomationGridPattern gridPattern) {
            return new GridPattern(element: element, gridPattern: gridPattern);
        }

        public AutomationElement GetItem(int row, int column) {
            return new AutomationElement(autoElement: this._gridPattern.GetItem(row: row, column: column));
        }

        public struct GridPatternInformation {
            readonly AutomationElement _el;
            readonly bool _useCache;

            internal GridPatternInformation(AutomationElement el, bool useCache) {
                this._el = el;
                this._useCache = useCache;
            }

            public int RowCount {
                get { return (int) this._el.GetPatternPropertyValue(property: RowCountProperty, useCache: this._useCache); }
            }

            public int ColumnCount {
                get { return (int) this._el.GetPatternPropertyValue(property: ColumnCountProperty, useCache: this._useCache); }
            }
        }
    }
}