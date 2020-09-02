// Decompiled with JetBrains decompiler
// Type: MitaBroker.FindElementByAbsoluteXPath
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using MS.Internal.Mita.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Automation;

namespace MitaBroker
{
  internal class FindElementByAbsoluteXPath
  {
    private static List<string> s_listExpectedProperty = new List<string>()
    {
      "Name",
      "ClassName",
      "AutomationId"
    };

    private static string XmlDecode(string strData)
    {
      if (strData != null)
      {
        strData = strData.Replace("&amp;", "&");
        strData = strData.Replace("&quot;", "\"");
        strData = strData.Replace("&apos;", "'");
        strData = strData.Replace("&lt;", "<");
        strData = strData.Replace("&gt;", ">");
        strData = strData.Replace("\\\\", "\\");
      }
      return strData;
    }

    private static Dictionary<string, CompareMethodValue> GetTagAndAttributes(
      string xpath,
      ref string xpathToGo,
      ref bool bIsSimpleAttributeExpression)
    {
      Dictionary<string, CompareMethodValue> source = new Dictionary<string, CompareMethodValue>();
      List<string> stringList = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      string str1 = (string) null;
      int startIndex = 0;
      int num = 0;
      foreach (char ch in xpath)
      {
        switch (ch)
        {
          case '/':
            if (num == 0)
            {
              if (str1 == null && startIndex > 0)
              {
                str1 = stringBuilder.ToString();
                goto label_13;
              }
              else
              {
                if (str1 == null)
                  break;
                goto label_13;
              }
            }
            else
              goto default;
          case '[':
            ++num;
            if (stringBuilder.Length > 0 && str1 == null && num == 1)
            {
              str1 = stringBuilder.ToString();
              stringBuilder.Clear();
              break;
            }
            break;
          case ']':
            --num;
            if (num == 0)
            {
              stringList.Add(stringBuilder.ToString());
              stringBuilder.Clear();
              break;
            }
            break;
          default:
            stringBuilder.Append(ch);
            break;
        }
        ++startIndex;
      }
label_13:
      xpathToGo = xpath.Substring(startIndex);
      CompareMethodValue compareMethodValue = (CompareMethodValue) null;
      if (!string.IsNullOrWhiteSpace(str1) && str1.Length > 1)
        compareMethodValue = new CompareMethodValue(CompareMethod.Equal, "ControlType." + str1);
      else if (stringBuilder.Length > 1)
        compareMethodValue = new CompareMethodValue(CompareMethod.Equal, "ControlType." + stringBuilder.ToString());
      if (compareMethodValue != null)
      {
        source.Add("Tag", compareMethodValue);
        foreach (string input in stringList)
        {
          int count = source.Count;
          Match match1 = new Regex("@([^=]+)=([^\\]]+)", RegexOptions.IgnoreCase).Match(input);
          if (match1.Success && match1.Groups.Count == 3)
          {
            string strData = match1.Groups[2].Value;
            if (strData.StartsWith("\"") || strData.StartsWith("'"))
              strData = strData.Substring(1, strData.Length - 2);
            if (!FindElementByAbsoluteXPath.s_listExpectedProperty.Contains(match1.Groups[1].Value))
            {
              bIsSimpleAttributeExpression = false;
              return source;
            }
            source.Add(match1.Groups[1].Value, new CompareMethodValue(CompareMethod.Equal, FindElementByAbsoluteXPath.XmlDecode(strData)));
          }
          else
          {
            Match match2 = new Regex("(starts-with|contains)\\(@([^,]+)\\,([^\\]]+)", RegexOptions.IgnoreCase).Match(input);
            if (match2.Success && match2.Groups.Count == 4)
            {
              string strData = match2.Groups[3].Value;
              if (strData.StartsWith("\"") || strData.StartsWith("'"))
                strData = strData.Substring(1, strData.Length - 3);
              if (!FindElementByAbsoluteXPath.s_listExpectedProperty.Contains(match2.Groups[2].Value))
              {
                bIsSimpleAttributeExpression = false;
                return source;
              }
              CompareMethod method = match2.Groups[1].Value == "starts-with" ? CompareMethod.StartsWith : CompareMethod.Contains;
              string str2 = FindElementByAbsoluteXPath.XmlDecode(strData);
              source.Add(match2.Groups[2].Value, new CompareMethodValue(method, str2));
            }
            else
            {
              Match match3 = new Regex("position\\(\\)=(\\d+)", RegexOptions.IgnoreCase).Match(input);
              if (match3.Success && match3.Groups.Count == 2)
                source.Add("position", new CompareMethodValue(CompareMethod.Position, match3.Groups[1].Value));
              if (source.Count == count)
              {
                bIsSimpleAttributeExpression = false;
                return source;
              }
            }
          }
        }
        if (source.FirstOrDefault<KeyValuePair<string, CompareMethodValue>>((Func<KeyValuePair<string, CompareMethodValue>, bool>) (attr => new Regex("\\s+[^\\s]+\\s+@", RegexOptions.IgnoreCase).Match(attr.Value.CompareValue).Success)).Value == null)
          return source;
        bIsSimpleAttributeExpression = false;
        return (Dictionary<string, CompareMethodValue>) null;
      }
      bIsSimpleAttributeExpression = false;
      return source;
    }

