// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Utilities.RuntimeId
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Globalization;
using System.Text;

namespace MS.Internal.Mita.Foundation.Utilities {
    internal static class RuntimeId {
        const char _delimiter = '.';

        public static int[] PartsFromString(string runtimeIdString) {
            var strArray = runtimeIdString.Split(separator: '.');
            var length = strArray.Length;
            var numArray = new int[length];
            for (var index = 0; index < length; ++index)
                numArray[index] = int.Parse(s: strArray[index], provider: CultureInfo.InvariantCulture);
            return numArray;
        }

        public static string StringFromParts(int[] runtimeIdParts) {
            var index1 = runtimeIdParts.Length - 1;
            var stringBuilder = new StringBuilder();
            for (var index2 = 0; index2 < index1; ++index2) {
                stringBuilder.Append(value: Convert.ToString(value: runtimeIdParts[index2], provider: CultureInfo.InvariantCulture));
                stringBuilder.Append(value: '.');
            }

            if (index1 >= 0)
                stringBuilder.Append(value: Convert.ToString(value: runtimeIdParts[index1], provider: CultureInfo.InvariantCulture));
            return stringBuilder.ToString();
        }
    }
}