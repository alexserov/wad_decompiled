// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TransformPattern2
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class TransformPattern2 : TransformPattern
  {
    public new static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<TransformPattern2, IUIAutomationTransformPattern2>(10028, "TransformPattern2Identifiers.Pattern", new Func<AutomationElement, IUIAutomationTransformPattern2, TransformPattern2>(TransformPattern2.Wrap));
    public static readonly AutomationProperty CanZoomProperty = new AutomationProperty(30133, "TransformPattern2Identifiers.CanZoomProperty");
    public static readonly AutomationProperty ZoomLevelProperty = new AutomationProperty(30145, "TransformPattern2Identifiers.ZoomLevelProperty");
    public static readonly AutomationProperty ZoomMinimumProperty = new AutomationProperty(30146, "TransformPattern2Identifiers.ZoomMinimumProperty");
    public static readonly AutomationProperty ZoomMaximumProperty = new AutomationProperty(30147, "TransformPattern2Identifiers.ZoomMaximumProperty");
    private readonly IUIAutomationTransformPattern2 _transformPattern2;

    private TransformPattern2(
      AutomationElement element,
      IUIAutomationTransformPattern2 transformPattern2)
      : base(element, (IUIAutomationTransformPattern) transformPattern2)
      => this._transformPattern2 = transformPattern2;

    internal static TransformPattern2 Wrap(
      AutomationElement element,
      IUIAutomationTransformPattern2 transformPattern2) => new TransformPattern2(element, transformPattern2);
  }
}
