// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Localization.StringResourceData
// Assembly: MitaLite.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FE6CFDB-BB88-427C-96A4-C26318ECB83B
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Localization.dll

namespace MS.Internal.Mita.Localization
{
  public class StringResourceData : IStringResourceData
  {
    private string _rawText;
    private string _debug;

    public StringResourceData(string rawText)
    {
      Validate.ArgumentNotNull((object) rawText, nameof (rawText));
      this._rawText = rawText;
    }

    public StringResourceData(string rawText, string debugInfo)
    {
      Validate.ArgumentNotNull((object) rawText, nameof (rawText));
      Validate.ArgumentNotNull((object) debugInfo, nameof (debugInfo));
      this._rawText = rawText;
      this._debug = debugInfo;
    }

    public string Raw() => this._rawText;

    public virtual string Parsed() => TaggedTextHelpers.RemoveHotkeyModifier(this.Raw());

    public virtual char Hotkey()
    {
      char hotkey;
      return TaggedTextHelpers.ExtractHotkey(this.Raw(), out hotkey) ? hotkey : char.MinValue;
    }

    public virtual string Accelerator() => string.Empty;

    public virtual string DebugInfo() => this._debug;

    public override string ToString() => this.Raw();

    public virtual string DefaultString() => this.Raw();
  }
}
