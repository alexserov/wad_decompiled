// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.Text.TextPatternRange
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Collections.Generic;
using System.Windows.Types;
using UIAutomationAdapter.Utilities;
using UIAutomationClient;

namespace System.Windows.Automation.Text
{
  public class TextPatternRange
  {
    private IUIAutomationTextRange _textPatternRange;

    internal TextPatternRange(IUIAutomationTextRange textPatternRange)
    {
      Validate.ArgumentNotNull((object) textPatternRange, nameof (textPatternRange));
      this._textPatternRange = textPatternRange;
    }

    public void AddToSelection() => this._textPatternRange.AddToSelection();

    public TextPatternRange Clone() => new TextPatternRange(this._textPatternRange.Clone());

    public bool Compare(TextPatternRange range)
    {
      Validate.ArgumentNotNull((object) range, nameof (range));
      return Convert.ToBoolean(this._textPatternRange.Compare(range.IUIAutomationTextRange));
    }

    public int CompareEndpoints(
      System.Windows.Automation.TextPatternRangeEndpoint endpoint,
      TextPatternRange targetRange,
      System.Windows.Automation.TextPatternRangeEndpoint targetEndpoint)
    {
      Validate.ArgumentNotNull((object) targetRange, nameof (targetRange));
      return this._textPatternRange.CompareEndpoints(UiaConvert.Convert(endpoint), targetRange.IUIAutomationTextRange, UiaConvert.Convert(targetEndpoint));
    }

    public void ExpandToEnclosingUnit(System.Windows.Automation.TextUnit unit) => this._textPatternRange.ExpandToEnclosingUnit(UiaConvert.Convert(unit));

    public TextPatternRange FindAttribute(
      AutomationTextAttribute attribute,
      object value,
      bool backward)
    {
      Validate.ArgumentNotNull((object) attribute, nameof (attribute));
      Variant variant = value.ToVariant();
      TextPatternRange textPatternRange = new TextPatternRange(this._textPatternRange.FindAttribute(attribute.Id, variant, Convert.ToInt32(backward)));
      variant.Free();
      return textPatternRange;
    }

    public TextPatternRange FindText(string text, bool backward, bool ignoreCase)
    {
      Validate.StringNeitherNullNorEmpty(text, nameof (text));
      TextPatternRange textPatternRange = (TextPatternRange) null;
      try
      {
        textPatternRange = new TextPatternRange(this._textPatternRange.FindText(text, Convert.ToInt32(backward), Convert.ToInt32(ignoreCase)));
      }
      catch (NotSupportedException ex)
      {
        string str = this.GetText(-1).Replace("\n", "");
        StringComparison comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
        int count = backward ? str.LastIndexOf(text, comparisonType) : str.IndexOf(text, comparisonType);
        if (count != -1)
        {
          int num = count + text.Length;
          textPatternRange = this.Clone();
          textPatternRange.MoveEndpointByUnit(System.Windows.Automation.TextPatternRangeEndpoint.Start, System.Windows.Automation.TextUnit.Character, count);
          textPatternRange.MoveEndpointByUnit(System.Windows.Automation.TextPatternRangeEndpoint.End, System.Windows.Automation.TextUnit.Character, num - str.Length);
        }
      }
      return textPatternRange;
    }

    public object GetAttributeValue(AutomationTextAttribute attribute)
    {
      Validate.ArgumentNotNull((object) attribute, nameof (attribute));
      return (object) this._textPatternRange.GetAttributeValue(attribute.Id);
    }

    public Rect[] GetBoundingRectangles()
    {
      List<Rect> rectList = new List<Rect>();
      double[] typedArray = this._textPatternRange.GetBoundingRectangles().ToTypedArray<double>();
      if (typedArray != null && typedArray.Length % 4 == 0 && typedArray.Length != 0)
      {
        for (int index = 0; index + 3 < typedArray.Length; index += 4)
          rectList.Add(new Rect()
          {
            X = typedArray[index],
            Y = typedArray[index + 1],
            Width = typedArray[index + 2],
            Height = typedArray[index + 3]
          });
      }
      return rectList.Count > 0 ? rectList.ToArray() : (Rect[]) null;
    }

    public AutomationElement[] GetChildren()
    {
      IUIAutomationElementArray children = this._textPatternRange.GetChildren();
      AutomationElement[] automationElementArray = new AutomationElement[children.Length];
      for (int index = 0; index < children.Length; ++index)
        automationElementArray[index] = new AutomationElement(children.GetElement(index));
      return automationElementArray;
    }

    public AutomationElement GetEnclosingElement() => new AutomationElement(this._textPatternRange.GetEnclosingElement());

    public string GetText(int maxLength) => this._textPatternRange.GetText(maxLength);

    public int Move(System.Windows.Automation.TextUnit unit, int count) => this._textPatternRange.Move(UiaConvert.Convert(unit), count);

    public void MoveEndpointByRange(
      System.Windows.Automation.TextPatternRangeEndpoint endpoint,
      TextPatternRange targetRange,
      System.Windows.Automation.TextPatternRangeEndpoint targetEndpoint)
    {
      Validate.ArgumentNotNull((object) targetRange, nameof (targetRange));
      this._textPatternRange.MoveEndpointByRange(UiaConvert.Convert(endpoint), targetRange.IUIAutomationTextRange, UiaConvert.Convert(targetEndpoint));
    }

    public int MoveEndpointByUnit(System.Windows.Automation.TextPatternRangeEndpoint endpoint, System.Windows.Automation.TextUnit unit, int count) => this._textPatternRange.MoveEndpointByUnit(UiaConvert.Convert(endpoint), UiaConvert.Convert(unit), count);

    public void RemoveFromSelection() => this._textPatternRange.RemoveFromSelection();

    public void ScrollIntoView(bool alignToTop) => this._textPatternRange.ScrollIntoView(Convert.ToInt32(alignToTop));

    public void Select() => this._textPatternRange.Select();

    internal IUIAutomationTextRange IUIAutomationTextRange => this._textPatternRange;
  }
}
