// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.TextBlock
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Text;
using MS.Internal.Mita.Foundation.Patterns;

namespace MS.Internal.Mita.Foundation.Controls {
    public class TextBlock : UIObject, IText {
        static IFactory<TextBlock> _factory;
        IText _textPattern;

        public TextBlock(UIObject uiObject)
            : base(uiObject: uiObject) {
            Initialize();
        }

        public TextBlock(AutomationElement element)
            : base(element: element) {
            Initialize();
        }

        public string DocumentText {
            get { return this._textPattern.DocumentRange.GetText(maxLength: -1); }
        }

        public List<TextPatternRange> TextRegionsByWord {
            get { return GetTextPatternRanges(textUnit: TextUnit.Word); }
        }

        public List<TextPatternRange> TextRegionsByLine {
            get { return GetTextPatternRanges(textUnit: TextUnit.Line); }
        }

        public static IFactory<TextBlock> Factory {
            get {
                if (_factory == null)
                    _factory = new TextBlockFactory();
                return _factory;
            }
        }

        public virtual bool SupportsTextSelection {
            get { return this._textPattern.SupportsTextSelection; }
        }

        public virtual TextPatternRange DocumentRange {
            get { return this._textPattern.DocumentRange; }
        }

        public virtual TextPatternRange GetSelection() {
            return this._textPattern.GetSelection();
        }

        public virtual TextPatternRange RangeFromPoint(PointI screenLocation) {
            return this._textPattern.RangeFromPoint(screenLocation: screenLocation);
        }

        public virtual TextPatternRange RangeFromChild(UIObject childElement) {
            return this._textPattern.RangeFromChild(childElement: childElement);
        }

        public virtual TextPatternRange GetVisibleRange() {
            return this._textPattern.GetVisibleRange();
        }

        void Initialize() {
            this._textPattern = new TextImplementation(uiObject: this);
        }

        protected List<TextPatternRange> GetTextPatternRanges(TextUnit textUnit) {
            var textPatternRangeList = new List<TextPatternRange>();
            var documentRange = this._textPattern.DocumentRange;
            documentRange.ExpandToEnclosingUnit(unit: TextUnit.Format);
            do {
                textPatternRangeList.Add(item: documentRange);
            } while (documentRange.Move(unit: textUnit, count: 1) != 0);

            return textPatternRangeList;
        }

        class TextBlockFactory : IFactory<TextBlock> {
            public TextBlock Create(UIObject element) {
                return new TextBlock(uiObject: element);
            }
        }
    }
}