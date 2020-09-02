// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Utilities.RuntimeId
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Globalization;
using System.Text;

namespace MS.Internal.Mita.Foundation.Utilities
{
  internal static class RuntimeId
  {
    private const char _delimiter = '.';

    public static int[] PartsFromString(string runtimeIdString)
    {
      string[] strArray = runtimeIdString.Split('.');
      int length = strArray.Length;
      int[] numArray = new int[length];
      for (int index = 0; index < length; ++index)
        numArray[index] = int.Parse(strArray[index], (IFormatProvider) CultureInfo.InvariantCulture);
      return numArray;
    }

    public static string StringFromParts(int[] runtimeIdParts)
    {
      int index1 = runtimeIdParts.Length - 1;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index2 = 0; index2 < index1; ++index2)
      {
        stringBuilder.Append(Convert.ToString(runtimeIdParts[index2], (IFormatProvider) CultureInfo.InvariantCulture));
        stringBuilder.Append('.');
      }
      if (index1 >= 0)
        stringBuilder.Append(Convert.ToString(runtimeIdParts[index1], (IFormatProvider) CultureInfo.InvariantCulture));
      return stringBuilder.ToString();
    }
  }
}
