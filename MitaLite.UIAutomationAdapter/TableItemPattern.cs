// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TableItemPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class TableItemPattern : GridItemPattern {
        public new static readonly AutomationPattern Pattern = TableItemPatternIdentifiers.Pattern;
        public static readonly AutomationProperty ColumnHeaderItemsProperty = TableItemPatternIdentifiers.ColumnHeaderItemsProperty;
        public static readonly AutomationProperty RowHeaderItemsProperty = TableItemPatternIdentifiers.RowHeaderItemsProperty;
        readonly IUIAutomationTableItemPattern _tableItemPattern;

        TableItemPattern(
            AutomationElement element,
            IUIAutomationTableItemPattern tableItemPattern)
            : base(element: element, gridItemPattern: tableItemPattern) {
            this._tableItemPattern = tableItemPattern;
        }

        public TableItemPatternInformation Cached {
            get { return new TableItemPatternInformation(el: this._el, useCache: true); }
        }

        public TableItemPatternInformation Current {
            get { return new TableItemPatternInformation(el: this._el, useCache: false); }
        }

        internal static TableItemPattern Wrap(
            AutomationElement element,
            IUIAutomationTableItemPattern pattern) {
            return new TableItemPattern(element: element, tableItemPattern: pattern);
        }

        public struct TableItemPatternInformation {
            readonly AutomationElement _el;
            readonly bool _useCache;

            internal TableItemPatternInformation(AutomationElement el, bool useCache) {
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

            public AutomationElement[] GetRowHeaderItems() {
                return (AutomationElement[]) (AutomationElementCollection) this._el.GetPatternPropertyValue(property: RowHeaderItemsProperty, useCache: this._useCache);
            }

            public AutomationElement[] GetColumnHeaderItems() {
                return (AutomationElement[]) (AutomationElementCollection) this._el.GetPatternPropertyValue(property: ColumnHeaderItemsProperty, useCache: this._useCache);
            }
        }
    }
}