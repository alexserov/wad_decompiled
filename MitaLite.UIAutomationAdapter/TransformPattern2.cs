// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TransformPattern2
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class TransformPattern2 : TransformPattern {
        public new static readonly AutomationPattern Pattern = new AutomationPattern<TransformPattern2, IUIAutomationTransformPattern2>(id: 10028, programmaticName: "TransformPattern2Identifiers.Pattern", wrap: Wrap);
        public static readonly AutomationProperty CanZoomProperty = new AutomationProperty(id: 30133, programmaticName: "TransformPattern2Identifiers.CanZoomProperty");
        public static readonly AutomationProperty ZoomLevelProperty = new AutomationProperty(id: 30145, programmaticName: "TransformPattern2Identifiers.ZoomLevelProperty");
        public static readonly AutomationProperty ZoomMinimumProperty = new AutomationProperty(id: 30146, programmaticName: "TransformPattern2Identifiers.ZoomMinimumProperty");
        public static readonly AutomationProperty ZoomMaximumProperty = new AutomationProperty(id: 30147, programmaticName: "TransformPattern2Identifiers.ZoomMaximumProperty");
        readonly IUIAutomationTransformPattern2 _transformPattern2;

        TransformPattern2(
            AutomationElement element,
            IUIAutomationTransformPattern2 transformPattern2)
            : base(element: element, transformPattern: transformPattern2) {
            this._transformPattern2 = transformPattern2;
        }

        internal static TransformPattern2 Wrap(
            AutomationElement element,
            IUIAutomationTransformPattern2 transformPattern2) {
            return new TransformPattern2(element: element, transformPattern2: transformPattern2);
        }
    }
}