// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TextPatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Runtime.InteropServices;
using UIAutomationClient;

namespace System.Windows.Automation {
    public static class TextPatternIdentifiers {
        public static readonly AutomationPattern Pattern = new AutomationPattern<TextPattern, IUIAutomationTextPattern>(id: 10014, programmaticName: "TextPatternIdentifiers.Pattern", wrap: TextPattern.Wrap);
        public static readonly object MixedAttributeValue = UiaGetReservedMixedAttributeValue();
        public static readonly AutomationTextAttribute AnnotationTypesAttribute = new AutomationTextAttribute(id: 40031, programmaticName: "TextPatternIdentifiers.AnnotationTypesAttribute");
        public static readonly AutomationTextAttribute AnimationStyleAttribute = new AutomationTextAttribute(id: 40000, programmaticName: "TextPatternIdentifiers.AnimationStyleAttribute");
        public static readonly AutomationTextAttribute BackgroundColorAttribute = new AutomationTextAttribute(id: 40001, programmaticName: "TextPatternIdentifiers.BackgroundColorAttribute");
        public static readonly AutomationTextAttribute BulletStyleAttribute = new AutomationTextAttribute(id: 40002, programmaticName: "TextPatternIdentifiers.BulletStyleAttribute");
        public static readonly AutomationTextAttribute CapStyleAttribute = new AutomationTextAttribute(id: 40003, programmaticName: "TextPatternIdentifiers.CapStyleAttribute");
        public static readonly AutomationTextAttribute CultureAttribute = new AutomationTextAttribute(id: 40004, programmaticName: "TextPatternIdentifiers.CultureAttribute");
        public static readonly AutomationTextAttribute FontNameAttribute = new AutomationTextAttribute(id: 40005, programmaticName: "TextPatternIdentifiers.FontNameAttribute");
        public static readonly AutomationTextAttribute FontSizeAttribute = new AutomationTextAttribute(id: 40006, programmaticName: "TextPatternIdentifiers.FontSizeAttribute");
        public static readonly AutomationTextAttribute FontWeightAttribute = new AutomationTextAttribute(id: 40007, programmaticName: "TextPatternIdentifiers.FontWeightAttribute");
        public static readonly AutomationTextAttribute ForegroundColorAttribute = new AutomationTextAttribute(id: 40008, programmaticName: "TextPatternIdentifiers.ForegroundColorAttribute");
        public static readonly AutomationTextAttribute HorizontalTextAlignmentAttribute = new AutomationTextAttribute(id: 40009, programmaticName: "TextPatternIdentifiers.HorizontalTextAlignmentAttribute");
        public static readonly AutomationTextAttribute IndentationFirstLineAttribute = new AutomationTextAttribute(id: 40010, programmaticName: "TextPatternIdentifiers.IndentationFirstLineAttribute");
        public static readonly AutomationTextAttribute IndentationLeadingAttribute = new AutomationTextAttribute(id: 40011, programmaticName: "TextPatternIdentifiers.IndentationLeadingAttribute");
        public static readonly AutomationTextAttribute IndentationTrailingAttribute = new AutomationTextAttribute(id: 40012, programmaticName: "TextPatternIdentifiers.IndentationTrailingAttribute");
        public static readonly AutomationTextAttribute IsHiddenAttribute = new AutomationTextAttribute(id: 40013, programmaticName: "TextPatternIdentifiers.IsHiddenAttribute");
        public static readonly AutomationTextAttribute IsItalicAttribute = new AutomationTextAttribute(id: 40014, programmaticName: "TextPatternIdentifiers.IsItalicAttribute");
        public static readonly AutomationTextAttribute IsReadOnlyAttribute = new AutomationTextAttribute(id: 40015, programmaticName: "TextPatternIdentifiers.IsReadOnlyAttribute");
        public static readonly AutomationTextAttribute IsSubscriptAttribute = new AutomationTextAttribute(id: 40016, programmaticName: "TextPatternIdentifiers.IsSubscriptAttribute");
        public static readonly AutomationTextAttribute IsSuperscriptAttribute = new AutomationTextAttribute(id: 40017, programmaticName: "TextPatternIdentifiers.IsSuperscriptAttribute");
        public static readonly AutomationTextAttribute MarginBottomAttribute = new AutomationTextAttribute(id: 40018, programmaticName: "TextPatternIdentifiers.MarginBottomAttribute");
        public static readonly AutomationTextAttribute MarginLeadingAttribute = new AutomationTextAttribute(id: 40019, programmaticName: "TextPatternIdentifiers.MarginLeadingAttribute");
        public static readonly AutomationTextAttribute MarginTopAttribute = new AutomationTextAttribute(id: 40020, programmaticName: "TextPatternIdentifiers.MarginTopAttribute");
        public static readonly AutomationTextAttribute MarginTrailingAttribute = new AutomationTextAttribute(id: 40021, programmaticName: "TextPatternIdentifiers.MarginTrailingAttribute");
        public static readonly AutomationTextAttribute OutlineStylesAttribute = new AutomationTextAttribute(id: 40022, programmaticName: "TextPatternIdentifiers.OutlineStylesAttribute");
        public static readonly AutomationTextAttribute OverlineColorAttribute = new AutomationTextAttribute(id: 40023, programmaticName: "TextPatternIdentifiers.OverlineColorAttribute");
        public static readonly AutomationTextAttribute OverlineStyleAttribute = new AutomationTextAttribute(id: 40024, programmaticName: "TextPatternIdentifiers.OverlineStyleAttribute");
        public static readonly AutomationTextAttribute StrikethroughColorAttribute = new AutomationTextAttribute(id: 40025, programmaticName: "TextPatternIdentifiers.StrikethroughColorAttribute");
        public static readonly AutomationTextAttribute StrikethroughStyleAttribute = new AutomationTextAttribute(id: 40026, programmaticName: "TextPatternIdentifiers.StrikethroughStyleAttribute");
        public static readonly AutomationTextAttribute TabsAttribute = new AutomationTextAttribute(id: 40027, programmaticName: "TextPatternIdentifiers.TabsAttribute");
        public static readonly AutomationTextAttribute TextFlowDirectionsAttribute = new AutomationTextAttribute(id: 40028, programmaticName: "TextPatternIdentifiers.TextFlowDirectionsAttribute");
        public static readonly AutomationTextAttribute UnderlineColorAttribute = new AutomationTextAttribute(id: 40029, programmaticName: "TextPatternIdentifiers.UnderlineColorAttribute");
        public static readonly AutomationTextAttribute UnderlineStyleAttribute = new AutomationTextAttribute(id: 40030, programmaticName: "TextPatternIdentifiers.UnderlineStyleAttribute");
        public static readonly AutomationEvent TextChangedEvent = new AutomationEvent(id: 20015, programmaticName: "TextPatternIdentifiers.TextChangedEvent");
        public static readonly AutomationEvent TextSelectionChangedEvent = new AutomationEvent(id: 20014, programmaticName: "TextPatternIdentifiers.TextSelectionChangedEvent");

        static object UiaGetReservedMixedAttributeValue() {
            object mixedAttributeValue;
            Marshal.ThrowExceptionForHR(errorCode: RawUiaGetReservedMixedAttributeValue(mixedAttributeValue: out mixedAttributeValue));
            return mixedAttributeValue;
        }

        [DllImport(dllName: "UIAutomationCore.dll", EntryPoint = "UiaGetReservedNotSupportedValue", CharSet = CharSet.Unicode)]
        static extern int RawUiaGetReservedMixedAttributeValue([MarshalAs(unmanagedType: UnmanagedType.IUnknown)]
                                                               out object mixedAttributeValue);
    }
}