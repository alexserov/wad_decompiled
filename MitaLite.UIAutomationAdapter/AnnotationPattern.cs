// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AnnotationPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class AnnotationPattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<AnnotationPattern, IUIAutomationAnnotationPattern>(10023, "AnnotationPatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationAnnotationPattern, AnnotationPattern>(AnnotationPattern.Wrap));
    public static readonly AutomationProperty AnnotationTypeIdProperty = new AutomationProperty(30113, "AnnotationPatternIdentifiers.AnnotationTypeIdProperty");
    public static readonly AutomationProperty AnnotationTypeNameProperty = new AutomationProperty(30114, "AnnotationPatternIdentifiers.AnnotationTypeNameProperty");
    public static readonly AutomationProperty AuthorProperty = new AutomationProperty(30115, "AnnotationPatternIdentifiers.AuthorProperty");
    public static readonly AutomationProperty DateTimeProperty = new AutomationProperty(30116, "AnnotationPatternIdentifiers.DateTimeProperty");
    public static readonly AutomationProperty TargetProperty = new AutomationProperty(30117, "AnnotationPatternIdentifiers.TargetProperty");
    private readonly IUIAutomationAnnotationPattern _annotationPattern;

    private AnnotationPattern(
      AutomationElement element,
      IUIAutomationAnnotationPattern annotationPattern)
      : base(element)
      => this._annotationPattern = annotationPattern;

    internal static AnnotationPattern Wrap(
      AutomationElement element,
      IUIAutomationAnnotationPattern annotationPattern) => new AnnotationPattern(element, annotationPattern);
  }
}
