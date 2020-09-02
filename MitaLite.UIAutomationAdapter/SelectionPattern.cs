// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.SelectionPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class SelectionPattern : BasePattern {
        public static readonly AutomationPattern Pattern = SelectionPatternIdentifiers.Pattern;
        public static readonly AutomationProperty CanSelectMultipleProperty = SelectionPatternIdentifiers.CanSelectMultipleProperty;
        public static readonly AutomationProperty IsSelectionRequiredProperty = SelectionPatternIdentifiers.IsSelectionRequiredProperty;
        public static readonly AutomationProperty SelectionProperty = SelectionPatternIdentifiers.SelectionProperty;
        public static readonly AutomationEvent InvalidatedEvent = SelectionPatternIdentifiers.InvalidatedEvent;
        readonly IUIAutomationSelectionPattern _selectionPattern;

        SelectionPattern(
            AutomationElement element,
            IUIAutomationSelectionPattern selectionPattern)
            : base(el: element) {
            this._selectionPattern = selectionPattern;
        }

        public SelectionPatternInformation Cached {
            get { return new SelectionPatternInformation(el: this._el, useCache: true); }
        }

        public SelectionPatternInformation Current {
            get { return new SelectionPatternInformation(el: this._el, useCache: false); }
        }

        internal static SelectionPattern Wrap(
            AutomationElement element,
            IUIAutomationSelectionPattern selectionPattern) {
            return new SelectionPattern(element: element, selectionPattern: selectionPattern);
        }

        public struct SelectionPatternInformation {
            readonly AutomationElement _el;
            readonly bool _useCache;

            internal SelectionPatternInformation(AutomationElement el, bool useCache) {
                this._el = el;
                this._useCache = useCache;
            }

            public AutomationElement[] GetSelection() {
                return (AutomationElement[]) (AutomationElementCollection) this._el.GetPatternPropertyValue(property: SelectionProperty, useCache: this._useCache);
            }

            public bool CanSelectMultiple {
                get { return (bool) this._el.GetPatternPropertyValue(property: CanSelectMultipleProperty, useCache: this._useCache); }
            }

            public bool IsSelectionRequired {
                get { return (bool) this._el.GetPatternPropertyValue(property: IsSelectionRequiredProperty, useCache: this._useCache); }
            }
        }
    }
}