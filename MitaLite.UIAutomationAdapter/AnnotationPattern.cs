// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AnnotationPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class AnnotationPattern : BasePattern {
        public static readonly AutomationPattern Pattern = new AutomationPattern<AnnotationPattern, IUIAutomationAnnotationPattern>(id: 10023, programmaticName: "AnnotationPatternIdentifiers.Pattern", wrap: Wrap);
        public static readonly AutomationProperty AnnotationTypeIdProperty = new AutomationProperty(id: 30113, programmaticName: "AnnotationPatternIdentifiers.AnnotationTypeIdProperty");
        public static readonly AutomationProperty AnnotationTypeNameProperty = new AutomationProperty(id: 30114, programmaticName: "AnnotationPatternIdentifiers.AnnotationTypeNameProperty");
        public static readonly AutomationProperty AuthorProperty = new AutomationProperty(id: 30115, programmaticName: "AnnotationPatternIdentifiers.AuthorProperty");
        public static readonly AutomationProperty DateTimeProperty = new AutomationProperty(id: 30116, programmaticName: "AnnotationPatternIdentifiers.DateTimeProperty");
        public static readonly AutomationProperty TargetProperty = new AutomationProperty(id: 30117, programmaticName: "AnnotationPatternIdentifiers.TargetProperty");
        readonly IUIAutomationAnnotationPattern _annotationPattern;

        AnnotationPattern(
            AutomationElement element,
            IUIAutomationAnnotationPattern annotationPattern)
            : base(el: element) {
            this._annotationPattern = annotationPattern;
        }

        internal static AnnotationPattern Wrap(
            AutomationElement element,
            IUIAutomationAnnotationPattern annotationPattern) {
            return new AnnotationPattern(element: element, annotationPattern: annotationPattern);
        }
    }
}