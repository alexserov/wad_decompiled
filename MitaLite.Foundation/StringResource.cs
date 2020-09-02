// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.StringResource
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Globalization;
using System.Reflection;
using System.Resources;

namespace MS.Internal.Mita.Foundation {
    internal static class StringResource {
        static readonly ResourceManager _resourceManager = new ResourceManager(baseName: "MS.Internal.Mita.Foundation.StringResource", assembly: typeof(StringResource).GetTypeInfo().Assembly);

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