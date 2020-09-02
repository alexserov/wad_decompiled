// Decompiled with JetBrains decompiler
// Type: MitaBroker.FindElementByAbsoluteXPath
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation;

namespace MitaBroker {
    internal class FindElementByAbsoluteXPath {
        static readonly List<string> s_listExpectedProperty = new List<string> {
            "Name",
            "ClassName",
            "AutomationId"
        };

        static string XmlDecode(string strData) {
            if (strData != null) {
                strData = strData.Replace(oldValue: "&amp;", newValue: "&");
                strData = strData.Replace(oldValue: "&quot;", newValue: "\"");
                strData = strData.Replace(oldValue: "&apos;", newValue: "'");
                strData = strData.Replace(oldValue: "&lt;", newValue: "<");
                strData = strData.Replace(oldValue: "&gt;", newValue: ">");
                strData = strData.Replace(oldValue: "\\\\", newValue: "\\");
            }

            return strData;
        }

        static Dictionary<string, CompareMethodValue> GetTagAndAttributes(
            string xpath,
            ref string xpathToGo,
            ref bool bIsSimpleAttributeExpression) {
            var source = new Dictionary<string, CompareMethodValue>();
            var stringList = new List<string>();
            var stringBuilder = new StringBuilder();
            string str1 = null;
            var startIndex = 0;
            var num = 0;
            foreach (var ch in xpath) {
                switch (ch) {
                    case '/':
                        if (num == 0) {
                            if (str1 == null && startIndex > 0) {
                                str1 = stringBuilder.ToString();
                                goto label_13;
                            }

                            if (str1 == null)
                                break;
                            goto label_13;
                        } else {
                            goto default;
                        }
                    case '[':
                        ++num;
                        if (stringBuilder.Length > 0 && str1 == null && num == 1) {
                            str1 = stringBuilder.ToString();
                            stringBuilder.Clear();
                        }

                        break;
                    case ']':
                        --num;
                        if (num == 0) {
                            stringList.Add(item: stringBuilder.ToString());
                            stringBuilder.Clear();
                        }

                        break;
                    default:
                        stringBuilder.Append(value: ch);
                        break;
                }

                ++startIndex;
            }

            label_13:
            xpathToGo = xpath.Substring(startIndex: startIndex);
            CompareMethodValue compareMethodValue = null;
            if (!string.IsNullOrWhiteSpace(value: str1) && str1.Length > 1)
                compareMethodValue = new CompareMethodValue(method: CompareMethod.Equal, value: "ControlType." + str1);
            else if (stringBuilder.Length > 1)
                compareMethodValue = new CompareMethodValue(method: CompareMethod.Equal, value: "ControlType." + stringBuilder);
            if (compareMethodValue != null) {
                source.Add(key: "Tag", value: compareMethodValue);
                foreach (var input in stringList) {
                    var count = source.Count;
                    var match1 = new Regex(pattern: "@([^=]+)=([^\\]]+)", options: RegexOptions.IgnoreCase).Match(input: input);
                    if (match1.Success && match1.Groups.Count == 3) {
                        var strData = match1.Groups[groupnum: 2].Value;
                        if (strData.StartsWith(value: "\"") || strData.StartsWith(value: "'"))
                            strData = strData.Substring(startIndex: 1, length: strData.Length - 2);
                        if (!s_listExpectedProperty.Contains(item: match1.Groups[groupnum: 1].Value)) {
                            bIsSimpleAttributeExpression = false;
                            return source;
                        }

                        source.Add(key: match1.Groups[groupnum: 1].Value, value: new CompareMethodValue(method: CompareMethod.Equal, value: XmlDecode(strData: strData)));
                    } else {
                        var match2 = new Regex(pattern: "(starts-with|contains)\\(@([^,]+)\\,([^\\]]+)", options: RegexOptions.IgnoreCase).Match(input: input);
                        if (match2.Success && match2.Groups.Count == 4) {
                            var strData = match2.Groups[groupnum: 3].Value;
                            if (strData.StartsWith(value: "\"") || strData.StartsWith(value: "'"))
                                strData = strData.Substring(startIndex: 1, length: strData.Length - 3);
                            if (!s_listExpectedProperty.Contains(item: match2.Groups[groupnum: 2].Value)) {
                                bIsSimpleAttributeExpression = false;
                                return source;
                            }

                            var method = match2.Groups[groupnum: 1].Value == "starts-with" ? CompareMethod.StartsWith : CompareMethod.Contains;
                            var str2 = XmlDecode(strData: strData);
                            source.Add(key: match2.Groups[groupnum: 2].Value, value: new CompareMethodValue(method: method, value: str2));
                        } else {
                            var match3 = new Regex(pattern: "position\\(\\)=(\\d+)", options: RegexOptions.IgnoreCase).Match(input: input);
                            if (match3.Success && match3.Groups.Count == 2)
                                source.Add(key: "position", value: new CompareMethodValue(method: CompareMethod.Position, value: match3.Groups[groupnum: 1].Value));
                            if (source.Count == count) {
                                bIsSimpleAttributeExpression = false;
                                return source;
                            }
                        }
                    }
                }

                if (source.FirstOrDefault(predicate: attr => new Regex(pattern: "\\s+[^\\s]+\\s+@", options: RegexOptions.IgnoreCase).Match(input: attr.Value.CompareValue).Success).Value == null)
                    return source;
                bIsSimpleAttributeExpression = false;
                return null;
            }

            bIsSimpleAttributeExpression = false;
            return source;
        }

