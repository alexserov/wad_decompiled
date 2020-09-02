// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Localization.INativeMethods
// Assembly: MitaLite.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FE6CFDB-BB88-427C-96A4-C26318ECB83B
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Localization.dll

using System.Runtime.InteropServices;

namespace MS.Internal.Mita.Localization
{
  internal interface INativeMethods
  {
    void SetUserGeoID([In] int geoId);

    string GetUserLanguages([In] char languageDelimiter);

    void SetUserLanguages([In] string languages, [In] char languageDelimiter);
  }
}