    public static object GetPropValue(object src, string propName) => src.GetType().GetProperty(propName).GetValue(src, (object[]) null);

    public static void FindChildrenWithMatchingAttributes(
      UIObject rootObject,
      string xpath,
      List<UIObject> foundList,
      ref bool bIsSimpleAttributeExpression,
      bool isFindSingle = false)
    {
      if (!bIsSimpleAttributeExpression)
        return;
      string xpathToGo = (string) null;
      Dictionary<string, CompareMethodValue> tagAndAttributes = FindElementByAbsoluteXPath.GetTagAndAttributes(xpath, ref xpathToGo, ref bIsSimpleAttributeExpression);
      if (!bIsSimpleAttributeExpression)
        return;
      int num = 0;
      foreach (UIObject child in rootObject.Children)
      {
        ++num;
        bool flag = true;
        foreach (KeyValuePair<string, CompareMethodValue> keyValuePair in tagAndAttributes)
        {
          if (keyValuePair.Key.Equals("Tag", StringComparison.OrdinalIgnoreCase))
          {
            ControlType controlType;
            try
            {
              controlType = child.ControlType;
            }
            catch (Exception ex)
            {
              flag = false;
              break;
            }
            if (controlType.ProgrammaticName.ToLower() != keyValuePair.Value.CompareValue.ToLower())
            {
              flag = false;
              break;
            }
          }
          else if (keyValuePair.Key.Equals("position", StringComparison.OrdinalIgnoreCase))
          {
            int result = -1;
            int.TryParse(keyValuePair.Value.CompareValue, out result);
            if (result != num)
            {
              flag = false;
              break;
            }
          }
          else
          {
            string propValue = (string) FindElementByAbsoluteXPath.GetPropValue((object) child, keyValuePair.Key);
            if (propValue == null)
            {
              bIsSimpleAttributeExpression = false;
              return;
            }
            if (keyValuePair.Value.CompareMethod == CompareMethod.Equal)
            {
              if (!propValue.Equals(keyValuePair.Value.CompareValue, StringComparison.OrdinalIgnoreCase))
              {
                flag = false;
                break;
              }
            }
            else if (keyValuePair.Value.CompareMethod == CompareMethod.StartsWith)
            {
              if (!propValue.StartsWith(keyValuePair.Value.CompareValue, StringComparison.OrdinalIgnoreCase))
              {
                flag = false;
                break;
              }
            }
            else if (keyValuePair.Value.CompareMethod == CompareMethod.Contains && propValue.IndexOf(keyValuePair.Value.CompareValue, StringComparison.OrdinalIgnoreCase) < 0)
            {
              flag = false;
              break;
            }
          }
        }
        if (flag)
        {
          if (string.IsNullOrEmpty(xpathToGo))
          {
            foundList.Add(child);
            if (isFindSingle)
              break;
          }
          else
            FindElementByAbsoluteXPath.FindChildrenWithMatchingAttributes(child, xpathToGo, foundList, ref bIsSimpleAttributeExpression);
        }
      }
    }

