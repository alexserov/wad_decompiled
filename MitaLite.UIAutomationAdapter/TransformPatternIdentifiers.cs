// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TransformPatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public static class TransformPatternIdentifiers {
        public static readonly AutomationPattern Pattern = new AutomationPattern<TransformPattern, IUIAutomationTransformPattern>(id: 10016, programmaticName: "TransformPatternIdentifiers.Pattern", wrap: TransformPattern.Wrap);
        public static readonly AutomationProperty CanMoveProperty = new AutomationProperty(id: 30087, programmaticName: "TransformPatternIdentifiers.CanMoveProperty");
        public static readonly AutomationProperty CanResizeProperty = new AutomationProperty(id: 30088, programmaticName: "TransformPatternIdentifiers.CanResizeProperty");
        public static readonly AutomationProperty CanRotateProperty = new AutomationProperty(id: 30089, programmaticName: "TransformPatternIdentifiers.CanRotateProperty");
    }
}