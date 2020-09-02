// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TextChildPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class TextChildPattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<TextChildPattern, IUIAutomationTextChildPattern>(10029, "TextChildPatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationTextChildPattern, TextChildPattern>(TextChildPattern.Wrap));
    private readonly IUIAutomationTextChildPattern _textChildPattern;

    private TextChildPattern(
      AutomationElement element,
      IUIAutomationTextChildPattern textChildPattern)
      : base(element)
      => this._textChildPattern = textChildPattern;

    internal static TextChildPattern Wrap(
      AutomationElement element,
      IUIAutomationTextChildPattern textChildPattern) => new TextChildPattern(element, textChildPattern);
  }
}
