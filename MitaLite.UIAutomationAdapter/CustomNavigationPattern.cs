// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.CustomNavigationPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class CustomNavigationPattern : BasePattern {
        public static readonly AutomationPattern Pattern = CustomNavigationPatternIdentifiers.Pattern;
        readonly IUIAutomationCustomNavigationPattern _customNavigationPattern;

        internal CustomNavigationPattern(
            AutomationElement element,
            IUIAutomationCustomNavigationPattern CustomNavigationPattern)
            : base(el: element) {
            this._customNavigationPattern = CustomNavigationPattern;
        }

        internal static CustomNavigationPattern Wrap(
            AutomationElement element,
            IUIAutomationCustomNavigationPattern CustomNavigationPattern) {
            return new CustomNavigationPattern(element: element, CustomNavigationPattern: CustomNavigationPattern);
        }

        public AutomationElement Navigate(NavigateDirection direction) {
            return new AutomationElement(autoElement: this._customNavigationPattern.Navigate(direction: direction));
        }
    }
}