        public static object GetPropValue(object src, string propName) {
            return src.GetType().GetProperty(name: propName).GetValue(obj: src, index: null);
        }

        public static void FindChildrenWithMatchingAttributes(
            UIObject rootObject,
            string xpath,
            List<UIObject> foundList,
            ref bool bIsSimpleAttributeExpression,
            bool isFindSingle = false) {
            if (!bIsSimpleAttributeExpression)
                return;
            string xpathToGo = null;
            var tagAndAttributes = GetTagAndAttributes(xpath: xpath, xpathToGo: ref xpathToGo, bIsSimpleAttributeExpression: ref bIsSimpleAttributeExpression);
            if (!bIsSimpleAttributeExpression)
                return;
            var num = 0;
            foreach (var child in rootObject.Children) {
                ++num;
                var flag = true;
                foreach (var keyValuePair in tagAndAttributes)
                    if (keyValuePair.Key.Equals(value: "Tag", comparisonType: StringComparison.OrdinalIgnoreCase)) {
                        ControlType controlType;
                        try {
                            controlType = child.ControlType;
                        } catch (Exception ex) {
                            flag = false;
                            break;
                        }

                        if (controlType.ProgrammaticName.ToLower() != keyValuePair.Value.CompareValue.ToLower()) {
                            flag = false;
                            break;
                        }
                    } else if (keyValuePair.Key.Equals(value: "position", comparisonType: StringComparison.OrdinalIgnoreCase)) {
                        var result = -1;
                        int.TryParse(s: keyValuePair.Value.CompareValue, result: out result);
                        if (result != num) {
                            flag = false;
                            break;
                        }
                    } else {
                        var propValue = (string) GetPropValue(src: child, propName: keyValuePair.Key);
                        if (propValue == null) {
                            bIsSimpleAttributeExpression = false;
                            return;
                        }

                        if (keyValuePair.Value.CompareMethod == CompareMethod.Equal) {
                            if (!propValue.Equals(value: keyValuePair.Value.CompareValue, comparisonType: StringComparison.OrdinalIgnoreCase)) {
                                flag = false;
                                break;
                            }
                        } else if (keyValuePair.Value.CompareMethod == CompareMethod.StartsWith) {
                            if (!propValue.StartsWith(value: keyValuePair.Value.CompareValue, comparisonType: StringComparison.OrdinalIgnoreCase)) {
                                flag = false;
                                break;
                            }
                        } else if (keyValuePair.Value.CompareMethod == CompareMethod.Contains && propValue.IndexOf(value: keyValuePair.Value.CompareValue, comparisonType: StringComparison.OrdinalIgnoreCase) < 0) {
                            flag = false;
                            break;
                        }
                    }

                if (flag) {
                    if (string.IsNullOrEmpty(value: xpathToGo)) {
                        foundList.Add(item: child);
                        if (isFindSingle)
                            break;
                    } else {
                        FindChildrenWithMatchingAttributes(rootObject: child, xpath: xpathToGo, foundList: foundList, bIsSimpleAttributeExpression: ref bIsSimpleAttributeExpression);
                    }
                }
            }
        }

