// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Localization.StringResource
// Assembly: MitaLite.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FE6CFDB-BB88-427C-96A4-C26318ECB83B
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Localization.dll

using System.Globalization;
using System.Reflection;
using System.Resources;

namespace MS.Internal.Mita.Localization {
    internal static class StringResource {
        static readonly ResourceManager _resourceManager = new ResourceManager(baseName: "MS.Internal.Mita.Localization.StringResource", assembly: typeof(StringResource).GetTypeInfo().Assembly);

        internal static ResourceManager ResourceManager {
            get { return _resourceManager; }
        }

        internal static string Get(string id, params object[] args) {
            var format = ResourceManager.GetString(name: id);
            if (!string.IsNullOrEmpty(value: format) && args != null && args.Length != 0)
                format = string.Format(provider: CultureInfo.InvariantCulture, format: format, args: args);
            return format;
        }
    }
}