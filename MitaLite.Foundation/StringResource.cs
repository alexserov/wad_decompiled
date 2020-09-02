// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.StringResource
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace MS.Internal.Mita.Foundation
{
  internal static class StringResource
  {
    private static ResourceManager _resourceManager = new ResourceManager("MS.Internal.Mita.Foundation.StringResource", typeof (StringResource).GetTypeInfo().Assembly);

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
