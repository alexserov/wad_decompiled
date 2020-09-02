// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.SelectionItemPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class SelectionItemPattern : BasePattern {
        public static readonly AutomationPattern Pattern = SelectionItemPatternIdentifiers.Pattern;
        public static readonly AutomationProperty SelectionContainerProperty = SelectionItemPatternIdentifiers.SelectionContainerProperty;
        public static readonly AutomationProperty IsSelectedProperty = SelectionItemPatternIdentifiers.IsSelectedProperty;
        public static readonly AutomationEvent ElementAddedToSelectionEvent = SelectionItemPatternIdentifiers.ElementAddedToSelectionEvent;
        public static readonly AutomationEvent ElementRemovedFromSelectionEvent = SelectionItemPatternIdentifiers.ElementRemovedFromSelectionEvent;
        public static readonly AutomationEvent ElementSelectedEvent = SelectionItemPatternIdentifiers.ElementSelectedEvent;
        readonly IUIAutomationSelectionItemPattern _selectionItemPattern;

        SelectionItemPattern(
            AutomationElement element,
            IUIAutomationSelectionItemPattern selectionItemPattern)
            : base(el: element) {
            this._selectionItemPattern = selectionItemPattern;
        }

        public SelectionItemPatternInformation Cached {
            get { return new SelectionItemPatternInformation(el: this._el, useCache: true); }
        }

        public SelectionItemPatternInformation Current {
            get { return new SelectionItemPatternInformation(el: this._el, useCache: false); }
        }

        internal static SelectionItemPattern Wrap(
            AutomationElement element,
            IUIAutomationSelectionItemPattern selectionItemPattern) {
            return new SelectionItemPattern(element: element, selectionItemPattern: selectionItemPattern);
        }

        public void Select() {
            this._selectionItemPattern.Select();
        }

        public void AddToSelection() {
            this._selectionItemPattern.AddToSelection();
        }

        public void RemoveFromSelection() {
            this._selectionItemPattern.RemoveFromSelection();
        }

        public struct SelectionItemPatternInformation {
            readonly AutomationElement _el;
            readonly bool _useCache;

            internal SelectionItemPatternInformation(AutomationElement el, bool useCache) {
                this._el = el;
                this._useCache = useCache;
            }

            public bool IsSelected {
                get { return (bool) this._el.GetPatternPropertyValue(property: IsSelectedProperty, useCache: this._useCache); }
            }

            public AutomationElement SelectionContainer {
                get { return (AutomationElement) this._el.GetPatternPropertyValue(property: SelectionContainerProperty, useCache: this._useCache); }
            }
        }
    }
}