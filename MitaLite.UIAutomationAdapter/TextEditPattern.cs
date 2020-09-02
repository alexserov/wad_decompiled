// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TextEditPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Windows.Automation.Text;
using UIAutomationClient;

namespace System.Windows.Automation
{
  public class TextEditPattern : TextPattern
  {
    public new static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<TextEditPattern, IUIAutomationTextEditPattern>(10032, "TextEditPatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationTextEditPattern, TextEditPattern>(TextEditPattern.Wrap));
    private readonly IUIAutomationTextEditPattern textEditPattern;

    public TextEditPattern(AutomationElement element, IUIAutomationTextEditPattern textEditPattern)
      : base(element, (IUIAutomationTextPattern) textEditPattern)
      => this.textEditPattern = textEditPattern;

    internal static TextEditPattern Wrap(
      AutomationElement element,
      IUIAutomationTextEditPattern textEditPattern) => new TextEditPattern(element, textEditPattern);

    public TextPatternRange GetActiveComposition() => new TextPatternRange(this.textEditPattern.GetActiveComposition());

    public TextPatternRange GetConversionTarget() => new TextPatternRange(this.textEditPattern.GetConversionTarget());
  }
}
