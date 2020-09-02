// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TextEditPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Windows.Automation.Text;
using UIAutomationClient;

namespace System.Windows.Automation {
    public class TextEditPattern : TextPattern {
        public new static readonly AutomationPattern Pattern = new AutomationPattern<TextEditPattern, IUIAutomationTextEditPattern>(id: 10032, programmaticName: "TextEditPatternIdentifiers.Pattern", wrap: Wrap);
        readonly IUIAutomationTextEditPattern textEditPattern;

        public TextEditPattern(AutomationElement element, IUIAutomationTextEditPattern textEditPattern)
            : base(element: element, textPattern: textEditPattern) {
            this.textEditPattern = textEditPattern;
        }

        internal static TextEditPattern Wrap(
            AutomationElement element,
            IUIAutomationTextEditPattern textEditPattern) {
            return new TextEditPattern(element: element, textEditPattern: textEditPattern);
        }

        public TextPatternRange GetActiveComposition() {
            return new TextPatternRange(textPatternRange: this.textEditPattern.GetActiveComposition());
        }

        public TextPatternRange GetConversionTarget() {
            return new TextPatternRange(textPatternRange: this.textEditPattern.GetConversionTarget());
        }
    }
}