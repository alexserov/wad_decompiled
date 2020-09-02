// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Localization.NativeMethods
// Assembly: MitaLite.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FE6CFDB-BB88-427C-96A4-C26318ECB83B
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Localization.dll

using System.Runtime.InteropServices;

namespace MS.Internal.Mita.Localization {
    internal class NativeMethods : INativeMethods {
        public const int GEOID_NOT_AVAILABLE = -1;

        public void SetUserGeoID([In] int geoId) {
            if (InternalNativeMethods.SetUserGeoID(geoId: geoId) == -1)
                throw new LanguageRegionManagerException(message: string.Format(format: "GEO ID specified was not found on the system: {0}", arg0: geoId));
        }

        public string GetUserLanguages([In] char languageDelimiter) {
            var empty = string.Empty;
            InternalNativeMethods.GetUserLanguages(delimiter: languageDelimiter, userLanguages: ref empty);
            return empty;
        }

        public void SetUserLanguages([In] string languages, [In] char languageDelimiter) {
            InternalNativeMethods.SetUserLanguages(delimiter: languageDelimiter, userLanguages: languages);
        }

        static class InternalNativeMethods {
            const string LOCALIZATION_DLL = "api-ms-win-core-localization-l1-2-1.dll";
            const string LANGS_DLL = "bcp47langs.dll";
            const string LANGDB_DLL = "winlangdb.dll";

            [DllImport(dllName: "api-ms-win-core-localization-l1-2-1.dll", CharSet = CharSet.Unicode)]
            public static extern int SetUserGeoID([In] int geoId);

            [DllImport(dllName: "bcp47langs.dll", CharSet = CharSet.Unicode)]
            public static extern int GetUserLanguages(char delimiter, [MarshalAs(unmanagedType: UnmanagedType.HString)]
                                                      ref string userLanguages);

            [DllImport(dllName: "winlangdb.dll", CharSet = CharSet.Unicode)]
            public static extern int SetUserLanguages(char delimiter, [MarshalAs(unmanagedType: UnmanagedType.HString)]
                                                      string userLanguages);
        }
    }
}