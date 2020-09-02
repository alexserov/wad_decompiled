// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Localization.NonLocalizingProvider
// Assembly: MitaLite.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FE6CFDB-BB88-427C-96A4-C26318ECB83B
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Localization.dll

using System.Globalization;

namespace MS.Internal.Mita.Localization
{
  public class NonLocalizingProvider : ILocalizationProvider
  {
    public virtual IStringResourceData LoadExplicit(
      string resourceKey,
      string context,
      int processId,
      CultureInfo culture) => (IStringResourceData) new StringResourceData(string.Empty);

    public virtual IStringResourceData[] RetrieveSimilarStrings(
      string nativeText,
      string context,
      int processId,
      CultureInfo culture) => new IStringResourceData[1]
    {
      (IStringResourceData) new StringResourceData(nativeText)
    };

    public virtual bool FoundSimilarMatch(string nativeText, string context, long index) => false;
  }
}
