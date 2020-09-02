// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TransformPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class TransformPattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = TransformPatternIdentifiers.Pattern;
    public static readonly AutomationProperty CanMoveProperty = TransformPatternIdentifiers.CanMoveProperty;
    public static readonly AutomationProperty CanResizeProperty = TransformPatternIdentifiers.CanResizeProperty;
    public static readonly AutomationProperty CanRotateProperty = TransformPatternIdentifiers.CanRotateProperty;
    private readonly IUIAutomationTransformPattern _transformPattern;

    internal TransformPattern(
      AutomationElement element,
      IUIAutomationTransformPattern transformPattern)
      : base(element)
      => this._transformPattern = transformPattern;

    internal static TransformPattern Wrap(
      AutomationElement element,
      IUIAutomationTransformPattern transformPattern) => new TransformPattern(element, transformPattern);

    public void Move(double x, double y) => this._transformPattern.Move(x, y);

    public void Resize(double width, double height) => this._transformPattern.Resize(width, height);

    public void Rotate(double degrees) => this._transformPattern.Rotate(degrees);

    public TransformPattern.TransformPatternInformation Cached => new TransformPattern.TransformPatternInformation(this._el, true);

    public TransformPattern.TransformPatternInformation Current => new TransformPattern.TransformPatternInformation(this._el, false);

    public struct TransformPatternInformation
    {
      private AutomationElement _el;
      private bool _useCache;

      internal TransformPatternInformation(AutomationElement el, bool useCache)
      {
        this._el = el;
        this._useCache = useCache;
      }

      public bool CanMove => (bool) this._el.GetPatternPropertyValue(TransformPattern.CanMoveProperty, this._useCache);

      public bool CanResize => (bool) this._el.GetPatternPropertyValue(TransformPattern.CanResizeProperty, this._useCache);

      public bool CanRotate => (bool) this._el.GetPatternPropertyValue(TransformPattern.CanRotateProperty, this._useCache);
    }
  }
}
