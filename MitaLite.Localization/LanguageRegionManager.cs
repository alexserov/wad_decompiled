// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Localization.LanguageRegionManager
// Assembly: MitaLite.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FE6CFDB-BB88-427C-96A4-C26318ECB83B
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Localization.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MS.Internal.Mita.Localization
{
  public class LanguageRegionManager
  {
    internal const char LANGUAGE_DELIMITER = ';';
    private static Dictionary<string, int> countryIdtoGeoIdDict = new Dictionary<string, int>();
    internal static INativeMethods nativeMethods = (INativeMethods) new NativeMethods();

    static LanguageRegionManager()
    {
      LanguageRegionManager.countryIdtoGeoIdDict.Add("ZA", 209);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("ET", 73);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("SA", 205);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("AZ", 5);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("BY", 29);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("BG", 35);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("BD", 23);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("ES", 217);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("CZ", 75);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("DK", 61);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("DE", 94);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("GR", 98);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("GB", 242);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("US", 244);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("MX", 166);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("EE", 70);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("IR", 116);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("FI", 77);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("PH", 201);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("CA", 39);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("FR", 84);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("NG", 175);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("IL", 117);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("IN", 113);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("HR", 108);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("HU", 109);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("ID", 111);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("IS", 110);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("IT", 118);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("JP", 122);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("KZ", 137);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("KH", 40);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("KR", 134);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("LA", 138);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("LT", 141);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("LV", 140);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("MK", 19618);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("MY", 167);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("NO", 177);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("NL", 176);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("PL", 191);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("BR", 32);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("PT", 193);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("RO", 200);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("RU", 203);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("SK", 143);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("SI", 212);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("AL", 6);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("RS", 271);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("SE", 221);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("KE", 129);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("TH", 227);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("TR", 235);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("UA", 241);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("UZ", 247);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("VN", 251);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("CN", 45);
      LanguageRegionManager.countryIdtoGeoIdDict.Add("TW", 237);
    }

    public static void SetHomeRegion(string countryId)
    {
      if (!LanguageRegionManager.countryIdtoGeoIdDict.ContainsKey(countryId.ToUpperInvariant()))
        throw new LanguageRegionManagerException(string.Format("Country mapping with the specified id was not found: {0}\nPlease look at the documentation for supported values.", (object) countryId));
      LanguageRegionManager.nativeMethods.SetUserGeoID(LanguageRegionManager.countryIdtoGeoIdDict[countryId.ToUpperInvariant()]);
    }

    public static void InstallLanguage(string languageId)
    {
      IList<string> installedLanguages = LanguageRegionManager.GetInstalledLanguages();
      if (installedLanguages.Contains(languageId))
        return;
      StringBuilder stringBuilder = new StringBuilder();
      foreach (string str in (IEnumerable<string>) installedLanguages)
      {
        stringBuilder.Append(str);
        stringBuilder.Append(';');
      }
      stringBuilder.Append(languageId);
      LanguageRegionManager.nativeMethods.SetUserLanguages(stringBuilder.ToString(), ';');
      Thread.Sleep(1000);
    }

    public static void SetLanguage(string languageId)
    {
      IList<string> installedLanguages = LanguageRegionManager.GetInstalledLanguages();
      if (!installedLanguages.Contains(languageId))
        throw new LanguageRegionManagerException(string.Format("Language specified does not exist on the system: {0}", (object) languageId));
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(languageId);
      foreach (string str in (IEnumerable<string>) installedLanguages)
      {
        if (!str.Equals(languageId, StringComparison.OrdinalIgnoreCase))
        {
          stringBuilder.Append(';');
          stringBuilder.Append(str);
        }
      }
      LanguageRegionManager.nativeMethods.SetUserLanguages(stringBuilder.ToString(), ';');
      Thread.Sleep(1000);
    }

    public static IList<string> GetInstalledLanguages()
    {
      List<string> stringList = new List<string>();
      string userLanguages = LanguageRegionManager.nativeMethods.GetUserLanguages(';');
      char[] chArray = new char[1]{ ';' };
      foreach (string str in userLanguages.Split(chArray))
        stringList.Add(str);
      return (IList<string>) stringList;
    }
  }
}
