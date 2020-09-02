// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TextPattern2
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Windows.Automation.Text;
using UIAutomationClient;

namespace System.Windows.Automation {
    public class TextPattern2 : TextPattern {
        public new static readonly AutomationPattern Pattern = new AutomationPattern<TextPattern2, IUIAutomationTextPattern2>(id: 10024, programmaticName: "TextPattern2Identifiers.Pattern", wrap: Wrap);
        readonly IUIAutomationTextPattern2 _textPattern2;

        TextPattern2(AutomationElement element, IUIAutomationTextPattern2 textPattern2)
            : base(element: element, textPattern: textPattern2) {
            this._textPattern2 = textPattern2;
        }

        internal static TextPattern2 Wrap(
            AutomationElement element,
            IUIAutomationTextPattern2 textPattern2) {
            return new TextPattern2(element: element, textPattern2: textPattern2);
        }

        public TextPatternRange RangeFromAnnotation(AutomationElement annotationElement) {
            if (annotationElement == null)
                throw new ArgumentNullException(paramName: "annotationElement is null");
            return new TextPatternRange(textPatternRange: this._textPattern2.RangeFromAnnotation(annotation: annotationElement.IUIAutomationElement));
        }

        public TextPatternRange GetCaretRange(out int isActive) {
            return new TextPatternRange(textPatternRange: this._textPattern2.GetCaretRange(isActive: out isActive));
        }
    }
}