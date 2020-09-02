// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TablePattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class TablePattern : GridPattern {
        public new static readonly AutomationPattern Pattern = TablePatternIdentifiers.Pattern;
        public static readonly AutomationProperty ColumnHeadersProperty = TablePatternIdentifiers.ColumnHeadersProperty;
        public static readonly AutomationProperty RowHeadersProperty = TablePatternIdentifiers.RowHeadersProperty;
        public static readonly AutomationProperty RowOrColumnMajorProperty = TablePatternIdentifiers.RowOrColumnMajorProperty;
        readonly IUIAutomationTablePattern _tablePattern;

        TablePattern(
            AutomationElement element,
            IUIAutomationTablePattern tablePattern,
            IUIAutomationGridPattern gridPattern)
            : base(element: element, gridPattern: gridPattern) {
            this._tablePattern = tablePattern;
        }

        public TablePatternInformation Cached {
            get { return new TablePatternInformation(el: this._el, useCache: true); }
        }

        public TablePatternInformation Current {
            get { return new TablePatternInformation(el: this._el, useCache: false); }
        }

        internal static TablePattern Wrap(
            AutomationElement element,
            IUIAutomationTablePattern tablePattern) {
            object obj = null;
            return new TablePattern(element: element, tablePattern: tablePattern, gridPattern: (IUIAutomationGridPattern) obj);
        }

        public struct TablePatternInformation {
            readonly AutomationElement _el;
            readonly bool _useCache;

            internal TablePatternInformation(AutomationElement el, bool useCache) {
                this._el = el;
                this._useCache = useCache;
            }

            public AutomationElement[] GetRowHeaders() {
                return (AutomationElement[]) (AutomationElementCollection) this._el.GetPatternPropertyValue(property: RowHeadersProperty, useCache: this._useCache);
            }

            public AutomationElement[] GetColumnHeaders() {
                return (AutomationElement[]) (AutomationElementCollection) this._el.GetPatternPropertyValue(property: ColumnHeadersProperty, useCache: this._useCache);
            }

            public int RowCount {
                get { return (int) this._el.GetPatternPropertyValue(property: RowCountProperty, useCache: this._useCache); }
            }

            public int ColumnCount {
                get { return (int) this._el.GetPatternPropertyValue(property: ColumnCountProperty, useCache: this._useCache); }
            }

            public RowOrColumnMajor RowOrColumnMajor {
                get { return (RowOrColumnMajor) this._el.GetPatternPropertyValue(property: RowOrColumnMajorProperty, useCache: this._useCache); }
            }
        }
    }
}