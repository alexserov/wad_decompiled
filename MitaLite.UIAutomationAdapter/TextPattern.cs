// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TextPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Windows.Automation.Text;
using System.Windows.Types;
using UIAutomationClient;

namespace System.Windows.Automation
{
  public class TextPattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = TextPatternIdentifiers.Pattern;
    public static readonly object MixedAttributeValue = TextPatternIdentifiers.MixedAttributeValue;
    public static readonly AutomationTextAttribute AnnotationTypesAttribute = TextPatternIdentifiers.AnnotationTypesAttribute;
    public static readonly AutomationTextAttribute AnimationStyleAttribute = TextPatternIdentifiers.AnimationStyleAttribute;
    public static readonly AutomationTextAttribute BackgroundColorAttribute = TextPatternIdentifiers.BackgroundColorAttribute;
    public static readonly AutomationTextAttribute BulletStyleAttribute = TextPatternIdentifiers.BulletStyleAttribute;
    public static readonly AutomationTextAttribute CapStyleAttribute = TextPatternIdentifiers.CapStyleAttribute;
    public static readonly AutomationTextAttribute CultureAttribute = TextPatternIdentifiers.CultureAttribute;
    public static readonly AutomationTextAttribute FontNameAttribute = TextPatternIdentifiers.FontNameAttribute;
    public static readonly AutomationTextAttribute FontSizeAttribute = TextPatternIdentifiers.FontSizeAttribute;
    public static readonly AutomationTextAttribute FontWeightAttribute = TextPatternIdentifiers.FontWeightAttribute;
    public static readonly AutomationTextAttribute ForegroundColorAttribute = TextPatternIdentifiers.ForegroundColorAttribute;
    public static readonly AutomationTextAttribute HorizontalTextAlignmentAttribute = TextPatternIdentifiers.HorizontalTextAlignmentAttribute;
    public static readonly AutomationTextAttribute IndentationFirstLineAttribute = TextPatternIdentifiers.IndentationFirstLineAttribute;
    public static readonly AutomationTextAttribute IndentationLeadingAttribute = TextPatternIdentifiers.IndentationLeadingAttribute;
    public static readonly AutomationTextAttribute IndentationTrailingAttribute = TextPatternIdentifiers.IndentationTrailingAttribute;
    public static readonly AutomationTextAttribute IsHiddenAttribute = TextPatternIdentifiers.IsHiddenAttribute;
    public static readonly AutomationTextAttribute IsItalicAttribute = TextPatternIdentifiers.IsItalicAttribute;
    public static readonly AutomationTextAttribute IsReadOnlyAttribute = TextPatternIdentifiers.IsReadOnlyAttribute;
    public static readonly AutomationTextAttribute IsSubscriptAttribute = TextPatternIdentifiers.IsSubscriptAttribute;
    public static readonly AutomationTextAttribute IsSuperscriptAttribute = TextPatternIdentifiers.IsSuperscriptAttribute;
    public static readonly AutomationTextAttribute MarginBottomAttribute = TextPatternIdentifiers.MarginBottomAttribute;
    public static readonly AutomationTextAttribute MarginLeadingAttribute = TextPatternIdentifiers.MarginLeadingAttribute;
    public static readonly AutomationTextAttribute MarginTopAttribute = TextPatternIdentifiers.MarginTopAttribute;
    public static readonly AutomationTextAttribute MarginTrailingAttribute = TextPatternIdentifiers.MarginTrailingAttribute;
    public static readonly AutomationTextAttribute OutlineStylesAttribute = TextPatternIdentifiers.OutlineStylesAttribute;
    public static readonly AutomationTextAttribute OverlineColorAttribute = TextPatternIdentifiers.OverlineColorAttribute;
    public static readonly AutomationTextAttribute OverlineStyleAttribute = TextPatternIdentifiers.OverlineStyleAttribute;
    public static readonly AutomationTextAttribute StrikethroughColorAttribute = TextPatternIdentifiers.StrikethroughColorAttribute;
    public static readonly AutomationTextAttribute StrikethroughStyleAttribute = TextPatternIdentifiers.StrikethroughStyleAttribute;
    public static readonly AutomationTextAttribute TabsAttribute = TextPatternIdentifiers.TabsAttribute;
    public static readonly AutomationTextAttribute TextFlowDirectionsAttribute = TextPatternIdentifiers.TextFlowDirectionsAttribute;
    public static readonly AutomationTextAttribute UnderlineColorAttribute = TextPatternIdentifiers.UnderlineColorAttribute;
    public static readonly AutomationTextAttribute UnderlineStyleAttribute = TextPatternIdentifiers.UnderlineStyleAttribute;
    public static readonly AutomationEvent TextChangedEvent = TextPatternIdentifiers.TextChangedEvent;
    public static readonly AutomationEvent TextSelectionChangedEvent = TextPatternIdentifiers.TextSelectionChangedEvent;
    private readonly IUIAutomationTextPattern _textPattern;

    internal TextPattern(AutomationElement element, IUIAutomationTextPattern textPattern)
      : base(element)
      => this._textPattern = textPattern;

    internal static TextPattern Wrap(
      AutomationElement element,
      IUIAutomationTextPattern pattern) => new TextPattern(element, pattern);

    public TextPatternRange[] GetSelection()
    {
      IUIAutomationTextRangeArray selection = this._textPattern.GetSelection();
      TextPatternRange[] textPatternRangeArray = new TextPatternRange[selection.Length];
      for (int index = 0; index < selection.Length; ++index)
        textPatternRangeArray[index] = new TextPatternRange(selection.GetElement(index));
      return textPatternRangeArray;
    }

    public TextPatternRange[] GetVisibleRanges()
    {
      IUIAutomationTextRangeArray visibleRanges = this._textPattern.GetVisibleRanges();
      TextPatternRange[] textPatternRangeArray = new TextPatternRange[visibleRanges.Length];
      for (int index = 0; index < visibleRanges.Length; ++index)
        textPatternRangeArray[index] = new TextPatternRange(visibleRanges.GetElement(index));
      return textPatternRangeArray;
    }

    public TextPatternRange RangeFromChild(AutomationElement childElement)
    {
      if (childElement == (AutomationElement) null)
        throw new ArgumentNullException("childElement is null");
      return new TextPatternRange(this._textPattern.RangeFromChild(childElement.IUIAutomationElement));
    }

    public TextPatternRange RangeFromPoint(Point screenLocation) => new TextPatternRange(this._textPattern.RangeFromPoint(new tagPOINT()
    {
      x = (int) screenLocation.X,
      y = (int) screenLocation.Y
    }));

    public TextPatternRange DocumentRange => new TextPatternRange(this._textPattern.DocumentRange);

    public SupportedTextSelection SupportedTextSelection => UiaConvert.Convert(this._textPattern.SupportedTextSelection);

    public enum AnnotationType
    {
      Unknown = 60000, // 0x0000EA60
      SpellingError = 60001, // 0x0000EA61
      GrammarError = 60002, // 0x0000EA62
      Comment = 60003, // 0x0000EA63
      FormulaError = 60004, // 0x0000EA64
      TrackChanges = 60005, // 0x0000EA65
      Header = 60006, // 0x0000EA66
      Footer = 60007, // 0x0000EA67
      Highlighted = 60008, // 0x0000EA68
      Endnote = 60009, // 0x0000EA69
      Footnote = 60010, // 0x0000EA6A
    }
  }
}
