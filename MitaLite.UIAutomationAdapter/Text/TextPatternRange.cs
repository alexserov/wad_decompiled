// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.Text.TextPatternRange
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Collections.Generic;
using System.Windows.Types;
using UIAutomationAdapter.Utilities;
using UIAutomationClient;

namespace System.Windows.Automation.Text {
    public class TextPatternRange {
        internal TextPatternRange(IUIAutomationTextRange textPatternRange) {
            Validate.ArgumentNotNull(parameter: textPatternRange, parameterName: nameof(textPatternRange));
            IUIAutomationTextRange = textPatternRange;
        }

        internal IUIAutomationTextRange IUIAutomationTextRange { get; }

        public void AddToSelection() {
            IUIAutomationTextRange.AddToSelection();
        }

        public TextPatternRange Clone() {
            return new TextPatternRange(textPatternRange: IUIAutomationTextRange.Clone());
        }

        public bool Compare(TextPatternRange range) {
            Validate.ArgumentNotNull(parameter: range, parameterName: nameof(range));
            return Convert.ToBoolean(value: IUIAutomationTextRange.Compare(range: range.IUIAutomationTextRange));
        }

        public int CompareEndpoints(
            TextPatternRangeEndpoint endpoint,
            TextPatternRange targetRange,
            TextPatternRangeEndpoint targetEndpoint) {
            Validate.ArgumentNotNull(parameter: targetRange, parameterName: nameof(targetRange));
            return IUIAutomationTextRange.CompareEndpoints(srcEndPoint: UiaConvert.Convert(textPatternRangeEndpoint: endpoint), range: targetRange.IUIAutomationTextRange, targetEndPoint: UiaConvert.Convert(textPatternRangeEndpoint: targetEndpoint));
        }

        public void ExpandToEnclosingUnit(TextUnit unit) {
            IUIAutomationTextRange.ExpandToEnclosingUnit(TextUnit: UiaConvert.Convert(textUnit: unit));
        }

        public TextPatternRange FindAttribute(
            AutomationTextAttribute attribute,
            object value,
            bool backward) {
            Validate.ArgumentNotNull(parameter: attribute, parameterName: nameof(attribute));
            var variant = value.ToVariant();
            var textPatternRange = new TextPatternRange(textPatternRange: IUIAutomationTextRange.FindAttribute(attr: attribute.Id, val: variant, backward: Convert.ToInt32(value: backward)));
            variant.Free();
            return textPatternRange;
        }

        public TextPatternRange FindText(string text, bool backward, bool ignoreCase) {
            Validate.StringNeitherNullNorEmpty(parameter: text, parameterName: nameof(text));
            TextPatternRange textPatternRange = null;
            try {
                textPatternRange = new TextPatternRange(textPatternRange: IUIAutomationTextRange.FindText(text: text, backward: Convert.ToInt32(value: backward), ignoreCase: Convert.ToInt32(value: ignoreCase)));
            } catch (NotSupportedException ex) {
                var str = GetText(maxLength: -1).Replace(oldValue: "\n", newValue: "");
                var comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                var count = backward ? str.LastIndexOf(value: text, comparisonType: comparisonType) : str.IndexOf(value: text, comparisonType: comparisonType);
                if (count != -1) {
                    var num = count + text.Length;
                    textPatternRange = Clone();
                    textPatternRange.MoveEndpointByUnit(endpoint: TextPatternRangeEndpoint.Start, unit: TextUnit.Character, count: count);
                    textPatternRange.MoveEndpointByUnit(endpoint: TextPatternRangeEndpoint.End, unit: TextUnit.Character, count: num - str.Length);
                }
            }

            return textPatternRange;
        }

        public object GetAttributeValue(AutomationTextAttribute attribute) {
            Validate.ArgumentNotNull(parameter: attribute, parameterName: nameof(attribute));
            return IUIAutomationTextRange.GetAttributeValue(attr: attribute.Id);
        }

        public Rect[] GetBoundingRectangles() {
            var rectList = new List<Rect>();
            var typedArray = IUIAutomationTextRange.GetBoundingRectangles().ToTypedArray<double>();
            if (typedArray != null && typedArray.Length % 4 == 0 && typedArray.Length != 0)
                for (var index = 0; index + 3 < typedArray.Length; index += 4)
                    rectList.Add(item: new Rect {
                        X = typedArray[index],
                        Y = typedArray[index + 1],
                        Width = typedArray[index + 2],
                        Height = typedArray[index + 3]
                    });
            return rectList.Count > 0 ? rectList.ToArray() : null;
        }

        public AutomationElement[] GetChildren() {
            var children = IUIAutomationTextRange.GetChildren();
            var automationElementArray = new AutomationElement[children.Length];
            for (var index = 0; index < children.Length; ++index)
                automationElementArray[index] = new AutomationElement(autoElement: children.GetElement(index: index));
            return automationElementArray;
        }

        public AutomationElement GetEnclosingElement() {
            return new AutomationElement(autoElement: IUIAutomationTextRange.GetEnclosingElement());
        }

        public string GetText(int maxLength) {
            return IUIAutomationTextRange.GetText(maxLength: maxLength);
        }

        public int Move(TextUnit unit, int count) {
            return IUIAutomationTextRange.Move(unit: UiaConvert.Convert(textUnit: unit), count: count);
        }

        public void MoveEndpointByRange(
            TextPatternRangeEndpoint endpoint,
            TextPatternRange targetRange,
            TextPatternRangeEndpoint targetEndpoint) {
            Validate.ArgumentNotNull(parameter: targetRange, parameterName: nameof(targetRange));
            IUIAutomationTextRange.MoveEndpointByRange(srcEndPoint: UiaConvert.Convert(textPatternRangeEndpoint: endpoint), range: targetRange.IUIAutomationTextRange, targetEndPoint: UiaConvert.Convert(textPatternRangeEndpoint: targetEndpoint));
        }

        public int MoveEndpointByUnit(TextPatternRangeEndpoint endpoint, TextUnit unit, int count) {
            return IUIAutomationTextRange.MoveEndpointByUnit(endpoint: UiaConvert.Convert(textPatternRangeEndpoint: endpoint), unit: UiaConvert.Convert(textUnit: unit), count: count);
        }

        public void RemoveFromSelection() {
            IUIAutomationTextRange.RemoveFromSelection();
        }

        public void ScrollIntoView(bool alignToTop) {
            IUIAutomationTextRange.ScrollIntoView(alignToTop: Convert.ToInt32(value: alignToTop));
        }

        public void Select() {
            IUIAutomationTextRange.Select();
        }
    }
}