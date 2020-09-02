// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.RichTextBlock
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Text;

namespace MS.Internal.Mita.Foundation.Controls {
    public class RichTextBlock : TextBlock {
        static IFactory<RichTextBlock> _factory;

        public RichTextBlock(UIObject uiObject)
            : base(uiObject: uiObject) {
        }

        public RichTextBlock(AutomationElement element)
            : base(element: element) {
        }

        public List<TextPatternRange> TextRegionsByFormat {
            get { return GetTextPatternRanges(textUnit: TextUnit.Format); }
        }

        public List<TextPatternRange> TextRegionsByParagraph {
            get { return GetTextPatternRanges(textUnit: TextUnit.Paragraph); }
        }

        public List<TextPatternRange> TextRegionsByPage {
            get { return GetTextPatternRanges(textUnit: TextUnit.Page); }
        }

        public static IFactory<RichTextBlock> Factory {
            get {
                if (_factory == null)
                    _factory = new RichTextBlockFactory();
                return _factory;
            }
        }

        class RichTextBlockFactory : IFactory<RichTextBlock> {
            public RichTextBlock Create(UIObject element) {
                return new RichTextBlock(uiObject: element);
            }
        }
    }
}