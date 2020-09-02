// Decompiled with JetBrains decompiler
// Type: MitaBroker.Utilities
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System;
using System.Xml;
using MS.Internal.Mita.Foundation;

namespace MitaBroker {
    internal static class Utilities {
        public static int[] GetRuntimeIdParts(string RuntimeIdString) {
            var strArray = RuntimeIdString.Split('.');
            var length = strArray.Length;
            var numArray = new int[length];
            for (var index = 0; index < length; ++index)
                numArray[index] = int.Parse(s: strArray[index]);
            return numArray;
        }

        public static string GetElementIdFromElement(UIObject element) {
            return !string.IsNullOrEmpty(value: element.RuntimeId) ? element.RuntimeId : Guid.NewGuid().ToString();
        }

        public static string GetElementIdFromXmlNode(XmlNode node) {
            var str = node.Attributes[name: "RuntimeId"].Value;
            if (str == string.Empty) {
                if (node.Attributes[name: "ElementId"] != null)
                    str = node.Attributes[name: "ElementId"].Value;
                if (str == string.Empty)
                    throw new Exception(message: "Unexpected element state encountered while building DOM: no unique ID present");
            }

            return str;
        }
    }
}