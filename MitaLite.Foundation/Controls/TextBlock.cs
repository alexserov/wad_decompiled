// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.TextBlock
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Text;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class TextBlock : UIObject, IText
  {
    private static IFactory<TextBlock> _factory;
    private IText _textPattern;

    public TextBlock(UIObject uiObject)
      : base(uiObject)
      => this.Initialize();

    public TextBlock(AutomationElement element)
      : base(element)
      => this.Initialize();

    private void Initialize() => this._textPattern = (IText) new TextImplementation((UIObject) this);

    public string DocumentText => this._textPattern.DocumentRange.GetText(-1);

    public List<TextPatternRange> TextRegionsByWord => this.GetTextPatternRanges(TextUnit.Word);

    public List<TextPatternRange> TextRegionsByLine => this.GetTextPatternRanges(TextUnit.Line);

    protected List<TextPatternRange> GetTextPatternRanges(TextUnit textUnit)
    {
      List<TextPatternRange> textPatternRangeList = new List<TextPatternRange>();
      TextPatternRange documentRange = this._textPattern.DocumentRange;
      documentRange.ExpandToEnclosingUnit(TextUnit.Format);
      do
      {
        textPatternRangeList.Add(documentRange);
      }
      while (documentRange.Move(textUnit, 1) != 0);
      return textPatternRangeList;
    }

    public virtual bool SupportsTextSelection => this._textPattern.SupportsTextSelection;

    public virtual TextPatternRange DocumentRange => this._textPattern.DocumentRange;

    public virtual TextPatternRange GetSelection() => this._textPattern.GetSelection();

    public virtual TextPatternRange RangeFromPoint(PointI screenLocation) => this._textPattern.RangeFromPoint(screenLocation);

    public virtual TextPatternRange RangeFromChild(UIObject childElement) => this._textPattern.RangeFromChild(childElement);

    public virtual TextPatternRange GetVisibleRange() => this._textPattern.GetVisibleRange();

    public static IFactory<TextBlock> Factory
    {
      get
      {
        if (TextBlock._factory == null)
          TextBlock._factory = (IFactory<TextBlock>) new TextBlock.TextBlockFactory();
        return TextBlock._factory;
      }
    }

    private class TextBlockFactory : IFactory<TextBlock>
    {
      public TextBlock Create(UIObject element) => new TextBlock(element);
    }
  }
}