        static UIObject FindChildWithMatchingAttributes(
            UIObject ae,
            string xpath,
            ref bool bIsSimpleAttributeExpression) {
            if (!bIsSimpleAttributeExpression)
                return null;
            string xpathToGo = null;
            var tagAndAttributes = GetTagAndAttributes(xpath: xpath, xpathToGo: ref xpathToGo, bIsSimpleAttributeExpression: ref bIsSimpleAttributeExpression);
            if (!bIsSimpleAttributeExpression)
                return null;
            var num = 0;
            foreach (var child in ae.Children) {
                ++num;
                var flag = true;
                foreach (var keyValuePair in tagAndAttributes)
                    if (keyValuePair.Key == "Tag") {
                        ControlType controlType;
                        try {
                            controlType = child.ControlType;
                        } catch (Exception ex) {
                            flag = false;
                            break;
                        }

                        if (controlType.ProgrammaticName.ToLower() != keyValuePair.Value.CompareValue.ToLower()) {
                            flag = false;
                            break;
                        }
                    } else if (keyValuePair.Key == "position") {
                        var result = -1;
                        int.TryParse(s: keyValuePair.Value.CompareValue, result: out result);
                        if (result != num) {
                            flag = false;
                            break;
                        }
                    } else {
                        var propValue = (string) GetPropValue(src: child, propName: keyValuePair.Key);
                        if (propValue == null) {
                            bIsSimpleAttributeExpression = false;
                            return null;
                        }

                        if (keyValuePair.Value.CompareMethod == CompareMethod.Equal) {
                            if (propValue != keyValuePair.Value.CompareValue) {
                                flag = false;
                                break;
                            }
                        } else if (keyValuePair.Value.CompareMethod == CompareMethod.StartsWith) {
                            if (!propValue.StartsWith(value: keyValuePair.Value.CompareValue)) {
                                flag = false;
                                break;
                            }
                        } else if (keyValuePair.Value.CompareMethod == CompareMethod.Contains && !propValue.Contains(value: keyValuePair.Value.CompareValue)) {
                            flag = false;
                            break;
                        }
                    }

                if (flag) {
                    if (string.IsNullOrEmpty(value: xpathToGo))
                        return child;
                    var matchingAttributes = FindChildWithMatchingAttributes(ae: child, xpath: xpathToGo, bIsSimpleAttributeExpression: ref bIsSimpleAttributeExpression);
                    if (matchingAttributes != null || !bIsSimpleAttributeExpression)
                        return matchingAttributes;
                }
            }

            return null;
        }

        public static UIObject FindTarget(
            string xpath,
            out string runtimeid,
            ref bool bIsSimpleAttributeExpression,
            UIObject rootUIObject,
            bool isPreview) {
            var uiObject1 = rootUIObject;
            if ((object) uiObject1 == null)
                uiObject1 = UIObject.Root;
            rootUIObject = uiObject1;
            runtimeid = string.Empty;
            UIObject uiObject2;
            if (isPreview) {
                var uiObjectList = new List<UIObject>();
                FindChildrenWithMatchingAttributes(rootObject: rootUIObject, xpath: xpath, foundList: uiObjectList, bIsSimpleAttributeExpression: ref bIsSimpleAttributeExpression, isFindSingle: true);
                uiObject2 = uiObjectList.FirstOrDefault();
            } else {
                uiObject2 = FindChildWithMatchingAttributes(ae: rootUIObject, xpath: xpath, bIsSimpleAttributeExpression: ref bIsSimpleAttributeExpression);
            }

            if (!(uiObject2 != null))
                return null;
            runtimeid = uiObject2.RuntimeId;
            if (runtimeid.Length == 0)
                runtimeid = xpath.GetHashCode().ToString();
            return new UIObject(uiObject: uiObject2);
        }
    }
}