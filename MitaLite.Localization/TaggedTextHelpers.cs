// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Localization.TaggedTextHelpers
// Assembly: MitaLite.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FE6CFDB-BB88-427C-96A4-C26318ECB83B
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Localization.dll

using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace MS.Internal.Mita.Localization
{
  public static class TaggedTextHelpers
  {
    private const string _rKeyPrefix = "°<ªKey>";
    private const string _fixedPrefix = "°<ªFix>";
    private const string _localizablePrefix = "°<ªLoc>";
    private static readonly int _prefixLength = "°<ªKey>".Length;
    private const char _hotkeyCharacter = '&';
    private const string _tagStartElement = "info";
    private const string _tagContextAttribute = "context";
    private const string _tagNativeAttribute = "native";
    private const string _tagProcessIDAttribute = "proc_id";
    private const string _tagCultureAttribute = "cultureName";
    private const string _tagProxyNameAttribute = "proxy";

    public static string RemoveHotkeyModifier(string original)
    {
      Validate.ArgumentNotNull((object) original, nameof (original));
      int num = original.Length - 1;
      for (int index = 0; index < num; ++index)
      {
        if ('&' == original[index])
        {
          original = original.Remove(index, 1);
          --num;
        }
      }
      return original;
    }

    public static bool ExtractHotkey(string original, out char hotkey)
    {
      Validate.ArgumentNotNull((object) original, nameof (original));
      bool flag = false;
      hotkey = char.MinValue;
      for (int index = 0; index < original.Length - 1; ++index)
      {
        if (original[index] == '&')
        {
          if (original[index + 1] == '&')
          {
            ++index;
          }
          else
          {
            hotkey = original[index + 1];
            flag = true;
            break;
          }
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
      string proxyName)
    {
      Validate.ArgumentNotNull((object) culture, nameof (culture));
      bool flag = true;
      string str;
      switch (textType)
      {
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
          throw new ArgumentException(StringResource.Get("InvalidTaggedType", (object) textType));
      }
      if (flag)
      {
        StringWriter stringWriter = new StringWriter((IFormatProvider) CultureInfo.InvariantCulture);
        XmlWriter xmlWriter = XmlWriter.Create((TextWriter) stringWriter);
        try
        {
          xmlWriter.WriteStartElement("info");
          xmlWriter.WriteStartAttribute(nameof (context), string.Empty);
          xmlWriter.WriteString(context);
          xmlWriter.WriteEndAttribute();
          xmlWriter.WriteStartAttribute("native", string.Empty);
          xmlWriter.WriteString(nativeText);
          xmlWriter.WriteEndAttribute();
          xmlWriter.WriteStartAttribute("proc_id", string.Empty);
          xmlWriter.WriteString(processId.ToString((IFormatProvider) CultureInfo.InvariantCulture));
          xmlWriter.WriteEndAttribute();
          xmlWriter.WriteStartAttribute("cultureName", string.Empty);
          xmlWriter.WriteString(culture.DisplayName);
          xmlWriter.WriteEndAttribute();
          xmlWriter.WriteStartAttribute("proxy", string.Empty);
          xmlWriter.WriteString(proxyName);
          xmlWriter.WriteEndAttribute();
          xmlWriter.WriteEndElement();
        }
        finally
        {
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
      out TaggedType textType)
    {
      Validate.ArgumentNotNull((object) tagged, nameof (tagged));
      bool flag = true;
      string str = string.Empty;
      string s = string.Empty;
      if (tagged.Length > TaggedTextHelpers._prefixLength)
      {
        str = tagged.Substring(0, TaggedTextHelpers._prefixLength);
        s = tagged.Substring(TaggedTextHelpers._prefixLength);
      }
      proxyName = string.Empty;
      context = string.Empty;
      processId = 0;
      native = string.Empty;
      culture = (CultureInfo) null;
      if (!(str == "°<ªKey>"))
      {
        if (!(str == "°<ªFix>"))
        {
          if (str == "°<ªLoc>")
          {
            textType = TaggedType.LocalizableText;
          }
          else
          {
            textType = TaggedType.Unknown;
            native = tagged;
            flag = false;
          }
        }
        else
          textType = TaggedType.FixedText;
      }
      else
        textType = TaggedType.ResourceKey;
      if (flag)
      {
        XmlReader xmlReader = XmlReader.Create((TextReader) new StringReader(s));
        try
        {
          int content = (int) xmlReader.MoveToContent();
          if (xmlReader.MoveToAttribute(nameof (context)))
            context = xmlReader.Value;
          if (xmlReader.MoveToAttribute("proc_id"))
            processId = XmlConvert.ToInt32(xmlReader.Value);
          if (xmlReader.MoveToAttribute("proxy"))
            proxyName = xmlReader.Value;
          if (xmlReader.MoveToAttribute("cultureName"))
            culture = new CultureInfo(xmlReader.Value);
          if (xmlReader.MoveToAttribute(nameof (native)))
            native = xmlReader.Value;
        }
        finally
        {
          xmlReader.Dispose();
        }
      }
      return true;
    }
  }
}