    private static UIObject FindChildWithMatchingAttributes(
      UIObject ae,
      string xpath,
      ref bool bIsSimpleAttributeExpression)
    {
      if (!bIsSimpleAttributeExpression)
        return (UIObject) null;
      string xpathToGo = (string) null;
      Dictionary<string, CompareMethodValue> tagAndAttributes = FindElementByAbsoluteXPath.GetTagAndAttributes(xpath, ref xpathToGo, ref bIsSimpleAttributeExpression);
      if (!bIsSimpleAttributeExpression)
        return (UIObject) null;
      int num = 0;
      foreach (UIObject child in ae.Children)
      {
        ++num;
        bool flag = true;
        foreach (KeyValuePair<string, CompareMethodValue> keyValuePair in tagAndAttributes)
        {
          if (keyValuePair.Key == "Tag")
          {
            ControlType controlType;
            try
            {
              controlType = child.ControlType;
            }
            catch (Exception ex)
            {
              flag = false;
              break;
            }
            if (controlType.ProgrammaticName.ToLower() != keyValuePair.Value.CompareValue.ToLower())
            {
              flag = false;
              break;
            }
          }
          else if (keyValuePair.Key == "position")
          {
            int result = -1;
            int.TryParse(keyValuePair.Value.CompareValue, out result);
            if (result != num)
            {
              flag = false;
              break;
            }
          }
          else
          {
            string propValue = (string) FindElementByAbsoluteXPath.GetPropValue((object) child, keyValuePair.Key);
            if (propValue == null)
            {
              bIsSimpleAttributeExpression = false;
              return (UIObject) null;
            }
            if (keyValuePair.Value.CompareMethod == CompareMethod.Equal)
            {
              if (propValue != keyValuePair.Value.CompareValue)
              {
                flag = false;
                break;
              }
            }
            else if (keyValuePair.Value.CompareMethod == CompareMethod.StartsWith)
            {
              if (!propValue.StartsWith(keyValuePair.Value.CompareValue))
              {
                flag = false;
                break;
              }
            }
            else if (keyValuePair.Value.CompareMethod == CompareMethod.Contains && !propValue.Contains(keyValuePair.Value.CompareValue))
            {
              flag = false;
              break;
            }
          }
        }
        if (flag)
        {
          if (string.IsNullOrEmpty(xpathToGo))
            return child;
          UIObject matchingAttributes = FindElementByAbsoluteXPath.FindChildWithMatchingAttributes(child, xpathToGo, ref bIsSimpleAttributeExpression);
          if (matchingAttributes != (UIObject) null || !bIsSimpleAttributeExpression)
            return matchingAttributes;
        }
      }
      return (UIObject) null;
    }

    public static UIObject FindTarget(
      string xpath,
      out string runtimeid,
      ref bool bIsSimpleAttributeExpression,
      UIObject rootUIObject,
      bool isPreview)
    {
      UIObject uiObject1 = rootUIObject;
      if ((object) uiObject1 == null)
        uiObject1 = UIObject.Root;
      rootUIObject = uiObject1;
      runtimeid = string.Empty;
      UIObject uiObject2;
      if (isPreview)
      {
        List<UIObject> uiObjectList = new List<UIObject>();
        FindElementByAbsoluteXPath.FindChildrenWithMatchingAttributes(rootUIObject, xpath, uiObjectList, ref bIsSimpleAttributeExpression, true);
        uiObject2 = uiObjectList.FirstOrDefault<UIObject>();
      }
      else
        uiObject2 = FindElementByAbsoluteXPath.FindChildWithMatchingAttributes(rootUIObject, xpath, ref bIsSimpleAttributeExpression);
      if (!(uiObject2 != (UIObject) null))
        return (UIObject) null;
      runtimeid = uiObject2.RuntimeId;
      if (runtimeid.Length == 0)
        runtimeid = xpath.GetHashCode().ToString();
      return new UIObject(uiObject2);
    }
  }
}
