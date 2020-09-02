// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TextPattern2
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Windows.Automation.Text;
using UIAutomationClient;

namespace System.Windows.Automation
{
  public class TextPattern2 : TextPattern
  {
    public new static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<TextPattern2, IUIAutomationTextPattern2>(10024, "TextPattern2Identifiers.Pattern", new Func<AutomationElement, IUIAutomationTextPattern2, TextPattern2>(TextPattern2.Wrap));
    private readonly IUIAutomationTextPattern2 _textPattern2;

    private TextPattern2(AutomationElement element, IUIAutomationTextPattern2 textPattern2)
      : base(element, (IUIAutomationTextPattern) textPattern2)
      => this._textPattern2 = textPattern2;

    internal static TextPattern2 Wrap(
      AutomationElement element,
      IUIAutomationTextPattern2 textPattern2) => new TextPattern2(element, textPattern2);

    public TextPatternRange RangeFromAnnotation(AutomationElement annotationElement)
    {
      if (annotationElement == (AutomationElement) null)
        throw new ArgumentNullException("annotationElement is null");
      return new TextPatternRange(this._textPattern2.RangeFromAnnotation(annotationElement.IUIAutomationElement));
    }

    public TextPatternRange GetCaretRange(out int isActive) => new TextPatternRange(this._textPattern2.GetCaretRange(out isActive));
  }
}
