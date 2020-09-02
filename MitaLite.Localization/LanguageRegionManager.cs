// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Localization.LanguageRegionManager
// Assembly: MitaLite.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FE6CFDB-BB88-427C-96A4-C26318ECB83B
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Localization.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MS.Internal.Mita.Localization {
    public class LanguageRegionManager {
        internal const char LANGUAGE_DELIMITER = ';';
        static readonly Dictionary<string, int> countryIdtoGeoIdDict = new Dictionary<string, int>();
        internal static INativeMethods nativeMethods = new NativeMethods();

        static LanguageRegionManager() {
            countryIdtoGeoIdDict.Add(key: "ZA", value: 209);
            countryIdtoGeoIdDict.Add(key: "ET", value: 73);
            countryIdtoGeoIdDict.Add(key: "SA", value: 205);
            countryIdtoGeoIdDict.Add(key: "AZ", value: 5);
            countryIdtoGeoIdDict.Add(key: "BY", value: 29);
            countryIdtoGeoIdDict.Add(key: "BG", value: 35);
            countryIdtoGeoIdDict.Add(key: "BD", value: 23);
            countryIdtoGeoIdDict.Add(key: "ES", value: 217);
            countryIdtoGeoIdDict.Add(key: "CZ", value: 75);
            countryIdtoGeoIdDict.Add(key: "DK", value: 61);
            countryIdtoGeoIdDict.Add(key: "DE", value: 94);
            countryIdtoGeoIdDict.Add(key: "GR", value: 98);
            countryIdtoGeoIdDict.Add(key: "GB", value: 242);
            countryIdtoGeoIdDict.Add(key: "US", value: 244);
            countryIdtoGeoIdDict.Add(key: "MX", value: 166);
            countryIdtoGeoIdDict.Add(key: "EE", value: 70);
            countryIdtoGeoIdDict.Add(key: "IR", value: 116);
            countryIdtoGeoIdDict.Add(key: "FI", value: 77);
            countryIdtoGeoIdDict.Add(key: "PH", value: 201);
            countryIdtoGeoIdDict.Add(key: "CA", value: 39);
            countryIdtoGeoIdDict.Add(key: "FR", value: 84);
            countryIdtoGeoIdDict.Add(key: "NG", value: 175);
            countryIdtoGeoIdDict.Add(key: "IL", value: 117);
            countryIdtoGeoIdDict.Add(key: "IN", value: 113);
            countryIdtoGeoIdDict.Add(key: "HR", value: 108);
            countryIdtoGeoIdDict.Add(key: "HU", value: 109);
            countryIdtoGeoIdDict.Add(key: "ID", value: 111);
            countryIdtoGeoIdDict.Add(key: "IS", value: 110);
            countryIdtoGeoIdDict.Add(key: "IT", value: 118);
            countryIdtoGeoIdDict.Add(key: "JP", value: 122);
            countryIdtoGeoIdDict.Add(key: "KZ", value: 137);
            countryIdtoGeoIdDict.Add(key: "KH", value: 40);
            countryIdtoGeoIdDict.Add(key: "KR", value: 134);
            countryIdtoGeoIdDict.Add(key: "LA", value: 138);
            countryIdtoGeoIdDict.Add(key: "LT", value: 141);
            countryIdtoGeoIdDict.Add(key: "LV", value: 140);
            countryIdtoGeoIdDict.Add(key: "MK", value: 19618);
            countryIdtoGeoIdDict.Add(key: "MY", value: 167);
            countryIdtoGeoIdDict.Add(key: "NO", value: 177);
            countryIdtoGeoIdDict.Add(key: "NL", value: 176);
            countryIdtoGeoIdDict.Add(key: "PL", value: 191);
            countryIdtoGeoIdDict.Add(key: "BR", value: 32);
            countryIdtoGeoIdDict.Add(key: "PT", value: 193);
            countryIdtoGeoIdDict.Add(key: "RO", value: 200);
            countryIdtoGeoIdDict.Add(key: "RU", value: 203);
            countryIdtoGeoIdDict.Add(key: "SK", value: 143);
            countryIdtoGeoIdDict.Add(key: "SI", value: 212);
            countryIdtoGeoIdDict.Add(key: "AL", value: 6);
            countryIdtoGeoIdDict.Add(key: "RS", value: 271);
            countryIdtoGeoIdDict.Add(key: "SE", value: 221);
            countryIdtoGeoIdDict.Add(key: "KE", value: 129);
            countryIdtoGeoIdDict.Add(key: "TH", value: 227);
            countryIdtoGeoIdDict.Add(key: "TR", value: 235);
            countryIdtoGeoIdDict.Add(key: "UA", value: 241);
            countryIdtoGeoIdDict.Add(key: "UZ", value: 247);
            countryIdtoGeoIdDict.Add(key: "VN", value: 251);
            countryIdtoGeoIdDict.Add(key: "CN", value: 45);
            countryIdtoGeoIdDict.Add(key: "TW", value: 237);
        }

        public static void SetHomeRegion(string countryId) {
            if (!countryIdtoGeoIdDict.ContainsKey(key: countryId.ToUpperInvariant()))
                throw new LanguageRegionManagerException(message: string.Format(format: "Country mapping with the specified id was not found: {0}\nPlease look at the documentation for supported values.", arg0: countryId));
            nativeMethods.SetUserGeoID(geoId: countryIdtoGeoIdDict[key: countryId.ToUpperInvariant()]);
        }

        public static void InstallLanguage(string languageId) {
            var installedLanguages = GetInstalledLanguages();
            if (installedLanguages.Contains(item: languageId))
                return;
            var stringBuilder = new StringBuilder();
            foreach (var str in installedLanguages) {
                stringBuilder.Append(value: str);
                stringBuilder.Append(value: ';');
            }

            stringBuilder.Append(value: languageId);
            nativeMethods.SetUserLanguages(languages: stringBuilder.ToString(), languageDelimiter: ';');
            Thread.Sleep(millisecondsTimeout: 1000);
        }

        public static void SetLanguage(string languageId) {
            var installedLanguages = GetInstalledLanguages();
            if (!installedLanguages.Contains(item: languageId))
                throw new LanguageRegionManagerException(message: string.Format(format: "Language specified does not exist on the system: {0}", arg0: languageId));
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(value: languageId);
            foreach (var str in installedLanguages)
                if (!str.Equals(value: languageId, comparisonType: StringComparison.OrdinalIgnoreCase)) {
                    stringBuilder.Append(value: ';');
                    stringBuilder.Append(value: str);
                }

            nativeMethods.SetUserLanguages(languages: stringBuilder.ToString(), languageDelimiter: ';');
            Thread.Sleep(millisecondsTimeout: 1000);
        }

        public static IList<string> GetInstalledLanguages() {
            var stringList = new List<string>();
            var userLanguages = nativeMethods.GetUserLanguages(languageDelimiter: ';');
            var chArray = new char[1] {';'};
            foreach (var str in userLanguages.Split(separator: chArray))
                stringList.Add(item: str);
            return stringList;
        }
    }
}