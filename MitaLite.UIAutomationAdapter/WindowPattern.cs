// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.WindowPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class WindowPattern : BasePattern {
        public static readonly AutomationPattern Pattern = WindowPatternIdentifiers.Pattern;
        public static readonly AutomationProperty CanMaximizeProperty = WindowPatternIdentifiers.CanMaximizeProperty;
        public static readonly AutomationProperty CanMinimizeProperty = WindowPatternIdentifiers.CanMinimizeProperty;
        public static readonly AutomationProperty IsModalProperty = WindowPatternIdentifiers.IsModalProperty;
        public static readonly AutomationProperty IsTopmostProperty = WindowPatternIdentifiers.IsTopmostProperty;
        public static readonly AutomationProperty WindowInteractionStateProperty = WindowPatternIdentifiers.WindowInteractionStateProperty;
        public static readonly AutomationProperty WindowVisualStateProperty = WindowPatternIdentifiers.WindowVisualStateProperty;
        public static readonly AutomationEvent WindowClosedEvent = WindowPatternIdentifiers.WindowClosedEvent;
        public static readonly AutomationEvent WindowOpenedEvent = WindowPatternIdentifiers.WindowOpenedEvent;
        readonly IUIAutomationWindowPattern _windowPattern;

        WindowPattern(AutomationElement element, IUIAutomationWindowPattern windowPattern)
            : base(el: element) {
            this._windowPattern = windowPattern;
        }

        public WindowPatternInformation Cached {
            get { return new WindowPatternInformation(el: this._el, useCache: true); }
        }

        public WindowPatternInformation Current {
            get { return new WindowPatternInformation(el: this._el, useCache: false); }
        }

        internal static WindowPattern Wrap(
            AutomationElement element,
            IUIAutomationWindowPattern windowPattern) {
            return new WindowPattern(element: element, windowPattern: windowPattern);
        }

        public void SetWindowVisualState(WindowVisualState state) {
            this._windowPattern.SetWindowVisualState(state: UiaConvert.Convert(state: state));
        }

        public void Close() {
            this._windowPattern.Close();
        }

        public bool WaitForInputIdle(int milliseconds) {
            return this._windowPattern.WaitForInputIdle(milliseconds: milliseconds) != 0;
        }

        public struct WindowPatternInformation {
            readonly AutomationElement _el;
            readonly bool _useCache;

            internal WindowPatternInformation(AutomationElement el, bool useCache) {
                this._el = el;
                this._useCache = useCache;
            }

            public bool CanMaximize {
                get { return (bool) this._el.GetPatternPropertyValue(property: CanMaximizeProperty, useCache: this._useCache); }
            }

            public bool CanMinimize {
                get { return (bool) this._el.GetPatternPropertyValue(property: CanMinimizeProperty, useCache: this._useCache); }
            }

            public bool IsModal {
                get { return (bool) this._el.GetPatternPropertyValue(property: IsModalProperty, useCache: this._useCache); }
            }

            public WindowVisualState WindowVisualState {
                get { return (WindowVisualState) this._el.GetPatternPropertyValue(property: WindowVisualStateProperty, useCache: this._useCache); }
            }

            public WindowInteractionState WindowInteractionState {
                get { return (WindowInteractionState) this._el.GetPatternPropertyValue(property: WindowInteractionStateProperty, useCache: this._useCache); }
            }

            public bool IsTopmost {
                get { return (bool) this._el.GetPatternPropertyValue(property: IsTopmostProperty, useCache: this._useCache); }
            }
        }
    }
}