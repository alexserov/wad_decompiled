// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Localization.StringResource
// Assembly: MitaLite.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FE6CFDB-BB88-427C-96A4-C26318ECB83B
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Localization.dll

using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace MS.Internal.Mita.Localization
{
  internal static class StringResource
  {
    private static ResourceManager _resourceManager = new ResourceManager("MS.Internal.Mita.Localization.StringResource", typeof (StringResource).GetTypeInfo().Assembly);

    internal static string Get(string id, params object[] args)
    {
      string format = StringResource.ResourceManager.GetString(id);
      if (!string.IsNullOrEmpty(format) && args != null && args.Length != 0)
        format = string.Format((IFormatProvider) CultureInfo.InvariantCulture, format, args);
      return format;
    }

    internal static ResourceManager ResourceManager => StringResource._resourceManager;
  }
}
