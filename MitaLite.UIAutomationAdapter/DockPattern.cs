// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.DockPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class DockPattern : BasePattern {
        public static readonly AutomationPattern Pattern = DockPatternIdentifiers.Pattern;
        public static readonly AutomationProperty DockPositionProperty = DockPatternIdentifiers.DockPositionProperty;
        readonly IUIAutomationDockPattern _dockPattern;

        DockPattern(AutomationElement element, IUIAutomationDockPattern dockPattern)
            : base(el: element) {
            this._dockPattern = dockPattern;
        }

        public DockPatternInformation Cached {
            get { return new DockPatternInformation(el: this._el, useCache: true); }
        }

        public DockPatternInformation Current {
            get { return new DockPatternInformation(el: this._el, useCache: false); }
        }

        internal static DockPattern Wrap(
            AutomationElement element,
            IUIAutomationDockPattern dockPattern) {
            return new DockPattern(element: element, dockPattern: dockPattern);
        }

        public void SetDockPosition(DockPosition dockPosition) {
            this._dockPattern.SetDockPosition(dockPos: UiaConvert.Convert(position: dockPosition));
        }

        public struct DockPatternInformation {
            readonly AutomationElement _el;
            readonly bool _useCache;

            internal DockPatternInformation(AutomationElement el, bool useCache) {
                this._el = el;
                this._useCache = useCache;
            }

            public DockPosition DockPosition {
                get { return (DockPosition) this._el.GetPatternPropertyValue(property: DockPositionProperty, useCache: this._useCache); }
            }
        }
    }
}