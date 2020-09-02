// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TextPatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Runtime.InteropServices;
using UIAutomationClient;

namespace System.Windows.Automation
{
  public static class TextPatternIdentifiers
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<TextPattern, IUIAutomationTextPattern>(10014, "TextPatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationTextPattern, TextPattern>(TextPattern.Wrap));
    public static readonly object MixedAttributeValue = TextPatternIdentifiers.UiaGetReservedMixedAttributeValue();
    public static readonly AutomationTextAttribute AnnotationTypesAttribute = new AutomationTextAttribute(40031, "TextPatternIdentifiers.AnnotationTypesAttribute");
    public static readonly AutomationTextAttribute AnimationStyleAttribute = new AutomationTextAttribute(40000, "TextPatternIdentifiers.AnimationStyleAttribute");
    public static readonly AutomationTextAttribute BackgroundColorAttribute = new AutomationTextAttribute(40001, "TextPatternIdentifiers.BackgroundColorAttribute");
    public static readonly AutomationTextAttribute BulletStyleAttribute = new AutomationTextAttribute(40002, "TextPatternIdentifiers.BulletStyleAttribute");
    public static readonly AutomationTextAttribute CapStyleAttribute = new AutomationTextAttribute(40003, "TextPatternIdentifiers.CapStyleAttribute");
    public static readonly AutomationTextAttribute CultureAttribute = new AutomationTextAttribute(40004, "TextPatternIdentifiers.CultureAttribute");
    public static readonly AutomationTextAttribute FontNameAttribute = new AutomationTextAttribute(40005, "TextPatternIdentifiers.FontNameAttribute");
    public static readonly AutomationTextAttribute FontSizeAttribute = new AutomationTextAttribute(40006, "TextPatternIdentifiers.FontSizeAttribute");
    public static readonly AutomationTextAttribute FontWeightAttribute = new AutomationTextAttribute(40007, "TextPatternIdentifiers.FontWeightAttribute");
    public static readonly AutomationTextAttribute ForegroundColorAttribute = new AutomationTextAttribute(40008, "TextPatternIdentifiers.ForegroundColorAttribute");
    public static readonly AutomationTextAttribute HorizontalTextAlignmentAttribute = new AutomationTextAttribute(40009, "TextPatternIdentifiers.HorizontalTextAlignmentAttribute");
    public static readonly AutomationTextAttribute IndentationFirstLineAttribute = new AutomationTextAttribute(40010, "TextPatternIdentifiers.IndentationFirstLineAttribute");
    public static readonly AutomationTextAttribute IndentationLeadingAttribute = new AutomationTextAttribute(40011, "TextPatternIdentifiers.IndentationLeadingAttribute");
    public static readonly AutomationTextAttribute IndentationTrailingAttribute = new AutomationTextAttribute(40012, "TextPatternIdentifiers.IndentationTrailingAttribute");
    public static readonly AutomationTextAttribute IsHiddenAttribute = new AutomationTextAttribute(40013, "TextPatternIdentifiers.IsHiddenAttribute");
    public static readonly AutomationTextAttribute IsItalicAttribute = new AutomationTextAttribute(40014, "TextPatternIdentifiers.IsItalicAttribute");
    public static readonly AutomationTextAttribute IsReadOnlyAttribute = new AutomationTextAttribute(40015, "TextPatternIdentifiers.IsReadOnlyAttribute");
    public static readonly AutomationTextAttribute IsSubscriptAttribute = new AutomationTextAttribute(40016, "TextPatternIdentifiers.IsSubscriptAttribute");
    public static readonly AutomationTextAttribute IsSuperscriptAttribute = new AutomationTextAttribute(40017, "TextPatternIdentifiers.IsSuperscriptAttribute");
    public static readonly AutomationTextAttribute MarginBottomAttribute = new AutomationTextAttribute(40018, "TextPatternIdentifiers.MarginBottomAttribute");
    public static readonly AutomationTextAttribute MarginLeadingAttribute = new AutomationTextAttribute(40019, "TextPatternIdentifiers.MarginLeadingAttribute");
    public static readonly AutomationTextAttribute MarginTopAttribute = new AutomationTextAttribute(40020, "TextPatternIdentifiers.MarginTopAttribute");
    public static readonly AutomationTextAttribute MarginTrailingAttribute = new AutomationTextAttribute(40021, "TextPatternIdentifiers.MarginTrailingAttribute");
    public static readonly AutomationTextAttribute OutlineStylesAttribute = new AutomationTextAttribute(40022, "TextPatternIdentifiers.OutlineStylesAttribute");
    public static readonly AutomationTextAttribute OverlineColorAttribute = new AutomationTextAttribute(40023, "TextPatternIdentifiers.OverlineColorAttribute");
    public static readonly AutomationTextAttribute OverlineStyleAttribute = new AutomationTextAttribute(40024, "TextPatternIdentifiers.OverlineStyleAttribute");
    public static readonly AutomationTextAttribute StrikethroughColorAttribute = new AutomationTextAttribute(40025, "TextPatternIdentifiers.StrikethroughColorAttribute");
    public static readonly AutomationTextAttribute StrikethroughStyleAttribute = new AutomationTextAttribute(40026, "TextPatternIdentifiers.StrikethroughStyleAttribute");
    public static readonly AutomationTextAttribute TabsAttribute = new AutomationTextAttribute(40027, "TextPatternIdentifiers.TabsAttribute");
    public static readonly AutomationTextAttribute TextFlowDirectionsAttribute = new AutomationTextAttribute(40028, "TextPatternIdentifiers.TextFlowDirectionsAttribute");
    public static readonly AutomationTextAttribute UnderlineColorAttribute = new AutomationTextAttribute(40029, "TextPatternIdentifiers.UnderlineColorAttribute");
    public static readonly AutomationTextAttribute UnderlineStyleAttribute = new AutomationTextAttribute(40030, "TextPatternIdentifiers.UnderlineStyleAttribute");
    public static readonly AutomationEvent TextChangedEvent = new AutomationEvent(20015, "TextPatternIdentifiers.TextChangedEvent");
    public static readonly AutomationEvent TextSelectionChangedEvent = new AutomationEvent(20014, "TextPatternIdentifiers.TextSelectionChangedEvent");

    private static object UiaGetReservedMixedAttributeValue()
    {
      object mixedAttributeValue;
      Marshal.ThrowExceptionForHR(TextPatternIdentifiers.RawUiaGetReservedMixedAttributeValue(out mixedAttributeValue));
      return mixedAttributeValue;
    }

    [DllImport("UIAutomationCore.dll", EntryPoint = "UiaGetReservedNotSupportedValue", CharSet = CharSet.Unicode)]
    private static extern int RawUiaGetReservedMixedAttributeValue([MarshalAs(UnmanagedType.IUnknown)] out object mixedAttributeValue);
  }
}
