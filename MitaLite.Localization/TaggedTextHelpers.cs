// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Localization.TaggedTextHelpers
// Assembly: MitaLite.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FE6CFDB-BB88-427C-96A4-C26318ECB83B
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Localization.dll

using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace MS.Internal.Mita.Localization {
    public static class TaggedTextHelpers {
        const string _rKeyPrefix = "°<ªKey>";
        const string _fixedPrefix = "°<ªFix>";
        const string _localizablePrefix = "°<ªLoc>";
        const char _hotkeyCharacter = '&';
        const string _tagStartElement = "info";
        const string _tagContextAttribute = "context";
        const string _tagNativeAttribute = "native";
        const string _tagProcessIDAttribute = "proc_id";
        const string _tagCultureAttribute = "cultureName";
        const string _tagProxyNameAttribute = "proxy";
        static readonly int _prefixLength = "°<ªKey>".Length;

        public static string RemoveHotkeyModifier(string original) {
            Validate.ArgumentNotNull(parameter: original, parameterName: nameof(original));
            var num = original.Length - 1;
            for (var index = 0; index < num; ++index)
                if ('&' == original[index: index]) {
                    original = original.Remove(startIndex: index, count: 1);
                    --num;
                }

            return original;
        }

        public static bool ExtractHotkey(string original, out char hotkey) {
            Validate.ArgumentNotNull(parameter: original, parameterName: nameof(original));
            var flag = false;
            hotkey = char.MinValue;
            for (var index = 0; index < original.Length - 1; ++index)
                if (original[index: index] == '&') {
                    if (original[index: index + 1] == '&') {
                        ++index;
                    } else {
                        hotkey = original[index: index + 1];
                        flag = true;
                        break;
                    }
                }

            return flag;
        }

        public static string CreateTaggedText(
            TaggedType textType,
            string nativeText,
            string context,
            int processId,
            CultureInfo culture,
            string proxyName) {
            Validate.ArgumentNotNull(parameter: culture, parameterName: nameof(culture));
            var flag = true;
            string str;
            switch (textType) {
                case TaggedType.Unknown:
                    str = nativeText;
                    flag = false;
                    break;
                case TaggedType.LocalizableText:
                    str = "°<ªLoc>";
                    break;
                case TaggedType.ResourceKey:
                    str = "°<ªKey>";
                    break;
                case TaggedType.FixedText:
                    str = "°<ªFix>";
                    break;
                default:
                    throw new ArgumentException(message: StringResource.Get(id: "InvalidTaggedType", (object) textType));
            }

            if (flag) {
                var stringWriter = new StringWriter(formatProvider: CultureInfo.InvariantCulture);
                var xmlWriter = XmlWriter.Create(output: stringWriter);
                try {
                    xmlWriter.WriteStartElement(localName: "info");
                    xmlWriter.WriteStartAttribute(localName: nameof(context), ns: string.Empty);
                    xmlWriter.WriteString(text: context);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute(localName: "native", ns: string.Empty);
                    xmlWriter.WriteString(text: nativeText);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute(localName: "proc_id", ns: string.Empty);
                    xmlWriter.WriteString(text: processId.ToString(provider: CultureInfo.InvariantCulture));
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute(localName: "cultureName", ns: string.Empty);
                    xmlWriter.WriteString(text: culture.DisplayName);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute(localName: "proxy", ns: string.Empty);
                    xmlWriter.WriteString(text: proxyName);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteEndElement();
                } finally {
                    xmlWriter.Flush();
                    xmlWriter.Dispose();
                    str += stringWriter.ToString();
                }
            }

            return str;
        }

        public static bool ParseTaggedText(
            string tagged,
            out string native,
            out string proxyName,
            out string context,
            out int processId,
            ref CultureInfo culture,
            out TaggedType textType) {
            Validate.ArgumentNotNull(parameter: tagged, parameterName: nameof(tagged));
            var flag = true;
            var str = string.Empty;
            var s = string.Empty;
            if (tagged.Length > _prefixLength) {
                str = tagged.Substring(startIndex: 0, length: _prefixLength);
                s = tagged.Substring(startIndex: _prefixLength);
            }

            proxyName = string.Empty;
            context = string.Empty;
            processId = 0;
            native = string.Empty;
            culture = null;
            if (!(str == "°<ªKey>")) {
                if (!(str == "°<ªFix>")) {
                    if (str == "°<ªLoc>") {
                        textType = TaggedType.LocalizableText;
                    } else {
                        textType = TaggedType.Unknown;
                        native = tagged;
                        flag = false;
                    }
                } else {
                    textType = TaggedType.FixedText;
                }
            } else {
                textType = TaggedType.ResourceKey;
            }

            if (flag) {
                var xmlReader = XmlReader.Create(input: new StringReader(s: s));
                try {
                    var content = (int) xmlReader.MoveToContent();
                    if (xmlReader.MoveToAttribute(name: nameof(context)))
                        context = xmlReader.Value;
                    if (xmlReader.MoveToAttribute(name: "proc_id"))
                        processId = XmlConvert.ToInt32(s: xmlReader.Value);
                    if (xmlReader.MoveToAttribute(name: "proxy"))
                        proxyName = xmlReader.Value;
                    if (xmlReader.MoveToAttribute(name: "cultureName"))
                        culture = new CultureInfo(name: xmlReader.Value);
                    if (xmlReader.MoveToAttribute(name: nameof(native)))
                        native = xmlReader.Value;
                } finally {
                    xmlReader.Dispose();
                }
            }

            return true;
        }
    }
}