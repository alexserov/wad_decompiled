// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Localization.ILocalizationProvider2
// Assembly: MitaLite.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FE6CFDB-BB88-427C-96A4-C26318ECB83B
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Localization.dll

using System.Globalization;

namespace MS.Internal.Mita.Localization
{
  public interface ILocalizationProvider2
  {
    IStringResourceData LoadExplicit(
      string resourceKey,
      string context,
      int processId,
      CultureInfo culture);

    IStringResourceData[] RetrieveSimilarStrings(
      string nativeText,
      object contextElement);

    bool FoundSimilarMatch(object element, string nativeText, long index);
  }
}
