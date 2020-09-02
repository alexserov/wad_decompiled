// Decompiled with JetBrains decompiler
// Type: MitaBroker.Utilities
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using MS.Internal.Mita.Foundation;
using System;
using System.Xml;

namespace MitaBroker
{
  internal static class Utilities
  {
    public static int[] GetRuntimeIdParts(string RuntimeIdString)
    {
      string[] strArray = RuntimeIdString.Split('.');
      int length = strArray.Length;
      int[] numArray = new int[length];
      for (int index = 0; index < length; ++index)
        numArray[index] = int.Parse(strArray[index]);
      return numArray;
    }

    public static string GetElementIdFromElement(UIObject element) => !string.IsNullOrEmpty(element.RuntimeId) ? element.RuntimeId : Guid.NewGuid().ToString();

    public static string GetElementIdFromXmlNode(XmlNode node)
    {
      string str = node.Attributes["RuntimeId"].Value;
      if (str == string.Empty)
      {
        if (node.Attributes["ElementId"] != null)
          str = node.Attributes["ElementId"].Value;
        if (str == string.Empty)
          throw new Exception("Unexpected element state encountered while building DOM: no unique ID present");
      }
      return str;
    }
  }
}
