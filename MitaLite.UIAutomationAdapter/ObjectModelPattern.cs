// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ObjectModelPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class ObjectModelPattern : BasePattern {
        public static readonly AutomationPattern Pattern = new AutomationPattern<ObjectModelPattern, IUIAutomationObjectModelPattern>(id: 10022, programmaticName: "ObjectModelPatternIdentifiers.Pattern", wrap: Wrap);
        readonly IUIAutomationObjectModelPattern _objectModelPattern;

        ObjectModelPattern(
            AutomationElement element,
            IUIAutomationObjectModelPattern objectModelPattern)
            : base(el: element) {
            this._objectModelPattern = objectModelPattern;
        }

        internal static ObjectModelPattern Wrap(
            AutomationElement element,
            IUIAutomationObjectModelPattern objectModelPattern) {
            return new ObjectModelPattern(element: element, objectModelPattern: objectModelPattern);
        }

        public object GetUnderlyingObjectModel() {
            return this._objectModelPattern.GetUnderlyingObjectModel();
        }
    }
}